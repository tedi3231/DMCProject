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

public partial class TrojanKeyWordList : System.Web.UI.Page
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
        DataSet ds = db.GetpPageData(ConfigurationManager.ConnectionStrings["ConfigConnStr"].ConnectionString, "TS_TrojanList", "id", AspNetPager1.CurrentPageIndex, 20, "*", "id asc", this.where, out itemCount, out pageCount);

        GridView1.DataSource = ds.Tables[0].DefaultView;
        GridView1.DataBind();

        AspNetPager1.RecordCount = itemCount;
        AspNetPager1.DataBind();
    }

    /// <summary>
    /// 将整数协议转化为协议名称
    /// </summary>
    /// <param name="prc">
    /// 数字表示的协议名称
    /// <p> 0 所有，1 TCP，2 UDP，3 其它</p>
    /// </param>
    /// <returns>转换后的协议名称</returns>
    public string FormatProtocol(int level)
    {       
            switch (level)
            {
                case 0:
                    return "所有";

                case 1:
                    return "TCP";

                case 2:
                    return "UDP";

                default:
                    return "其它";
            }       
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
        string dns = tbHorse.Text;
       
        if (dns != null || dns.Trim() != "")
        {
            where = " vName like '%" + dns + "%' ";
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
            if (db.DelHorseModel(id)) // 删除成功
            {
                PageDataBind();
            }
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
                db.DelHorseModel(Convert.ToInt32(GridView1.DataKeys[objItem.RowIndex].Value.ToString()));
        }

        PageDataBind();        
    }
}
