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

public partial class src_UpdateBlackDNS : System.Web.UI.Page
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

        DataSet ds = db.GetBlackWhiteModel(Convert.ToInt32(id));

        if (ds == null || ds.Tables[0].Rows.Count <= 0)
        {
            Message.Show(Page, "没有找到数据");
            return;
        }

        DataRow row = ds.Tables[0].Rows[0];

        this.TextBox1.Text = row["vName"].ToString();
        this.tbContent.Text = row["vContent"].ToString();
        int vLevel = Convert.ToInt32(row["vLevel"].ToString());

        foreach (ListItem item in this.ddlTypeList.Items)
        {
            if (Convert.ToInt32(item.Value) == vLevel)
            {
                item.Selected = true;
                break;
            }
        }       
        hdId.Value = id;
    }

    /// <summary>
    /// 提交修改方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(hdId.Value);
        string ip = this.TextBox1.Text;
      
        if (string.IsNullOrEmpty(ip))
        {
            Message.Show(Page, "DNS不能为空");
            return;
        }

        if (db.UpdateBlackAndWhite(id,ip,Convert.ToInt32(ddlTypeList.SelectedValue),0,tbContent.Text))
        {
            //Message.Show(Page, "修改域名成功");
            ltMsg.Text = "<script>window.close();window.opener.location.href=window.opener.location.href;</script>";
            return;
        }
        else
        {
            Message.Show(Page, "修改DNS失败");
            return;
        }

    }
}
