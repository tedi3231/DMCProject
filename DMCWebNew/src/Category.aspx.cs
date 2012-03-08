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

public partial class src_Category : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();
    private string strType;
    protected void Page_Load(object sender, EventArgs e)
    {
        strType = Request.QueryString["type"];
        if (strType == "1")
            lblCategoryTitle.Text = "垃圾邮件类别管理";
        if (strType == "2")
            lblCategoryTitle.Text = "敏感信息类别管理";
        if (!Page.IsPostBack)
        {
            BinddtgData();
        }
    }
    private void BinddtgData()
    {
        PagedDataSource objPage = new PagedDataSource();
        DataView dv = DB.getCategories(strType).Tables[0].DefaultView;
        if (dv.Table.Rows.Count == 0)
            CheckAll.Visible = false;
        else
            CheckAll.Visible = true;
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
    protected void dtgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        CheckBox chkSelect = null;
        Button btnDelete = null;
        Button btnEdit = null;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                chkSelect = (CheckBox)e.Row.Cells[0].FindControl("chkData");//复选框
                btnDelete = (Button)e.Row.Cells[3].FindControl("btnDelete");//删除按钮
                btnEdit = (Button)e.Row.Cells[3].FindControl("btnEdit");//编辑按钮
                if (DB.CategoryIsUsed(dtgData.DataKeys[e.Row.RowIndex].Value.ToString()))
                {
                    chkSelect.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                }
                else
                {
                    chkSelect.Enabled = true;
                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;
                    btnDelete.OnClientClick = "if(!confirm('是否要删除该记录？')) return false;";
                }
            }
        }
    }
    //删除
    protected void dtgData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DB.DeleteCategory(dtgData.DataKeys[e.RowIndex].Value.ToString());
        pagecount.Text = "";
        BinddtgData();
        lblMessage.Visible = false;
        Message.ResponseScript(this.Page, "self.opener.location.replace(self.opener.location.href);");
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
        string vCategory = ((TextBox)dtgData.Rows[e.RowIndex].Cells[1].FindControl("txtCategory")).Text.ToString();
        string vRemark = ((TextBox)dtgData.Rows[e.RowIndex].Cells[1].FindControl("txtRemark")).Text.ToString();
        string vUpdateUser = Page.User.Identity.Name;
        string err = DB.UpdateCategory(nId, vCategory, vRemark, strType, vUpdateUser);
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
        Message.ResponseScript(this.Page, "self.opener.location.replace(self.opener.location.href);");
    }
    //保存
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string vCategory = txtNewCategory.Text.ToString();
        string vRemark = txtNewRemark.Text.ToString();
        string vUpdateUser = Page.User.Identity.Name;
        string err = DB.InsertCategory(vCategory, vRemark, strType, vUpdateUser);
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
        Message.ResponseScript(this.Page, "self.opener.location.replace(self.opener.location.href);");
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dtgData.EditIndex = -1;
        BinddtgData();
        lblMessage.Visible = false;
        tblAddNew.Visible = true;
    }
    protected void btnDelAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow objRow in dtgData.Rows)
        {
            CheckBox chk = (CheckBox)objRow.FindControl("chkData");
            if (chk.Checked)
                DB.DeleteCategory(dtgData.DataKeys[objRow.RowIndex].Value.ToString());
        }
        pagecount.Text = "";
        BinddtgData();
        lblMessage.Visible = false;
        Message.ResponseScript(this.Page, "self.opener.location.replace(self.opener.location.href);");
    }
    protected void btnNewCancel_Click(object sender, EventArgs e)
    {
        tblAddNew.Visible = false;
    }
}
