﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddContentSite.aspx.cs" Inherits="src_AddContentSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新建节点</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="inc/common.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 500px;">
                <tr>
                    <th colspan="2">
                        新建节点
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal></th>
                </tr>
                <tr>
                    <td width="80">
                        <div align="right">
                            节点编号：</div>
                    </td>
                    <td width="339">
                        <div align="left">
                            <asp:TextBox ID="tbId" runat="server"></asp:TextBox></div>
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        <div align="right">
                            单位名称：</div>
                    </td>
                    <td>
                        <div align="left">
                            <asp:TextBox ID="tbName" runat="server"></asp:TextBox></div>
                    </td>
                </tr>
                <tr>
                    <td width="80">
                        <div align="right">
                            附件目录：</div>
                    </td>
                    <td>
                        <div align="left">
                            <asp:TextBox ID="tbDic" runat="server" Text=""></asp:TextBox></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="left">
                            前端抓取内容：</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="left">
                            <asp:CheckBoxList ID="cblList" runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">http</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">msn</asp:ListItem>
                                <asp:ListItem Value="3" Selected="True">yahoo通</asp:ListItem>
                                <asp:ListItem Value="4" Selected="True">ftp</asp:ListItem>
                                <asp:ListItem Value="5" Selected="True">pop3</asp:ListItem>
                                <asp:ListItem Value="6" Selected="True">smtp</asp:ListItem>
                                <asp:ListItem Value="7" Selected="True">web收</asp:ListItem>
                                <asp:ListItem Value="8" Selected="True">web发</asp:ListItem>
                                <asp:ListItem Value="9" Selected="True">dns</asp:ListItem>
                                <asp:ListItem Value="10" Selected="True">木马</asp:ListItem>
                                <%--<asp:ListItem Value="11" Selected="True">HTTP</asp:ListItem>--%>
                            </asp:CheckBoxList></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="left">
                            白名单是否入库：</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="left">
                            <asp:CheckBoxList ID="cblWhiteList" runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="False">垃圾邮件关键字</asp:ListItem>
                              <%--  <asp:ListItem Value="2" Selected="False">白IP</asp:ListItem>
                                <asp:ListItem Value="3" Selected="False">白MAC</asp:ListItem>
                                <asp:ListItem Value="4" Selected="False">白邮箱</asp:ListItem>--%>
                                <asp:ListItem Value="5" Selected="False">DNS白名单</asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btSave" runat="server" Text="保   存" OnClick="btSave_Click" /></td>
                    <td>
                        <asp:Button ID="btExit" runat="server" OnClientClick="this.window.close();" Text="退   出"
                            OnClick="btExit_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
