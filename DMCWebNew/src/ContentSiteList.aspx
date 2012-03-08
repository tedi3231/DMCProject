<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContentSiteList.aspx.cs"
    Inherits="src_ContentSiteList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>节点管理</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <!-- <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />-->
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var oWindow;
        var listen_started = false;
        function showParentWin() {
            var url = ""; //定义弹出窗口的URL   
            var model = ""; //定义弹出窗口样式   
            oWindow = window.open('AddContentSite.aspx', 'newwindow', 'height=300, width=500, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
            //注意下面才是重点   
            if (!listen_started) {
                setTimeout(refreshSelf, 1000);
            }
            else {
                listen_started = true;
            }
        }

        //
        function showUpdateWin(id) {
            var url = ""; //定义弹出窗口的URL   
            var model = ""; //定义弹出窗口样式   
            oWindow = window.open('UpdateContentSite.aspx?id=' + id, 'newwindow', 'height=300, width=500, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
            //注意下面才是重点   
            if (!listen_started) {
                setTimeout(refreshSelf, 1000);
            }
            else {
                listen_started = true;
            }
        }
        //刷新本页面   
        function refreshSelf() {
            if (!oWindow.closed) {
                setTimeout(refreshSelf, 500);
            }
            else {
                listen_started = false;
                var form = document.forms[0];
                //form.action = "刷新本页面的URL";   
                form.action = "ContentSiteList.aspx";
                form.submit();
            }
        }   
    </script>
</head>
<body onload="showByOldDisplay();top.frames[1].location.reload();" class="bg">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" class="aa">
        <tr>
            <td>
            </td>
            <td align="center" valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" background="images/bg.jpg"
                    class="bg">
                    <tr>
                        <td>
                        </td>
                        <td valign="top" align="center" style="height: 546px">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center" valign="middle" class="title" height="40">
                                        节点管理
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" height="330">
                                        <hr width="90%" />
                                        <table border="0" cellpadding="0" cellspacing="1" width="650">
                                            <tr>
                                                <td width="50" align="center" valign="middle" style="height: 19px">
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="GridView1" runat="server" Width="98%" AutoGenerateColumns="False"
                                            DataKeyNames="nid" CssClass="tt" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                            <Columns>
                                                <asp:BoundField HeaderText="编号" DataField="nId">
                                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="激活时间" DataField="VActiveDate">
                                                    <ItemStyle HorizontalAlign="Left" Width="17%" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="附件目录">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container,"DataItem.vFilePath") %>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="单位名称">
                                                    <ItemTemplate>
                                                        <%#DataBinder.Eval(Container,"DataItem.vCorpName") %>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="抓取内容">
                                                    <ItemTemplate>
                                                        <%#MakeSiteString(DataBinder.Eval(Container, "DataItem.vFlag"))%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="28%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="白名单入库">
                                                    <ItemTemplate>
                                                        <%#WhiteMakeSiteString(DataBinder.Eval(Container, "DataItem.vType"))%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="状态">
                                                    <ItemTemplate>
                                                        <%#getState(DataBinder.Eval(Container, "DataItem.vActive"))%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" Width="8%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="操作">
                                                    <ItemTemplate>
                                                        <asp:LinkButton OnClientClick="return confirm('您确定要删除选中的项吗？');" Visible="true" ID="btDelete"
                                                            CommandArgument='<%#Eval("nKeyId")%>' CommandName="del" runat="server" Text="删除" />
                                                        <a onclick="showUpdateWin('<%#Eval("nKeyId")%>');" style="cursor: pointer">修改</a>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" Width="20%" />
                                                    <HeaderTemplate>
                                                        <asp:Button ID="btNew" runat="server" OnClientClick="showParentWin();" Text="新建" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="tt" />
                                            <EmptyDataTemplate>
                                                <asp:Button ID="btNew" runat="server" OnClientClick="showParentWin();" Text="新建" />
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <!--翻页起始-->
                                        <table width="100%" height="25" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="center" style="height: 19px">
                                                </td>
                                            </tr>
                                        </table>
                                        <!--翻页结束-->
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
