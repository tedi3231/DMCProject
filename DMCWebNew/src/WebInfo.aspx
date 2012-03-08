<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebInfo.aspx.cs" Inherits="src_WebInfo" %>

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">    
    <table width="99%">
        <tr>
            <td width="15%">用户网卡地址：</td>
            <td width="35%">
                <asp:TextBox ID="txtSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="100%"></asp:TextBox>
            </td>
            <td width="15%">服务器网卡地址： </td>
            <td width="35%">
                <asp:TextBox ID="txtDstMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>用户IP地址：
            </td>
            <td>
                <asp:TextBox ID="txtSrcAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="78%"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="归属地" Width="20%" />
            </td>
            <td>服务器IP地址： </td>
            <td>
                <asp:TextBox ID="txtDstAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="78%"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="归属地" Width="20%" />
            </td>
        </tr>
        <tr>
            <td>用户名：</td>
            <td>
                <asp:TextBox ID="txtLogin" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                    BorderStyle="Solid" BorderWidth="1"  Width="99%"></asp:TextBox>
            </td>
            <td>密码： </td>
            <td>
                <asp:TextBox ID="txtPwd" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                    BorderStyle="Solid" BorderWidth="1" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>站点：
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtSiteName" runat="server" BackColor="#efefef" BorderColor="#A9A9A9"
                    BorderStyle="Solid" BorderWidth="1" ReadOnly="true" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>IP地址归属地：</td>
            <td colspan="3">
                <asp:TextBox BorderColor="#A9A9A9" BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"
                    runat="server" ID="ltArea" Width="99%"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
