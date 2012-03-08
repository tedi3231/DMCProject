<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HttpInfo.aspx.cs" Inherits="src_HttpInfo" %>

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="inc/common.js"></script>

</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
        <table align="center" cellpadding="0" cellspacing="0" border="0" width="850px">
            <tr>
                <td width="90px">
                    时间：</td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtCapture" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="160px"></asp:TextBox></td>
                <td width="90px">
                    用户IP地址：</td>
                <td style="width: 160px">
                    <asp:TextBox ID="txtSrcAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="100px"></asp:TextBox>
                    <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="归属地" Width="50px" /></td>
                <td width="100px">
                    用户网卡地址：</td>
                <td width="150px">
                    <asp:TextBox ID="txtSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    被访问主机：</td>
                <td>
                    <asp:TextBox ID="txtHost" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                        BorderStyle="Solid" BorderWidth="1" Width="160"></asp:TextBox></td>
                <td>
                    目的IP地址：</td>
                <td>
                    <asp:TextBox ID="txtDstAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="100"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="归属地" Width="50px" /></td>
                <td>
                   目的网卡地址：</td>
                <td>
                    <asp:TextBox ID="txtDstMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    被访问网站：</td>
                <td colspan="5">
                    <asp:TextBox ID="txtSiteName" runat="server" ReadOnly="true" Width="660px" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"></asp:TextBox>
                    <input type="button" value="访问" onclick="window.open(txtSiteName.value,'_blank')" Width="50px" />
                </td>
            </tr>
            <tr>
                <td>
                    网站地址：</td>
                <td colspan="5">
                    <asp:TextBox ID="txtUrl" runat="server" ReadOnly="true" Width="660px" BorderColor="#A9A9A9"
                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"></asp:TextBox>
                    <input type="button" value="访问" onclick="window.open(txtUrl.value,'_blank')" Width="50px" />
                </td>
            </tr>
            <tr>
                <td Width="100px">
                    IP地址归属地：</td>
                <td colspan="5">
                    <%--<asp:Literal ID="ltArea" runat="server"></asp:Literal>--%>
                    <asp:TextBox BorderColor="#A9A9A9" BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"
                        runat="server" ID="ltArea" Width="705px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
