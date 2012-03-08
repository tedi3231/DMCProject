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

public partial class src_getstate : System.Web.UI.Page
{
    dbConfig DB = new dbConfig();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (File.Exists(Server.MapPath(ConfigurationManager.AppSettings["Getstatefile"].ToString())))
            {
                BinddtgData();
            }
           
        }
    }

    private void BinddtgData()
    {
        PagedDataSource objPage = new PagedDataSource();
        DataView dv = DB.getSites(Page.User.Identity.Name).Tables[0].DefaultView;
        objPage.DataSource = dv;
        objPage.AllowPaging = true;
        objPage.PageSize = 20;

        if (pagecount.Text.ToString() != "")
            currentpage.Text = pagecount.Text.ToString();
        else
            currentpage.Text = "1";

        objPage.CurrentPageIndex = Convert.ToInt32(currentpage.Text) - 1;
        if (!objPage.IsFirstPage)
            lnkPrev.Visible = true;
        else
            lnkPrev.Visible = false;
        if (!objPage.IsLastPage)
            lnkNext.Visible = true;
        else
            lnkNext.Visible = false;
        BindlsbPage(objPage.PageCount, objPage.CurrentPageIndex);
        gdvData.DataSource = objPage;
        gdvData.DataBind();
        lblCurPage.Text = "第" + currentpage.Text + "页/共" + objPage.PageCount + "页（" + dv.Count + "条记录）";
    }
    protected void BindlsbPage(int PageCount, int CurPage)
    {
        ListItem li;
        string strPageNum;
        lsbPage.Items.Clear();
        for (int i = 0; i < PageCount; i++)
        {
            strPageNum = Convert.ToString(i + 1);
            li = new ListItem(strPageNum, strPageNum);
            if (strPageNum == Convert.ToString(CurPage + 1))
                li.Selected = true;
            lsbPage.Items.Add(li);
        }
    }
    //上一页
    protected void lnkPrev_Click(object sender, EventArgs e)
    {
        pagecount.Text = Convert.ToString(Convert.ToInt32(currentpage.Text.ToString()) - 1);
        BinddtgData();
    }
    //下一页
    protected void lnkNext_Click(object sender, EventArgs e)
    {
        pagecount.Text = Convert.ToString(Convert.ToInt32(currentpage.Text.ToString()) + 1);
        BinddtgData();
    }
    //跳转
    protected void lsbPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        pagecount.Text = lsbPage.SelectedValue.ToString();
        BinddtgData();
    }
   
    protected string fun2(object s)
    {
        string _s = s.ToString();

        StreamReader objReader = new StreamReader(Server.MapPath(ConfigurationManager.AppSettings["Getstatefile"].ToString()));

        string sLine = "";
        ArrayList arrText = new ArrayList();
        string[] sArray;
        string state = "";
     
        while (sLine != null)
        {

            sLine = objReader.ReadLine();
            if (sLine != null)
                arrText.Add(sLine);

        }

        for (int i = 1; i < arrText.Count;i++ )
        {
            sArray = arrText[i].ToString().Trim().Split('=');
            if (sArray.Length > 0)
            {
                if (sArray[0].ToString().Trim() == "COUNT")
                {
                    state = sArray[1].ToString().Trim();
                    if (i < arrText.Count-1)
                    {
                        sArray = arrText[i + 1].ToString().Trim().Split('=');
                        if (sArray[1].ToString().Trim() == s.ToString())
                        {
                            if (Convert.ToInt32(state) > 0)
                                _s = "在线";
                            else
                                _s = "离线";
                            break;
                        }
                        else
                            _s = "离线";
                    }
                }
            }
        }
        objReader.Close();

        return _s;
    }
}



