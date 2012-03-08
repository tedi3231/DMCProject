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

public partial class BlackWhiteDNSKeyWord : System.Web.UI.Page
{
    private dbConfig db = new dbConfig();

    private string where = "";

    private int itemCount, pageCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // PageDataBind();
            FormInit();
        }
    }

    private void FormInit()
    {
        //string type = Request.QueryString["type"];

        //if (string.IsNullOrEmpty(type))
        //{
        //    return;
        //}
        //this.where = " vLevel =  "+type;

        //if (type.Trim() == "4")
        //{
        //    this.DropDownList1.Items.Clear();
        //    this.DropDownList1.Items.Add(new ListItem("动态域名","4"));
        //}

        //this.DropDownList1.SelectedValue = type.Trim();

        PageDataBind();
    }

    /// <summary>
    /// 数据绑定操作
    /// </summary>
    private void PageDataBind()
    {
        DataSet ds = db.GetpPageData(ConfigurationManager.ConnectionStrings["ConfigConnStr"].ConnectionString, "TS_DnsList", "nid", AspNetPager1.CurrentPageIndex, 10, "*", "nid asc", this.where, out itemCount, out pageCount);
        

        GridView1.DataSource = ds.Tables[0].DefaultView;
        GridView1.DataBind();

        AspNetPager1.RecordCount = itemCount;
        AspNetPager1.DataBind();
    }    

    /// <summary>
    /// 将整数等级转化为字符等级
    /// </summary>
    /// <param name="type">数字表示的等级</param>
    /// <returns>转换后的等级字符</returns>
    public string FormatLevel(int level)
    {
        switch (level)
        {
            case 0:
                return "<span style=\"width:100%;background-color:#0099FF\">普通黑域名</span>";

            case 1:
                return "<span style=\"width:100%;background-color:yellow\">重要黑域名</span>";

            case 2:
                return "<span style=\"width:100%;background-color:red\">紧急黑域名</span>";
            case 3:
                return "白域名";
            case 4:
                return "动态域名";
            default:
                return " ";
        }
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        AspNetPager1.CurrentPageIndex = e.NewPageIndex;
        InitWhere();
        PageDataBind();
    }

    /// <summary>
    /// 初始化条件
    /// </summary>
    private void InitWhere()
    {
        string dns = tbDns.Text;
        int vLevel = Convert.ToInt32(this.DropDownList1.SelectedValue);

        if (!string.IsNullOrEmpty(dns))
        {
            where = " vName like '%" + dns.Trim() + "%' ";
        }
        else
        {
            where = " 1=1 ";
        }

        if (vLevel >= 0)
        {
            where = where + " and vLevel = " + vLevel;
        }
    }

    /// <summary>
    /// 检索按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btQuery_Click(object sender, EventArgs e)
    {
        InitWhere();
        this.AspNetPager1.CurrentPageIndex = 1;
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
            if (db.DelBlackAndWhite(id)) // 删除成功
            {
                PageDataBind();
            }
        }
        else if (e.CommandName.Equals("update")) //更新
        {

        }
    }
    protected void lbClearWhite_Click(object sender, EventArgs e)
    {
        string type = lbClearWhite.CommandArgument;
        if (deleteBlack(type))
        {
            PageDataBind();
        }
    }

    protected void lbClearBlack_Click(object sender, EventArgs e)
    {
        string type = lbClearBlack.CommandArgument;
        if (deleteBlack(type))
        {
            PageDataBind();
        }
    }

    private bool deleteBlack(string type)
    {
        int id = 0;
        if (!int.TryParse(type, out id))
        {
            return false;
        }

        return db.DelBlackAndWhite(type);
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
                db.DelBlackAndWhite(Convert.ToInt32(GridView1.DataKeys[objItem.RowIndex].Value.ToString()));
        }

        PageDataBind();
        //BinddtgData(qrytypelist.SelectedItem.Value);
    }

    protected void btImport_Click(object sender, EventArgs e)
    {
        //Response.Redirect("inputPass.aspx?type=5");
        //if (db.getUserPower(Page.User.Identity.Name) != "-1")//非管理员不得进行新增修改删除操作
        //{
        //    ltMsg.Text = "<script>alert('对不起，必须是管理员才有权限做此操作');window.close();</script>";
        //    return;
        //}

        ////确认管理员密码
        //Message.ShowModelDialog(this.Page, "inputPass.aspx", 200, 50);
    }
}
