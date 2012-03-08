using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class src_changeIpDnsList : System.Web.UI.Page
{
    private string conditon = "";

    private int itemCount, pageCount;

    private int pageSize = 20;

    private string endTime;
    private string startTime;
    private int dnsType;
    private string dnsName;
    private string dnsIp;
    private string userIp;


    private dbConfig db = new dbConfig();
    private dbDns dbDns = new dbDns();         

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
        StringBuilder bul = new StringBuilder();

        bul.Append(" 1=1 ");
        bul.Append(" and vType<>6 GROUP BY vName order by vTime");
        
        this.conditon = bul.ToString();
    }

    /// <summary>
    /// 查询到数据并绑定
    /// </summary>     
    private void pageDataBind()
    {
        DataSet ds = null;

        ds = dbDns.getIpChangeCount();

        GridView1.DataSource = ds.Tables[0].DefaultView;
        GridView1.DataBind();

        AspNetPager1.RecordCount = itemCount;
        AspNetPager1.DataBind();
    }

    /// <summary>
    /// 查询按钮事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        initCondition();
        pageDataBind();
    }

    /// <summary>
    /// 分页控件分页时方法
    /// </summary>
    /// <param name="src"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        pageDataBind();
    }

    /// <summary>
    /// 行数据绑定时触发的事件方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string test = string.Empty;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lb = (Label)e.Row.FindControl("lbArea");
            test = lb.Text;

            if (!common.isIP(test)) // 判断不是IP正规地址的情况
            {
                test = "未知地址";
            }
            else
            {
                test = db.GetIpArea(test);
                if (string.IsNullOrEmpty(test))
                {
                    test = "未知地址";
                }
            }

            lb.Text = test;
            //db.get
        }
    }
}
