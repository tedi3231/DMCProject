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

public partial class src_WebList : System.Web.UI.Page
{
    protected dbInfoAll DB = new dbInfoAll("Web");
    protected string Condition = "";

    private const int _PageSize = 250;
    private int _RecordCount = 0;
    private int _PageCount = 0;
    private int _PageIndex = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        _RecordCount = Convert.ToInt32(lblRecordCount.Text);
        _PageCount = _RecordCount / _PageSize;
        if (_RecordCount % _PageSize > 0) _PageCount++;
        _PageIndex = Convert.ToInt32(lblPageIndex.Text);

        string strPara = Request.QueryString["nparent"];
        string nParent;
        if (strPara.IndexOf("|") < 0)
        {
            nParent = Request.QueryString["nparent"];
            Condition += "nParent=" + nParent;
        }
        else
        {
            string[] sArray = strPara.Split('|');
            nParent = sArray[0];
            if (sArray[3] == "0")
                Condition += "vSrcMac='" + sArray[1] + "' ";
            else
                Condition += "vSrcAddr=dmc_config.dbo.[f_IP2int]('" + sArray[2] + "') ";
        }

        if (!Page.IsPostBack)
        {
            dbConfig configDB = new dbConfig();
            string gUserName = configDB.getUserName(User.Identity.Name);
            string gIP = Request.ServerVariables["Remote_Addr"];
            string gContent = "查看节点" + configDB.getSiteName(nParent) + "的Web帐号内容";
            common.setLog(User.Identity.Name, gUserName, gIP, gContent);
            formInit();
        }
        else
        {
            switch (qrytypelist.SelectedItem.Value)
            {
                case "0"://自选时间段
                    Condition = "vTime > '" + Convert.ToDateTime(sdate.Value.ToString()).ToString("yyyy-MM-dd") + "' and " + Condition;
                    Condition = "vTime < '" + Convert.ToDateTime(edate.Value.ToString()).AddDays(1).ToString("yyyy-MM-dd") + "' and " + Condition;
                    break;
                case "1"://前三天
                    Condition = "vTime > '" + DateTime.Today.AddDays(-3).ToString("yyyy-MM-dd") + "' and " + Condition;
                    Condition = "vTime < '" + DateTime.Today.ToString("yyyy-MM-dd") + "' and " + Condition;
                    break;
                case "2"://前一周
                    Condition = "vTime > '" + DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd") + "' and " + Condition;
                    Condition = "vTime < '" + DateTime.Today.ToString("yyyy-MM-dd") + "' and " + Condition;
                    break;
                case "3"://前一月
                    Condition = "vTime > '" + DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd") + "' and " + Condition;
                    Condition = "vTime < '" + DateTime.Today.ToString("yyyy-MM-dd") + "' and " + Condition;
                    break;
                case "5"://昨天
                    Condition = "vTime > '" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "' and " + Condition;
                    Condition = "vTime < '" + DateTime.Today.ToString("yyyy-MM-dd") + "' and " + Condition;
                    break;
            }
            if (rdlMode.SelectedValue == "1")//敏感模式
                Condition += " and nKey > 0";
        }
    }

    protected void formInit()
    {
        sdate.MaxDate = DateTime.Today.AddDays(-1);
        edate.MaxDate = DateTime.Today.AddDays(-1);
        sdate.Value = Session["FromDate"];
        edate.Value = Session["ToDate"];
        BinddtgData("4");//显示当天数据
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (qrytypelist.SelectedItem.Value == "0")
        {
            DateTime fromDate = Convert.ToDateTime(sdate.Value.ToString()).Date;
            DateTime toDate = Convert.ToDateTime(edate.Value.ToString()).Date;
            Session["FromDate"] = fromDate;
            Session["ToDate"] = toDate;
        }
        _PageIndex = 1;
        BinddtgData(qrytypelist.SelectedItem.Value);
    }
    private void BinddtgData(string TableType)
    {
        if (_PageIndex < 1) _PageIndex = 1;
        _RecordCount = DB.getCount(qrytypelist.SelectedItem.Value, Condition);
        if (_RecordCount > 0)
        {
            _PageCount = _RecordCount / _PageSize;
            if (_RecordCount % _PageSize > 0) _PageCount++;
        }

        dtgData.DataSource = DB.RunProcedure(TableType, _PageSize, _PageIndex, rdlSort.SelectedItem.Value, Condition);
        dtgData.DataBind();

        lblRecordCount.Text = _RecordCount.ToString();
        lblPageCount.Text = _PageCount.ToString();
        lblPageIndex.Text = _PageIndex.ToString();

        if (_PageIndex > 1)
            lnkPrev.Visible = true;
        else
            lnkPrev.Visible = false;
        if (_PageIndex < _PageCount)
            lnkNext.Visible = true;
        else
            lnkNext.Visible = false;
        BindlsbPage(_PageCount, _PageIndex);
    }
    protected void BindlsbPage(int PageCount, int CurPage)
    {
        ListItem li;
        string strPageNum;
        lsbPage.Items.Clear();
        for (int i = 1; i <= PageCount; i++)
        {
            li = new ListItem(i.ToString(), i.ToString());
            if (i == CurPage)
                li.Selected = true;
            lsbPage.Items.Add(li);
        }
    }
    //上一页
    protected void lnkPrev_Click(object sender, EventArgs e)
    {
        _PageIndex--;
        BinddtgData(qrytypelist.SelectedItem.Value);
    }
    //下一页
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        _PageIndex++;
        BinddtgData(qrytypelist.SelectedItem.Value);
    }
    //跳转
    protected void lsbPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        _PageIndex = Convert.ToInt32(lsbPage.SelectedValue);
        BinddtgData(qrytypelist.SelectedItem.Value);
    }
    protected void dtgData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells[0].Text.ToString() == "1")
                e.Item.Style.Add("color", "#FF9000");
            e.Item.Style.Add("cursor", "hand");
            e.Item.Attributes.Add("onclick", "ShowDetail('WebInfo.aspx','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Item.ItemIndex].ToString() + "');");
            e.Item.Attributes.Add("onmouseover", "DoMouseOver();");
            e.Item.Attributes.Add("onmouseout", "DoMouseOut();");
            e.Item.ToolTip = e.Item.Cells[4].Text;
            ((Label)e.Item.Cells[2].FindControl("lblID")).Text = (e.Item.ItemIndex + 1).ToString();
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (DataGridItem objItem in dtgData.Items)
        {
            CheckBox chk = (CheckBox)objItem.FindControl("chkData");
            if (chk.Checked)
                DB.DeleteRecord(qrytypelist.SelectedItem.Value, dtgData.DataKeys[objItem.ItemIndex].ToString());
        }
        BinddtgData(qrytypelist.SelectedItem.Value);
    }
}



