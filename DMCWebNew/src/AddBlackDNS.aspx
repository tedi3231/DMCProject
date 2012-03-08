﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddBlackDNS.aspx.cs" Inherits="src_AddBlackDNS" %>

<%@ Register Src="control/DNSTypeUserControl.ascx" TagName="DNSTypeUserControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新建域名</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:500px;">
             <tr>
                    <th colspan="2">新建域名</th>
                </tr>
                <tr>
                    <td style="width: 40px;">类型：</td>
                    <td>
                         <asp:DropDownList ID="ddlTypeList" runat="server" Width="150px">                                       
                                        <asp:ListItem Value="0">普通黑域名</asp:ListItem>
                                        <asp:ListItem Value="1">重要黑域名</asp:ListItem>
                                        <asp:ListItem Value="2">紧急黑域名</asp:ListItem>
                                        <asp:ListItem Value="3">白域名</asp:ListItem>
                                        <asp:ListItem Value="4">动态域名</asp:ListItem>
                                    </asp:DropDownList>
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal></td>
                </tr>
                  <tr>
                    <td style="width: 40px">
                        DNS：</td>
                    <td><span style="color:Red">多DNS输入(注意:每行只能输入一个DNS)</span></td>
                </tr>
                  <tr>
                    <td colspan="2">
                        <asp:TextBox ID="TextBox1" runat="server" Height="200px" TextMode="MultiLine" Width="500px"></asp:TextBox></td>                   
                </tr>
                <tr>
                    <td style="width: 40px">
                        备注：</td>
                    <td></td>
                </tr>
                <tr>                  
                    <td colspan="2">
                        <asp:TextBox ID="tbContent" runat="server" Height="100px" TextMode="MultiLine" Width="500px"></asp:TextBox></td>                   
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btSave" runat="server" Text="保   存" OnClick="btSave_Click" /></td>
                    <td><asp:Button ID="btExit" runat="server"  OnClientClick="this.window.close();" Text="退   出" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
