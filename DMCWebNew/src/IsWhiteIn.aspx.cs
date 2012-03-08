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

public partial class src_IsWhiteIn : System.Web.UI.Page
{
    private dbConfig db = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = db.GetWhiteInList();

            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return;
            }

            DataRow row = ds.Tables[0].Rows[0];

            tbMark.Text = row["vMark"] == DBNull.Value ? "" : row["vMark"].ToString();
            hdId.Value = row["nId"].ToString();

            int flag = row["vFlag"] == DBNull.Value ? 0 : Convert.ToInt32(row["vFlag"]);


            IList<int> ids = SeprateAnInt(flag);

            if (ids != null && ids.Count > 0)
            {
                foreach (int temp in ids)
                {
                    cblList.Items[temp - 1].Selected = true;
                }
            }
        }
    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btSave_Click(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(hdId.Value);

        IList<int> vals = new List<int>();

        foreach (ListItem item in cblList.Items)
        {
            if (item.Selected)
            {
                vals.Add(Convert.ToInt32(item.Value));
            }
        }

        string vMark = tbMark.Text.Trim();
        int vFlag = MakeAnInt(vals);

        if (id == 0) //新建
        {
            if (db.AddWhiteIn(vFlag, vMark))
            {
                Message.Show(this.Page, "添加记录成功");
            }
            else
            {
                Message.Show(this.Page, "添加记录失败");
            }
        }
        else // 更新
        {
            if (db.UpdateWhiteIn(id, vFlag, vMark))
            {
                Message.Show(this.Page, "更新记录成功");
            }
            else
            {
                Message.Show(this.Page, "更新记录失败");
            }
        }
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
            }
        }

        return temp;
    }
}
