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
using System.Data.SqlClient;
using System.IO;
using Excel;

public partial class src_datamanage : System.Web.UI.Page
{
    dbConfig sys = new dbConfig();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (sys.getUserPower(Page.User.Identity.Name) == "0")//一般用户不能操作该模块
        {
            Response.Write("对不起，您无权访问该模块");
            Response.End();
        }
        if (!Page.IsPostBack)
        {
            MsnStartDate.MaxDate = getMaxDate();
            MsnEndDate.MaxDate = getMaxDate();
            PopMailStartDate.MaxDate = getMaxDate();
            PopMailEndDate.MaxDate = getMaxDate();
            SmtpMailStartDate.MaxDate = getMaxDate();
            SmtpMailEndDate.MaxDate = getMaxDate();
            HttpStartDate.MaxDate = getMaxDate();
            HttpEndDate.MaxDate = getMaxDate();
            FtpStartDate.MaxDate = getMaxDate();
            FtpEndDate.MaxDate = getMaxDate();
            WebStartDate.MaxDate = getMaxDate();
            WebEndDate.MaxDate = getMaxDate();
            SendWebMailStartDate.MaxDate = getMaxDate();
            SendWebMailEndDate.MaxDate = getMaxDate();
            GetWebMailStartDate.MaxDate = getMaxDate();
            GetWebMailEndDate.MaxDate = getMaxDate();
            BBSStartDate.MaxDate = getMaxDate();
            BBSEndDate.MaxDate = getMaxDate();
            QQStartDate.MaxDate = getMaxDate();
            QQEndDate.MaxDate = getMaxDate();
            YahooStartDate.MaxDate = getMaxDate();
            YahooEndDate.MaxDate = getMaxDate();
            UnlimitStartDate.MaxDate = getMaxDate();
            UnlimitEndDate.MaxDate = getMaxDate();

            MsnStartDate.Value = getMaxDate().ToString();
            MsnEndDate.Value = getMaxDate().ToString();
            PopMailStartDate.Value = getMaxDate().ToString();
            PopMailEndDate.Value = getMaxDate().ToString();
            SmtpMailStartDate.Value = getMaxDate().ToString();
            SmtpMailEndDate.Value = getMaxDate().ToString();
            HttpStartDate.Value = getMaxDate().ToString();
            HttpEndDate.Value = getMaxDate().ToString();
            FtpStartDate.Value = getMaxDate().ToString();
            FtpEndDate.Value = getMaxDate().ToString();
            WebStartDate.Value = getMaxDate().ToString();
            WebEndDate.Value = getMaxDate().ToString();
            SendWebMailStartDate.Value = getMaxDate().ToString();
            SendWebMailEndDate.Value = getMaxDate().ToString();
            GetWebMailStartDate.Value = getMaxDate().ToString();
            GetWebMailEndDate.Value = getMaxDate().ToString();
            BBSStartDate.Value = getMaxDate().ToString();
            BBSEndDate.Value = getMaxDate().ToString();
            QQStartDate.Value = getMaxDate().ToString();
            QQEndDate.Value = getMaxDate().ToString();
            YahooStartDate.Value = getMaxDate().ToString();
            YahooEndDate.Value = getMaxDate().ToString();
            UnlimitStartDate.Value = getMaxDate().ToString();
            UnlimitEndDate.Value = getMaxDate().ToString();

            this.dnsStartDate.Value = getMaxDate().ToString();
            this.dnsEndDate.Value = getMaxDate().ToString();

            this.horseStartDate.Value = getMaxDate().ToString();
            this.horseEndDate.Value = getMaxDate().ToString();

            iniform();
        }
    }

    protected void iniform()
    {
        System.Web.UI.WebControls.CheckBox chk = new System.Web.UI.WebControls.CheckBox();
        System.Web.UI.WebControls.TextBox txt = new System.Web.UI.WebControls.TextBox();
        DropDownList ddl = new DropDownList();
        string subID;
        DataSet ds;
        foreach (Control obj in frmSetting.Controls)
        {
            if (obj.GetType().Name == "CheckBox")
            {
                subID = obj.ID.Substring(4);
                chk = (System.Web.UI.WebControls.CheckBox)FindControl("chk_" + subID);
                txt = (System.Web.UI.WebControls.TextBox)FindControl("txt_" + subID);
                ddl = (DropDownList)FindControl("ddl_" + subID);
                ds = sys.GetPeriods(subID + "_");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["vUnit"].ToString() == "-")
                    {
                        chk.Checked = false;
                        txt.Text = "";
                        ddl.SelectedIndex = 0;
                    }
                    else
                    {
                        chk.Checked = true;
                        txt.Text = ds.Tables[0].Rows[0]["nCounter"].ToString();
                        ddl.SelectedValue = ds.Tables[0].Rows[0]["vUnit"].ToString();
                    }
                }
            }
        }
    }

    //获取日期最大值
    protected DateTime getMaxDate()
    {
        return System.DateTime.Today.AddDays(-1);
    }


    //删除MSN数据
    protected void btnMSNDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(MsnStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(MsnEndDate.Value.ToString()).Date;
        dbMsgAll DB = new dbMsgAll("Msn");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出MSN数据
    protected void btnMSNExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(MsnStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(MsnEndDate.Value.ToString()).Date;
        dbMsgAll DB = new dbMsgAll("Msn");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除PopMail数据
    protected void btnPopDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(PopMailStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(PopMailEndDate.Value.ToString()).Date;
        dbMailBox DB = new dbMailBox("Pop");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出PopMail数据
    protected void btnPopExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(PopMailStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(PopMailEndDate.Value.ToString()).Date;
        dbMailBox DB = new dbMailBox("Pop");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除SmtpMail数据
    protected void btnSmtpDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(SmtpMailStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(SmtpMailEndDate.Value.ToString()).Date;
        dbMailBox DB = new dbMailBox("Smtp");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出SmtpMail数据
    protected void btnSmtpExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(SmtpMailStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(SmtpMailEndDate.Value.ToString()).Date;
        dbMailBox DB = new dbMailBox("Smtp");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除HTTP数据
    protected void btnHttpDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(HttpStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(HttpEndDate.Value.ToString()).Date;
        dbHttpAll DB = new dbHttpAll();
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出HTTP数据
    protected void btnHttpExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(HttpStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(HttpEndDate.Value.ToString()).Date;
        dbHttpAll DB = new dbHttpAll();
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除FTP数据
    protected void btnFtpDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(FtpStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(FtpEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("Ftp");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出FTP数据
    protected void btnFtpExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(FtpStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(FtpEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("Ftp");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除Web数据
    protected void btnWebDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(WebStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(WebEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("Web");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出Web数据
    protected void btnWebExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(WebStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(WebEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("Web");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除SendWebMail数据
    protected void btnSendDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(SendWebMailStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(SendWebMailEndDate.Value.ToString()).Date;
        dbMailSite DB = new dbMailSite("Send");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出SendWebMail数据
    protected void btnSendExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(SendWebMailStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(SendWebMailEndDate.Value.ToString()).Date;
        dbMailSite DB = new dbMailSite("Send");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除GetWebMail数据
    protected void btnGetDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(GetWebMailStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(GetWebMailEndDate.Value.ToString()).Date;
        dbMailSite DB = new dbMailSite("Get");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出GetWebMail数据
    protected void btnGetExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(GetWebMailStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(GetWebMailEndDate.Value.ToString()).Date;
        dbMailSite DB = new dbMailSite("Get");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除BBS数据
    protected void btnBBSDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(BBSStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(BBSEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("BBS");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出BBS数据
    protected void btnBBSExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(BBSStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(BBSEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("BBS");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除QQ数据
    protected void btnQQDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(QQStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(QQEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("QQ");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出QQ数据
    protected void btnQQExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(QQStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(QQEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("QQ");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除Yahoo数据
    protected void btnYahooDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(YahooStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(YahooEndDate.Value.ToString()).Date;
        dbMsgAll DB = new dbMsgAll("Yahoo");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出Yahoo数据
    protected void btnYahooExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(YahooStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(YahooEndDate.Value.ToString()).Date;
        dbMsgAll DB = new dbMsgAll("Yahoo");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    //删除UnLimit数据
    protected void btnLimitDelete_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(UnlimitStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(UnlimitEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("Unlimit");
        DB.DelTables(dteFromDate, dteToDate);
    }

    //导出UnLimit数据
    protected void btnLimitExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(UnlimitStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(UnlimitEndDate.Value.ToString()).Date;
        dbInfoAll DB = new dbInfoAll("Unlimit");
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    private void SendToClient(string FileName)
    {
        System.IO.FileInfo file = new System.IO.FileInfo(FileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
        Response.AddHeader("Content-Length", file.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载 
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端 
        Response.WriteFile(file.FullName);
        // 停止页面的执行 
        Response.End();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        System.Web.UI.WebControls.CheckBox chk = new System.Web.UI.WebControls.CheckBox();
        System.Web.UI.WebControls.TextBox txt = new System.Web.UI.WebControls.TextBox();
        DropDownList ddl = new DropDownList();
        string subID;
        foreach (Control obj in frmSetting.Controls)
        {
            if (obj.GetType().Name == "CheckBox")
            {
                subID = obj.ID.Substring(4);
                chk = (System.Web.UI.WebControls.CheckBox)FindControl("chk_" + subID);
                txt = (System.Web.UI.WebControls.TextBox)FindControl("txt_" + subID);
                ddl = (DropDownList)FindControl("ddl_" + subID);
                if (chk.Checked)
                    sys.UpdatePeriods(subID + "_", ddl.SelectedValue, txt.Text);
                else
                    sys.UpdatePeriods(subID + "_", "-", "-1");
            }
        }
        iniform();
    }

    /// <summary>
    /// 导出DNS数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btDnsExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(dnsStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(dnsEndDate.Value.ToString()).Date;
        dbDns DB = new dbDns();
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }

    /// <summary>
    /// 删除DNS数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btDnsDel_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(dnsStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(dnsEndDate.Value.ToString()).Date;
        dbDns DB = new dbDns();
        DB.DelTables(dteFromDate, dteToDate);
    }

    /// <summary>
    /// 删除木马数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btHorseDel_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(horseStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(horseEndDate.Value.ToString()).Date;
        dbHorse DB = new dbHorse();
        DB.DelTables(dteFromDate, dteToDate);
    }
    /// <summary>
    /// 导出木马数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btHorseExport_Click(object sender, EventArgs e)
    {
        DateTime dteFromDate, dteToDate;
        dteFromDate = Convert.ToDateTime(horseStartDate.Value.ToString()).Date;
        dteToDate = Convert.ToDateTime(horseEndDate.Value.ToString()).Date;
        dbHorse DB = new dbHorse();
        string FileName = Server.MapPath(".") + "\\temp\\" + common.GetFileName() + ".xls";
        DB.ExportToExcel(dteFromDate, dteToDate, FileName);
        SendToClient(FileName);
    }
}



