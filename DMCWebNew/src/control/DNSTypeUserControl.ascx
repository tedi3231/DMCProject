<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DNSTypeUserControl.ascx.cs" Inherits="src_control_DNSTypeUserControl" %>
<asp:DropDownList ID="DropDownList1" runat="server" Width="150px">
    <asp:ListItem>所有域名</asp:ListItem>
    <asp:ListItem>所有白域名</asp:ListItem>  
    <asp:ListItem>所有黑域名</asp:ListItem>    
    <asp:ListItem>普通黑域名(蓝色)</asp:ListItem>    
    <asp:ListItem>重要黑域名(黄色)</asp:ListItem>
    <asp:ListItem>紧急黑域名(红色)</asp:ListItem>
</asp:DropDownList>

<!-- 
蓝色 普通黑域名
黄色 重要黑域名
红色 紧急黑域名
-->
