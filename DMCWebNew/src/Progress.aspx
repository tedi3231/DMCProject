<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Progress.aspx.cs" Inherits="src_Progress" %>

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <base target="_self">
</head>
<body class="bg">
	<form id="Form1" method="post" runat="server">
	<table border="0" cellpadding="0" cellspacing="0" height="100%" align="center">
    <tr>
        <td valign="middle">
			<asp:Label id="lblMessages" runat="server"></asp:Label>
			<asp:Panel id="panelBarSide" runat="server" Width="300px" BorderStyle="Solid" BorderWidth="1px"
				ForeColor="Silver">
				<asp:Panel id="panelProgress" runat="server" Width="10px" BackColor="Green"></asp:Panel>
			</asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label id="lblPercent" runat="server" ForeColor="Blue"></asp:Label>
            <asp:Button ID="btnClose" runat="server" Visible="false" Text="关闭" OnClientClick="window.close();return false;" />
        </td>
    </tr>
    </table>		
	</form>
</body>
</html>
