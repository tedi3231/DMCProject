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

public partial class src_ManageUser : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BinddtgData();
            gdvData.ShowFooter = false;
            if (DB.getUserPower(Page.User.Identity.Name) == "0")//一般用户不能操作新增按钮
                btnAdd.Enabled = false;
        }
        //Response.Write(Request.Form.ToString());
    }
    private void BinddtgData()
    {
        gdvData.DataSource = DB.getUsers(Page.User.Identity.Name);
        gdvData.DataBind();
    }
    protected string GetPower(object nType)
    {
        if (nType.ToString() == "-1")
            return "管理员";
        else
            return "一般用户";
    }
    protected string GetParent(object nId)
    {
        DataSet ds = DB.getHostsByUserID(nId.ToString());
        string result = "|";
        for (int i=0; i<ds.Tables[0].Rows.Count; i++)
            result += " " + ds.Tables[0].Rows[i]["vCorpName"] + " |";
        if (result == "|")
            result = "";
        return result;
    }
    protected void gdvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Button btn = new Button();
        CheckBoxList rbl = new CheckBoxList();
        TextBox txt = new TextBox();
        int iRow;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                btn = (Button)e.Row.Cells[5].FindControl("btnDelete");//删除按钮
                if (DB.getUserPower(Page.User.Identity.Name) == "0")
                    btn.Enabled = false;
                else if (gdvData.DataKeys[e.Row.RowIndex].Value.ToString() == Page.User.Identity.Name)
                    btn.Enabled = false;
                else
                    btn.OnClientClick = "if(!confirm('是否要删除该记录？')) return false;";
            }
            if (e.Row.RowState.ToString().IndexOf("Edit") >= 0)
            {
                rbl = (CheckBoxList)e.Row.Cells[4].FindControl("cblParent");
                txt = (TextBox)e.Row.Cells[1].FindControl("txtLogin");
                string curUserID = gdvData.DataKeys[e.Row.RowIndex].Value.ToString();
                DataSet ds = DB.getHostsByUserID(curUserID);
                if (curUserID == "1")//admin用户不允许修改用户名
                    txt.Enabled = false;
                if (DB.getUserPower(curUserID) == "-1")//管理用户不允许修改权限
                    rbl.Enabled = false;
                iRow = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = iRow; j < rbl.Items.Count; j++)
                    {
                        if (rbl.Items[j].Value.ToString() == ds.Tables[0].Rows[i]["nId"].ToString())
                        {
                            rbl.Items[j].Selected = true;
                            iRow = j+1;
                            break;
                        }
                    }
                }
                DropDownList ddlPower = (DropDownList)e.Row.Cells[3].FindControl("ddlPower");
                for (int j = 0; j < ddlPower.Items.Count; j++)
                {
                    if (ddlPower.Items[j].Value == ddlPower.ToolTip)
                    {
                        ddlPower.SelectedIndex = j;
                        break;
                    }
                }
                if (DB.getUserPower(Page.User.Identity.Name) == "0")
                {
                    rbl.Enabled = false;
                    ddlPower.Enabled = false;
                }
            }
        }
    }
    //删除
    protected void gdvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string err = DB.DeleteUser(gdvData.DataKeys[e.RowIndex].Value.ToString());
        gdvData.ShowFooter = false;
        if (err == "")
        {
            gdvData.ShowFooter = false;
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Text = err;
            lblMessage.Visible = true;
        }
        BinddtgData();
    }
    //编辑
    protected void gdvData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gdvData.EditIndex = e.NewEditIndex;
        gdvData.ShowFooter = false;
        BinddtgData();
        lblMessage.Visible = false;
    }
    //取消
    protected void gdvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvData.EditIndex = -1;
        gdvData.ShowFooter = false;
        BinddtgData();
        lblMessage.Visible = false;
    }
    //更新、保存
    protected void gdvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string UserID,UserName, Password, PowerType;
        string Powers = "|";
        string err = "";
        if (gdvData.ShowFooter)//保存
        {
            UserName = ((TextBox)gdvData.FooterRow.Cells[1].FindControl("txtAddLogin")).Text.ToString();
            Password = ((TextBox)gdvData.FooterRow.Cells[2].FindControl("txtAddPsw")).Text.ToString();
            PowerType = ((DropDownList)gdvData.FooterRow.Cells[3].FindControl("ddlAddPower")).SelectedValue.ToString();
            CheckBoxList cblParent = (CheckBoxList)gdvData.FooterRow.Cells[4].FindControl("cblAddParent");
            foreach (ListItem li in cblParent.Items)
            {
                if (li.Selected)
                    Powers += li.Value + "|";
            }
            err = DB.InsertUser(UserName, Password, PowerType, Powers);
        }
        else//更新
        {
            UserID = gdvData.DataKeys[e.RowIndex].Value.ToString();
            UserName = ((TextBox)gdvData.Rows[e.RowIndex].Cells[1].FindControl("txtLogin")).Text.ToString();
            Password = ((TextBox)gdvData.Rows[e.RowIndex].Cells[2].FindControl("txtPsw")).Text.ToString();
            PowerType = ((DropDownList)gdvData.Rows[e.RowIndex].Cells[3].FindControl("ddlPower")).SelectedValue.ToString();
            CheckBoxList cblParent = (CheckBoxList)gdvData.Rows[e.RowIndex].Cells[4].FindControl("cblParent");
            foreach (ListItem li in cblParent.Items)
            {
                if (li.Selected)
                    Powers += li.Value + "|";
            }
            err = DB.UpdateUser(UserID, UserName, Password, PowerType, Powers);
        }
        gdvData.EditIndex = -1;
        if (err == "")
        {
            gdvData.ShowFooter = false;
            lblMessage.Visible = false;
        }
        else
        {
            lblMessage.Text = err;
            lblMessage.Visible = true;
        }
        BinddtgData();
    }
    //翻页
    protected void gdvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvData.PageIndex = e.NewPageIndex;
        gdvData.ShowFooter = false;
        BinddtgData();
        lblMessage.Visible = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        gdvData.EditIndex = -1;
        gdvData.ShowFooter = true;
        BinddtgData();
        lblMessage.Visible = false;
    }
}

