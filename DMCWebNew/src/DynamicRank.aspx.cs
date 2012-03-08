using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class src_DynamicRank : System.Web.UI.Page
{
    private string conditon = "";

    private int itemCount, pageCount;

    private int pageSize = 20;
    private dbDns db = new dbDns();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            init();
        }
    }

    /// <summary>
    /// 初始化操作
    /// </summary>
    private void init()
    {
        initCondition();
        pageDataBind();
        this.AspNetPager1.PageSize = this.pageSize;
    }


    /// <summary>
    /// 根据不同的情况设定查询条件
    /// </summary>
    private void initCondition()
    {
        //清空条件
        this.conditon = string.Empty;       

        if (!string.IsNullOrEmpty(Request.Params["type"]))
            this.hdType.Value = Request.Params["type"];

        if (string.IsNullOrEmpty(this.hdType.Value)) //按DNS域名排名
        {
            this.conditon = "vType=1";
            this.GridView1.Columns[0].HeaderText = "域名";
            ltTitle.Text = "动态域名排名(按域名)";
            this.Title = "动态域名排名(按域名)";
        }
        else //按服务器地址排名
        {
            this.conditon = "vType=2";
            this.GridView1.Columns[0].HeaderText = "内网IP地址";
            ltTitle.Text = "动态域名排名(按IP)";
            this.Title = " 动态域名排名(按IP)";
        }          
    }

    /// <summary>
    /// 查询到数据并绑定
    /// </summary>     
    private void pageDataBind()
    {
        DataSet ds = db.GetpPageData(ConfigurationManager.ConnectionStrings["DnsAllConnStr"].ConnectionString, "TC_Dynamic_rank", "nid", AspNetPager1.CurrentPageIndex, this.pageSize, "*", "nid asc", this.conditon, out itemCount, out pageCount);

        GridView1.DataSource = ds.Tables[0].DefaultView;
        GridView1.DataBind();

        AspNetPager1.RecordCount = itemCount;
        AspNetPager1.DataBind();
    }
    
    /// <summary>
    /// 分页方法
    /// </summary>
    /// <param name="src"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        this.AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        initCondition();
        pageDataBind();
    }
}
