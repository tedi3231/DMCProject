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

public partial class src_control_DNSTypeUserControl : System.Web.UI.UserControl
{
    /// <summary>
    /// DNS名单类型
    /// </summary>
    private int dnsType;

    /// <summary>
    /// 域名的等级
    /// </summary>
    private int vLevel;

    /// <summary>
    /// 域名的等级
    /// </summary>
    public int VLevel
    {
        get { return vLevel; }
        set { vLevel = value; }
    }


    /// <summary>
    /// DNS名单类型
    /// </summary>
    public int DnsType
    {
        get
        {
            switch (DropDownList1.SelectedValue.Trim())
            {
                case "所有白域名":
                    dnsType = 2;
                    break;

                case "所有异常域名":
                    dnsType = 3;
                    break;

                case "所有动态域名":
                    dnsType = 1;
                    break;

                case "所有黑域名":
                    dnsType = 0;
                    break;

                case "普通黑名单":
                    dnsType = 0;
                    VLevel = 0;
                    break;

                case "重要黑名单":
                    dnsType = 0;
                    VLevel = 1;
                    break;

                case "紧急黑名单":
                    dnsType = 0;
                    VLevel = 2;
                    break;

                default:
                    dnsType = -1;
                    VLevel = -1;
                    break;
            }
            return dnsType;
        }

        set
        {
            //this.DropDownList1.SelectedIndex = -1;
            dnsType = value;
            if (dnsType == 2)
            {
                this.DropDownList1.Items[1].Selected = true;
            }
            else if (dnsType == 3)
            {
                this.DropDownList1.Items[2].Selected = true;
            }
            else if (dnsType == 1)
            {
                this.DropDownList1.Items[3].Selected = true;
            }
            else if (dnsType == 0)
            {
                this.DropDownList1.Items[4].Selected = true;
            }
            else if (dnsType == 0 && vLevel == 0)
            {
                this.DropDownList1.Items[5].Selected = true;
            }
            else if (dnsType == 0 && vLevel == 1)
            {
                this.DropDownList1.Items[6].Selected = true;
            }
            else if (dnsType == 0 && vLevel == 2)
            {
                this.DropDownList1.Items[7].Selected = true;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
