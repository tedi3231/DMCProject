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
using System.Data.SqlClient;
using System.IO;

public partial class src_mailboxinfo : System.Web.UI.Page
{
    protected dbMailBox DB;
    private string strMailAddr="";
    public string emlFile = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds;
            if (Request.QueryString.ToString() != "")
            {
                DB = new dbMailBox(Request.QueryString["mail"]);
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
                    strMailAddr = ds.Tables[0].Rows[0]["vLocalFile"].ToString();

                    tbBcc.Text = ds.Tables[0].Rows[0]["vLpBcc"].ToString();

                    emlFile = common.GetFileUrl(strMailAddr);

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
    }

    /// <summary>
    /// 获取下载地址
    /// </summary>
    /// <returns></returns>
    public string getServerFile()
    {
        string _str = this.txtmapadd.Text;

        if (!string.IsNullOrEmpty(_str))
        {
            _str = _str.Replace('\\', '/');
        }
        else
        {
            _str = string.Empty;
        }
        //
        //if (_str.Length > 0)
        //{
        //    if (Request.IsLocal)//如果是在服务器本机操作
        //    {
        //        _str = Request.MapPath(common.GetFileUrl(_str));
        //        _str = _str.Replace("\\", "\\\\");
        //    }
        //    else
        //    {
        //        //_str = "file://" + Request.ServerVariables[48].ToString() + common.GetHttpHead(Request.ServerVariables[42].ToString()) + common.GetFileUrl(_str, 2);
        //        _str = "file://" + Request.ServerVariables[48].ToString() + "/" + common.GetFileUrl(_str);
        //        _str = _str.Replace("http://", "");
        //    }
        //}
        return _str;
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


