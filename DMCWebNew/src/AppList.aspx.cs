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

public partial class src_AppList : System.Web.UI.Page
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
        DataView dv = DB.getAppUrls().Tables[0].DefaultView;
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
                btn = (Button)e.Row.Cells[3].FindControl("btnDelete");//删除按钮
                btn.OnClientClick = "if(!confirm('是否要删除该记录？')) return false;";
            }
        }
    }
    //删除
    protected void gdvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DB.DeleteAppUrl(gdvData.DataKeys[e.RowIndex].Value.ToString());
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
        string vAppName = ((TextBox)gdvData.Rows[e.RowIndex].Cells[1].FindControl("txtApp")).Text.ToString();
        string vUrl = ((TextBox)gdvData.Rows[e.RowIndex].Cells[2].FindControl("txtUrl")).Text.ToString();
        string vUpdateUser = Page.User.Identity.Name;
        string err = DB.UpdateAppUrl(nId, vAppName, vUrl,vUpdateUser);
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
        string vAppName = txtNewApp.Text.ToString();
        string vUrl = txtNewUrl.Text.ToString();
        string vUpdateUser = Page.User.Identity.Name;
        string err = DB.InsertAppUrl(vAppName, vUrl, vUpdateUser);
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
}
