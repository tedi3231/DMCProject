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

public partial class src_BlackWhiteMacKeyWord : System.Web.UI.Page
{
    private dbConfig db = new dbConfig();

    private string where = "";

    private int itemCount, pageCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageDataBind();
        }
    }

    /// <summary>
    /// 数据绑定操作
    /// </summary>
    private void PageDataBind()
    {
        DataSet ds = db.GetpPageData(ConfigurationManager.ConnectionStrings["ConfigConnStr"].ConnectionString, "TS_BWMacList", "id", AspNetPager1.CurrentPageIndex, 20, "*", "id asc", this.where, out itemCount, out pageCount);

        GridView1.DataSource = ds.Tables[0].DefaultView;
        GridView1.DataBind();

        AspNetPager1.RecordCount = itemCount;
        AspNetPager1.DataBind();
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        AspNetPager1.RecordCount = 100;
        PageDataBind();
    }


    /// <summary>
    /// 检索按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btQuery_Click(object sender, EventArgs e)
    {
        string ip = tbIP.Text;

        where = " 1=1";

        if (!string.IsNullOrEmpty(ip))
        {
            where = where + " and vName like '%" + ip + "%' ";
        }

        string type = this.DropDownList1.SelectedValue;

        if (Convert.ToInt32(type) != -1)
        {
            where = where + " and vType = " + type;
        }

        PageDataBind();
    }

    /// <summary>
    /// 行内控件事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = 0;

        if (e.CommandArgument != null)
        {
            if (!int.TryParse(e.CommandArgument.ToString(), out id))//说明参数不正确            
            {
                return;
            }
        }

        if (e.CommandName.Equals("del")) //删除
        {
            if (db.DelMacModel(id)) // 删除成功
            {
                PageDataBind();
            }
        }
    }

    protected void lbClearWhite_Click(object sender, EventArgs e)
    {
        if (db.DelMacByType(1))
        {
            PageDataBind();
        }
    }

    protected void lbClearBlack_Click(object sender, EventArgs e)
    {
        if (db.DelMacByType(0))
        {
            PageDataBind();
        }
    }

    /// <summary>
    /// 删除选中项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btDel_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow objItem in GridView1.Rows)
        {
            CheckBox chk = (CheckBox)objItem.FindControl("cbMail");
            if (chk.Checked)
                db.DelMacModel(Convert.ToInt32(GridView1.DataKeys[objItem.RowIndex].Value.ToString()));
        }

        PageDataBind();
        //BinddtgData(qrytypelist.SelectedItem.Value);
    }
}
