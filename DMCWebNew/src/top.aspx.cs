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
using System.Web.Configuration;

public partial class src_top : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();
    public double TimeOutCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
        Configuration _configuration = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        AuthenticationSection _authenticationSection = (AuthenticationSection)_configuration.GetSection("system.web/authentication");
        FormsAuthenticationConfiguration _formsAuthentication = _authenticationSection.Forms;
        TimeOutCount = _formsAuthentication.Timeout.TotalSeconds - 5;
        */
        //hidFromDate.Value = System.DateTime.Today.ToString();
        //hidToDate.Value = System.DateTime.Today.ToString();
        if (DB.getUserPower(Page.User.Identity.Name) == "0")//一般用户不能操作数据管理模块
        {
            tdUser.Visible = true;
            tdAdmin.Visible = false;
        }
        else
        {
            tdUser.Visible = false;
            tdAdmin.Visible = true;
        }
    }

    protected void imbLogout_Click(object sender, ImageClickEventArgs e)
    {
        string userid = Page.User.Identity.Name;
        string userip = Request.ServerVariables["Remote_Addr"].ToString();
        //DB.changeuserstate(userid, "0", userip);
        common.setLog(userid, DB.getUserName(userid), userip, "退出");
        FormsAuthentication.SignOut();
        //FormsAuthentication.RedirectToLoginPage();
        //Response.Redirect("login.aspx");
        Response.Write("<script>javascript:window.open('login.aspx','_top')</script>");
    }
}


