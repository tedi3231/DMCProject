<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SensitiveControl.ascx.cs"
    Inherits="src_control_SensitiveControl" %>
<asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal">
    <asp:ListItem Value="1">IP黑名单</asp:ListItem>
    <asp:ListItem Value="2">邮箱黑名单</asp:ListItem>
    <asp:ListItem Value="3">关键字</asp:ListItem>
    <asp:ListItem Value="4">垃圾邮件关键字</asp:ListItem>
</asp:CheckBoxList>
