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

public partial class src_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            //自动更新
            try
            {
                VersionUpdate.update(true);
            }
            catch { }
            txtUser.Text = "";
            //txtPassword1.Text = "";
            txtUser.Focus();
        }
        if (Request.IsAuthenticated)
            Response.Redirect("Main.aspx");

    }

    protected void imgLogin_Click(object sender, ImageClickEventArgs e)
    {
        string sql = "SELECT nId,IsLogin,LoginIP FROM TS_Login WHERE vLogin='{0}' and vPsw='{1}'";

        string pass = common.md5(this.txtPassword.Text.Trim());

        sql = String.Format(sql, txtUser.Text, pass);
        dbConfig DB = new dbConfig();
        //保存APPPath
        DB.UpdateProperty("AppPath", Server.MapPath(Request.ApplicationPath));

        string UserID = DB.ExecuteScalar(sql);
        if (UserID != "")
        {
            string useridstr = DB.CreateDataSet(sql).Tables[0].Rows[0]["nId"].ToString();
            string userislogin = DB.CreateDataSet(sql).Tables[0].Rows[0]["IsLogin"].ToString();
            string useripstr = DB.CreateDataSet(sql).Tables[0].Rows[0]["LoginIP"].ToString();
            string userip = Request.ServerVariables["Remote_Addr"].ToString();
            Session["FromDate"] = System.DateTime.Today.AddDays(-1);
            Session["ToDate"] = System.DateTime.Today.AddDays(-1);
            //将登录信息定入日志文件
            common.setLog(useridstr, DB.getUserName(UserID), userip, "登录");
            Session["adminName"] = txtUser.Text.Trim();

            /*if (userislogin == "1")
            {
                if (userip == useripstr)
                {
                    FormsAuthentication.RedirectFromLoginPage(UserID, false);
                    
                    DB.changeuserstate(useridstr, "1", userip);
                    Response.Redirect("Main.aspx");
                }
                else
                {
                    lblMessage.Text = "该用户已经登录！";
                    lblMessage.Visible = true;
                }
            }
            else
            {*/
            FormsAuthentication.RedirectFromLoginPage(UserID, false);

            //DB.changeuserstate(useridstr, "1", userip);
            Response.Redirect("Main.aspx");
            //}
        }
        else
        {
            txtUser.Focus();
            lblMessage.Text = "用户名或密码错误，请重新输入！";
            lblMessage.Visible = true;
        }
    }
    protected void imgCancel_Click(object sender, ImageClickEventArgs e)
    {
        txtUser.Text = "";
        //txtPassword1.Text = "";
        txtUser.Focus();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sql = "SELECT nId,IsLogin,LoginIP FROM TS_Login WHERE vLogin='{0}' and vPsw='{1}'";

        string pass = common.md5(this.txtPassword.Text.Trim());

        sql = String.Format(sql, txtUser.Text, pass);
        dbConfig DB = new dbConfig();
        //保存APPPath
        DB.UpdateProperty("AppPath", Server.MapPath(Request.ApplicationPath));

        string UserID = DB.ExecuteScalar(sql);
        if (UserID != "")
        {
            string useridstr = DB.CreateDataSet(sql).Tables[0].Rows[0]["nId"].ToString();
            string userislogin = DB.CreateDataSet(sql).Tables[0].Rows[0]["IsLogin"].ToString();
            string useripstr = DB.CreateDataSet(sql).Tables[0].Rows[0]["LoginIP"].ToString();
            string userip = Request.ServerVariables["Remote_Addr"].ToString();
            Session["FromDate"] = System.DateTime.Today.AddDays(-1);
            Session["ToDate"] = System.DateTime.Today.AddDays(-1);
            //将登录信息定入日志文件
            common.setLog(useridstr, DB.getUserName(UserID), userip, "登录");
            Session["adminName"] = txtUser.Text.Trim();

            /*if (userislogin == "1")
            {
                if (userip == useripstr)
                {
                    FormsAuthentication.RedirectFromLoginPage(UserID, false);
                    
                    DB.changeuserstate(useridstr, "1", userip);
                    Response.Redirect("Main.aspx");
                }
                else
                {
                    lblMessage.Text = "该用户已经登录！";
                    lblMessage.Visible = true;
                }
            }
            else
            {*/
            FormsAuthentication.RedirectFromLoginPage(UserID, false);

            //DB.changeuserstate(useridstr, "1", userip);
            Response.Redirect("Main.aspx");
            //}
        }
        else
        {
            txtUser.Focus();
            lblMessage.Text = "用户名或密码错误，请重新输入！";
            lblMessage.Visible = true;
        }
    }
}
