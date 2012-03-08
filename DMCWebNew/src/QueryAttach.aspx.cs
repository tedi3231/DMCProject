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
using System.Data.OleDb;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

public partial class src_QueryAttach : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string text = txtQueryText.Text.Trim();
        if (text.Length > 0)
        {
            using (OleDbConnection conn = new OleDbConnection("PROVIDER=MSIDXS;DATA SOURCE=" + txtIndex.Text))
            {
                conn.Open();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"select Rank,HitCount,Filename,Size,Write,PATH,Directory,DocAppName,DocCategory,DocKeywords from Scope() where CONTAINS ( '{0}') order by Rank desc,WRITE desc", txtQueryText.Text.Trim());

                    //MSIDXS 不支持参数
                    //cmd.Parameters.Add("Pram1",OleDbType.VarWChar);
                    //cmd.Parameters[0].Value = txtQueryText.Text;
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {

                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            gdvFiles.DataSource = dt;
                            gdvFiles.DataBind();
                        }

                    }
                }
            }
        }
    }

    protected void gdvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "download")
        {
            GridView gv = sender as GridView;
            if (gv != null)
            {
                string path = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
                downloadFile(path);
            }
        }
    }

    private void downloadFile(string FullPath)
    {

        string extension = Path.GetExtension(FullPath);
        string fileName = Path.GetFileName(FullPath);
        string ct = "";
        try
        {
            //取得扩展名对应的 mime 类型
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(extension);
            ct = regKey.GetValue("Content Type") as string;
        }
        catch (Exception ex)
        {
            Debug.Write(ex.Message);
        }

        if (ct == null) //没取到的就按照文件
            ct = "application/octet-stream";

        this.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
        this.Response.ContentType = ct;
        this.Response.WriteFile(FullPath);
        this.Response.End();
    }

    protected void gdvFiles_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToLower() == "download")
        {
            GridView gv = sender as GridView;
            if (gv != null)
            {
                string path = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
                downloadFile(path);

            }
        }
    }
}
