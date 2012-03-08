<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YahooInfo.aspx.cs" Inherits="src_YahooInfo" %>

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
    <table align="center" cellpadding="0" cellspacing="0" border="0" width="99%">
        <tr>
            <td width="10%">
                用户网卡地址：
            </td>
            <td>
                <asp:TextBox ID="txtSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                用户IP地址：
            </td>
            <td>
                <asp:TextBox ID="txtSrcAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="89%"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="归属地" Width="10%" />
            </td>
        </tr>
        <tr>
            <td>
                目的IP地址：
            </td>
            <td >
                <asp:TextBox ID="txtDstAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="89%"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="归属地" Width="10%" />
            </td>
        </tr>
        <tr>
            <td>
                聊天内容：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtMessage" runat="server" ReadOnly="true" TextMode="MultiLine"
                    Width="99%" BorderColor="#A9A9A9" BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"
                    Height="30"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                IP地址归属地：
            </td>
            <td colspan="5">
                <asp:TextBox BorderColor="#A9A9A9" BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"
                    runat="server" ID="ltArea" Width="99%"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
