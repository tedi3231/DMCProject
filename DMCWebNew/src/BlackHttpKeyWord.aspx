﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlackHttpKeyWord.aspx.cs"
    Inherits="src_BlackWhiteIPKeyWord" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="control/IssuedControl.ascx" tagname="IssuedControl" tagprefix="uc1" %>
<html>
<head id="Head1" runat="server">
    <title>HTTP黑名单列表管理</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <%--    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />
    --%>
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var oWindow;
        var listen_started = false;
        function showParentWin() {
            var url = ""; //定义弹出窗口的URL   
            var model = ""; //定义弹出窗口样式   
            oWindow = window.open('AddBlackHttp.aspx', 'newwindow', 'height=350, width=500, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
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
            oWindow = window.open('UpdateBlackHttp.aspx?id=' + id, 'newwindow', 'height=120, width=500, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');
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
                form.action = "BlackWhiteIPKeyWord.aspx";
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
                                        HTTP黑名单列表 <a href='searchBlack.aspx'>返回</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="center" height="330">
                                        <hr width="90%" />
                                        <table border="0" cellpadding="0" cellspacing="1" width="750">
                                            <tr>
                                                <td width="100" style="width: 100px; text-align: right; margin-right: 5px;">
                                                    HTTP地址：
                                                </td>
                                                <td width="100">
                                                    <asp:TextBox ID="tbIP" Width="100px" runat="server"></asp:TextBox>
                                                </td>
                                                <td width="60">
                                                    类型：
                                                </td>
                                                <td width="100">
                                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                                                        <asp:ListItem Selected="True" Value="-1">所有</asp:ListItem>
                                                        <asp:ListItem Value="0"> HTTP黑名单</asp:ListItem>
                                                        <%-- <asp:ListItem Value="1">白IP</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="40">
                                                    <asp:Button ID="btQuery" runat="server" Text="检索" OnClick="btQuery_Click" />
                                                </td>
                                                <td width="30">
                                                    <asp:Button ID="btNew" runat="server" OnClientClick="showParentWin();" Text="新建" />
                                                </td>
                                                <td width="50">
                                                    <asp:Button ID="btDel" runat="server" OnClick="btDel_Click" OnClientClick="return confirm('您确定要删除选中的项吗？');"
                                                        Text="删除选中项" Width="80px" />
                                                </td>
                                                <td width="70">
                                                    <asp:Button ID="btImport" runat="server" OnClientClick="window.open('importFileForBlack.aspx?type=1');"
                                                        Text="导入导出" Visible="true" />
                                                </td>
                                                <td>
                                                
                                                    <uc1:IssuedControl ID="IssuedControl1" runat="server" DataType="1" />
                                                
                                                </td>
                                                <td width="120">
                                                    <asp:LinkButton ID="lbClearWhite" runat="server" CommandArgument="2" OnClientClick="return confirm('您确定要删除所有白HTTP吗？');"
                                                        Visible="false" OnClick="lbClearWhite_Click">清空所有白HTTP</asp:LinkButton>
                                                </td>
                                                <td width="120">
                                                    <asp:LinkButton ID="lbClearBlack" runat="server" CommandArgument="0" OnClientClick="return confirm('您确定要删除所有HTTP黑名单吗？');"
                                                        OnClick="lbClearBlack_Click">清空所有HTTP黑名单</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="GridView1" runat="server" Width="80%" AutoGenerateColumns="False"
                                            OnRowCommand="GridView1_RowCommand" CssClass="tt">
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
                                                <asp:TemplateField HeaderText="HTTP">
                                                    <ItemTemplate>
                                                        <%#Eval("vName")%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="类型">
                                                    <ItemTemplate>
                                                        <%# Eval("vType").ToString() == "0" ? "HTTP黑名单" : "HTTP白名单"%>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="ctenertd" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="操作">
                                                    <ItemTemplate>
                                                        <asp:LinkButton Visible="true" ID="btDelete" CommandArgument='<%#Eval("id")%>' CommandName="del"
                                                            runat="server" Text="删除" />
                                                        <a href="#" onclick="showUpdateWin('<%#Eval("id")%>');">修改</a>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="90px" CssClass="ctenertd" />
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
