using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class src_SearchClues : System.Web.UI.Page
{
    protected database DB = null;
    protected dbConfig configDB = new dbConfig();


    protected string Condition = "";

    private const int _PageSize = 250;
    private int _RecordCount = 0;
    private string sParent = string.Empty;
    private int selectedCount = 0;
    private string ipAddr = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dbConfig configDB = new dbConfig();
            string gUserName = configDB.getUserName(User.Identity.Name);
            string gIP = Request.ServerVariables["Remote_Addr"];
            string gContent = "线索分析";
            common.setLog(User.Identity.Name, gUserName, gIP, gContent);

            cblHost.DataSource = configDB.getSites(User.Identity.Name);
            cblHost.DataBind();

            FormInit();
        }
    }


    private void Init()
    {
        DateTime beginTime = DateTime.Now;
        DateTime endTime = DateTime.Now;

        switch (qrytypelist.SelectedItem.Value)
        {
            case "0"://自选时间段
                beginTime = Convert.ToDateTime(sdate.Value.ToString());
                endTime = Convert.ToDateTime(edate.Value.ToString()).AddDays(1);
                break;

            case "1"://前三天
                beginTime = DateTime.Today.AddDays(-3);
                endTime = DateTime.Now;
                break;

            case "2"://前一周
                beginTime = DateTime.Today.AddDays(-7);
                endTime = DateTime.Now;
                break;

            case "3"://前一月
                beginTime = DateTime.Today.AddMonths(-1);
                endTime = DateTime.Now;
                break;

            case "5"://昨天
                beginTime = DateTime.Today.AddDays(-1);
                endTime = DateTime.Now;
                break;
        }

        //获取选中的服务器
        foreach (ListItem li in cblHost.Items)
        {
            if (li.Selected)
            {
                if (selectedCount == 0)  //防止只选一个站点时，出现在,1的情况 查询报错
                {
                    sParent = li.Value;
                }
                else
                {
                    sParent += "," + li.Value + "";
                }

                selectedCount++;
            }
        }

        if (selectedCount <= 0)//tedi3231 added 2010.02.01 没有选中任何站点时显示用户能看到的所有站点 
        {
            sParent = common.GetHosrList(User.Identity.Name);
        }

        sParent = sParent.Replace("'", "");

        sParent = "(" + sParent + ")";
        ipAddr = tbIp.Text.Trim();

        dbConfig conDB = new dbConfig();

        DataSet ds = conDB.ExecuteClues(ipAddr, beginTime, endTime, sParent);

        if (ds == null)
        {

        }
        else
        {
            ltDns.Text = ds.Tables[0].Rows[0]["DnsCount"].ToString();
            ltHttp.Text = ds.Tables[0].Rows[0]["httpCount"].ToString();
            ltMsn.Text = ds.Tables[0].Rows[0]["MsnCount"].ToString();
            ltYahoo.Text = ds.Tables[0].Rows[0]["YahooCount"].ToString();
            ltTrojan.Text = ds.Tables[0].Rows[0]["TrojanCount"].ToString();

            ltPop.Text = ds.Tables[0].Rows[0]["Pop3MailCount"].ToString();
            ltSmtp.Text = ds.Tables[0].Rows[0]["SmtpMailCount"].ToString();

            ltSend.Text = ds.Tables[0].Rows[0]["SendWebMailCount"].ToString();
            ltRev.Text = ds.Tables[0].Rows[0]["GetWebMailCount"].ToString();

            ltWeb.Text = ds.Tables[0].Rows[0]["WebCount"].ToString();

            ltFtp.Text = ds.Tables[0].Rows[0]["FtpCount"].ToString();

        }
    }

    private void FormInit()
    {
        sdate.MaxDate = DateTime.Today.AddDays(-1);
        edate.MaxDate = DateTime.Today.AddDays(-1);
        sdate.Value = Session["FromDate"];
        edate.Value = Session["ToDate"];
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Init();
    }
}
