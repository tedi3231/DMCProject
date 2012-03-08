<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dnsInfo.aspx.cs" Inherits="src_dnsInfo"
    ValidateRequest="false" %>

<html>
<head id="Head1" runat="server">
    <title>DNS信息</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="styles/div.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="inc/common.js" type="text/javascript"></script>

    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
    <table width="820" border="0" align="center" cellpadding="0" cellspacing="0" style="width: 942px">
        <tr>
            <td width="125" style="text-align: right" class="style1">
                域名名称：
            </td>
            <td colspan="3" class="style1">
                <div align="left">
                    <asp:TextBox ID="txtvName" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                        BorderStyle="Solid" BorderWidth="1" Width="626px" Height="18px"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td width="125" style="height: 22px; text-align: right;">
                用户IP地址：
            </td>
            <td style="height: 22px; width: 284px;">
                <asp:TextBox ID="txtvSrcIp" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="200px"></asp:TextBox>
                <asp:Button ID="Button4" runat="server" Text="归属地" OnClick="Button4_Click" />
            </td>
            <td width="149" style="height: 22px; text-align: right;">
                <div align="right">
                    用户MAC地址：</div>
            </td>
            <td width="411" style="height: 22px">
                <asp:TextBox ID="txtvSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="125" style="height: 22px; text-align: right;">
                域名服务器IP：
            </td>
            <td style="height: 22px; width: 284px;">
                <asp:TextBox ID="txtvDstIp" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="200px"></asp:TextBox>&nbsp;<asp:Button
                        ID="Button3" runat="server" Text="归属地" OnClick="Button3_Click" />
            </td>
            <td width="149" style="text-align: right">
                <div align="right">
                网关MAC地址：</div>
            </td>
            <td width="411">
                <asp:TextBox ID="txtvDstMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 124px; text-align: right; height: 25px;">
                域名解析IP：
            </td>
            <td style="height: 25px; width: 284px;">
                <asp:TextBox ID="tbvAddr" runat="server" BackColor="#efefef" BorderColor="#A9A9A9"
                    BorderStyle="Solid" BorderWidth="1" ReadOnly="true" Width="200px"></asp:TextBox>&nbsp;<asp:Button
                        ID="Button2" runat="server" Text="归属地" OnClick="Button2_Click" />&nbsp;&nbsp;
            </td>
            <td align="right" style="width: 149px; text-align: right; height: 25px;">
                <%--<div align="right">
                        IP详细归属地：</div>--%>域名类型：
            </td>
            <td style="height: 25px">
                <asp:TextBox ID="txtvType" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                    BorderStyle="Solid" BorderWidth="1" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 124px; height: 25px; text-align: right">
                IP地址归属地：
            </td>
            <td colspan="3" style="height: 25px">
                <%--<asp:Literal ID="ltArea" runat="server"></asp:Literal>--%><asp:TextBox BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" runat="server" ID="ltArea"
                    Width="624px"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
