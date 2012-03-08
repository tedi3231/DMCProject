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

public partial class src_searchinfo : System.Web.UI.Page
{
    protected dbInfoAll DB;
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

        InfoTypeList.Attributes.Add("onchange", "DoTypeChange()");
        string sParent = "'0'";
        DB = new dbInfoAll(InfoTypeList.SelectedValue.ToString());
        frameurl.Text = "<iframe src='' id='frmContent' name='frmContent' scrolling='no' frameborder='0' width='100%' height='120px'></iframe>";
        if (!IsPostBack)
        {
            dbConfig configDB = new dbConfig();
            string gUserName = configDB.getUserName(User.Identity.Name);
            string gIP = Request.ServerVariables["Remote_Addr"];
            string gContent = "其他查询";
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

            if (selectedCount <= 0)//tedi3231 added 2010.02.01 没有选中任何站点时显示用户能看到的所有站点 
            {
                sParent = common.GetHosrList(User.Identity.Name);
            }

            Condition += " and nParent in (" + sParent + ")";


            if (txtSrcAddr.Text.ToString() != "")
                Condition += " and vSrcAddr = dmc_config.dbo.[f_IP2int]('" + txtSrcAddr.Text.ToString() + "')";

            if (txtSrcMac.Text.ToString() != "")
                Condition += " and vSrcMac = '" + txtSrcMac.Text.ToString() + "'";

            if (txtDstAddr.Text.ToString() != "")
                Condition += " and vDstAddr = dmc_config.dbo.[f_IP2int]('" + txtDstAddr.Text.ToString() + "')";

            if (txtDstMac.Text.ToString() != "")
                Condition += " and vDstMac = '" + txtDstMac.Text.ToString() + "'";

            if (txtLogin.Text.ToString() != "")
                Condition += " and vLogin like '%" + txtLogin.Text.ToString() + "%'";

            if (InfoTypeList.SelectedValue.ToString() == "web")//Web帐号
            {
                if (txtSiteName.Text.ToString() != "")
                    Condition += " and vSiteName like '%" + txtSiteName.Text.ToString() + "%'";
            }
            else
            {
                if (tbSiteName.Text.ToString() != "")
                    Condition += " and vSiteName like '%" + txtSiteName.Text.ToString() + "%'";
            }

            if (cbkEmpty.Checked) //用户名和密码非空
            {
                Condition += " and (vLogin is not null) and (vPwd is not null)";
            }

            int senstive = common.GetSenstiveCheck(this.CheckBoxList2);

            if (senstive > 0)
                Condition += " and ( (nKey & " + senstive + ")>0 ) ";

            // 条件传给存储过程的时候，最前面不需要跟上and
            if (!string.IsNullOrEmpty(Condition) && Condition.Length > 4 && Condition.Substring(0, 5) == " and ")
                Condition = Condition.Substring(5);
        }
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
        DataSet ds = DB.RunProcedure(TableType, _PageSize, _PageIndex, rdlSort.SelectedItem.Value, Condition);

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

    protected void dtgData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells[0].Text.ToString() == "1")
                e.Item.Style.Add("color", "#FF9000");
            e.Item.Style.Add("cursor", "hand");
            if (this.InfoTypeList.SelectedValue.ToLower().Trim() == "ftp")
            {
                e.Item.Attributes.Add("onclick", "ShowInfoDetail('FtpInfo.aspx','" + InfoTypeList.SelectedValue.ToString() + "','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Item.ItemIndex].ToString() + "');");
            }
            else
            {
                e.Item.Attributes.Add("onclick", "ShowInfoDetail('InfoInfo.aspx','" + InfoTypeList.SelectedValue.ToString() + "','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Item.ItemIndex].ToString() + "');");
            }
            e.Item.Attributes.Add("onmouseover", "DoMouseOver();");
            e.Item.Attributes.Add("onmouseout", "DoMouseOut();");
            e.Item.ToolTip = e.Item.Cells[4].Text;
            ((Label)e.Item.Cells[2].FindControl("lblID")).Text = (e.Item.ItemIndex + 1).ToString();

            HiddenField hdKey = (HiddenField)e.Item.FindControl("hdKey");
            string nKey = hdKey.Value;

            if (!string.IsNullOrEmpty(nKey))
            {
                int key = Convert.ToInt32(nKey);
                e.Item.Cells[10].Text = common.FormatVal(key, 1);
                e.Item.Cells[11].Text = common.FormatVal(key, 3);
            }  
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
        string SQL = DB.GetSQL(qrytypelist.SelectedItem.Value, Condition, rdlSort.SelectedItem.Value);
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

    public string getContent(object SiteName, object UserName, object Password)
    {
        string _str = "用户名：" + UserName.ToString() + " | ";

        _str += "密码：" + Password.ToString() + " | ";

        if (InfoTypeList.SelectedValue.ToString() == "web")//Web帐号
        {
            _str += "站点：" + SiteName.ToString();
        }
        else
        {
            _str += "文件名：" + SiteName.ToString();
        }


        return common.SubContent(_str, 45);
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



