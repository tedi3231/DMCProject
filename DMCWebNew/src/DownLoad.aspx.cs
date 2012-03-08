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

public partial class src_DownLoad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 写文件
        string filePath = Request.Params["filename"];

        Response.Clear();
        Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filePath, System.Text.Encoding.UTF8));
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
        try
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            Response.BinaryWrite(bytes);
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('下载文件时出错,ERROR:" + ex.Message + "!');window.close();</script>");
        }
        Response.End();
        #endregion
    }


}
