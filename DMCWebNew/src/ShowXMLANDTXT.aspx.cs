using System;
using System.IO;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;

public partial class src_ShowXMLANDTXT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ShowFile();
        }
    }

    /// <summary>
    /// 显示文件XML和TXT
    /// </summary>
    private void ShowFile()
    {
        string file = Request.Params["file"];
        Response.Write( helper.XmlTxtOperator.GetContentFromFile(file)  );
    }
}
