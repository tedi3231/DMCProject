<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsnInfo.aspx.cs" Inherits="src_msninfo" %>

<html>
<head runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
    <table align="center" cellpadding="0" cellspacing="0" border="0" width="750px">
        <tr>
            <td width="90px">
                用户IP地址：
            </td>
            <td>
                <asp:TextBox ID="txtSrcAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="110px"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="归属地" Width="45px" />
            </td>
            <td>
            </td>
            <td>
                &nbsp;
            </td>
            <td width="90px">
                目的IP地址：
            </td>
            <td>
                <asp:TextBox ID="txtDstAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="110px"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="归属地" Width="45px" />
            </td>
        </tr>
        <tr>
            <td width="90px">
                发送方邮箱：
            </td>
            <td>
                <asp:TextBox ID="txtSrcMail" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="160px"></asp:TextBox>
            </td>
            <td width="90px">
                接收方邮箱：
            </td>
            <td>
                <asp:TextBox ID="txtDstMail" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="160px"></asp:TextBox>
            </td>
            <td width="90px">
                用户网卡地址：
            </td>
            <td>
                <asp:TextBox ID="txtSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="160px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="90px">
                聊天内容：
            </td>
            <td colspan="5">
                <asp:TextBox ID="txtMessage" runat="server" ReadOnly="true" TextMode="MultiLine"
                    Width="660px" BorderColor="#A9A9A9" BackColor="#efefef" 
                    BorderStyle="Solid" BorderWidth="1"
                    Height="50px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="90px">
                IP地址归属地：
            </td>
            <td colspan="5">
                <%--<asp:Literal ID="ltArea" runat="server"></asp:Literal>--%>
                <asp:TextBox BorderColor="#A9A9A9" BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"
                    runat="server" ID="ltArea" Width="660px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
