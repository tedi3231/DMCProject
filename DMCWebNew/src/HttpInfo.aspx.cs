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

public partial class src_HttpInfo : System.Web.UI.Page
{
    protected dbHttpAll DB = new dbHttpAll();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds;
        if (Request.QueryString.ToString() != "")
        {
            ds = DB.GetContentByID(Request.QueryString["type"], Request.QueryString["id"]);
           
            txtCapture.Text = common.SecondToDate( Convert.ToInt64( ds.Tables[0].Rows[0]["dCapture"]) );
            txtSrcAddr.Text = common.NumberToIP( ds.Tables[0].Rows[0]["vSrcAddr"]);
            txtSrcMac.Text = ds.Tables[0].Rows[0]["vSrcMac"].ToString();
            txtDstAddr.Text = common.NumberToIP( ds.Tables[0].Rows[0]["vDstAddr"] );
            txtDstMac.Text = ds.Tables[0].Rows[0]["vDstMac"].ToString();
            txtHost.Text = ds.Tables[0].Rows[0]["vHost"].ToString();
            string url = ds.Tables[0].Rows[0]["vUrl"].ToString();

            url = url.Substring(7, (url.Length - 7));
            url = "http://" + url.Substring(0, url.IndexOf('/'));
            txtSiteName.Text = url;
            txtUrl.Text = ds.Tables[0].Rows[0]["vUrl"].ToString();
            //txtSiteName.Text = ds.Tables[0].Rows[0]["vSiteName"].ToString();
            DB.SetReaded(Request.QueryString["type"], Request.QueryString["id"]);
        }
    }
    /// <summary>
    /// 显示归属地内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        ltArea.Text = common.GetIpAreaInfo(null, txtSrcAddr.Text);
    }

    //服务器归属地查询
    protected void Button1_Click1(object sender, EventArgs e)
    {
        ltArea.Text = common.GetIpAreaInfo(null, txtDstAddr.Text);
    }
}



