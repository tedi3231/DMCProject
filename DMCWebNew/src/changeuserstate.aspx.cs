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

public partial class src_changeuserstate : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();
    protected void Page_Load(object sender, EventArgs e)
    {
        string userid = Page.User.Identity.Name.ToString();
        string userip = Request.ServerVariables["Remote_Addr"].ToString();
        common.setLog(userid, DB.getUserName(userid), userip, "退出");
        if (userid != "")
        {
            //DB.changeuserstate(userid, "0", userip);
            FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
        }
        //Response.Write("<script>alert('dddddd');</script>");
        //Response.Redirect("top.aspx");
    }
}



