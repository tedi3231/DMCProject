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

public partial class src_BBSInfo : System.Web.UI.Page
{
    protected dbInfoAll DB = new dbInfoAll("BBS");

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds;
        if (Request.QueryString.ToString() != "")
        {
            ds = DB.GetContentByID(Request.QueryString["type"], Request.QueryString["id"]);

            int ipsrcnum = ds.Tables[0].Rows[0]["vSrcAddr"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["vSrcAddr"]);
            int ipdstnum = ds.Tables[0].Rows[0]["vDstAddr"] == DBNull.Value ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0]["vDstAddr"]);

            txtSrcMac.Text = ds.Tables[0].Rows[0]["vSrcMac"].ToString();
            txtSrcAddr.Text = common.NumberToIP(ipsrcnum);
            txtDstMac.Text = ds.Tables[0].Rows[0]["vDstMac"].ToString();
            txtDstAddr.Text = common.NumberToIP(ipdstnum);
            txtLogin.Text = ds.Tables[0].Rows[0]["vLogin"].ToString();
            txtPwd.Text = ds.Tables[0].Rows[0]["vPwd"].ToString();

            dbConfig df = new dbConfig();

            ltArea.Text = df.GetIpArea(ipdstnum);

            //DB.SetReaded(Request.QueryString["type"], Request.QueryString["id"]);
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



