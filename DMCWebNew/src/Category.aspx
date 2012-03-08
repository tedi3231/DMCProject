<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Category.aspx.cs" Inherits="src_Category" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
    <script language="javascript">
        //验证输入内容的有效性
        function DoCheck()
        {
            if (event.srcElement.parentElement.parentElement.children(1).children(0).value == "")
            {
                alert("请输入类别名称！");
                event.srcElement.parentElement.parentElement.children(1).children(0).focus();
                return false;
            }
            return true;
        }
        function DoReload(objWindow)
        {
            objWindow.location.reload();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" background="images/bg.jpg" class="bg">
    <tr>
        <td style="width:2px;cursor:e-resize; background-color:#6D9EDB"></td>
        <td valign="top" align="center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr><td align="center" valign="middle" class="title" height="40"><asp:Label ID="lblCategoryTitle" runat="server" Text="关键字类别"></asp:Label><a href='GarbageMail.aspx'>返回</a></td></tr>
            <tr>
                <td valign="top" align="center" height="330">
                    <hr width="90%" />
                    <table border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                        <tr>
                            <td width="20" bgcolor="#efefef"><input id="CheckAll" runat="server" onclick="DoSelectAll();" title="全选/全取消" type="checkbox"></td>
                            <td width="200" bgcolor="#efefef" align="center">类别名称</td>
                            <td width="300" bgcolor="#efefef" align="center">备注</td>
                            <td width="120" bgcolor="#efefef" align="center"><asp:Button ID="btnDelAll" Width="120px" runat="server" Text="删除选中记录" OnClick="btnDelAll_Click" OnClientClick="if(!confirm('您是否真的要删除这些记录？')) return false;" /><br /><asp:Button ID="btnAdd" runat="server" Text="新增记录" OnClick="btnAdd_Click" Width="120px" /></td>
                        </tr>
                    </table>
                    <asp:GridView ID="dtgData" runat="server" AutoGenerateColumns="False"
                        OnRowDataBound="dtgData_RowDataBound" DataKeyNames="nId" BorderStyle="Solid" BorderWidth="1px" PageSize="20" OnRowCancelingEdit="dtgData_RowCancelingEdit" OnRowDeleting="dtgData_RowDeleting" OnRowEditing="dtgData_RowEditing" OnRowUpdating="dtgData_RowUpdating" CellPadding="0" ShowHeader="False" BackColor="#E0E0E0">
                        <HeaderStyle BackColor="#EFEFEF" Font-Size="14px" />
                        <Columns>
                           <asp:TemplateField HeaderText="复选框">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkData" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="类别名称">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vCategory") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCategory" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vCategory") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ControlStyle Width="200px" />
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemark" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vRemark") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRemark" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vRemark") %>'></asp:TextBox>
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
                                    <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="更新"  OnClientClick="return DoCheck();"/>
                                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="取消" />
                                </EditItemTemplate>
                                <ItemStyle Width="120px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <table runat="server" id="tblAddNew" visible="false" border="0" cellspacing="1" cellpadding="0" bgcolor="#ffffff">
                        <tr>
                            <td width="20" bgcolor="gainsboro"><img src="images/cursor.gif" visible="false"></td>
                            <td width="200" bgcolor="gainsboro"><asp:TextBox ID="txtNewCategory" runat="server" Width="200px"></asp:TextBox></td>
                            <td width="300" bgcolor="gainsboro"><asp:TextBox ID="txtNewRemark" runat="server" Width="300px"></asp:TextBox></td>
                            <td width="120" bgcolor="gainsboro" align="center">
                                <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="保存" OnClick="btnSave_Click" OnClientClick="return DoCheck();"/>
                                <asp:Button ID="btnNewCancel" runat="server" CommandName="Cancel" Text="取消" OnClick="btnNewCancel_Click" />
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
                                <asp:Label ID="pagecount" runat="server" Visible="False"></asp:Label><asp:Label ID="currentpage" runat="server" Visible="False"></asp:Label>
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
