<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inputPass.aspx.cs" Inherits="src_inputPass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="inc/common.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div runat="server" id="passDiv">
        管理员密码：<asp:TextBox ID="TextBox1" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
        &nbsp;
        <asp:Button ID="btEnter" runat="server" OnClick="btEnter_Click" Text="确  定" />
        <asp:HiddenField ID="hdType" runat="server" />
        <asp:HiddenField ID="hdTbName" runat="server" />
        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
    </div>
    <div runat="server" id="divImport">
        <table style="width: 500px;">
            <tr>
                <th colspan="2">
                    <asp:Literal ID="ltType" runat="server"></asp:Literal>
                </th>
            </tr>
            <tr>
                <td width="100" style="width: 100px;">
                    选择类型：
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="100px">
                        <asp:ListItem Value="0">黑名单</asp:ListItem>
                        <asp:ListItem Value="1">白名单</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="100" style="width: 100px">
                    选择文件：
                </td>
                <td>
                    <span style="color: Red">
                        <asp:FileUpload ID="fileImport" runat="server" /></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btSave" runat="server" Text="导   入" OnClick="btSave_Click" />
                    &nbsp;&nbsp; &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="导   出" OnClick="Button1_Click" />
                    &nbsp;&nbsp; &nbsp;<asp:Button ID="btExit" runat="server" OnClientClick="reLoadParentPage();"
                        Text="退   出" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
