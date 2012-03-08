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
using System.Data.SqlClient;

public partial class src_left : System.Web.UI.Page
{
    protected dbConfig DB = new dbConfig();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (DB.getUserPower(Page.User.Identity.Name) != "0")
            tvwMenu.Nodes.Add(new TreeNode("<img src='images/list9.gif' border=0 align='absmiddle'>用户操作日志", "tndSystem", "", "LogList.aspx", "mainFrame"));
    }


    protected void tvwMenu_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        if (e.Node.ChildNodes.Count == 0)
        {
            string[] nodevalue = e.Node.ValuePath.Split('/');
            switch (nodevalue[0])
            {
                case "tndSite"://控制中心
                    switch (e.Node.Depth)//节点深度
                    {
                        case 0:
                            CreateSiteCategories(e.Node);
                            break;
                        case 1:
                            CreateProducts(e.Node);
                            break;
                    }
                    break;
                case "tndMIU"://重点怀疑对象
                    switch (e.Node.Depth)
                    {
                        case 0:
                            CreateMIUCategories(e.Node);
                            break;
                        case 1:
                            CreateProducts(e.Node);
                            break;
                    }
                    break;
                case "tndApp"://数据管理中心
                    //if (e.Node.Depth == 0)
                    //    CreateAppList(e.Node);
                    break;
                case "tndSystem"://系统日志
                    break;
            }
        }
    }

    //建立控制中心单位结点
    void CreateSiteCategories(TreeNode node)
    {
        DataSet ds = DB.getSites(User.Identity.Name);
        TreeNode newNode;
        int i = 0;
        if (ds.Tables.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                newNode = new TreeNode(row["VCorpName"].ToString(), row["nid"].ToString());
                //if (i == 0)
                //    newNode.Expand();
                //else
                newNode.Collapse();
                newNode.PopulateOnDemand = true;
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                newNode.ImageUrl = "~/src/images/isite.gif";
                node.ChildNodes.Add(newNode);
                i = i + 1;
            }
        }
    }

    //建立重点怀疑对象单位结点
    void CreateMIUCategories(TreeNode node)
    {
        DataSet ds = DB.getMacIpUsers(User.Identity.Name);
        TreeNode newNode;
        int i = 0;
        string nodeText, nodeValue;
        if (ds.Tables.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                nodeText = "<img src='images/isite3.gif' border=0 align='absmiddle'>" + row["vIp"].ToString() + "[" + row["vMac"].ToString() + "]<br>　∟[" + row["vMark"].ToString() + "]";
                nodeValue = row["nParent"].ToString() + "|" + row["vMac"].ToString() + "|" + row["vIp"].ToString() + "|" + row["nType"].ToString();
                newNode = new TreeNode(nodeText, nodeValue);
                //if (i == 0)
                //    newNode.Expand();
                //else
                newNode.Collapse();
                newNode.PopulateOnDemand = true;
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                //newNode.ImageUrl = "~/src/images/isite3.gif";
                node.ChildNodes.Add(newNode);
                i = i + 1;
            }
        }
    }

    /// <summary>
    /// 在每一个单位下要显示的内容
    /// </summary>
    /// <param name="node">单位结点</param>
    void CreateProducts(TreeNode node)
    {
        TreeNode newNode;

        newNode = new TreeNode("<img src='images/list1.gif' border=0 align='absmiddle'>Http网站", node.Value, "", "HttpList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

        newNode = new TreeNode("<img src='images/list2.gif' border=0 align='absmiddle'>MSN聊天", node.Value, "", "MsnList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

        newNode = new TreeNode("<img src='images/list11.gif' border=0 align='absmiddle'>雅虎聊天", node.Value, "", "YahooList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

        newNode = new TreeNode("<img src='images/list3.gif' border=0 align='absmiddle'>邮件分析", node.Value, "", "MailBoxList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

  

        newNode = new TreeNode("<img src='images/list7.gif' border=0 align='absmiddle'>Web帐号", node.Value, "", "WebList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

        newNode = new TreeNode("<img src='images/list6.gif' border=0 align='absmiddle'>Web邮件", node.Value, "", "MailSiteList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

        newNode = new TreeNode("<img src='images/list8.gif' border=0 align='absmiddle'>实时域名", node.Value, "", "DNSList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

        //newNode = new TreeNode("BBS内容", node.Value, "", "BBSContentList.aspx?nparent=" + node.Value, "mainFrame");
        //newNode.PopulateOnDemand = false;
        //newNode.SelectAction = TreeNodeSelectAction.Select;
        //newNode.ImageUrl = "~/src/images/list9.gif";
        //node.ChildNodes.Add(newNode);

        newNode = new TreeNode("<img src='images/list10.gif' border=0 align='absmiddle'>木马分析", node.Value, "", "HorseList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

        newNode = new TreeNode("<img src='images/list4.gif' border=0 align='absmiddle'>FTP分析", node.Value, "", "FtpList.aspx?nparent=" + node.Value, "mainFrame");
        newNode.PopulateOnDemand = false;
        newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        node.ChildNodes.Add(newNode);

        //newNode = new TreeNode("<img src='images/list12.gif' border=0 align='absmiddle'>突破封锁", node.Value, "", "UnlimitList.aspx?nparent=" + node.Value, "mainFrame");
        //newNode.PopulateOnDemand = false;
        //newNode.SelectAction = TreeNodeSelectAction.SelectExpand;
        //node.ChildNodes.Add(newNode);
    }

    void CreateAppList(TreeNode node)
    {
        DataSet ds = DB.getAppUrls();
        if (ds.Tables.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                node.ChildNodes.Add(new TreeNode("<img src='images/app-2.png' border=0 align='absmiddle'>" + row["vAppName"].ToString(), "", "", row["vUrl"].ToString(), "_block"));
            }
        }
    }
}



