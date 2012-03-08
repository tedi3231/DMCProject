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

public partial class src_AddBlackIP : System.Web.UI.Page
{
    private dbConfig db = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        int type = Convert.ToInt32(this.DropDownList1.SelectedValue) ;       
        string ips = this.tbName.Text;

        if (string.IsNullOrEmpty(ips))
        {
            Message.Show(Page, "对不起，IP地址不能为空");
            return;
        }

        if (type < 0)
        {
            Message.Show(this, "对不起，必须选择IP类型");
            return;
        }

        string[] strIps = ips.Split(new char[] { '\n' });

        if (strIps == null || strIps.Length <= 0)
        {
            Message.Show(Page, "对不起，输入的域名每行必须以换行结尾");
            return;
        }

        DataSet ds = null;

        foreach (string ip in strIps)
        {
            string temp = ip.Trim();

            if (!common.isIP(temp))
            {
                continue;
            }

            ds = db.GetIPModel(temp);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                continue;                
            }
            else
            {
                db.AddIPModel(temp,type);
            }
        }

        ltMsg.Text = "<script>window.close();window.opener.location.href=window.opener.location.href;</script>";
    }
}
