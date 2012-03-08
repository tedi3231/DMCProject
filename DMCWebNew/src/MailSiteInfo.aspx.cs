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

public partial class src_MailSiteInfo : System.Web.UI.Page
{
    protected dbMailSite DB;
    private string strMailAddr = "";
    public string emlFile = "";
    public string emlFile2 = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds;
        if (Request.QueryString.ToString() != "")
        {
            DB = new dbMailSite(Request.QueryString["mail"]);
            ds = DB.GetContentByID(Request.QueryString["type"], Request.QueryString["id"]);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtLpFrom.Text = ds.Tables[0].Rows[0]["vLpFrom"].ToString();
                txtCapture.Text = ds.Tables[0].Rows[0]["dCapture"].ToString();
                txtLpTo.Text = ds.Tables[0].Rows[0]["vLpTo"].ToString();
                txtLpCc.Text = ds.Tables[0].Rows[0]["vLpCc"].ToString();
                txtLogin.Text = ds.Tables[0].Rows[0]["vLogin"].ToString();
                txtPwd.Text = ds.Tables[0].Rows[0]["vPwd"].ToString();
                txtSrcMac.Text = ds.Tables[0].Rows[0]["vSrcMac"].ToString();
                txtDstMac.Text = ds.Tables[0].Rows[0]["vDstMac"].ToString();
                txtSrcAddr.Text = common.NumberToIP(ds.Tables[0].Rows[0]["vSrcAddr"]);
                txtDstAddr.Text = common.NumberToIP(ds.Tables[0].Rows[0]["vDstAddr"]);
                txtLpTitle.Text = ds.Tables[0].Rows[0]["vLpTitle"].ToString();
                txtmapadd.Text = ds.Tables[0].Rows[0]["vLocalFile"].ToString();
                txtWebFile.Text = ds.Tables[0].Rows[0]["vWebFile"].ToString();
                tbBcc.Text = ds.Tables[0].Rows[0]["vLpBcc"].ToString();
                if (ds.Tables[0].Rows[0]["nAttach"].ToString() == "1")
                {
                    txtWebFile.Width = 200;
                    imbWebFile.Visible = true;
                }
                else
                {
                    txtWebFile.Width = 220;
                    imbWebFile.Visible = false;
                }

                strMailAddr = ds.Tables[0].Rows[0]["vLocalFile"].ToString();
                //emlFile = common.GetFileUrl(strMailAddr);
                emlFile2 = strMailAddr;
                if (!string.IsNullOrEmpty(strMailAddr))//&& System.IO.File.Exists(strMailAddr)
                {
                    emlFile = "../" + strMailAddr.Substring(strMailAddr.LastIndexOf('\\') + 1);
                }
                else
                {
                    emlFile = string.Empty;
                }

                DB.SetReaded(Request.QueryString["type"], Request.QueryString["id"]);
            }
        }
    }
    public string getServerFile()
    {
        //string _str = strMailAddr.Replace('\\', '/');

        string _str = this.txtmapadd.Text;

        if (!string.IsNullOrEmpty(_str))
        {
            _str = _str.Replace('\\', '/');
        }
        else
        {
            _str = string.Empty;
        }
        _str = common.GetFileUrl(_str);
        return _str;
        //return emlFile;

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



