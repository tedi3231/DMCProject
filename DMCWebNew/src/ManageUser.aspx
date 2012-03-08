<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageUser.aspx.cs" Inherits="src_ManageUser" %>
<html>
<head id="Head1" runat="server">
    <title>用户管理</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" background="images/bg.jpg" class="bg">
    <tr>
        <td valign="top" align="center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr><td align="center" valign="middle" class="title" height="40">用户管理</td></tr>
            <tr>
                <td valign="top" align="center" height="330">
                    <table border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                        <tr>
                            <td width="20" bgcolor="#efefef"></td>
                            <td width="80" bgcolor="#efefef" align="center">用户名</td>
                            <td width="80" bgcolor="#efefef" align="center">密码</td>
                            <td width="80" bgcolor="#efefef" align="center">角色</td>
                            <td width="400" bgcolor="#efefef" align="center">可操作客户端</td>
                            <td width="90" bgcolor="#efefef" align="center"><asp:Button ID="btnAdd" runat="server" Text="新增" Width="84px" OnClick="btnAdd_Click" /></td>
                        </tr>
                    </table>
                    <asp:GridView ID="gdvData" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        OnRowDataBound="gdvData_RowDataBound" DataKeyNames="nId" BorderStyle="Solid" BorderWidth="1px" PageSize="20" OnPageIndexChanging="gdvData_PageIndexChanging" OnRowCancelingEdit="gdvData_RowCancelingEdit" OnRowDeleting="gdvData_RowDeleting" OnRowEditing="gdvData_RowEditing" OnRowUpdating="gdvData_RowUpdating" CellPadding="0" ShowHeader="False" BackColor="#E0E0E0">
                        <PagerSettings Mode="NextPrevious" NextPageText="下一页" PreviousPageText="上一页" />
                        <HeaderStyle BackColor="#EFEFEF" Font-Size="14px" /><Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="20px"/>
                                <ItemTemplate>
                                    <img src="images/cursor.gif">
                                </ItemTemplate>
                                <FooterTemplate>
                                    <img src="images/cursor.gif">
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="用户名">
                                <ItemTemplate>
                                    <asp:Label ID="lblLogin" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vLogin") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtLogin" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vLogin") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddLogin" runat="server" Width="80px"></asp:TextBox>
                                </FooterTemplate>
                                <ControlStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="密码">
                                <ItemTemplate>
                                    <asp:Label ID="lblPsw" runat="server" Text="******"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPsw" TextMode="Password" runat="server" Text=""></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddPsw" TextMode="Password" runat="server" Width="80px"></asp:TextBox>
                                </FooterTemplate>
                                <ControlStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="角色">
                                <ItemTemplate>
                                    <asp:Label ID="lblPower" runat="server" Text='<%#GetPower(DataBinder.Eval(Container,"DataItem.nPower")) %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlPower" runat="server" ToolTip='<%#DataBinder.Eval(Container,"DataItem.nPower") %>'>
                                        <asp:ListItem Text="管理员" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="一般用户" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList Width="80px" ID="ddlAddPower" runat="server">
                                        <asp:ListItem Text="管理员" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="一般用户" Value="0" Selected></asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ControlStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="可操作客户端">
                                <ItemTemplate>
                                    <asp:Label ID="lblParent" runat="server" Text='<%#GetParent(DataBinder.Eval(Container,"DataItem.nId")) %>'>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBoxList ID="cblParent" runat="server" DataTextField="vCorpName" DataSourceID="objParent"
                                        DataValueField="nId" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="6">
                                    </asp:CheckBoxList>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBoxList ID="cblAddParent" runat="server" DataTextField="vCorpName" DataSourceID="objParent"
                                        DataValueField="nId" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="6">
                                    </asp:CheckBoxList>
                                </FooterTemplate>
                                <ControlStyle Width="400px" />
                                <ItemStyle Width="400px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="编辑" />
                                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="删除" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="更新" />
                                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="取消" />
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Button ID="btnAddUpdate" runat="server" CommandName="Update" Text="保存" />
                                    <asp:Button ID="btnAddCancel" runat="server" CommandName="Cancel" Text="取消" />
                                </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" BackColor="#EFEFEF" />
                    </asp:GridView>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
          </table>
            <asp:ObjectDataSource ID="objParent" runat="server" SelectMethod="getHosts" TypeName="dbConfig">
            </asp:ObjectDataSource>
          </td>
        </tr>
    </table>
    </form>
</body>
</html>


