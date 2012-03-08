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

public partial class src_inTimeDnsList : System.Web.UI.Page
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
        string type = Request.Params["type"];
        if (!string.IsNullOrEmpty(type))
        {
            hdType.Value = type;

            foreach (ListItem item in ddDnsType.Items)
            {
                if (item.Value == type)
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        initSite();
        this.AspNetPager1.PageSize = this.pageSize;
    }

    /// <summary>
    /// 对单位进行初始化
    /// </summary>
    private void initSite()
    {
        dbConfig dbHost = new dbConfig();
        cblHost.DataSource = dbHost.getSites(User.Identity.Name);
        cblHost.DataBind();
    }


    /// <summary>
    /// 根据不同的情况设定查询条件
    /// </summary>
    private void initCondition()
    {
        //清空条件
        this.conditon = string.Empty;

        //初始化各查询条件
        this.endTime = dtEnd.Text;
        this.startTime = dtStart.Text;
        this.dnsType = Convert.ToInt32( this.ddDnsType.SelectedValue );
        this.dnsIp = tbDnsIp.Text;
        this.dnsName = tbDns.Text;
        this.userIp = tbInIp.Text;

        IList<string> pList = new List<string>();

        foreach (ListItem item in cblHost.Items)
        {
            if (item.Selected)
            {
                pList.Add(item.Value);
            }
        }

        StringBuilder bul = new StringBuilder();

        bul.Append(" 1=1 ");
        bul.Append(" and vType=");
        bul.Append(this.dnsType );


        if (!string.IsNullOrEmpty(dnsIp))
        {
            bul.Append(" and vDstIp='");
            bul.Append(dnsIp);
            bul.Append("' ");
        }

        if (!string.IsNullOrEmpty(this.dnsName))
        {
            bul.Append(" and vName='");
            bul.Append(this.dnsName);
            bul.Append("' ");
        }

        if (!string.IsNullOrEmpty(this.userIp))
        {
            bul.Append(" and vSrcIp='");
            bul.Append(this.userIp);
            bul.Append("' ");
        }

        if (!string.IsNullOrEmpty(startTime) && !string.IsNullOrEmpty(endTime))
        {
            bul.Append(" and (vTime between '");
            bul.Append(startTime);
            bul.Append("' and '");
            bul.Append(endTime);
            bul.Append("') ");
        }

        if (pList.Count > 0)
        {
            bul.Append(" and (");
            for (int i = 0; i < pList.Count; i++)
            {
                if (i == 0)
                {
                    bul.Append("nParent=");
                    bul.Append(pList[i]);
                    bul.Append(" ");
                }
                if (i != 0)
                {
                    bul.Append(" or nParent=");
                    bul.Append(pList[i]);
                    bul.Append(" ");
                }
            }
            bul.Append(")");
        }

        this.conditon = bul.ToString();
    }

    /// <summary>
    /// 查询到数据并绑定
    /// </summary>     
    private void pageDataBind()
    {
        DataSet ds = null;

        //if (TableType == "4")
        //{
        //    ds = db.GetpPageData(ConfigurationManager.ConnectionStrings["HorseAllConnStr"].ConnectionString,
        //    "TC_Horse_" + DateTime.Now.ToString("yyyyMMdd"), "nId", AspNetPager1.CurrentPageIndex,
        //    this.pageSize, "*", "nId asc",
        //    this.Condition, out itemCount, out pageCount);
        //}
        //else
        {
            ds = db.GetpPageData(ConfigurationManager.ConnectionStrings["DnsAllConnStr"].ConnectionString,
            "Temp_Dns_All", "vTime", AspNetPager1.CurrentPageIndex,
            this.pageSize, "vName,dmc_config.dbo.[f_Int2IP](vDstIp) as vDstIp,vDstPort,dmc_config.dbo.[f_Int2IP](vSrcIp) as vSrcIp,vType,vtime", "vTime asc",
            this.conditon, out itemCount, out pageCount);
        }

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
