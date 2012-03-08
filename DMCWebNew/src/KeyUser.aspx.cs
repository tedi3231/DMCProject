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
using System.Text;
using System.IO;
using System.Xml;

public partial class src_KeyUser : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();
    ArrayList alKeys = new ArrayList();
    int recCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    private void BinddtgData()
    {
        PagedDataSource objPage = new PagedDataSource();
        DataView dv = DB.getKeyUsers(ddlCate.SelectedValue.ToString()).Tables[0].DefaultView;
        if (dv.Table.Rows.Count == 0)
            CheckAll.Visible = false;
        else
            CheckAll.Visible = true;
        objPage.DataSource = dv;
        objPage.AllowPaging = true;
        objPage.PageSize = 15;

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
    protected string GetCategory(object nCategory)
    {
        return DB.getCategoryByID(nCategory.ToString()).Tables[0].Rows[0]["vCategory"].ToString();
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
    protected void dtgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Button btn = new Button();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                btn = (Button)e.Row.Cells[3].FindControl("btnDelete");//删除按钮
                btn.OnClientClick = "if(!confirm('是否要删除该记录？')) return false;";
            }
            if (e.Row.RowState.ToString().IndexOf("Edit") >= 0)
            {
                DropDownList ddlCategory = (DropDownList)e.Row.Cells[1].FindControl("ddlCategory");
                for (int j = 0; j < ddlCategory.Items.Count; j++)
                {
                    if (ddlCategory.Items[j].Value == ddlCategory.ToolTip)
                    {
                        ddlCategory.SelectedIndex = j;
                        break;
                    }
                }
            }
        }
    }
    //删除
    protected void dtgData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DB.DeleteKeyUser(dtgData.DataKeys[e.RowIndex].Value.ToString());
        pagecount.Text = "";
        BinddtgData();
        lblMessage.Visible = false;
    }
    //编辑
    protected void dtgData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dtgData.EditIndex = e.NewEditIndex;
        BinddtgData();
        lblMessage.Visible = false;
        tblAddNew.Visible = false;
    }
    //取消
    protected void dtgData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        dtgData.EditIndex = -1;
        BinddtgData();
        lblMessage.Visible = false;
    }
    //更新
    protected void dtgData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string nId = dtgData.DataKeys[e.RowIndex].Value.ToString();
        string vKey = ((TextBox)dtgData.Rows[e.RowIndex].Cells[1].FindControl("txtKey")).Text.ToString();
        string nCategory = ((DropDownList)dtgData.Rows[e.RowIndex].Cells[1].FindControl("ddlCategory")).SelectedValue.ToString();
        string vUpdateUser = Page.User.Identity.Name;
        string err = DB.UpdateKeyUser(nId, vKey, nCategory, vUpdateUser);
        dtgData.EditIndex = -1;
        if (err == "")
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Text = err;
            lblMessage.Visible = true;
        }
        BinddtgData();
    }
    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string vKey = txtNewKey.Text.ToString();
        string nCategory = ddlNewCategory.SelectedValue.ToString();
        string vUpdateUser = Page.User.Identity.Name;
        string err = DB.InsertKeyUser(vKey, nCategory, vUpdateUser);
        if (err == "")
        {
            lblMessage.Text = "";
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Text = err;
            lblMessage.Visible = true;
        }
        BinddtgData();
        tblAddNew.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dtgData.EditIndex = -1;
        BinddtgData();
        lblMessage.Visible = false;
        tblAddNew.Visible = true;
        ddlNewCategory.DataBind();
        ddlNewCategory.SelectedIndex = ddlCate.SelectedIndex;
    }
    protected void btnDelAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow objRow in dtgData.Rows)
        {
            CheckBox chk = (CheckBox)objRow.FindControl("chkData");
            if (chk.Checked)
                DB.DeleteKeyUser(dtgData.DataKeys[objRow.RowIndex].Value.ToString());
        }
        pagecount.Text = "";
        BinddtgData();
        lblMessage.Visible = false;
    }
    protected void ddlCate_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinddtgData();
        if (tblAddNew.Visible == true)
            ddlNewCategory.SelectedIndex = ddlCate.SelectedIndex;
    }
    protected void btnNewCancel_Click(object sender, EventArgs e)
    {
        tblAddNew.Visible = false;
    }
    protected void ddlCate_DataBound(object sender, EventArgs e)
    {
        BinddtgData();
    }

    //比对单个关键字
    protected void dtgData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Check")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = dtgData.Rows[index];
            Label lbl = (Label)row.Cells[2].FindControl("lblKey");
            string key = Server.HtmlDecode(lbl.Text);
            alKeys.Clear();
            alKeys.Add(key);
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(RunCheck));
            thread.Start();
            Session["State"] = 0;
            Message.OpenProgressBar(this.Page);
        }
    }

    //比对
    private void RunCheck()
    {
        int percent = 50;
        int state = 0;
        recCount = 0;//中标数
        if (alKeys.Count > 0)
        {
            percent /= alKeys.Count;
            foreach (string sKey in alKeys)
            {
                state = Convert.ToInt32(Session["State"].ToString());

                files attachFiles = new files();
                recCount += attachFiles.searchFile(sKey, 0);
                Session["State"] = state + percent;
                recCount += DB.CheckData(0, sKey, "", "");
                Session["State"] = state + percent * 2;
            }
            Session["recCount"] = recCount;
            Session["State"] = 100;
        }
    }

    //private void RunTask()
    //{
    //    int percent = 16;
    //    int state = 0;
    //    recCount = 0; //中标数

    //    if (alKeys.Count > 0)
    //    {
    //        percent /= alKeys.Count;
    //        foreach (string sKey in alKeys)
    //        {
    //            state = Convert.ToInt32(Session["State"].ToString());

    //            files attachFiles = new files();
    //            dbMailBox PopMail = new dbMailBox("Pop");
    //            dbMailBox SmtpMail = new dbMailBox("Smtp");
    //            dbMailSite GetMail = new dbMailSite("Get");
    //            dbMailSite SendMail = new dbMailSite("Send");
    //            dbMsgAll Msn = new dbMsgAll("Msn");

    //            recCount += attachFiles.searchFile(sKey, 0);
    //            Session["State"] = state + percent;

    //            recCount += PopMail.CheckData(sKey, 0, "", "");
    //            Session["State"] = state + percent * 2;
                
    //            recCount += SmtpMail.CheckData(sKey, 0, "", "");
    //            Session["State"] = state + percent * 3;
               
    //            recCount += GetMail.CheckData(sKey, 0, "", "");
    //            Session["State"] = state + percent * 4;
                
    //            recCount += SendMail.CheckData(sKey, 0, "", "");
    //            Session["State"] = state + percent * 5;
                
    //            recCount += Msn.CheckData(sKey, 0, "", "");
    //            Session["State"] = state + percent * 6;
    //        }
    //    }
    //    Session["recCount"] = recCount;
    //    Session["State"] = 100;
    //}

    protected void dtgData_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button chkButton = (Button)e.Row.Cells[3].FindControl("btnCheck");
            chkButton.CommandArgument = e.Row.RowIndex.ToString();
        }
    }
    
    //比对所有关键字
    protected void btnCheckAll_Click(object sender, EventArgs e)
    {
        DataTable dtKeys = DB.getKeyUsers().Tables[0];
        alKeys.Clear();
        for (int i = 0; i < dtKeys.Rows.Count; i++)
            alKeys.Add(dtKeys.Rows[i]["vKey"].ToString());
        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(RunCheck));
        thread.Start();
        Session["State"] = 0;
        Message.OpenProgressBar(this.Page);
    }

    //比对选中关键字
    protected void btnCheckSeleced_Click(object sender, EventArgs e)
    {
        CheckBox oCheckbox;
        Label oLabel;
        alKeys.Clear();
        for (int i = 0; i < dtgData.Rows.Count; i++)
        {
            oCheckbox = (CheckBox)dtgData.Rows[i].Cells[0].FindControl("chkData");
            if (oCheckbox.Checked)
            {
                oLabel = (Label)dtgData.Rows[i].Cells[1].FindControl("lblKey");
                alKeys.Add(oLabel.Text);
            }
        }
        System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(RunCheck));
        thread.Start();
        Session["State"] = 0;
        Message.OpenProgressBar(this.Page);
    }

    //导入关键字
    protected void btnImportKey_Click(object sender, EventArgs e)
    {
        DataSet dsNewKey = new DataSet();
        DataRow parentRow = null;
        string nId;
        string vCategory;
        string vRemark;
        string nType;
        bool fileOK = false;
        string xmlPath = "";
        
        if (fileImport.HasFile)
        {
            string fileExtension = Path.GetExtension(fileImport.FileName).ToLower();
            if (fileExtension == ".xml") fileOK = true;
        }
        if (fileOK)
        {
            try
            {
                xmlPath = Server.MapPath("~/src/temp/") + "KeywordsBy" + Page.User.Identity.Name + ".xml";
                fileImport.PostedFile.SaveAs(xmlPath);
            }
            catch
            {
                xmlPath = "";
            }
        }
        else
            Message.Show(this.Page, "请选择一个XML文件用于导入关键字！");
        if (xmlPath != "")
        {
            dsNewKey.ReadXml(xmlPath);

            if (dsNewKey.Tables.Count > 1)
            {
                foreach (DataRow childRow in dsNewKey.Tables[1].Rows)
                {
                    parentRow = childRow.GetParentRow(dsNewKey.Relations[0]);
                    vCategory = parentRow["vCategory"].ToString();
                    vRemark = parentRow["vRemark"].ToString();
                    nType = parentRow["nType"].ToString();
                    if (nType == "2")
                    {
                        nId = DB.getCategoryID(vCategory, nType);
                        if (nId == "")
                        {
                            DB.InsertCategory(vCategory, vRemark, nType, Page.User.Identity.Name);
                            nId = DB.getCategoryID(vCategory, nType);
                        }
                        DB.InsertKeyUser(childRow["vKey"].ToString(), nId, Page.User.Identity.Name);
                    }
                    else
                    {
                        Message.Show(this.Page, "文件中没有敏感信息关键字！");
                        break;
                    }
                }
                Message.Show(this.Page, "敏感信息关键字导入成功，重复的关键字只保留一个！");
            }
            else
                Message.Show(this.Page, "文件中没有敏感信息关键字！");
            BinddtgData();
        }
    }
    //导出关键字
    protected void btnExportKey_Click(object sender, EventArgs e)
    {
        string xmlPath = Server.MapPath("~/src/temp/") + "KeywordsBy" + Page.User.Identity.Name + ".xml";
        DataSet dsKey = DB.getAllKeywords();
        StreamWriter sw = new StreamWriter(xmlPath);
        XmlTextWriter xmlWriter = new XmlTextWriter(sw);
        xmlWriter.Formatting = Formatting.Indented;
        xmlWriter.Indentation = 10;
        dsKey.Relations[0].Nested = true;
        dsKey.WriteXml(xmlWriter);
        xmlWriter.Close();
        btnImportKey.Enabled = true;
        Message.Show(this.Page, "敏感信息关键字导出成功！");
        SendToClient(xmlPath);
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
        Response.ContentType = "application/xml";
        // 把文件流发送到客户端 
        Response.WriteFile(file.FullName);
        // 停止页面的执行 
        Response.End();
    }
}



