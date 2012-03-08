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

public partial class src_InfoInfo : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        dbInfoAll DB = new dbInfoAll(Request.QueryString["infotype"].ToString());
        DataSet ds;
        if (Request.QueryString["type"] != null)
        {
            ds = DB.GetContentByID(Request.QueryString["type"], Request.QueryString["id"]);
            txtSrcMac.Text = ds.Tables[0].Rows[0]["vSrcMac"].ToString();
            txtSrcAddr.Text = common.NumberToIP(ds.Tables[0].Rows[0]["vSrcAddr"]);
            txtDstMac.Text = ds.Tables[0].Rows[0]["vDstMac"].ToString();
            txtDstAddr.Text = common.NumberToIP( ds.Tables[0].Rows[0]["vDstAddr"]);
            txtLogin.Text = ds.Tables[0].Rows[0]["vLogin"].ToString();
            txtPwd.Text = ds.Tables[0].Rows[0]["vPwd"].ToString();
            //DB.SetReaded(Request.QueryString["type"], Request.QueryString["id"]);
            if (Request.QueryString["infotype"].ToString() == "web")
            {
                trSite.Visible = true;
                txtSiteName.Text = ds.Tables[0].Rows[0]["vsitename"].ToString();
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        ltArea.Text = common.GetIpAreaInfo(null, txtSrcAddr.Text);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ltArea.Text = common.GetIpAreaInfo(null, txtDstAddr.Text);
    }
}



