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

public partial class UpdateTrojan : System.Web.UI.Page
{
    dbConfig db = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageDataBind();
        }
    }

    private void PageDataBind()
    {
        string id = Request.Params["id"];
        if (string.IsNullOrEmpty(id))
        {
            Message.Show(Page, "参数为空");
            return;
        }

        DataSet ds = db.GetHorseModel(Convert.ToInt32(id));

        if (ds == null || ds.Tables[0].Rows.Count <= 0)
        {
            Message.Show(Page, "没有找到数据");
            return;
        }

        DataRow row = ds.Tables[0].Rows[0];

        tbName.Text = row["vName"].ToString();
        tbKey.Text = row["vKey"].ToString();
        tbContent.Text = row["vContent"].ToString();
        tbPort.Text = row["vPort"].ToString();
        RadioButtonList1.SelectedValue = row["vProtocl"].ToString();

        rblFlag.SelectedValue = row["vFlag"].ToString();

        hdID.Value = id;
    }

    /// <summary>
    /// 提交修改方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        string id = hdID.Value;
        string hName = tbName.Text;
        string hKey = tbKey.Text;
        string hContent = tbContent.Text;
        string hPort = tbPort.Text;
        int protocl = Convert.ToInt32( this.RadioButtonList1.SelectedValue );


        string content = tbContent.Text;

        if (string.IsNullOrEmpty(hKey))
        {
            Message.Show(Page, "木马特征码不能为空");
            return;
        }

        if (db.UpdateHorseModel(Convert.ToInt32(id),hName,hKey,protocl,hPort,Convert.ToInt32(rblFlag.SelectedValue),hContent))
        {
            //Message.Show(Page, "修改域名成功");
            ltMsg.Text = "<script>window.close();window.opener.location.href=window.opener.location.href;</script>";
            return;
        }
        else
        {
            Message.Show(Page, "修改木马失败");
            ltMsg.Text = "<script>window.close();</script>";
            return;
        }

    }
}
