using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class src_LogList : System.Web.UI.Page
{
    protected dbConfig DB = new dbConfig();
    protected DateTime fromDate = System.DateTime.Now;
    protected DateTime toDate = System.DateTime.Now;
    protected string Condition = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DB.getUserPower(Page.User.Identity.Name) == "0")//一般用户不能操作该模块
        {
            Response.Write("对不起，您无权访问该模块");
            Response.End();
        }
        if (!Page.IsPostBack)
        {
            formInit();
        }
    }

    protected void formInit()
    {
        sdate.Value = System.DateTime.Now;
        edate.Value = System.DateTime.Now;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        pagecount.Text = "";

        switch (qrytypelist.SelectedItem.Value.ToString())
        {
            case "0"://任意选择时间范围
                fromDate = Convert.ToDateTime(sdate.Value);
                toDate = Convert.ToDateTime(edate.Value);
                break;
            case "1"://近三天
                fromDate = toDate.AddDays(-3);
                break;
            case "2"://近一周
                fromDate = toDate.AddDays(-7);
                break;
            case "3"://近一月
                fromDate = toDate.AddMonths(-1);
                break;
            case "4"://当天
                fromDate = toDate;
                break;
            case "5"://昨天
                fromDate = toDate.AddDays(-1);
                toDate = toDate.AddDays(-1);
                break;
        }

        BinddtgData();
    }

    private void BinddtgData()
    {
        PagedDataSource objPage = new PagedDataSource();
        if (ddlUsers.SelectedValue != "0")
            Condition += " and nUserID = " + ddlUsers.SelectedValue;
        Condition += " and dOperate >= '" + fromDate.Date.ToString() + "'";
        Condition += " and dOperate < '" + toDate.Date.AddDays(1).Date.ToString() + "'";
        Condition += " order by dOperate " + rdlSort.SelectedValue;
        DataView dv = DB.getLogs(Condition).Tables[0].DefaultView;
        objPage.DataSource = dv;
        objPage.AllowPaging = true;
        objPage.PageSize = 250;

        if (pagecount.Text.ToString() != "")
            currentpage.Text = pagecount.Text.ToString();
        else
            currentpage.Text = "1";

        objPage.CurrentPageIndex = Convert.ToInt32(currentpage.Text) - 1;
        if (!objPage.IsFirstPage)
            lnkPrev.Visible = true;
        else
            lnkPrev.Visible = false;
        if (!objPage.IsLastPage)
            lnkNext.Visible = true;
        else
            lnkNext.Visible = false;
        BindlsbPage(objPage.PageCount, objPage.CurrentPageIndex);
        dtgData.DataSource = objPage;
        dtgData.DataBind();
        lblCurPage.Text = "第" + currentpage.Text + "页/共" + objPage.PageCount + "页（" + dv.Count + "条记录）";
    }

    protected void BindlsbPage(int PageCount, int CurPage)
    {
        ListItem li;
        string strPageNum;
        lsbPage.Items.Clear();
        for (int i = 0; i < PageCount; i++)
        {
            strPageNum = Convert.ToString(i + 1);
            li = new ListItem(strPageNum, strPageNum);
            if (strPageNum == Convert.ToString(CurPage + 1))
                li.Selected = true;
            lsbPage.Items.Add(li);
        }
    }

    //上一页
    protected void lnkPrev_Click(object sender, EventArgs e)
    {
        pagecount.Text = Convert.ToString(Convert.ToInt32(currentpage.Text.ToString()) - 1);
        BinddtgData();
    }
    //下一页
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        pagecount.Text = Convert.ToString(Convert.ToInt32(currentpage.Text.ToString()) + 1);
        BinddtgData();
    }
    //跳转
    protected void lsbPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        pagecount.Text = lsbPage.SelectedValue.ToString();
        BinddtgData();
    }
    protected void dtgData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells[0].Text.ToString() == "1")
                e.Item.Style.Add("color", "#FF9000");
            e.Item.Style.Add("cursor", "hand");
            e.Item.Attributes.Add("onmouseover", "DoMouseOver();");
            e.Item.Attributes.Add("onmouseout", "DoMouseOut();");
            ((Label)e.Item.Cells[2].FindControl("lblID")).Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (DataGridItem objItem in dtgData.Items)
        {
            CheckBox chk = (CheckBox)objItem.FindControl("chkData");
            if (chk.Checked)
                DB.DeleteLog(dtgData.DataKeys[objItem.ItemIndex].ToString());
        }
        BinddtgData();
    }
    protected void ddlUsers_DataBound(object sender, EventArgs e)
    {
        BinddtgData();//显示当天数据
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string SQL = "SELECT * FROM TS_Log WHERE 1=1" + Condition;
        string FileName = Server.MapPath("~/src/temp/") + common.GetFileName() + ".xls";
        DB.ExportToExcel(SQL, FileName);
        SendToClient(FileName);
    }

    private void SendToClient(string FileName)
    {
        System.IO.FileInfo file = new System.IO.FileInfo(FileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
        Response.AddHeader("Content-Length", file.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载 
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端 
        Response.WriteFile(file.FullName);
        // 停止页面的执行 
        Response.End();
    }
}