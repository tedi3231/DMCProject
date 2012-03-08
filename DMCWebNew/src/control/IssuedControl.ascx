<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IssuedControl.ascx.cs" Inherits="src_control_IssuedControl" %>
<span style="color:Red">
<asp:Button ID="Button1" runat="server" Text="下 发" onclick="Button1_Click" /> 
    <asp:HiddenField ID="hdType" runat="server" />
     <!-- 更新后必须下发才能生效 -->
</span>

