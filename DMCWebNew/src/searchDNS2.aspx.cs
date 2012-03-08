using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

public partial class src_searchDNS2 : System.Web.UI.Page
{
    protected database DB = null;
    protected dbConfig configDB = new dbConfig();
    protected string Condition = "";

    private const int _PageSize = 250;
    private int _RecordCount = 0;
    private string sParent = string.Empty;
    private int selectedCount = 0;


    /// <summary>
    /// 根据条件对域名的类型进行初始化
    /// </summary>
    private void initDnsTypeList()
    {
        int type = -1;

        if (!string.IsNullOrEmpty(Request.QueryString["type"]))
        {
            this.hdDnsType.Value = Request.QueryString["type"];
        }

        if (string.IsNullOrEmpty(this.hdDnsType.Value))
        {
            return;
        }

        type = Convert.ToInt32(this.hdDnsType.Value);

        switch (type)
        {
            case 0:
                ddlDnsTypelist.Items.Clear();
                ddlDnsTypelist.Items.Add(new ListItem("所有黑域名", "0"));
                ddlDnsTypelist.Items.Add(new ListItem("普通黑域名(蓝色)", "7"));
                ddlDnsTypelist.Items.Add(new ListItem("重要黑域名(黄色)", "8"));
                ddlDnsTypelist.Items.Add(new ListItem("紧急黑域名(红色)", "9"));
                //ddlDnsTypelist.Items[0].Selected = true;
                this.Title = "黑域名查询";
                break;
            case 1:
                ddlDnsTypelist.Items.Clear();
                ddlDnsTypelist.Items.Add(new ListItem("所有动态域名", "1"));
                ddlDnsTypelist.Items[0].Selected = true;
                this.Title = "动态域名查询";
                break;
            case 2:
                ddlDnsTypelist.Items.Clear();
                ddlDnsTypelist.Items.Add(new ListItem("所有可疑域名", "2"));
                ddlDnsTypelist.Items[0].Selected = true;
                this.Title = "可疑域名查询";
                break;
            case 3:
                ddlDnsTypelist.Items.Clear();
                ddlDnsTypelist.Items.Add(new ListItem("所有异常域名", "10"));
                ddlDnsTypelist.Items.Add(new ListItem("异常域名(低风险)", "3"));
                ddlDnsTypelist.Items.Add(new ListItem("异常域名(中风险)", "4"));
                ddlDnsTypelist.Items.Add(new ListItem("异常域名(高风险)", "5"));
                //ddlDnsTypelist.Items[0].Selected = true;
                this.Title = "异常域名查询";
                break;
            default:
                break;
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (ddlDnsTypelist.SelectedValue.Trim().Equals("6"))
        {
            DB = new dbDns("dns");
        }
        else
        {
            DB = new dbDns("dnsalarm");
        }

        if (!IsPostBack)
        {
            initDnsTypeList();
            dbConfig configDB = new dbConfig();
            string gUserName = configDB.getUserName(User.Identity.Name);
            string gIP = Request.ServerVariables["Remote_Addr"];
            string gContent = "木马查询";
            common.setLog(User.Identity.Name, gUserName, gIP, gContent);
            formInit();
        }
        else
        {
            switch (qrytypelist.SelectedItem.Value)
            {
                case "0"://自选时间段
                    Condition += "vTime > '" + Convert.ToDateTime(sdate.Value.ToString()).ToString("yyyy-MM-dd") + "'";
                    Condition += " and vTime < '" + Convert.ToDateTime(edate.Value.ToString()).AddDays(1).ToString("yyyy-MM-dd") + "'";
                    break;
                case "1"://前三天
                    Condition += "vTime > '" + DateTime.Today.AddDays(-3).ToString("yyyy-MM-dd") + "'";
                    Condition += " and vTime < '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
                    break;
                case "2"://前一周
                    Condition += "vTime > '" + DateTime.Today.AddDays(-7).ToString("yyyy-MM-dd") + "'";
                    Condition += " and vTime < '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
                    break;
                case "3"://前一月
                    Condition += "vTime > '" + DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd") + "'";
                    Condition += " and vTime < '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
                    break;
                case "5"://昨天
                    Condition += "vTime > '" + DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + "'";
                    Condition += " and vTime < '" + DateTime.Today.ToString("yyyy-MM-dd") + "'";
                    break;
            }

            //获取选中的服务器
            foreach (ListItem li in cblHost.Items)
            {
                if (li.Selected)
                {
                    if (selectedCount == 0)  //防止只选一个站点时，出现在,1的情况 查询报错
                    {
                        sParent = li.Value;
                    }
                    else
                    {
                        sParent += "," + li.Value + "";
                    }

                    selectedCount++;
                }
            }

            if (selectedCount <= 0)//tedi3231 added 2010.02.01 没有选中任何站点时显示用户能看到的所有站点 
            {
                sParent = common.GetHosrList(User.Identity.Name);
            }

            Condition += " and nParent in (" + sParent + ")";

            if (txtvDstIp.Text.ToString() != "")//数据库中为numeric 类型，因而需要先将值转化为数字类型
                Condition += " and vDstIp = dmc_config.dbo.f_IP2Int('" + txtvDstIp.Text.ToString() + "')";

            if (txtvDstMac.Text.ToString() != "")
                Condition += " and vDstMac ='" + txtvDstMac.Text.ToString() + "' ";

            if (txtvAddr.Text.ToString() != "")//数据库中为numeric 类型，因而需要先将值转化为数字类型
                Condition += " and vAddr = dmc_config.dbo.f_IP2Int('" + txtvAddr.Text.ToString() + "') ";

            if (txtvName.Text.ToString() != "")
            {
                if (fromeq.Checked)
                    Condition += " and vName = '" + txtvName.Text.ToString() + "'";
                else
                    Condition += " and vName like '%" + txtvName.Text.ToString() + "%'";
            }

            //用户IP
            if (!string.IsNullOrEmpty(tbUserIp.Text))
            {
                Condition += " and vSrcIp = dmc_config.dbo.f_IP2Int('" + tbUserIp.Text.ToString() + "') ";
            }

            //用户MAC
            if (!string.IsNullOrEmpty(tbUserMac.Text))
            {
                Condition += " and vSrcMac= '" + tbUserMac.Text.ToString() + "' ";
            }

            // 是否静默
            if (this.ddlStateFlag.SelectedValue.Trim() == "0")
            {
                Condition += " and vStateFlag = 0 ";
            }
            else if (this.ddlStateFlag.SelectedValue.Trim() == "1")
            {
                 Condition += " and vStateFlag = 1 ";
            }

            //域名类型
            string typeval = ddlDnsTypelist.SelectedValue.Trim();

            if (!string.IsNullOrEmpty(typeval) && typeval != "-1")
            {
                if (typeval == "0") // 如果为所有黑名单，则显示黑名单的三种类别
                {
                    Condition += " and (vType=7 or vType=8 or vType=9 ) ";
                }
                else if (typeval == "10")
                {
                    Condition += " and (vType=3 or vType=4 or vType=5 ) ";
                }
                else if (typeval == "11") //其他域名 可疑域名 所有异常域名
                {
                    Condition += " and (vType=2 or vType=3 or vType=4 or vType=5 ) ";
                }
                else
                {
                    Condition += " and vType = " + typeval + " ";
                }
            }

            //ip归属地查询
            if (ddlIpList.SelectedValue.Trim() != "-1")
            {
                Condition += " and ipnum = " + ddlIpList.SelectedValue.Trim();
            }


            int senstive = common.GetSenstiveCheck(this.CheckBoxList2);

            if (senstive > 0)
                Condition += " and ( (nKey & " + senstive + ")>0 ) ";

            // 条件传给存储过程的时候，最前面不需要跟上and
            if (!string.IsNullOrEmpty(Condition) && Condition.Length > 4 && Condition.Substring(0, 5) == " and ")
                Condition = Condition.Substring(5);
        }
    }

    /// <summary>
    /// 对窗体控件初始化
    /// </summary>
    protected void formInit()
    {
        sdate.MaxDate = DateTime.Today.AddDays(-1);
        edate.MaxDate = DateTime.Today.AddDays(-1);
        sdate.Value = Session["FromDate"];
        edate.Value = Session["ToDate"];

        #region 绑定要查询的单位
        dbConfig dbHost = new dbConfig();
        cblHost.DataSource = dbHost.getSites(User.Identity.Name);
        cblHost.DataBind();
        #endregion

    }

    /// <summary>
    /// 查询方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (qrytypelist.SelectedItem.Value == "0")
        {
            DateTime fromDate = Convert.ToDateTime(sdate.Value.ToString()).Date;
            DateTime toDate = Convert.ToDateTime(edate.Value.ToString()).Date;
            Session["FromDate"] = fromDate;
            Session["ToDate"] = toDate;
        }

        BinddtgData(qrytypelist.SelectedItem.Value);
    }

    /// <summary>
    /// 页面数据绑定
    /// </summary>
    /// <param name="TableType"> 表格类型，4 表示当天</param>
    private void BinddtgData(string TableType)
    {
        this.AspNetPager1.PageSize = _PageSize;

        _RecordCount = DB.getCount(qrytypelist.SelectedItem.Value, Condition);
        DataSet ds = DB.RunProcedure(TableType, _PageSize, this.AspNetPager1.CurrentPageIndex, rdlSort.SelectedItem.Value, Condition);

        dtgData.DataSource = ds.Tables[0].DefaultView;
        dtgData.DataBind();
        this.AspNetPager1.RecordCount = _RecordCount;
        this.AspNetPager1.DataBind();

        ViewState["ds"] = ds;
    }


    /// <summary>
    /// 删除所选记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow objItem in dtgData.Rows)
        {
            CheckBox chk = (CheckBox)objItem.FindControl("cbMail");

            if (chk != null && chk.Checked)
                DB.DeleteRecord(qrytypelist.SelectedItem.Value, dtgData.DataKeys[objItem.RowIndex].Value.ToString());
        }
        BinddtgData(qrytypelist.SelectedItem.Value);
    }

    /// <summary>
    /// 导出查询的内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string SQL = DB.GetSQL(qrytypelist.SelectedItem.Value, Condition, rdlSort.SelectedItem.Value);
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(SQL, FileName);
        SendToClient(FileName);
    }

    /// <summary>
    /// 分页控件的分页方法
    /// </summary>
    /// <param name="src"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        BinddtgData(this.qrytypelist.SelectedValue);
    }


    /// <summary>
    /// 行绑定时方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dtgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string test = string.Empty;
        Label lb = null;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            #region 设置访问路径
            lb = (Label)e.Row.FindControl("lbUrl");
            Label lbId = (Label)e.Row.FindControl("lblID");
            if (lb != null && lbId != null)
            {
                e.Row.Attributes.Add("onclick", "");
                test = lb.Text;
                string id = lbId.Text;
                lb.Text = "<a target='frmContent' href='dnsInfo.aspx?vType=" + test + "&type=" + this.qrytypelist.SelectedValue + "&id=" + dtgData.DataKeys[e.Row.RowIndex].Value.ToString() + "'>点击查看</a>";
            }
            #endregion

            #region 设置编号

            ((Label)e.Row.Cells[2].FindControl("lblID")).Text = (e.Row.RowIndex + 1).ToString();

            #endregion

            #region 设置行效果
            if (e.Row.Cells[0].Text.ToString() == "1")
                e.Row.Cells[0].Style.Add("color", "#FF9000");
            e.Row.Style.Add("cursor", "hand");

            lb = (Label)e.Row.Cells[6].FindControl("lbFlag");
            if (lb != null)
            {
                e.Row.Attributes.Add("onclick", "ShowDetail2('dnsInfo.aspx','" + qrytypelist.SelectedItem.Value + "','" + lb.Text + "','" + dtgData.DataKeys[e.Row.RowIndex].Value.ToString() + "');");
            }
            else
            {
                e.Row.Attributes.Add("onclick", "ShowDetail('dnsInfo.aspx','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Row.RowIndex].Value.ToString() + "');");
            }

            //e.Row.Attributes.Add("onclick", "ShowDnsDetail('dnsInfo.aspx" + "','"+ this.ddlDnsTypelist.SelectedValue +"','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Row.RowIndex].Value.ToString() + "');");
            e.Row.Attributes.Add("onmouseover", "DoMouseOver();");
            e.Row.Attributes.Add("onmouseout", "DoMouseOut();");
            e.Row.ToolTip = e.Row.Cells[4].Text;
            #endregion

            #region 是否为IP黑名单
            HiddenField hdKey = (HiddenField)e.Row.FindControl("hdKey");
            string nKey = hdKey.Value;

            if (!string.IsNullOrEmpty(nKey))
            {
                Label ltIp = (Label)(e.Row.FindControl("lbIp"));
                if (ltIp == null)
                    return;
                int key = Convert.ToInt32(nKey);
                ltIp.Text = common.FormatVal(key, 1);               
            }  
            #endregion
        }
    }

    /// <summary>
    /// 将数据发送到客户端
    /// </summary>
    /// <param name="FileName"></param>
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

    protected void btDelAll_Click(object sender, EventArgs e)
    {
        string tableName = string.Empty;
        if (ddlDnsTypelist.SelectedValue.Trim().Equals("6"))
        {
            tableName = "dns";
        }
        else
        {
            tableName = "dnsalarm";
        }
        dbDns db = new dbDns();
        db.DeleteAllRecord(tableName, this.qrytypelist.SelectedValue, Condition);
        BinddtgData(this.qrytypelist.SelectedValue);
    }

    protected void btExportAll_Click(object sender, EventArgs e)
    {
        //GetSelectedTable(dtgData, "cbMail");
        DataSet ds = (DataSet)ViewState["ds"];
        DataTableExport.DownloadAsExcel(dtgData, ds, "cbMail", "aaa.xls");
    }



}
