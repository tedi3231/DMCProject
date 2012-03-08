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
using System.Data.SqlClient;
using System.IO;


public partial class src_searchMail : System.Web.UI.Page
{
    protected database DB;
    protected string Condition = "";
    int CurrentRow;
    string CurrentId;
    private string rarPath = "";

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

        string sParent = "0";
        if (rdlCategory.SelectedValue == "mail_box")
            DB = new dbMailBox(rdlMailType.SelectedValue);
        else
        {
            if (rdlMailType.SelectedValue == "Pop")
                DB = new dbMailSite("Get");
            else
                DB = new dbMailSite("Send");
        }
        if (!IsPostBack)
        {
            dbConfig configDB = new dbConfig();
            string gUserName = configDB.getUserName(User.Identity.Name);
            string gIP = Request.ServerVariables["Remote_Addr"];
            string gContent = "邮件查询";
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
            if (selectedCount < cblHost.Items.Count)
                Condition += " and nParent in (" + sParent + ")";
            if (txtSrcAddr.Text.ToString() != "")
                Condition += " and vSrcAddr = dmc_config.dbo.[f_IP2int]('" + txtSrcAddr.Text.ToString() + "')";
            if (txtSrcMac.Text.ToString() != "")
                Condition += " and vSrcMac = dmc_config.dbo.[f_IP2int]('" + txtSrcMac.Text.ToString() + "')";
            if (txtDstAddr.Text.ToString() != "")
                Condition += " and vDstAddr = dmc_config.dbo.[f_IP2int]('" + txtDstAddr.Text.ToString() + "')";
            if (txtDstMac.Text.ToString() != "")
                Condition += " and vDstMac = '" + txtDstMac.Text.ToString() + "'";
            if (txtDstAddr.Text.ToString() != "")
                Condition += " and vDstAddr = dmc_config.dbo.[f_IP2int]('" + txtDstAddr.Text.ToString() + "')";
            if (txtDstMac.Text.ToString() != "")
                Condition += " and vDstMac = '" + txtDstMac.Text.ToString() + "'";
            if (txtLpFrom.Text.ToString() != "")
            {
                if (fromeq.Checked)
                    Condition += " and vLpFrom = '" + txtLpFrom.Text.ToString() + "'";
                else
                    Condition += " and vLpFrom like '%" + txtLpFrom.Text.ToString() + "%'";
            }
            if (txtLpTo.Text.ToString() != "")
            {
                if (toeq.Checked)
                    Condition += " and vLpTo = '" + txtLpTo.Text.ToString() + "'";
                else
                    Condition += " and vLpTo like '%" + txtLpTo.Text.ToString() + "%'";
            }
            if (txtLpTitle.Text.ToString() != "")
                Condition += " and vLpTitle like '%" + txtLpTitle.Text.ToString() + "%'";
            if (txtLogin.Text.ToString() != "")
                Condition += " and vLogin like '%" + txtLogin.Text.ToString() + "%'";

            if (rdlMode.SelectedValue == "1")//敏感模式
                Condition += " and nKey > 0";

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
        trTop.Style.Add("display", "block");
        imgTopCursor.Src = "images/up.gif";

        if (qrytypelist.SelectedItem.Value == "0")
        {
            DateTime fromDate = Convert.ToDateTime(sdate.Value.ToString()).Date;
            DateTime toDate = Convert.ToDateTime(edate.Value.ToString()).Date;
            Session["FromDate"] = fromDate;
            Session["ToDate"] = toDate;
        }
        _PageIndex = 1;
        BinddtgData(qrytypelist.SelectedItem.Value);
        ShowContent(0);
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
        ViewState["ds"] = ds;
        dtgData.DataSource = ds.Tables[0].DefaultView;
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

    //showType:0为列表模式，1为详细信息模式
    private void ShowContent(int showType)
    {
        if (showType == 0)
        {
            trContent.Style.Add("display", "block");
            trBottom.Height = "150";
            imgBottomCursor.Src = "images/up.gif";
        }
        else
        {
            trContent.Style.Add("display", "none");
            trBottom.Height = "100%";
            imgBottomCursor.Src = "images/down.gif";
        }
        if (_RecordCount > 0)
        {
            CurrentRow = Convert.ToInt32(hidRow.Value);
            if (CurrentRow < 0)
            {
                if (_PageIndex > 1)
                {
                    _PageIndex--;
                    BinddtgData(qrytypelist.SelectedItem.Value);
                    CurrentRow = dtgData.Items.Count - 1;
                    hidRow.Value = CurrentRow.ToString();
                }
            }
            if (CurrentRow > dtgData.Items.Count - 1)
            {
                if (_PageIndex < _PageCount)
                {
                    _PageIndex++;
                    BinddtgData(qrytypelist.SelectedItem.Value);
                    CurrentRow = 0;
                    hidRow.Value = CurrentRow.ToString();
                }
            }

            string strMailType;
            if (CurrentRow >= 0 && CurrentRow <= dtgData.Items.Count - 1)
            {
                CurrentId = dtgData.DataKeys[CurrentRow].ToString();
                dtgData.Items[CurrentRow].Style.Add("color", "#FF9000");
                if (rdlCategory.SelectedValue == "mail_box")
                    frmContent.Attributes["src"] = "mailboxinfo.aspx?mail=" + rdlMailType.SelectedItem.Value + "&type=" + qrytypelist.SelectedItem.Value + "&id=" + CurrentId;
                else
                {
                    if (rdlMailType.SelectedValue == "Pop")
                        strMailType = "Get";
                    else
                        strMailType = "Send";
                    frmContent.Attributes["src"] = "mailsiteinfo.aspx?mail=" + strMailType + "&type=" + qrytypelist.SelectedItem.Value + "&id=" + CurrentId;
                }
            }
        }
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

    protected string formatAttach(object s)
    {
        string _s = s.ToString();
        if (_s == "1")
            _s = "☆";
        else
            _s = "";
        return _s;
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
        ShowContent(0);
    }
    //下一页
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        _PageIndex++;
        BinddtgData(qrytypelist.SelectedItem.Value);
        ShowContent(0);
    }
    //跳转
    protected void lsbPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        _PageIndex = Convert.ToInt32(lsbPage.SelectedValue);
        BinddtgData(qrytypelist.SelectedItem.Value);
        ShowContent(0);
    }

    protected void dtgData_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string strMailType;
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            if (e.Item.Cells[0].Text.ToString() == "1")
                e.Item.Style.Add("color", "#FF9000");
            e.Item.Style.Add("cursor", "hand");
            if (rdlCategory.SelectedValue == "mail_box")
                e.Item.Attributes.Add("onclick", "hidRow.value='" + e.Item.ItemIndex + "';ShowMail('mailboxinfo.aspx','" + rdlMailType.SelectedItem.Value + "','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Item.ItemIndex].ToString() + "');");
            else
            {
                if (rdlMailType.SelectedValue == "Pop")
                    strMailType = "Get";
                else
                    strMailType = "Send";
                e.Item.Attributes.Add("onclick", "hidRow.value='" + e.Item.ItemIndex + "';ShowMail('mailsiteinfo.aspx','" + strMailType + "','" + qrytypelist.SelectedItem.Value + "','" + dtgData.DataKeys[e.Item.ItemIndex].ToString() + "');");
            }
            e.Item.Attributes.Add("onmouseover", "DoMouseOver();");
            e.Item.Attributes.Add("onmouseout", "DoMouseOut();");
            e.Item.ToolTip = e.Item.Cells[5].Text;
            ((Label)e.Item.Cells[2].FindControl("lblID")).Text = (e.Item.ItemIndex + 1).ToString();

            HiddenField hdKey = (HiddenField)e.Item.FindControl("hdKey");
            string nKey = hdKey.Value;

            if (!string.IsNullOrEmpty(nKey))
            {
                int key = Convert.ToInt32(nKey);
                e.Item.Cells[11].Text = common.FormatVal(key, 1);
                e.Item.Cells[12].Text = common.FormatVal(key, 2);
                e.Item.Cells[13].Text = common.FormatVal(key, 3);
                e.Item.Cells[14].Text = common.FormatVal(key, 4);
            }  
        }
    }

    //删除所选记录
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        foreach (DataGridItem objItem in dtgData.Items)
        {
            CheckBox chk = (CheckBox)objItem.FindControl("chkData");
            if (chk.Checked)
                DB.DeleteRecord(qrytypelist.SelectedItem.Value, dtgData.DataKeys[objItem.ItemIndex].ToString());
        }
        BinddtgData(qrytypelist.SelectedItem.Value);
        ShowContent(0);
    }

    //导出查询结果的内容文件并打包成RAR文件
    protected void btnExport_Click(object sender, EventArgs e)
    {
        string SQL = DB.GetSQL(qrytypelist.SelectedItem.Value, Condition, rdlSort.SelectedItem.Value);
        string sName = "temp\\mail\\export" + common.GetFileName();
        string SourcePath = "";
        string DescPath = "";
        string Filename = "";
        int iStation = 0;

        DataSet ds = DB.CreateDataSet(SQL);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            if (ds.Tables[0].Rows[i]["vLocalFile"].ToString() != "")
            {
                SourcePath = ds.Tables[0].Rows[i]["vLocalFile"].ToString();
                iStation = SourcePath.IndexOf('/');
                
                if (iStation < 0)
                    continue;

                Filename = SourcePath.Substring(iStation + 1);
                SourcePath = SourcePath.Substring(0, iStation) + "\\";
                DescPath = "D:\\\\" + sName + "\\";
                if (!Directory.Exists(DescPath))
                    Directory.CreateDirectory(DescPath);
                if (File.Exists(SourcePath + Filename)) File.Copy(SourcePath + Filename, DescPath + Filename);
            }
        }
        rarPath = Server.MapPath(".") + "\\" + sName + ".rar";
        DB.ExportToRar(DescPath, rarPath);
    }

    //导出所选邮件内容并打包成RAR文件
    protected void btnExportSelected_Click(object sender, EventArgs e)
    {
        string sName = "temp\\mail\\export" + common.GetFileName();
        string SourcePath = "";
        string DescPath = "";
        string Filename = "";
        int iStation = 0;
        DataSet ds;

        foreach (DataGridItem objItem in dtgData.Items)
        {
            CheckBox chk = (CheckBox)objItem.FindControl("chkData");
            if (chk.Checked)
            {
                ds = DB.GetContentByID(qrytypelist.SelectedItem.Value, dtgData.DataKeys[objItem.ItemIndex].ToString());
                SourcePath = ds.Tables[0].Rows[0]["vLocalFile"].ToString();
                if(string.IsNullOrEmpty(SourcePath))
                {
                    continue;
                }
                
                iStation = SourcePath.IndexOf('/');
                if (iStation < 0)
                {
                    iStation = SourcePath.Length - 1;
                }

                Filename = SourcePath.Substring(iStation + 1);
                SourcePath = SourcePath.Substring(0, iStation) + "\\";
                DescPath = "D:\\\\" + sName + "\\";
                if (!Directory.Exists(DescPath))
                    Directory.CreateDirectory(DescPath);
                if (File.Exists(SourcePath + Filename)) File.Copy(SourcePath + Filename, DescPath + Filename);
            }
        }
        rarPath = Server.MapPath(".") + "\\" + sName + ".rar";

        File.Create(rarPath);

        DB.ExportToRar(DescPath, rarPath);
        //SendToClient(rarPath);
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
        Response.ContentType = "application/octet-stream";
        // 把文件流发送到客户端 
        Response.WriteFile(file.FullName);
        // 停止页面的执行 
        Response.End();
    }
    //上一封
    protected void btnPrev_Click(object sender, EventArgs e)
    {
        hidRow.Value = Convert.ToString(Convert.ToInt32(hidRow.Value) - 1);
        ShowContent(1);
    }
    //下一封
    protected void btnNext_Click(object sender, EventArgs e)
    {
        hidRow.Value = Convert.ToString(Convert.ToInt32(hidRow.Value) + 1);
        ShowContent(1);
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
}



