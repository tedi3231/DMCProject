<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppList.aspx.cs" Inherits="src_AppList" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" background="images/bg.jpg" class="bg">
    <tr>
        <td style="width:2px;cursor:e-resize; background-color:#6D9EDB"></td>
        <td valign="top" align="center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr><td align="center" valign="middle" class="title" height="40">数据管理中心访问列表</td></tr>
            <tr>
                <td valign="top" align="center" height="330">
                    <table border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                        <tr>
                            <td width="20" bgcolor="#efefef"></td>
                            <td width="200" bgcolor="#efefef" align="center">名称</td>
                            <td width="400" bgcolor="#efefef" align="center">链接地址</td>
                            <td width="90" bgcolor="#efefef" align="center"><asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" OnClientClick="tblAddNew.style.display='block';return false;" Width="84px" /></td>
                        </tr>
                    </table>
                    <asp:GridView ID="gdvData" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                        OnRowDataBound="gdvData_RowDataBound" DataKeyNames="nId" BorderStyle="Solid" BorderWidth="1px" PageSize="20" OnRowCancelingEdit="gdvData_RowCancelingEdit" OnRowDeleting="gdvData_RowDeleting" OnRowEditing="gdvData_RowEditing" OnRowUpdating="gdvData_RowUpdating" CellPadding="0" ShowHeader="False" BackColor="#E0E0E0">
                        <HeaderStyle BackColor="#EFEFEF" Font-Size="14px" /><Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="20px"/>
                                <ItemTemplate>
                                    <img src="images/cursor.gif">
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="名称">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkApp" runat="server" ForeColor="blue" Font-Underline="true" NavigateUrl='<%#DataBinder.Eval(Container,"DataItem.vUrl") %>' Target="_blank"><%#DataBinder.Eval(Container,"DataItem.vAppName") %></asp:HyperLink>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtApp" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vAppName") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="链接地址">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlkUrl" runat="server" ForeColor="blue" Font-Underline="true" NavigateUrl='<%#DataBinder.Eval(Container,"DataItem.vUrl") %>' Target="_blank"><%#DataBinder.Eval(Container,"DataItem.vUrl") %></asp:HyperLink>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtUrl" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vUrl") %>'></asp:TextBox>
                                </EditItemTemplate>
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
                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table id="tblAddNew" style="display:none;" border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                        <tr>
                            <td width="20" bgcolor="#ffffff"><img src="images/cursor.gif"></td>
                            <td width="200" bgcolor="#ffffff"><asp:TextBox ID="txtNewApp" runat="server" Width="200px"></asp:TextBox></td>
                            <td width="400" bgcolor="#ffffff"><asp:TextBox ID="txtNewUrl" runat="server" Width="400px"></asp:TextBox></td>
                            <td width="90" bgcolor="#ffffff" align="center">
                                <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="保存" OnClick="btnSave_Click" />
                                <asp:Button ID="btnNewCancel" runat="server" CommandName="Cancel" Text="取消" OnClientClick="tblAddNew.style.display='none';" />
                            </td>
                        </tr>
                    </table>
                    <!--翻页起始-->
                    <table width="100%" height="25" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="center" style="height: 19px">
                                <asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_Click" Visible="False">上一页</asp:LinkButton>&nbsp;
                                <asp:Label id="lblCurPage" runat="server"></asp:Label>&nbsp;
                                <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" Visible="False">下一页</asp:LinkButton>&nbsp;
                                跳转到第<asp:ListBox ID="lsbPage" runat="server" Rows="1" AutoPostBack="True" OnSelectedIndexChanged="lsbPage_SelectedIndexChanged"></asp:ListBox>页&nbsp;
                                <asp:Label ID="pagecount" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="currentpage" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <!--翻页结束-->
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
          </table>
        </td>
        </tr>
    </table>
    </form>
</body>
</html>
