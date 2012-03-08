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

public partial class DNSList : System.Web.UI.Page
{
    protected dbDns dbdns = new dbDns();
    protected dbConfig configDB = new dbConfig();

    protected string Condition = "";

    private const int _PageSize = 200;
    private int _RecordCount = 0;

    //当前要查询的表名
    private string tableName = string.Empty;

    private string nParent = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        //string strPara = Request.QueryString["nparent"];

        //if (strPara.IndexOf("|") < 0)
        //{
        //    nParent = Request.QueryString["nparent"];
        //    //Condition += "nParent=" + nParent;
        //}
        //else
        //{
        //    string[] sArray = strPara.Split('|');
        //    nParent = sArray[0];
        //    if (sArray[3] == "0")
        //        Condition += "vSrcMac='" + sArray[1] + "' ";
        //    else
        //        Condition += "vSrcAddr='" + sArray[2] + "' ";
        //}

        

        if (!Page.IsPostBack)
        {
            initCondition();
            string gUserName = configDB.getUserName(User.Identity.Name);
            string gIP = Request.ServerVariables["Remote_Addr"];
            string gContent = "查看节点" + configDB.getSiteName(nParent) + "的DNS内容";
            common.setLog(User.Identity.Name, gUserName, gIP, gContent);
            formInit();
        }
    }

    protected void formInit()
    {
        sdate.MaxDate = DateTime.Today.AddDays(-1);
        edate.MaxDate = DateTime.Today.AddDays(-1);
        sdate.Value = Session["FromDate"];
        edate.Value = Session["ToDate"];
        //BinddtgData("4");//显示当天数据
        BinddtgData("4");
    }

    /// <summary>
    /// 绑定页面上数据
    /// <param name="tableType">表格类型 4 表示当天</param>
    /// </summary>
    private void BinddtgData(string tableType)
    {
        _RecordCount = dbdns.getCount(qrytypelist.SelectedItem.Value, Condition);

        dtgData.DataSource = dbdns.RunProcedure(this.qrytypelist.SelectedValue, _PageSize, this.AspNetPager1.CurrentPageIndex, rdlSort.SelectedItem.Value, Condition);
        dtgData.DataBind();

        this.AspNetPager1.RecordCount = _RecordCount;
        this.AspNetPager1.PageSize = _PageSize;
        this.AspNetPager1.DataBind();
    }

    /// <summary>
    /// 初始化条件
    /// </summary>
    private void initCondition()
    {
        string strPara = Request.QueryString["nparent"];
        //string nParent;

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
                Condition += "vSrcIp=dmc_config.dbo.[f_IP2int]('" + sArray[2] + "') ";
        }

        this.hdnParent.Value = nParent;


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

        /// 白名单
        if (ddlDnsType.SelectedValue == "6")
        {
            dbdns.TablePrefix = "TC_Dns_";
            dbdns.TempTablePrefix = "Temp_Dns_";
        }
        else // 非白名单
        {
            dbdns.TablePrefix = "TC_dnsalarm_";
            dbdns.TempTablePrefix = "Temp_dnsalarm_";
        }

        //ip归属地查询
        if (ddlIpList.SelectedValue.Trim() != "-1")
        {
            Condition += " and ipnum = " + ddlIpList.SelectedValue.Trim();
        }

        //域名类型
        string typeval = ddlDnsType.SelectedValue.Trim();

        if (!string.IsNullOrEmpty(typeval) && typeval != "-1")
        {
            if (typeval == "0") // 如果为所有黑名单，则显示黑名单的三种类别
            {
                Condition += " and (vType=7 or vType=8 or vType=9 ) ";
            }
            else
            {
                Condition += " and vType = " + typeval + " ";
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        initCondition();
        foreach (GridViewRow objItem in dtgData.Rows)
        {
            CheckBox chk = (CheckBox)objItem.FindControl("cbMail");

            if (chk.Checked)
            {
                dbdns.DeleteRecord(qrytypelist.SelectedItem.Value, dtgData.DataKeys[objItem.RowIndex].Value.ToString());
            }
        }
        //this.AspNetPager1.CurrentPageIndex = 1;
        BinddtgData(qrytypelist.SelectedItem.Value);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        initCondition();
        if (rdlSort.SelectedValue != "4")
        {
            BinddtgData(string.Empty);
        }
        else
        {
            BinddtgData("4");
        }
    }

    /// <summary>
    /// IP归属地查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dtgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string test = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            #region 设置IP归属地
            Label lb = (Label)e.Row.FindControl("lbArea");
            if (lb != null)
            {
                test = lb.Text;

                if (!common.isIP(test)) // 判断不是IP正规地址的情况
                {
                    test = "未知地址";
                }
                else
                {
                    test = configDB.GetIpArea(test);
                    if (string.IsNullOrEmpty(test))
                    {
                        test = "未知地址";
                    }
                }
                lb.Text = test;
            }
            #endregion

            #region 设置访问路径
            lb = (Label)e.Row.FindControl("lbUrl");
            Label lbId = (Label)e.Row.FindControl("lblID");
            if (lb != null && lbId != null)
            {
                test = lb.Text;
                string id = lbId.Text;
                lb.Text = "<a target='frmContent' href='dnsInfo.aspx?vType=" + test + "&type=" + this.qrytypelist.SelectedValue + "&id=" + dtgData.DataKeys[e.Row.RowIndex].Value.ToString() + "'>点击查看</a>";
            }
            #endregion

            #region 设置编号

            ((Label)e.Row.Cells[2].FindControl("lblID")).Text = (e.Row.RowIndex + 1).ToString();

            #endregion

            #region 设置行效果

            Label lbTemp = ((Label) e.Row.Cells[0].FindControl("lbState"));

            if (lbTemp!=null && lbTemp.Text.Trim() == "1")
                e.Row.Style.Add("color", "#FF9000");

            e.Row.Style.Add("cursor", "hand");
            e.Row.Attributes.Add("onclick", "ShowDnsDetail('dnsInfo.aspx" + "','" + this.ddlDnsType.SelectedValue + "','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Row.RowIndex].Value.ToString() + "');");
            //e.Row.Attributes.Add("onclick", "ShowDetail('dnsInfo.aspx?vType=test" + "','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Row.RowIndex].Value.ToString() + "');");
            e.Row.Attributes.Add("onmouseover", "DoMouseOver();");
            e.Row.Attributes.Add("onmouseout", "DoMouseOut();");
            e.Row.ToolTip = e.Row.Cells[4].Text;
            #endregion
        }
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        initCondition();
        if (rdlSort.SelectedValue != "4")
        {
            BinddtgData(string.Empty);
        }
        else
        {
            BinddtgData("4");
        }
    }
}
