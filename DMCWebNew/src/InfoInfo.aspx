<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfoInfo.aspx.cs" Inherits="src_InfoInfo" %>

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
                用户网卡地址：
            </td>
            <td style="width: 220px">
                <asp:TextBox ID="txtSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox>
            </td>
            <td width="120px">
                &nbsp;&nbsp;服务器网卡地址：
            </td>
            <td style="width: 221px">
                <asp:TextBox ID="txtDstMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox>
            </td>
            <td width="70px">
                &nbsp;&nbsp;用户名：
            </td>
            <td width="150px">
                <asp:TextBox ID="txtLogin" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                    BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="90px">
                用户IP地址：
            </td>
            <td style="width: 220px">
                <asp:TextBox ID="txtSrcAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox><asp:Button
                        ID="Button3" runat="server" OnClick="Button3_Click" Text="归属地" />
            </td>
            <td width="120px">
                &nbsp;&nbsp;服务器IP地址：
            </td>
            <td style="width: 221px">
                <asp:TextBox ID="txtDstAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox><asp:Button
                        ID="Button2" runat="server" OnClick="Button2_Click" Text="归属地" />
            </td>
            <td width="70px">
                &nbsp;&nbsp;密码：
            </td>
            <td width="150px">
                <asp:TextBox ID="txtPwd" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                    BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox>
            </td>
        </tr>
        <tr id="trSite" runat="server" visible="false">
            <td width="90px">
                站点：
            </td>
            <td style="width: 220px">
                <asp:TextBox ID="txtSiteName" runat="server" BackColor="#efefef" BorderColor="#A9A9A9"
                    BorderStyle="Solid" BorderWidth="1" ReadOnly="true" Width="150"></asp:TextBox>
            </td>
            <td width="120px">
                &nbsp;&nbsp;
            </td>
            <td style="width: 221px">
                &nbsp;&nbsp;
            </td>
            <td width="70px">
                &nbsp;&nbsp;
            </td>
            <td width="150px">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr runat="server">
            <td width="90">
                IP地址归属地：
            </td>
            <td colspan="5">
                <asp:TextBox BorderColor="#A9A9A9" BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"
                    runat="server" ID="ltArea" Width="778px" TextMode="multiLine" 
                    Height="25px"></asp:TextBox>
                <%--<asp:Literal ID="ltArea" runat="server"></asp:Literal>--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
