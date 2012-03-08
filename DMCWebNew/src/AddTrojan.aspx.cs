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

public partial class AddTrojan : System.Web.UI.Page
{
    private dbConfig db = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btSave_Click(object sender, EventArgs e)
    {
        string hName = tbName.Text;
        string hKey = tbKey.Text;
        string hContent = tbContent.Text;
        string hPort = tbPort.Text;
        int protocol = Convert.ToInt32(this.RadioButtonList1.SelectedValue);


        if (string.IsNullOrEmpty(hName))
        {
            Message.Show(Page, "对不起，木马名称不能为空");
            return;
        }

        if (string.IsNullOrEmpty(hKey))
        {
            Message.Show(this, "对不起，木马特征码不能为空");
            return;
        }

      DataSet  ds = db.GetHorseModel(hName);

        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            Message.Show(Page, "对不起，此木马 " + hName + "已存在,点击确定继续");
        }
        else
        {
            db.AddHorseModel(hName, hKey, protocol, hPort, Convert.ToInt32(this.rblFlag.SelectedValue), hContent);
        }

        ltMsg.Text = "<script>window.close();window.opener.location.href=window.opener.location.href;</script>";
    }
}
