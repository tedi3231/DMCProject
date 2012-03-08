<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BBSInfo.aspx.cs" Inherits="src_BBSInfo" %>

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="inc/common.js"></script>

</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
        <table align="center" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td width="90px">
                    用户网卡地址：</td>
                <td style="width: 224px">
                    <asp:TextBox ID="txtSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox></td>
                <td width="120px">
                    &nbsp;&nbsp;服务器网卡地址：</td>
                <td width="150px">
                    <asp:TextBox ID="txtDstMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox></td>
            </tr>
            <tr>
                <td width="90px">
                    用户IP地址：</td>
                <td style="width: 224px">
                    <asp:TextBox ID="txtSrcAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" Text="归属地" Width="65px" OnClick="Button1_Click" /></td>
                <td width="120px">
                    &nbsp;&nbsp;服务器IP地址：</td>
                <td width="400px">
                    <asp:TextBox ID="txtDstAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox>&nbsp;
                    <asp:Button ID="Button1" runat="server" Text="归属地" Width="65px" OnClick="Button1_Click1" /></td>
            </tr>
            <tr>
                <td width="90px" style="height: 22px">
                    用户名：</td>
                <td style="height: 22px; width: 224px;">
                    <asp:TextBox ID="txtLogin" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                        BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox></td>
                <td width="120px" style="height: 22px">
                    &nbsp;&nbsp;密码：</td>
                <td width="150px" style="height: 22px">
                    <asp:TextBox ID="txtPwd" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                        BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="height: 22px" width="90">
                    IP地址归属地：</td>
                <td colspan="3" style="height: 22px">
                    <%-- <asp:Literal ID="ltArea" runat="server"></asp:Literal>--%>
                    <asp:TextBox BorderColor="#A9A9A9" BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"
                        runat="server" ID="ltArea" Width="220" TextMode="multiLine" Height="70px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
