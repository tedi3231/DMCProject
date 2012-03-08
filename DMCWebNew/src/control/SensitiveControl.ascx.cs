using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class src_control_SensitiveControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 返回选中值的十进制值
    /// </summary>
    public int SentiveVal
    {
        get { return GetSenstiveCheck(); }
    }

    private int GetSenstiveCheck()
    {
        int result = 0;
        string[] temp = new string[4];
        foreach (ListItem item in this.CheckBoxList2.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "4")
                {
                    temp[3] = "1";
                }
                if (item.Value == "3")
                {
                    temp[2] = "1";
                }
                if (item.Value == "2")
                {
                    temp[1] = "1";
                }
                if (item.Value == "1")
                {
                    temp[0] = "1";
                }
            }
            else
            {
                if (item.Value == "4")
                {
                    temp[3] = "0";
                }
                if (item.Value == "3")
                {
                    temp[2] = "0";
                }
                if (item.Value == "2")
                {
                    temp[1] = "0";
                }
                if (item.Value == "1")
                {
                    temp[0] = "0";
                }
            }
        }

        string a = temp[3] + temp[2] + temp[1] + temp[0];
        result = Convert.ToInt32(a, 2);
        return result;
    }
}