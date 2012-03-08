<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GarbageMail.aspx.cs" Inherits="src_GarbageMail" %>

<%@ Register src="control/IssuedControl.ascx" tagname="IssuedControl" tagprefix="uc1" %>

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="inc/common.js"></script>

    <script language="javascript">
        //验证输入内容的有效性
        function DoCheck()
        {
            if (event.srcElement.parentElement.parentElement.children(1).children(0).selectedIndex < 1)
            {
                alert("请选择关键字类别！");
                return false;
            }
            if (event.srcElement.parentElement.parentElement.children(2).children(0).value == "")
            {
                alert("请输入关键字！");
                event.srcElement.parentElement.parentElement.children(2).children(0).focus();
                return false;
            }
            return true;
        }
        
        function openWindow(sFile)
        {
        winSet = window.open(sFile,"winSet","width=700,height=500,scrollbars=no,location=no,menubar=no,resizable=no,status=no");
        }
       function closeWindow()
       {
           if(winSet) winSet.close();
       }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" background="images/bg.jpg"
            class="bg">
            <tr>
                <td style="width: 2px; cursor: e-resize; background-color: #6D9EDB">
                </td>
                <td valign="top" align="center">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td align="center" valign="middle" class="title" height="40">
                                <asp:Literal ID="ltTitle" runat="server" Text="垃圾邮件关键字列表"></asp:Literal> <a href='searchBlack.aspx'>返回</a></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Literal ID="ltContent" runat="server" Text="(用于针对邮件标题、IP地址、MAC地址、发件人和收件人的邮件过滤)"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" height="330">
                                <hr width="90%" />
                                <table border="0" cellpadding="0" cellspacing="1" width="640">
                                    <tr>
                                        <td align="left">
                                            列出类别为<asp:DropDownList ID="ddlCate" runat="server" DataTextField="vCategory" DataValueField="nId"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCate_SelectedIndexChanged" OnDataBound="ddlCate_DataBound">
                                            </asp:DropDownList>的关键字
                                            <asp:HiddenField ID="hdType" runat="server" />
                                        </td>
                                        <td align="right">
                                           <a onClick="openWindow('Category.aspx?type=1')"
                                                style="text-decoration: underline; color: Blue; cursor: hand;">垃圾邮件类别管理</a></td>
                                    </tr>
                                    <tr>
                                    <td colspan="2">
                                        <span class="warningmsg">新增或修改内容后请点击“下发”将该内容下发到前端，否则新增或修改无效</span>
                                    </td>
                                </tr>
                                </table>
                                <table border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                                    <tr>
                                        <td width="20" bgcolor="#efefef">
                                            <input id="CheckAll" runat="server" onClick="DoSelectAll();" title="全选/全取消" type="checkbox"></td>
                                        <td width="200" bgcolor="#efefef" align="center">
                                            垃圾邮件类别</td>
                                        <td width="300" bgcolor="#efefef" align="center">
                                            垃圾邮件关键字</td>
                                        <td width="60" bgcolor="#efefef" align="center" style="display: none">
                                            操作</td>
                                        <td width="120" bgcolor="#efefef" align="center">
                                            <asp:Button ID="btnDelAll" Width="120px" runat="server" Text="删除选中记录" OnClick="btnDelAll_Click"
                                                OnClientClick="if(!confirm('您是否真的要删除这些记录？')) return false;" /><br />
                                            <asp:Button ID="btnAdd" runat="server" Text="新增记录" OnClick="btnAdd_Click" Width="120px" /></td>
                                    </tr>
                                </table>
                                <asp:GridView ID="dtgData" runat="server" AutoGenerateColumns="False" OnRowDataBound="dtgData_RowDataBound"
                                    DataKeyNames="nId" BorderStyle="Solid" BorderWidth="1px" PageSize="18" OnRowCancelingEdit="dtgData_RowCancelingEdit"
                                    OnRowDeleting="dtgData_RowDeleting" OnRowEditing="dtgData_RowEditing" OnRowUpdating="dtgData_RowUpdating"
                                    CellPadding="0" ShowHeader="False" BackColor="#E0E0E0" OnRowCommand="dtgData_RowCommand"
                                    OnRowCreated="dtgData_RowCreated">
                                    <HeaderStyle BackColor="#EFEFEF" Font-Size="14px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="复选框">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkData" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="关键字类别">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%#GetCategory(DataBinder.Eval(Container,"DataItem.nCategory")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlCategory" runat="server" ToolTip='<%#DataBinder.Eval(Container,"DataItem.nCategory") %>'
                                                    DataSourceID="odsCategory" DataTextField="vCategory" DataValueField="nId">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ControlStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="垃圾邮件关键字">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKey" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vKey") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtKey" runat="server" Text='<%#DataBinder.Eval(Container,"DataItem.vKey") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ControlStyle Width="300px" />
                                            <ItemStyle Width="300px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnCheck" runat="server" CommandName="Check" Text="比对" Visible="false" />
                                            </ItemTemplate>
                                            <ItemStyle Width="0px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="编辑" />
                                                <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="删除" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="更新" OnClientClick="return DoCheck();" />
                                                <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="取消" />
                                            </EditItemTemplate>
                                            <ItemStyle Width="120px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <table runat="server" id="tblAddNew" visible="false" border="0" cellspacing="1" cellpadding="0"
                                    bgcolor="#ffffff">
                                    <tr>
                                        <td width="20" bgcolor="gainsboro">
                                            <img src="images/cursor.gif" visible="false"></td>
                                        <td width="200" bgcolor="gainsboro">
                                            <asp:DropDownList Width="200px" ID="ddlNewCategory" runat="server" DataSourceID="odsCategory"
                                                DataTextField="vCategory" DataValueField="nId">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="300" bgcolor="gainsboro">
                                            <asp:TextBox ID="txtNewKey" runat="server" Width="300px"></asp:TextBox></td>
                                        <td bgcolor="gainsboro" width="60">
                                        </td>
                                        <td width="120" bgcolor="gainsboro" align="center">
                                            <asp:Button ID="btnSave" runat="server" CommandName="Update" Text="保存" OnClick="btnSave_Click"
                                                OnClientClick="return DoCheck();" />
                                            <asp:Button ID="btnNewCancel" runat="server" CommandName="Cancel" Text="取消" OnClick="btnNewCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <!--翻页起始-->
                                <table width="100%" height="25" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="center" style="height: 19px">
                                            <asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_Click" Visible="False">上一页</asp:LinkButton>&nbsp;
                                            <asp:Label ID="lblCurPage" runat="server"></asp:Label>&nbsp;
                                            <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" Visible="False">下一页</asp:LinkButton>&nbsp;
                                            跳转到第<asp:ListBox ID="lsbPage" runat="server" Rows="1" AutoPostBack="True" OnSelectedIndexChanged="lsbPage_SelectedIndexChanged">
                                            </asp:ListBox>页&nbsp;
                                            <asp:Label ID="pagecount" runat="server" Visible="False"></asp:Label><asp:Label ID="currentpage"
                                                runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <!--翻页结束-->
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False"></asp:Label><br />
                                
                                <uc1:IssuedControl ID="IssuedControl1" runat="server" DataType="5" />
                                
                                <asp:Button ID="btnCheckSeleced" runat="server" Text="比对选中关键字" OnClick="btnCheckSeleced_Click" />
                                <asp:Button ID="btnCheckAll" runat="server" Text="比对所有关键字" OnClick="btnCheckAll_Click" />
                                <asp:Button ID="btnExportKey" runat="server" Text="导出关键字" OnClick="btnExportKey_Click" />
                                <asp:FileUpload ID="fileImport" runat="server" />
                                <asp:Button ID="btnImportKey" runat="server" Text="导入关键字" OnClick="btnImportKey_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="odsCategory" runat="server" SelectMethod="listCategories"
            TypeName="dbConfig">
            <SelectParameters>
                <asp:Parameter DefaultValue="1" Name="nType" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
