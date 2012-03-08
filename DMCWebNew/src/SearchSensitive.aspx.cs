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

public partial class src_SearchSensitive : System.Web.UI.Page
{
    protected database DB;
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
        int selectedCount = 0;

        string sParent = "'0'";
        DB = CreateDataBase(this.DropDownList1.SelectedValue.Trim());

        if (!IsPostBack)
        {
            dbConfig configDB = new dbConfig();
            string gUserName = configDB.getUserName(User.Identity.Name);
            string gIP = Request.ServerVariables["Remote_Addr"];
            string gContent = "网站查询";
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
                    sParent += "," + li.Value + "";
                    selectedCount++;
                }
            }

            //tedi3231 added 2010.02.01 没有选中任何站点时显示用户能看到的所有站点
            if (sParent.Length <= 3)
            {
                sParent = common.GetHosrList(User.Identity.Name);
            }

            if (selectedCount < cblHost.Items.Count)
                Condition += " and nParent in (" + sParent + ")";
            if (txtSrcAddr.Text.ToString() != "")
                Condition += " and vSrcAddr = dmc_config.dbo.[f_IP2int]('" + txtSrcAddr.Text.ToString() + "')";
            if (txtSrcMac.Text.ToString() != "")
                Condition += " and vSrcMac = '" + txtSrcMac.Text.ToString() + "'";

            int senstive = GetSenstiveCheck();

            if( senstive> 0 )
                Condition += " and ( (nKey & " + senstive+ ")>0 ) ";
            //if (txtWeburl.Text.ToString() != "")
            //{
            //    if (weburleq.Checked)
            //        Condition += " and vURL = '" + txtWeburl.Text.ToString() + "'";
            //    else
            //        Condition += " and vURL like '%" + txtWeburl.Text.ToString().Trim() + "%'";
            //}
            //if (txtWebIP.Text.ToString() != "")
            //    Condition += " and vDstAddr = dmc_config.dbo.[f_IP2int]('" + txtWebIP.Text.ToString() + "')";

            // 条件传给存储过程的时候，最前面不需要跟上and
            if (!string.IsNullOrEmpty(Condition) && Condition.Length > 4 && Condition.Substring(0, 5) == " and ")
                Condition = Condition.Substring(5);
        }
    }

    private int GetSenstiveCheck()
    {
        int result = 0;
        string[] temp = new string[4];
        foreach( ListItem item in this.CheckBoxList2.Items )
        {
            if (item.Selected)
            {
                if (item.Value == "4")
                {
                    temp[3] = "1";
                }
                if (item.Value == "3")
                {
                    temp[2] = "1";
                }
                if (item.Value == "2")
                {
                    temp[1] = "1";
                }
                if (item.Value == "1")
                {
                    temp[0] = "1";
                }
            }
            else
            {
                if (item.Value == "4")
                {
                    temp[3] = "0";
                }
                if (item.Value == "3")
                {
                    temp[2] = "0";
                }
                if (item.Value == "2")
                {
                    temp[1] = "0";
                }
                if (item.Value == "1")
                {
                    temp[0] = "0";
                }
            }
        }

        string a = temp[3] + temp[2] + temp[1] + temp[0];
        result = Convert.ToInt32(a, 2);
        return result;
    }

    protected void formInit()
    {
        sdate.MaxDate = DateTime.Today.AddDays(-1);
        edate.MaxDate = DateTime.Today.AddDays(-1);
        sdate.Value = Session["FromDate"];
        edate.Value = Session["ToDate"];
        dbConfig dbHost = new dbConfig();
        cblHost.DataSource = dbHost.getSites(User.Identity.Name);
        cblHost.DataBind();
    }

    /// <summary>
    /// 根据选择的模块生成不同的类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private database CreateDataBase(string type)
    {
        database d = null;
        switch (type)
        {
            case "1":
                d = new dbHttpAll();
                break;
            case "2":
                d = new dbMsgAll("Msn");
                break;
            case "3":
                d = new dbMsgAll("Yahoo");
                break;
            case "4":
                d = new dbMailBox("Pop");
                break;
            case "5":
                d = new dbMailBox("Smtp");
                break;
            case "6":
                d = new dbMailSite("Get");
                break;
            case "7":
                d = new dbMailSite("Send");
                break;
            case "8":
                d = new dbInfoAll("Web");
                break;
            case "9":
                d = new dbDns();
                break;
            case "10":
                d = new dbHorse();
                break;
            case "11":
                d = new dbInfoAll("Ftp");
                break;
        }
        return d;
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
        DB = CreateDataBase(this.DropDownList1.SelectedValue.Trim());

        if (_PageIndex < 1) _PageIndex = 1;
        _RecordCount = DB.getCount(qrytypelist.SelectedItem.Value, Condition);
        if (_RecordCount > 0)
        {
            _PageCount = _RecordCount / _PageSize;
            if (_RecordCount % _PageSize > 0) _PageCount++;
        }
        DataSet ds = DB.RunProcedure(TableType, _PageSize, _PageIndex, "Asc", Condition);

        dtgData.DataSource = ds.Tables[0].DefaultView;
        dtgData.DataBind();
        ViewState["ds"] = ds;

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

    protected string getSiteName(object s)
    {
        return new dbConfig().getSiteName(s.ToString());
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


    /// <summary>
    /// 数据绑定事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void dtgData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdKey = (HiddenField)e.Item.Cells[7].FindControl("hdKey");

            string nKey = hdKey.Value;

            if (string.IsNullOrEmpty(nKey))
            {
                return;
            }

            int key = Convert.ToInt32(nKey);
            e.Item.Cells[8].Text = FormatVal(key, 1);
            e.Item.Cells[9].Text = FormatVal(key, 2);
            e.Item.Cells[10].Text = FormatVal(key, 4);
            e.Item.Cells[11].Text = FormatVal(key, 8);
            //设置自动编号
            //e.Item.Cells[1].Text = (this.dtgData.CurrentPageIndex * this.dtgData.PageSize + e.Item.ItemIndex + 1).ToString(); 
            e.Item.Attributes.Add("onclick",CreatItemLink( qrytypelist.SelectedItem.Value,Convert.ToInt32(this.DropDownList1.SelectedValue) ,dtgData.DataKeys[e.Item.ItemIndex].ToString()));
        }
    }

    private string CreatItemLink(string timeType, int dataType,string val)
    {
        string url = string.Empty;

        switch (dataType)
        {
            case 1:
                url = "HttpInfo.aspx";
                break;
            case 2:
                url = "msninfo.aspx";
                break;
            case 3:
                url = "YahooInfo.aspx";
                break;
            case 4:
                url = "mailboxinfo.aspx?mail=Pop";
                break;
            case 5:
                url = "mailboxinfo.aspx?mail=Smtp";
                break;
            case 6:
                url = "mailsiteinfo.aspx?mail=Get";
                break;
            case 7:
                url = "mailsiteinfo.aspx?mail=Send";
                break;
            case 8:
                url = "WebInfo.aspx";
                break;
            case 9:
                url = "dnsInfo.aspx";
                break;
            case 10:
                url = "horseInfo.aspx";
                break;
            case 11:
                url = "ftpinfo.aspx";
                break;
        }

        url = "ShowDetail('YahooInfo.aspx','" + timeType + "','" + val + "');";
        return url;
    }

    private string CreateItemLink(string type, string url,string val)
    {
        string str = "<a target='frmContent' href='" + url +"?type=" + type + "&id=" + val + "'>点击查看</a>";
        return str;
    }

    private string FormatVal(int val, int exceptVal)
    {
        if ((val & exceptVal) == exceptVal)
        {
            return "是";
        }
        else
        {
            return "否";
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string SQL = DB.GetSQL(qrytypelist.SelectedItem.Value, Condition, "Asc");//rdlSort.SelectedItem.Value
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
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


    /// <summary>
    /// 删除查询结果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btDelAll_Click(object sender, EventArgs e)
    {
        DB.DeleteAllRecord(string.Empty, this.qrytypelist.SelectedValue, Condition);
        BinddtgData(this.qrytypelist.SelectedValue);
    }

    /// <summary>
    /// 导出所选记录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btExportAll_Click(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)ViewState["ds"];
        DataTableExport.DownloadAsExcel(dtgData, ds, "chkData", "file.xls");
    }
}



