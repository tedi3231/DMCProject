<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListAttachFile.aspx.cs" Inherits="src_ListAttachFile"  validateRequest="false" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table algin="center" border="0" width="98%" cellpadding="0" cellspacing="0">
        <tr><td align="right"><asp:HyperLink ID="hplBack" runat="server" Font-Underline="True" ForeColor="Blue">返回</asp:HyperLink><br /></td></tr>
        <tr>
            <td valign="top" align="left">
                <asp:BulletedList ID="bulAttach" runat="server" BulletStyle="Square" DisplayMode="HyperLink" Target="_blank"></asp:BulletedList>
                <br />
            </td>
        </tr>
        
        <tr>
            <td align="center"><asp:Label ID="lblMessage" runat="server" Text="" ForeColor="red"></asp:Label></td>
        </tr>
    </table>
    </form>
</body>
</html>