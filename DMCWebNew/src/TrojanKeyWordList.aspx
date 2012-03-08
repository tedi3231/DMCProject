<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrojanKeyWordList.aspx.cs"
    Inherits="TrojanKeyWordList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="control/IssuedControl.ascx" TagName="IssuedControl" TagPrefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>木马管理</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <%--<link href="images/tablecloth.css" rel="stylesheet" type="text/css" />--%>
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var oWindow;
        var listen_started = false;

        function showParentWin() {
            var url = ""; //定义弹出窗口的URL   
            var model = ""; //定义弹出窗口样式   
            oWindow = window.open('AddTrojan.aspx', 'newwindow', 'height=550, width=580, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
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
            oWindow = window.open('UpdateTrojan.aspx?id=' + id, 'newwindow', 'height=550, width=580, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
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
                form.action = "TrojanKeyWordList.aspx";
                form.submit();
            }
        }   
    </script>
</head>
<body onload="showByOldDisplay();">
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
                                        木马管理 <a href='searchBlack.aspx'>返回</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" height="330">
                                        <hr width="90%" />
                                        <table border="0" cellpadding="0" cellspacing="1" width="60%">
                                            <tr>
                                                <td width="70px" valign="middle">
                                                    <div align="right">
                                                        木马名称：</div>
                                                </td>
                                                <td width="100px" align="left" valign="middle">
                                                    <asp:TextBox ID="tbHorse" Width="100px" runat="server"></asp:TextBox>
                                                </td>
                                                <td width="50px" align="center" valign="middle">
                                                    <asp:Button ID="btQuery" runat="server" Text="检索" OnClick="btQuery_Click" />
                                                </td>
                                                <td width="50px" align="center" valign="middle">
                                                    <asp:Button ID="btNew" runat="server" OnClientClick="showParentWin();" Text="新建" />
                                                </td>
                                                <td width="80px" align="center" valign="middle">
                                                    <asp:Button ID="btDel" OnClientClick="return confirm('您确定要删除选中的项吗？');" runat="server"
                                                        Text="删除选中项" OnClick="btDel_Click" Width="80px" />
                                                </td>
                                                <td>
                                                    <uc1:IssuedControl ID="IssuedControl1" runat="server" DataType="7" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <span class="warningmsg">新增或修改内容后请点击“下发”将该内容下发到前端，否则新增或修改无效</span>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="GridView1" runat="server" Width="90%" AutoGenerateColumns="False"
                                            OnRowCommand="GridView1_RowCommand" DataKeyNames="id" CssClass="tt">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbMail" runat="server" Checked="false" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <input name="checkbox" type="checkbox" id="Checkbox1" onclick="selectAll(this);" />
                                                    </HeaderTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="木马名称">
                                                    <ItemTemplate>
                                                        <%#Eval("vName")%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="特征码">
                                                    <ItemStyle Width="200px" Wrap="true" />
                                                    <ItemTemplate>
                                                        <%# Eval("vKey")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="协议类型">
                                                    <ItemTemplate>
                                                        <%# FormatProtocol( Convert.ToInt32(Eval("vProtocl").ToString()))%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="端口列表">
                                                    <ItemTemplate>
                                                        <%# Eval("vPort")%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="木马方向">
                                                    <ItemTemplate>
                                                        <%# common.FormatTrojanFlag(Convert.ToInt32(Eval("vFlag")))%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="木马描述">
                                                    <ItemTemplate>
                                                        <%# Eval("vContent")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="操作">
                                                    <ItemTemplate>
                                                        <asp:LinkButton Visible="true" ID="btDelete" CommandArgument='<%#Eval("id")%>' CommandName="del"
                                                            runat="server" Text="删除" />
                                                        <a href="#" onclick="showUpdateWin('<%#Eval("id")%>');">修改</a>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="tt" />
                                        </asp:GridView>
                                        <!--翻页起始-->
                                        <table width="100%" height="25" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="center" style="height: 19px">
                                                    <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging"
                                                        ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到"
                                                        PageSize="20" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
                                                    </webdiyer:AspNetPager>
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
