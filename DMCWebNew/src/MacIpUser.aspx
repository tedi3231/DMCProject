<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MacIpUser.aspx.cs" Inherits="src_MacIpUser" %>
<html>
<head runat="server">
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
            <tr><td align="center" valign="middle" class="title" height="40">重点怀疑对象列表</td></tr>
            <tr>
                <td valign="top" align="center" height="330">
                    <table border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                        <tr>
                            <td width="20" bgcolor="#efefef"></td>
                            <td width="100" bgcolor="#efefef" align="center">MAC地址</td>
                            <td width="100" bgcolor="#efefef" align="center">IP地址</td>
                            <td width="80" bgcolor="#efefef" align="center">所属单位</td>
                            <td width="80" bgcolor="#efefef" align="center">定位类型</td>
                            <td width="300" bgcolor="#efefef" align="center">备注</td>
                            <td width="90" bgcolor="#efefef" align="center"><asp:Button ID="btnAdd" runat="server" Text="新增" OnClick="btnAdd_Click" OnClientClick="tblAddNew.style.display='block';return false;" Width="84px" /></td>
                        </tr>
                    </table>
                    <asp:GridView ID="gdvData" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                        OnRowDataBound="gdvData_RowDataBound" DataKeyNames="nId" BorderStyle="Solid" BorderWidth="1px" PageSize="20" OnRowCancelingEdit="gdvData_RowCancelingEdit" OnRowDeleting="gdvData_RowDeleting" OnRowEditing="gdvData_RowEditing" OnRowUpdating="gdvData_RowUpdating" CellPadding="0" ShowHeader="False" BackColor="#E0E0E0">
                        <HeaderStyle BackColor="#EFEFEF" Font-Size="14px" /><Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="20px"/>
                                <ItemTemplate>
                                    <img src="images/cursor.gif" alt="" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MAC地址">
                                <ItemTemplate>
                                    <asp:Label ID="lblMac" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vMac") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMac" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vMac") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IP地址">
                                <ItemTemplate>
                                    <asp:Label ID="lblIp" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vIp") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtIp" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vIp") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="100px" />
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="所属单位">
                                <ItemTemplate>
                                    <asp:Label ID="lblParent" runat="server" Text='<%#GetParent(DataBinder.Eval(Container,"DataItem.nParent")) %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlParent" runat="server" DataSourceID="objParent" DataTextField="vCorpName" DataValueField="nId" ToolTip='<%#DataBinder.Eval(Container,"DataItem.nParent") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ControlStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="定位类型">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%#GetType(DataBinder.Eval(Container,"DataItem.nType")) %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlType" runat="server" ToolTip='<%#DataBinder.Eval(Container,"DataItem.nType") %>'>
                                        <asp:ListItem Text="按MAC定位" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="按IP定位" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ControlStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="lblMark" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vMark") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMark" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vMark") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="300px" />
                                <ItemStyle Width="300px" />
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
                            <td width="100" bgcolor="#ffffff"><asp:TextBox ID="txtNewMac" runat="server" Width="100px"></asp:TextBox></td>
                            <td width="100" bgcolor="#ffffff"><asp:TextBox ID="txtNewIp" runat="server" Width="100px"></asp:TextBox></td>
                            <td width="80" bgcolor="#ffffff"><asp:DropDownList Width="80px" ID="ddlNewParent" runat="server" DataSourceID="objParent" DataTextField="vCorpName" DataValueField="nId"></asp:DropDownList></td>
                            <td width="80" bgcolor="#ffffff">
                                <asp:DropDownList Width="80px" ID="ddlNewType" runat="server">
                                    <asp:ListItem Text="按MAC定位" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="按IP定位" Value="1" Selected></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td width="300" bgcolor="#ffffff"><asp:TextBox ID="txtNewMark" runat="server" Width="300px"></asp:TextBox></td>
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
                    <asp:Button ID="btnExportKey" runat="server" Text="导出重点怀疑对象" OnClick="btnExportKey_Click" />
                    <asp:FileUpload ID="fileImport" runat="server" />
                    <asp:Button ID="btnImportKey" runat="server" Text="导入重点怀疑对象" OnClick="btnImportKey_Click"/>
                    <asp:ObjectDataSource ID="objParent" runat="server" SelectMethod="getHosts" TypeName="dbConfig">
                    </asp:ObjectDataSource>
                </td>
            </tr>
          </table>
        </td>
        </tr>
    </table>
    </form>
</body>
</html>



