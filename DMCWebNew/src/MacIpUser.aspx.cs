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

public partial class src_MacIpUser : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BinddtgData();
        }
    }
    private void BinddtgData()
    {
        PagedDataSource objPage = new PagedDataSource();
        DataView dv = DB.getMacIpUsers(User.Identity.Name).Tables[0].DefaultView;
        objPage.DataSource = dv;
        objPage.AllowPaging = true;
        objPage.PageSize = 20;

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
        gdvData.DataSource = objPage;
        gdvData.DataBind();
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

    protected string GetType(object nType)
    {
        if (nType.ToString() == "0")
            return "按MAC定位";
        else
            return "按IP定位";
    }

    /// <summary>
    /// 节点名称
    /// </summary>
    /// <param name="nParent"></param>
    /// <returns></returns>
    protected string GetParent(object nParent)
    {
        DataSet ds = DB.getHostByID(nParent.ToString());
        if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
        {
            return string.Empty;
        }

        return ds.Tables[0].Rows[0]["vCorpName"].ToString();
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
    protected void gdvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Button btn = new Button();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                btn = (Button)e.Row.Cells[6].FindControl("btnDelete");//删除按钮
                btn.OnClientClick = "if(!confirm('是否要删除该记录？')) return false;";
            }
            if (e.Row.RowState.ToString().IndexOf("Edit") >= 0)
            {
                btn = (Button)e.Row.Cells[6].FindControl("btnUpdate");//更新按钮
                DropDownList ddlParent = (DropDownList)e.Row.Cells[3].FindControl("ddlParent");
                for (int i = 0; i < ddlParent.Items.Count; i++)
                {
                    if (ddlParent.Items[i].Value == ddlParent.ToolTip)
                    {
                        ddlParent.SelectedIndex = i;
                        break;
                    }
                }
                DropDownList ddlType = (DropDownList)e.Row.Cells[4].FindControl("ddlType");
                for (int j = 0; j < ddlType.Items.Count; j++)
                {
                    if (ddlType.Items[j].Value == ddlType.ToolTip)
                    {
                        ddlType.SelectedIndex = j;
                        break;
                    }
                }
            }
        }
    }
    //删除
    protected void gdvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DB.DeleteMacIpUser(gdvData.DataKeys[e.RowIndex].Value.ToString());
        pagecount.Text = "";
        BinddtgData();
        lblMessage.Visible = false;
        Message.ResponseScript(this.Page, "top.frames[1].location.reload();");
    }
    //编辑
    protected void gdvData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvData.EditIndex = e.NewEditIndex;
        BinddtgData();
        lblMessage.Visible = false;
    }
    //取消
    protected void gdvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvData.EditIndex = -1;
        BinddtgData();
        lblMessage.Visible = false;
    }
    //更新
    protected void gdvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string nId = gdvData.DataKeys[e.RowIndex].Value.ToString();
        string nParent = ((DropDownList)gdvData.Rows[e.RowIndex].Cells[3].FindControl("ddlParent")).SelectedValue.ToString();
        string vMac = ((TextBox)gdvData.Rows[e.RowIndex].Cells[1].FindControl("txtMac")).Text.ToString();
        string vIp = ((TextBox)gdvData.Rows[e.RowIndex].Cells[2].FindControl("txtIp")).Text.ToString();
        string nType = ((DropDownList)gdvData.Rows[e.RowIndex].Cells[4].FindControl("ddlType")).SelectedValue.ToString();
        string vMark = ((TextBox)gdvData.Rows[e.RowIndex].Cells[5].FindControl("txtMark")).Text.ToString();
        string vUpdateUser = Page.User.Identity.Name;

        string err = DB.UpdateMacIpUser(nId, nParent, vMac, vIp, nType, vMark, vUpdateUser);
        gdvData.EditIndex = -1;
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
        Message.ResponseScript(this.Page, "top.frames[1].location.reload();");
    }

    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string nParent = ddlNewParent.SelectedValue.ToString();
        string vMac = txtNewMac.Text.ToString();
        string vIp = txtNewIp.Text.ToString();
        string nType = ddlNewType.SelectedValue.ToString();
        string vMark = txtNewMark.Text.ToString();
        string vUpdateUser = Page.User.Identity.Name;
        string err = DB.InsertMacIpUser(nParent, vMac, vIp, nType, vMark, vUpdateUser);
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
        Message.ResponseScript(this.Page, "top.frames[1].location.reload();");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gdvData.EditIndex = -1;
        BinddtgData();
        lblMessage.Visible = false;
    }

    //导入重点怀疑对象
    protected void btnImportKey_Click(object sender, EventArgs e)
    {
        DataSet dsNewKey = new DataSet();
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
                xmlPath = Server.MapPath("~/src/temp/") + "MacIpUser" + Page.User.Identity.Name + ".xml";
                fileImport.PostedFile.SaveAs(xmlPath);
            }
            catch
            {
                xmlPath = "";
            }
        }
        else
            Message.Show(this.Page, "请选择一个XML文件用于导入重点怀疑对象！");
        if (xmlPath != "")
        {
            dsNewKey.ReadXml(xmlPath);

            if (dsNewKey.Tables.Count > 0)
            {
                DataTable dt = dsNewKey.Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["nType"].ToString() == "0")
                        DB.InsertMacIpUser(dt.Rows[i]["nParent"].ToString(), dt.Rows[i]["vMac"].ToString(), "", dt.Rows[i]["nType"].ToString(), dt.Rows[i]["vMark"].ToString(), Page.User.Identity.Name);
                    if (dt.Rows[i]["nType"].ToString() == "1")
                        DB.InsertMacIpUser(dt.Rows[i]["nParent"].ToString(), "", dt.Rows[i]["vIp"].ToString(), dt.Rows[i]["nType"].ToString(), dt.Rows[i]["vMark"].ToString(), Page.User.Identity.Name);
                }
                Message.ResponseScript(this.Page, "alert('重点怀疑对象导入成功，重复的内容只保留一个！');top.frames[1].location.reload();");
                BinddtgData();
            }
            else
                Message.Show(this.Page, "文件中没有重点怀疑对象！");
        }
    }

    //导出重点怀疑对象
    protected void btnExportKey_Click(object sender, EventArgs e)
    {
        string xmlPath = Server.MapPath("~/src/temp/") + "MacIpUser" + Page.User.Identity.Name + ".xml";
        DataSet dsKey = DB.getMacIpUsers();
        StreamWriter sw = new StreamWriter(xmlPath);
        XmlTextWriter xmlWriter = new XmlTextWriter(sw);
        xmlWriter.Formatting = Formatting.Indented;
        xmlWriter.Indentation = 10;
        dsKey.WriteXml(xmlWriter);
        xmlWriter.Close();
        Message.Show(this.Page, "重点怀疑对象导出成功！");
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



