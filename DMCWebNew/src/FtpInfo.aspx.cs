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

public partial class src_FtpInfo : System.Web.UI.Page
{
    protected dbInfoAll DB = new dbInfoAll("Ftp");
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds;
        if (Request.QueryString.ToString() != "")
        {
            ds = DB.GetContentByID(Request.QueryString["type"], Request.QueryString["id"]);
            txtSrcMac.Text = ds.Tables[0].Rows[0]["vSrcMac"].ToString();
            txtSrcAddr.Text = common.NumberToIP(ds.Tables[0].Rows[0]["vSrcAddr"]);
            txtDstMac.Text = ds.Tables[0].Rows[0]["vDstMac"].ToString();
            txtDstAddr.Text = common.NumberToIP(ds.Tables[0].Rows[0]["vDstAddr"]);
            txtLogin.Text = ds.Tables[0].Rows[0]["vLogin"].ToString();
            txtPwd.Text = ds.Tables[0].Rows[0]["vPwd"].ToString();

            //文件名和文件大小
            string temp = ds.Tables[0].Rows[0]["vSiteName"].ToString();

            if (!string.IsNullOrEmpty(temp))
            {
                string[] arr = temp.Split(new char[] { '|'});

                if (arr != null && arr.Length == 2)
                {
                    txtFilePath.Text = arr[0];
                    txtFileSize.Text = arr[1];
                }
            }
           
             
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



