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

public partial class src_HorseList : System.Web.UI.Page
{
    protected dbHorse db = new dbHorse();

    protected string Condition = "";

    private const int _PageSize = 200;
    private int _RecordCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        string strPara = Request.QueryString["nparent"];
        string nParent;

        if (string.IsNullOrEmpty(hdnParent.Value))
        {
            nParent = Request.QueryString["nparent"];
            hdnParent.Value = nParent;
        }
        else
        {
            nParent = hdnParent.Value;
        }

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
            string gContent = "查看节点" + configDB.getSiteName(nParent) + "的木马内容";
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
            //if (rdlMode.SelectedValue == "1")//敏感模式
            //   Condition += " and nKey > 0";
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
        if (qrytypelist.SelectedItem.Value == "0")//如果是任意选择的时间段
        {
            DateTime fromDate = Convert.ToDateTime(sdate.Value.ToString()).Date;
            DateTime toDate = Convert.ToDateTime(edate.Value.ToString()).Date;
            //Session["FromDate"] = fromDate;
            //Session["ToDate"] = toDate;
        }
        BinddtgData(qrytypelist.SelectedItem.Value);
    }

    private void BinddtgData(string TableType)
    {
        _RecordCount = db.getCount(qrytypelist.SelectedItem.Value, Condition);

        GridView1.DataSource = db.RunProcedure(this.qrytypelist.SelectedValue, _PageSize, this.AspNetPager1.CurrentPageIndex, rdlSort.SelectedItem.Value, Condition);
        GridView1.DataBind();

        this.AspNetPager1.RecordCount = _RecordCount;
        this.AspNetPager1.PageSize = _PageSize;
        this.AspNetPager1.DataBind();
    }

    protected string getSiteName(object s)
    {
        return new dbConfig().getSiteName(s.ToString());
    }


    protected void dtgData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow objItem in GridView1.Rows)
        {
            CheckBox chk = (CheckBox)objItem.FindControl("cbMail");

            if (chk.Checked)
            {
                db.DeleteRecord(qrytypelist.SelectedItem.Value, GridView1.DataKeys[objItem.RowIndex].Value.ToString());
            }
        }
        //this.AspNetPager1.CurrentPageIndex = 1;
        BinddtgData(qrytypelist.SelectedItem.Value);
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lb = null;
            string test = string.Empty;
            #region 设置访问路径
            lb = (Label)e.Row.FindControl("lbUrl");
            Label lbId = (Label)e.Row.FindControl("lblID");
            if (lb != null && lbId != null)
            {
                test = lb.Text;
                string id = lbId.Text;
                lb.Text = "<a target='frmContent' href='horseInfo.aspx?type=" + this.qrytypelist.SelectedValue + "&id=" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "'>点击查看</a>";
            }
            #endregion

            #region 设置编号
            Label lbTemp = ((Label)e.Row.Cells[0].FindControl("lbState"));

            if (lbTemp != null && lbTemp.Text.Trim() == "1")
                e.Row.Style.Add("color", "#FF9000");
            //e.Row.Cells[0].Style.Add("cursor", "hand");
            // e.Row.Cells[0].Attributes.Add("onclick", "ShowDetail('dnsInfo.aspx','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Row.RowIndex].ToString() + "');");
            //e.Row.Cells[0].Attributes.Add("onmouseover", "DoMouseOver();");
            // e.Row.Cells[0].Attributes.Add("onmouseout", "DoMouseOut();");
            // e.Row.Cells[0].ToolTip = e.Row.Cells[4].Text;
            ((Label)e.Row.Cells[2].FindControl("lblID")).Text = (e.Row.RowIndex + 1).ToString();
            #endregion

            #region 设置行效果
            e.Row.Style.Add("cursor", "hand");
            e.Row.Attributes.Add("onclick", "ShowDetail('horseInfo.aspx','" + qrytypelist.SelectedItem.Value + "','" + GridView1.DataKeys[e.Row.RowIndex].Value.ToString() + "');");
            e.Row.Attributes.Add("onmouseover", "DoMouseOver();");
            e.Row.Attributes.Add("onmouseout", "DoMouseOut();");
            e.Row.ToolTip = e.Row.Cells[4].Text;
            #endregion
        }
    }
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        BinddtgData(qrytypelist.SelectedValue);
    }
}
