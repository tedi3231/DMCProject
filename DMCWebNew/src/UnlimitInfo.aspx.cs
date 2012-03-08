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

public partial class src_UnlimitInfo : System.Web.UI.Page
{
    protected dbInfoAll DB = new dbInfoAll("Unlimit");
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds;
        if (Request.QueryString.ToString() != "")
        {
            ds = DB.GetContentByID(Request.QueryString["type"], Request.QueryString["id"]);
            txtSrcMac.Text = ds.Tables[0].Rows[0]["vSrcMac"].ToString();
            txtSrcAddr.Text = ds.Tables[0].Rows[0]["vSrcAddr"].ToString();
            txtDstMac.Text = ds.Tables[0].Rows[0]["vDstMac"].ToString();
            txtDstAddr.Text = ds.Tables[0].Rows[0]["vDstAddr"].ToString();
            txtLogin.Text = ds.Tables[0].Rows[0]["vLogin"].ToString();
            txtPwd.Text = ds.Tables[0].Rows[0]["vPwd"].ToString();

            //DB.SetReaded(Request.QueryString["type"], Request.QueryString["id"]);
        }
    }
}



