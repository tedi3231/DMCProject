<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateBlackMac.aspx.cs" Inherits="src_UpdateBlackMac" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>更新黑白MAC</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:500px;">
             <tr>
                    <th colspan="2">黑白MAC</th>
                </tr>
                <tr>
                    <td style="width: 40px;">类型：</td>
                    <td>
                         <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">                                        
                                        <asp:ListItem Value="0">黑名单</asp:ListItem>
                                        <asp:ListItem Value="1">白名单</asp:ListItem>
                                    </asp:DropDownList>
                                <asp:HiddenField ID="hdId" runat="server" />
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal></td>
                </tr>
                  <tr>
                    <td style="width: 40px">MAC：</td>
                 
                    <td colspan="2">
                        <asp:TextBox ID="tbName" runat="server"  TextMode="singleline" Width="200px"></asp:TextBox>
                        </td>                   
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btSave" runat="server" Text="保   存" OnClick="btSave_Click" /></td>
                    <td><asp:Button ID="btExit" runat="server"  OnClientClick="this.window.close();" Text="退   出" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
