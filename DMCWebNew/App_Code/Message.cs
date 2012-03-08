using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

/// <summary>
/// 显示消息提示对话框
/// </summary>
public class Message
{
    private Message()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 显示消息提示对话框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    public static void Show(Page page, string msg)
    {
        string script = "<script language='javascript'>alert('" + msg + "');</script>";
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", script);
    }

    /// <summary>
    /// 控件点击 消息确认提示框
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    public static void ShowConfirm(WebControl Control, string msg)
    {
        Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
    }

    /// <summary>
    /// 显示消息提示对话框，并进行页面跳转
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="msg">提示信息</param>
    /// <param name="url">跳转的目标URL</param>
    public static void ShowAndRedirect(Page page, string msg, string url)
    {
        StringBuilder Builder = new StringBuilder();
        Builder.Append("<script language='javascript'>");
        Builder.AppendFormat("alert('{0}');", msg);
        Builder.AppendFormat("self.location.href='{0}'", url);
        Builder.Append("</script>");
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());

    }
    /// <summary>
    /// 输出自定义脚本信息
    /// </summary>
    /// <param name="page">当前页面指针，一般为this</param>
    /// <param name="script">输出脚本</param>
    public static void ResponseScript(Page page, string script)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript'>" + script + "</script>");
    }

    public static void OpenProgressBar(Page Page)
    {
        StringBuilder sbScript = new StringBuilder();

        sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
        sbScript.Append("<!--\n");
        sbScript.Append("window.showModalDialog('Progress.aspx','','dialogHeight: 100px; dialogWidth: 350px; edge: Raised; center: Yes; help: No; resizable: No; status: No;scroll:No;');\n");
        //sbScript.Append("window.open('Progress.aspx','','width=350,height=100,top=200,left=200,scrollbars=no,location=no,menubar=no,resizable=no,status=no');\n");
        sbScript.Append("// -->\n");
        sbScript.Append("</script>\n");

        Page.RegisterClientScriptBlock("OpenProgressBar", sbScript.ToString());
    }

    public static void DownloadFile(Page Page)
    {
        StringBuilder sbScript = new StringBuilder();

        sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
        sbScript.Append("<!--\n");
        sbScript.Append("window.showModalDialog('Download.aspx','','dialogHeight: 100px; dialogWidth: 350px; edge: Raised; center: Yes; help: No; resizable: No; status: No;scroll:No;');\n");
        sbScript.Append("// -->\n");
        sbScript.Append("</script>\n");

        Page.RegisterClientScriptBlock("DownloadFile", sbScript.ToString());
    }

    /// <summary>
    /// 显示导出对话框
    /// </summary>
    /// <param name="path"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void ShowModelDialog(Page Page,string path, int width, int height)
    {
        StringBuilder sbScript = new StringBuilder();

        sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
        sbScript.Append("<!--\n");
        sbScript.Append("window.showModalDialog(\"'");
        sbScript.Append(path);
        sbScript.Append("','',dialogHeight: ");
        sbScript.Append(height);
        sbScript.Append("px;dialogWidth:");
        sbScript.Append(width);
        sbScript.Append("px;edge: Raised; center: Yes; help: No; resizable: No; status: No;scroll:No;');\n");        
        sbScript.Append("// -->\n");
        sbScript.Append("</script>\n");

        Page.RegisterClientScriptBlock("ShowModelDialog", sbScript.ToString());
    }
}