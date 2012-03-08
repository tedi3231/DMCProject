<%@ Page Language="C#" AutoEventWireup="true" CodeFile="horseInfo.aspx.cs" Inherits="src_horseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>木马信息</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
    <style type="text/css">
        .style5
        {
            width: 20px;
            height: 20px;
        }
    </style>
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
    
    <table align="center" cellpadding="0" cellspacing="0" border="0" width="800px">
        <tr>
            <td width="90px">
                木马名称：
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtvName" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                    BorderStyle="Solid" BorderWidth="1" Width="245px"></asp:TextBox>
            </td>
            <td align="center" width="90px"> 
                相关域名：
            </td>
            <td  colspan="2">
                <asp:TextBox ID="tbvDnsName" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="245px"></asp:TextBox>
            </td>
            
        </tr>
        <tr>
            <td Width="90px">文件路径：</td>
            <td colspan="5">
                <asp:TextBox BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" runat="server" ID="txtvLocalFile"
                    Width="660px"></asp:TextBox><img alt=""  src="images/list13.gif" />
            </td>
            
        </tr>
        <tr>
            <td width="90px">用户IP地址：</td>
            <td>
                <asp:TextBox ID="txtvSrcIp" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="100px"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="归属地" 
                    Width="50px" />
            </td>
            <td width="90px">服务器IP地址：</td>
            <td>
                <asp:TextBox ID="txtvDstIp" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="100px"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="归属地" 
                    Width="50px" />
            </td>
            <td align="right" width="90px">木马进出判断：</td>
            <td>
                <asp:TextBox ID="txtvHost" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                    BorderStyle="Solid" BorderWidth="1" Width="16px" Visible="false"></asp:TextBox>
                <asp:TextBox ID="tbvFlag" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                    BorderStyle="Solid" BorderWidth="1" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>用户MAC地址：</td>
            <td>
                <asp:TextBox ID="txtvSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="160px"></asp:TextBox>
            </td>
            <td>网关MAC地址：</td>
            <td>
                <asp:TextBox ID="txtvDstMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="160px"></asp:TextBox>
            </td>
            <td  align="right">流量：</td>
            <td>
                <asp:TextBox ID="txtvRate" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td Width="100px">IP地址归属地：</td>
            <td colspan="5">
                <asp:TextBox BorderColor="#A9A9A9"
                    BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" runat="server" ID="ltArea"
                    Width="660px"></asp:TextBox>
            </td>
        </tr> 
    </table>
    </form>
</body>
</html>
