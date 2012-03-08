<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDynamicBlackDNS.aspx.cs" Inherits="src_AddDynamicBlackDNS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>添加动态域名</title>
<link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:500px;">
             <tr>
                    <th colspan="2">新建动态域名</th>
                </tr>
                <tr>
                    <td style="width: 100px;">动态域名：</td>
                    <td>
                        <asp:TextBox ID="tbDns" Width="150px" runat="server"></asp:TextBox>
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal></td>
                </tr>
                  <tr>
                    <td style="width: 100px">域名描述：</td>
                    <td>
                    <asp:TextBox ID="tbContent" runat="server" Height="200px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btSave" runat="server" Text="保   存" OnClick="btSave_Click" /></td>
                    <td><asp:Button ID="btExit" runat="server"  OnClientClick="window.close();" Text="退   出" /></td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
