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

public partial class src_horseInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dbHorse DB = new dbHorse();

        DataSet ds = DB.GetContentByID(Request.QueryString["type"], Request.QueryString["id"]);

        if (ds == null || ds.Tables.Count < 1 || ds.Tables[0].Rows.Count < 1)
            return;

        txtvSrcIp.Text = common.NumberToIP( ds.Tables[0].Rows[0]["vSrcAddr"]) + " | " + ds.Tables[0].Rows[0]["vSrcPort"].ToString();
        txtvSrcMac.Text = ds.Tables[0].Rows[0]["vSrcMac"].ToString();
        txtvDstIp.Text = common.NumberToIP(ds.Tables[0].Rows[0]["vDstAddr"]) + " | " + ds.Tables[0].Rows[0]["vDstPort"].ToString(); ;
        txtvDstMac.Text = ds.Tables[0].Rows[0]["vDstMac"].ToString();
        txtvName.Text = ds.Tables[0].Rows[0]["vSiteName"].ToString();
        //txtvHost.Text = ds.Tables[0].Rows[0]["vHost"].ToString();
        tbvDnsName.Text = ds.Tables[0].Rows[0]["vDnsName"].ToString();

        tbvFlag.Text = common.FormatTrojanFlag(Convert.ToInt32( ds.Tables[0].Rows[0]["vFlag"].ToString()));
        txtvLocalFile.Text = ds.Tables[0].Rows[0]["vLocalFile"].ToString();
        txtvRate.Text = ds.Tables[0].Rows[0]["vRate"].ToString();
        DB.SetReaded(Request.QueryString["type"], Request.QueryString["id"]);
    }

    /// <summary>
    /// 显示归属地内容
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string ip = this.txtvSrcIp.Text;

        if (string.IsNullOrEmpty(ip))
        {
            return;
        }

        ip = ip.Substring(0,ip.IndexOf("|"));

        ltArea.Text = common.GetIpAreaInfo(null, ip);
    }

    //服务器归属地查询
    protected void Button1_Click1(object sender, EventArgs e)
    {        
        string ip = this.txtvDstIp.Text;

        if (string.IsNullOrEmpty(ip))
        {
            return;
        }

        ip = ip.Substring(0, ip.IndexOf("|"));

        ltArea.Text = common.GetIpAreaInfo(null, ip);
    }
   
}
