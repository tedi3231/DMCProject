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

public partial class src_HttpUrl : System.Web.UI.Page
{
    protected dbHttpAll DB = new dbHttpAll();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds;
        if (Request.QueryString.ToString() != "")
        {
            ds = DB.GetContentByID(Request.QueryString["type"], Request.QueryString["id"]);
            string url=ds.Tables[0].Rows[0]["vUrl"].ToString();
            urlTXT.Text = ds.Tables[0].Rows[0]["vUrl"].ToString();
            url = url.Substring(7, (url.Length - 7));
            url="http://"+url.Substring(0,url.IndexOf('/'));
            siteTXT.Text = url;

            HyperLink1.NavigateUrl = siteTXT.Text;
            HyperLink2.NavigateUrl = urlTXT.Text;


        }
    }

}



