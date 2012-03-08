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
using System.IO;
using System.Text;

public partial class src_ContentSiteList : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.GridView1.PageSize = 20;
            this.GridView1.AllowPaging = true;
            BinddtgData();
        }

        if (DB.getUserPower(Page.User.Identity.Name) != "-1")//非管理员不得进行新增修改删除操作
        {
            //tdAdd.Visible = false;
            //gdvData.Columns[5].Visible = false;
        }
    }

    /// <summary>
    /// 绑定页面
    /// </summary>
    private void BinddtgData()
    {
        DataSet ds = DB.getSites(Page.User.Identity.Name);

        DataTable dt = null;

        //if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
        //{
        //    dt = new DataTable();
        //    dt.Rows.Add(dt.NewRow());
        //}
        //else
        //{
        dt = ds.Tables[0];
        //}

        //DataView dv = DB.getSites(Page.User.Identity.Name).Tables[0].DefaultView;
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
    }

    //获取在线离线状态
    protected string getState(object siteID)
    {
        if (siteID == null)
            return "离线";

        if (siteID.ToString() == "1")
            return "在线";

        if( siteID.ToString() == "0" )
            return "离线";

        return "离线";

        //string _s = "离线";
        //string sLine = "";
        //StreamReader objReader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["Getstatefile"].ToString()));

        //while (sLine != null)
        //{
        //    sLine = objReader.ReadLine();

        //    if (sLine != null)
        //    {
        //        if (sLine.IndexOf("COUNT") < 0 && sLine.IndexOf("= " + siteID.ToString()) > 0)
        //        {
        //            _s = "在线";
        //            break;
        //        }
        //    }
        //}
        //objReader.Close();
        //return _s;
    }


    protected void btDelete_Click(object sender, EventArgs e)
    {

    }

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
            //Response.Write("<script>alert('" + e.CommandArgument + "');</script>");
            DB.DeleteSite(e.CommandArgument.ToString());
            BinddtgData();
        }
    }

    /// <summary>
    /// 由值分解出哪些白名单入库
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected string WhiteMakeSiteString(object str)
    {
        StringBuilder bul = new StringBuilder();

        if (str == DBNull.Value)
            return string.Empty;

        int val = Convert.ToInt32(str);

        if (val <= 0)
            return string.Empty ;

        int temp = 0;

        temp = val & 0x1;
        if (temp > 0)
            bul.Append("垃圾邮件关键字,");

        temp = val & 0x2;
        if (temp > 0)
            bul.Append("白IP,");

        temp = val & 0x4;
        if (temp > 0)
            bul.Append("白MAC,");

        temp = val & 0x8;
        if (temp > 0)
            bul.Append("白邮箱,");

        temp = val & 0x10;
        if (temp > 0)
            bul.Append("DNS白名单");

        return bul.ToString();
    }

    /// <summary>
    /// 将一数字变成一系列类型名称
    /// </summary>
    protected string MakeSiteString(object str)
    {
        StringBuilder bul = new StringBuilder();

        if (str == DBNull.Value)
            return string.Empty;

        int val = Convert.ToInt32(str);

        if (val <= 0)
            return null;

        int temp = 0;

        temp = val & 0x1;
        if (temp > 0)
            bul.Append("http,");

        temp = val & 0x2;
        if (temp > 0)
            bul.Append("msn,");

        temp = val & 0x4;
        if (temp > 0)
            bul.Append("yahoo通,");

        temp = val & 0x8;
        if (temp > 0)
            bul.Append("ftp,");

        temp = val & 0x10;
        if (temp > 0)
            bul.Append("pop3,");

        temp = val & 0x20;
        if (temp > 0)
            bul.Append("smtp,");

        temp = val & 0x40;
        if (temp > 0)
            bul.Append("web收,");

        temp = val & 0x80;
        if (temp > 0)
            bul.Append("web发,");

        temp = val & 0x100;
        if (temp > 0)
            bul.Append("dns,");

        temp = val & 0x200;
        if (temp > 0)
            bul.Append("木马");

        return bul.ToString();
    }

    /// <summary>
    /// 数据绑定动作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex < 0)
        {
            return;
        }

        string activeDate = e.Row.Cells[1].Text;

        if (activeDate == null || activeDate.Trim() == "" || activeDate == "&nbsp;")
        {
            return;
        }

        DateTime now = DateTime.Now;
        DateTime aDate = Convert.ToDateTime(activeDate);
        TimeSpan t1 = now - aDate;

        string nID = e.Row.Cells[0].Text;

        string overTime = ConfigurationManager.AppSettings["SiteOverTime"];

        //默认为5分钟
        int over = 300;

        if (!string.IsNullOrEmpty(overTime))
        {
            over = Convert.ToInt32(overTime);
        }

        if (t1.TotalSeconds > over)
        {
            DB.UpdateActiveStatus(nID, 0);
            e.Row.Cells[6].Text = "离线";
        }
        else
        {
            DB.UpdateActiveStatus(nID, 1);
            e.Row.Cells[6].Text = "在线";
        }
        
    }

 
}
