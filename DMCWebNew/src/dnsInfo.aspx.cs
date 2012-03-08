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

public partial class src_dnsInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string vType = Request.QueryString["vType"]!=null?Request.QueryString["vType"].ToString():"";
        string tType =Request.QueryString["type"]!=null? Request.QueryString["type"].ToString():"";
        string id = Request.QueryString["id"]!=null?Request.QueryString["id"].ToString():"";

        dbDns dbdns = null;

        /// 白名单
        if (vType == "6")
        {
            dbdns = new dbDns("dns");            
        }
        else // 非白名单
        {
            dbdns= new dbDns("dnsalarm");      
        }

        DataSet ds = dbdns.getDnsInfo(tType, id);

        if( ds==null || ds.Tables.Count<1 || ds.Tables[0].Rows.Count<1 )
            return ;

        txtvSrcIp.Text = common.NumberToIP( ds.Tables[0].Rows[0]["vSrcIp"] );
        txtvSrcMac.Text = ds.Tables[0].Rows[0]["vSrcMac"].ToString();
        txtvDstIp.Text = common.NumberToIP( ds.Tables[0].Rows[0]["vDstIp"] );
        txtvDstMac.Text = ds.Tables[0].Rows[0]["vDstMac"].ToString();
        txtvName.Text = ds.Tables[0].Rows[0]["vName"].ToString();
        txtvType.Text = common.FormatDnsType( Convert.ToInt32( ds.Tables[0].Rows[0]["vType"].ToString() ));
        tbvAddr.Text = common.NumberToIP(ds.Tables[0].Rows[0]["vAddr"]);

        dbdns.SetReaded(Request.QueryString["type"], Request.QueryString["id"]);
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    if (!string.IsNullOrEmpty(Request.Params["vFlag"]))
    //    {
    //        ltArea.Text = common.GetIpAreaInfo(Request.Params["vFlag"], txtvDstIp.Text);           
    //    }
    //}

    protected void Button3_Click(object sender, EventArgs e)
    {
        ltArea.Text = common.GetIpAreaInfo(null, txtvDstIp.Text);
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        ltArea.Text = common.GetIpAreaInfo(null, tbvAddr.Text);
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        ltArea.Text = common.GetIpAreaInfo(null, txtvSrcIp.Text);
    }
}
