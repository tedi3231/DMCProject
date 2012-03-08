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

public partial class src_AddDynamicBlackDNS : System.Web.UI.Page
{
    private dbConfig db = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        int type = 1;
        int level = 0;

        string ips = this.tbDns.Text;
        string content = this.tbContent.Text;

        if (string.IsNullOrEmpty(ips))
        {
            Message.Show(Page, "对不起，域名地址不能为空");
            return;
        }

        DataSet ds = null;


        string temp = ips.Trim();
        ds = db.GetBlackWhiteDynamicModel(ips);
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Message.Show(Page, "对不起，此域名 " + temp + "已存在,点击确定继续");
        }
        else
        {
            db.AddBlackAndWhiteDynamic(temp, content);
        }


        ltMsg.Text = "<script>window.close();window.opener.location.href=window.opener.location.href;</script>";
    }
}
