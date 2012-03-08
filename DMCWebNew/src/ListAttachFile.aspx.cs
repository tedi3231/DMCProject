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

public partial class src_ListAttachFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String directory = Request.Form["txtWebFile"].ToString();
        DirectoryInfo dir = new DirectoryInfo(directory);
        ListItem li;
        if (dir.Exists)
        {
            FileSystemInfo[] files = dir.GetFiles();
            if (files.Length > 0)
            {
                foreach (FileSystemInfo fsi in files)
                {
                    string temp = common.GetFileUrl(fsi.FullName);
                    li = new ListItem(fsi.Name,temp );                    
                    bulAttach.Items.Add(li);
                }
                lblMessage.Text = "";
            }
            else
                lblMessage.Text = "邮件附件不存在！";
        }
        else
            lblMessage.Text = "邮件附件地址错误！";
        hplBack.NavigateUrl = common.GetFileUrl(Request.Form["txtmapadd"].ToString());
    }
}
