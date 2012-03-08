<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IsWhiteIn.aspx.cs" Inherits="src_IsWhiteIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>白名单是否入库</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="inc/common.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 500px;">
                <tr>
                    <th colspan="2">
                        白名单是否入库
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal></th>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="left">
                            请选择入库类型：<asp:HiddenField ID="hdId" runat="server" Value="0" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="left">
                            <asp:CheckBoxList ID="cblList" runat="server" RepeatColumns="6" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="False">DNS白名单</asp:ListItem>
                                <asp:ListItem Value="2" Selected="False">垃圾邮件关键字</asp:ListItem>
                                <asp:ListItem Value="3" Selected="False">白IP</asp:ListItem>
                                <asp:ListItem Value="4" Selected="False">白MAC</asp:ListItem>
                                <asp:ListItem Value="5" Selected="False">白邮箱</asp:ListItem>
                            </asp:CheckBoxList></div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="left">
                            备 注：</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div align="left">
                            <asp:TextBox ID="tbMark" runat="server" Height="153px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btSave" runat="server" Text="保   存" OnClick="btSave_Click" /></td>
                    <td>
                        <asp:Button ID="btExit" runat="server" OnClientClick="javaScript:window.close();" Text="退   出"
                            /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
