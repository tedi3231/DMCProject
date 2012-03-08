using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class src_UpdateContentSite : System.Web.UI.Page
{
    dbConfig db = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageDataBind();
        }
    }

    private void PageDataBind()
    {
        string id = Request.Params["id"];

        if (string.IsNullOrEmpty(id))
        {
            Message.Show(Page, "参数为空");
            return;
        }

        DataSet ds = db.getSiteByKeyID(id);

        if (ds == null || ds.Tables[0].Rows.Count <= 0)
        {
            Message.Show(Page, "没有找到数据");
            return;
        }

        DataRow row = ds.Tables[0].Rows[0];

        tbName.Text = row["vCorpName"].ToString();
        tbId.Text = row["nId"].ToString();
        tbDic.Text = row["vFilePath"].ToString();
        int flag = row["vFlag"] == DBNull.Value ? 0 : Convert.ToInt32(row["vFlag"]);
        int vType = row["vType"] == DBNull.Value ? 0 : Convert.ToInt32(row["vType"]);


        // 抓取内容
        IList<int> ids = SeprateAnInt(flag);

        if (ids != null && ids.Count > 0)
        {
            foreach (int temp in ids)
            {
                cblList.Items[temp - 1].Selected = true;
            }
        }

        // 白名单是否入库
        IList<int> ids2 = WhiteSeprateAnInt(vType);

        if (ids2 != null && ids2.Count > 0)
        {
            foreach (int temp in ids2)
            {
                //this.cblWhiteList.Items[temp - 1].Selected = true;
                foreach( ListItem t in cblWhiteList.Items )
                {
                    if (t.Value == temp.ToString())
                    {
                        t.Selected = true;
                        break;
                    }
                    //this.cblWhiteList.SelectedValue = temp.ToString();
                }
            }
        }


        hdId.Value = id;
    }

    /// <summary>
    /// 提交修改方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {

        string name = tbName.Text;
        string nid = tbId.Text;
        string dic = tbDic.Text;

        IList<int> vals = new List<int>();

        foreach (ListItem item in cblList.Items)
        {
            if (item.Selected)
            {
                vals.Add(Convert.ToInt32(item.Value));
            }
        }


        IList<int> vals2 = new List<int>();

        foreach (ListItem item in this.cblWhiteList.Items)
        {
            if (item.Selected)
            {
                vals2.Add(Convert.ToInt32(item.Value));
            }
        }

        int val = MakeAnInt(vals);
        int vType = WhiteMakeAnInt(vals2);

        string vUpdateUser = Page.User.Identity.Name;

        string err = db.UpdateSite(hdId.Value, nid, name, dic, vUpdateUser, val, vType);

        if (err == "")
        {
            //ltMsg.Text = "<script>window.close();window.opener.location.href=window.opener.location.href;</script>";
        }
        else
        {
            ltMsg.Text = err;
            ltMsg.Visible = true;
        }

        Response.Write("<script>javascript:window.close();window.opener.location.href=window.opener.location.href;</script>");
    }

    /// <summary>
    /// 将一数字分成系列整形
    /// </summary>
    private IList<int> SeprateAnInt(int val)
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

        temp = val & 0x20;
        if (temp > 0)
            ids.Add(6);

        temp = val & 0x40;
        if (temp > 0)
            ids.Add(7);

        temp = val & 0x80;
        if (temp > 0)
            ids.Add(8);

        temp = val & 0x100;
        if (temp > 0)
            ids.Add(9);

        temp = val & 0x200;
        if (temp > 0)
            ids.Add(10);


        return ids;
    }



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
