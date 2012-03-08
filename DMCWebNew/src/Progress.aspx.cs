using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class src_Progress : System.Web.UI.Page
{
    private int state = 0;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["State"] != null)
        {
            state = Convert.ToInt32(Session["State"].ToString());
        }
        else
        {
            Session["State"] = 0;
        }
        if (state >= 0 && state <= 99)
        {
            this.lblMessages.Text = "比对进行中!";
            this.panelProgress.Width = state * 3;
            this.lblPercent.Text = state + "%";
            Message.ResponseScript(this.Page, "setTimeout('window.Form1.submit()',500);");
        }
        if (state == 100)
        {
            this.panelProgress.Visible = false;
            this.panelBarSide.Visible = false;
            this.lblPercent.Text = "100%";
            this.lblMessages.Text = "比对完成，共有" + Session["recCount"] + "条记录中标!";
            btnClose.Visible = true;
            //Message.ResponseScript(this.Page,"setTimeout('window.close()',1000);");
        }
    }
}
