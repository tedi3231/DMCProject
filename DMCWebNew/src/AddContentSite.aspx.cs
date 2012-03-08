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
using System.Collections.Generic;

public partial class src_AddContentSite : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        IList<int> vals = new List<int>();

        foreach (ListItem item in cblList.Items)
        {
            if (item.Selected)
            {
                vals.Add(Convert.ToInt32(item.Value));
            }
        }

        IList<int> vals2 = new List<int>();

        foreach (ListItem item in cblWhiteList.Items)
        {
            if (item.Selected)
            {
                vals2.Add(Convert.ToInt32(item.Value));
            }
        }

        int temp = MakeAnInt(vals);
        int vType = WhiteMakeAnInt(vals2);
        string name = tbName.Text.Trim();
        string id = tbId.Text.Trim();
        string dic = tbDic.Text.Trim();
        string vUpdateUser = Page.User.Identity.Name;

        #region 创建目录
        DataSet ds = DB.CreateDataSet("select top 1 * from TS_Property");

        string path = ds.Tables[0].Rows[0]["vValue"].ToString();
        //path = path + "\\" + dic.Substring(1);

        if (path.EndsWith("\\"))
        {
            //去掉目录中的最后一条路径分隔符 2010.04.05
            path = path.Substring(0, path.Length - 1);           
        }
        path = path + "\\src";

        if (System.IO.Directory.Exists(path))
        {
            System.IO.Directory.CreateDirectory(path);
        }
        #endregion
        string err = DB.InsertSite(id, name, dic, vUpdateUser, temp, vType, path);

        if (err == "")
        {
            ltMsg.Text = "";
            ltMsg.Visible = false;
            //ltMsg.Text = "<script>window.close();window.opener.location.href=window.opener.location.href;</script>";
        }
        else
        {
            ltMsg.Text = err;
            ltMsg.Visible = true;
        }

        Response.Write("<script>javascript:window.close();window.opener.location.href=window.opener.location.href;</script>");
    }

    #region 抓取内容操作方法
    /// <summary>
    /// 将一系列数字综合成一个整形
    /// </summary>
    /// <param name="vals"></param>
    /// <returns></returns>
    private int MakeAnInt(IList<int> vals)
    {
        int temp = 0;

        if (vals == null || vals.Count <= 0)
        {
            return 0;
        }

        foreach (int id in vals)
        {
            switch (id)
            {
                case 1:
                    temp = temp | 0x1;
                    break;
                case 2:
                    temp = temp | 0x2;
                    break;
                case 3:
                    temp = temp | 0x4;
                    break;
                case 4:
                    temp = temp | 0x8;
                    break;
                case 5:
                    temp = temp | 0x10;
                    break;
                case 6:
                    temp = temp | 0x20;
                    break;
                case 7:
                    temp = temp | 0x40;
                    break;
                case 8:
                    temp = temp | 0x80;
                    break;
                case 9:
                    temp = temp | 0x100;
                    break;
                case 10:
                    temp = temp | 0x200;
                    break;
            }
        }

        return temp;
    }

    #endregion

    #region 白名单是否入库操作
    /// <summary>
    /// 将一数字分成系列整形
    /// </summary>
    private IList<int> WhiteSeprateAnInt(int val)
    {
        if (val <= 0)
            return null;

        string str = Convert.ToString(val, 2);
        string str2 = Convert.ToString(0xF, 2);

        IList<int> ids = new List<int>();

        int temp = 0;

        temp = val & 0x1;
        if (temp > 0)
            ids.Add(1);

        temp = val & 0x2;
        if (temp > 0)
            ids.Add(2);

        temp = val & 0x4;
        if (temp > 0)
            ids.Add(3);

        temp = val & 0x8;
        if (temp > 0)
            ids.Add(4);

        temp = val & 0x10;
        if (temp > 0)
            ids.Add(5);

        return ids;
    }

    /// <summary>
    /// 将一系列数字综合成一个整形
    /// </summary>
    /// <param name="vals"></param>
    /// <returns></returns>
    private int WhiteMakeAnInt(IList<int> vals)
    {
        int temp = 0;

        if (vals == null || vals.Count <= 0)
        {
            return 0;
        }

        foreach (int id in vals)
        {
            switch (id)
            {
                case 1:
                    temp = temp | 0x1;
                    break;
                case 2:
                    temp = temp | 0x2;
                    break;
                case 3:
                    temp = temp | 0x4;
                    break;
                case 4:
                    temp = temp | 0x8;
                    break;
                case 5:
                    temp = temp | 0x10;
                    break;
            }
        }

        return temp;
    }
    #endregion

    protected void btExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script>javascript:window.close();window.opener.location.href=window.opener.location.href;</script>");
    }
}
