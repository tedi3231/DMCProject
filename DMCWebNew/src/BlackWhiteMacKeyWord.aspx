﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlackWhiteMacKeyWord.aspx.cs"
    Inherits="src_BlackWhiteMacKeyWord" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>黑白MAC管理</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <%-- <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />--%>
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var oWindow;
        var listen_started = false;
        function showParentWin() {
            var url = ""; //定义弹出窗口的URL   
            var model = ""; //定义弹出窗口样式   
            oWindow = window.open('AddBlackMac.aspx', 'newwindow', 'height=350, width=500, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
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
            oWindow = window.open('UpdateBlackMac.aspx?id=' + id, 'newwindow', 'height=120, width=500, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
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
                form.action = "BlackWhiteMacKeyWord.aspx";
                form.submit();
            }
        }   
    </script>
    <style type="text/css">
        .style1
        {
            width: 74px;
        }
    </style>
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
                                        黑白MAC <a href='searchBlack.aspx'>返回</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" height="330">
                                        <hr width="90%" />
                                        <table border="0" cellpadding="0" cellspacing="1" width="90%">
                                            <tr>
                                                <td class="style1">
                                                    MAC地址：
                                                </td>
                                                <td width="100px">
                                                    <asp:TextBox ID="tbIP" Width="100px" runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 50px; text-align: right; margin-right: 5px;">
                                                    类型：
                                                </td>
                                                <td width="100px">
                                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                                                        <asp:ListItem Selected="True" Value="-1">所有</asp:ListItem>
                                                        <asp:ListItem Value="0">黑MAC</asp:ListItem>
                                                        <asp:ListItem Value="1">白MAC</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="40px">
                                                    <asp:Button ID="btQuery" runat="server" Text="检索" OnClick="btQuery_Click" />
                                                </td>
                                                <td width="40px">
                                                    <asp:Button ID="btNew" runat="server" OnClientClick="showParentWin();" Text="新建" />
                                                </td>
                                                <td width="80px">
                                                    <asp:Button ID="btDel" OnClientClick="return confirm('您确定要删除选中的项吗？');" OnClick="btDel_Click"
                                                        runat="server" Width="80px" Text="删除选中项"></asp:Button>
                                                </td>
                                                <td width="70px">
                                                    <asp:Button ID="btImport" runat="server" OnClientClick="window.open('importFileForBlack.aspx?type=2');"
                                                        Text="导入导出" />
                                                </td>
                                                <td width="120px">
                                                    <asp:LinkButton ID="lbClearWhite" runat="server" CommandArgument="2" OnClientClick="return confirm('您确定要删除所有白MAC吗？');"
                                                        OnClick="lbClearWhite_Click">清空所有白MAC</asp:LinkButton>
                                                </td>
                                                <td width="120px">
                                                    <asp:LinkButton ID="lbClearBlack" runat="server" CommandArgument="0" OnClientClick="return confirm('您确定要删除所有黑MAC吗？');"
                                                        OnClick="lbClearBlack_Click">清空所有黑MAC</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="11">
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
                                                    <ItemStyle CssClass="ctenertd" Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MAC">
                                                    <ItemTemplate>
                                                        <%#Eval("vName")%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="类型">
                                                    <ItemTemplate>
                                                        <%# Eval("vType").ToString() == "0" ? "黑名单" : "白名单"%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="操作">
                                                    <ItemTemplate>
                                                        <asp:LinkButton Visible="true" ID="btDelete" CommandArgument='<%#Eval("id")%>' CommandName="del"
                                                            runat="server" Text="删除" />
                                                        <a onclick="showUpdateWin('<%#Eval("id")%>');">修改</a>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" Width="90px" />
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
