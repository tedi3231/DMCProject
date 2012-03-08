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

public partial class src_AddBlackDNS : System.Web.UI.Page
{

    private dbConfig db = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btSave_Click(object sender, EventArgs e)
    {

        int level = Convert.ToInt32(this.ddlTypeList.SelectedValue);// this.DNSTypeUserControl1.VLevel;
        string content = tbContent.Text;
        string ips = this.TextBox1.Text;

        if (string.IsNullOrEmpty(ips))
        {
            Message.Show(Page, "对不起，域名地址不能为空");
            return;
        }

        if (level < 0)
        {
            Message.Show(this, "对不起，必须选择DNS类型");
            return;
        }

        string[] strIps = ips.Split(new char[] { '\n'});

        if (strIps == null || strIps.Length <= 0)
        {
            Message.Show(Page, "对不起，输入的域名每行必须以换行结尾");
            return;
        }

        DataSet ds = null;

        dbDns dn = new dbDns();

        foreach (string ip in strIps)
        {
            string temp = ip.Trim();
            ds = db.GetBlackWhiteModel(temp);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (level == 4)
                {
                    dn.ExecuteScalar("DELETE FROM [TC_Dns_doubtlist] WHERE [vName]='" + ip+"'");
                }
                Message.Show(Page, "对不起，此域名 " + ip + "已存在,点击确定继续");
            }
            else
            {
                db.AddBlackAndWhite(temp,level,0,content);
            }
        }

        ltMsg.Text = "<script>window.close();window.opener.location.href=window.opener.location.href;</script>";
    }
}
