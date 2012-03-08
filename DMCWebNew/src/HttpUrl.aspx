<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HttpUrl.aspx.cs" Inherits="src_HttpUrl" %>

<html>
<head runat="server">
    
    <title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
    <table style="background-color: #DDECF9" width="100%" height="100%">
    <tr><td align="center" valign="middle">
       <table width="300" align="center">
       <tr><td width="20%">网站：</td><td>
           <asp:TextBox ID="siteTXT" runat="server" Width="179px"></asp:TextBox>
           <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank">访问</asp:HyperLink></td></tr>
       <tr><td>URL：</td><td>
           <asp:TextBox ID="urlTXT" runat="server" Width="178px"></asp:TextBox>
           <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank">访问</asp:HyperLink></td></tr>
       </table>
    </td></tr>
    </table>
    </form>
</body>
</html>



