<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getstate.aspx.cs" Inherits="src_getstate" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
    <tr>
        <td style="width:2px;cursor:e-resize; background-color:#6D9EDB"></td>
        <td valign="top" align="center">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr><td align="center" valign="middle" class="title" height="40">客户端状态</td></tr>
            <tr>
                <td valign="top" align="center" height="330">
                    <table border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                        <tr>
                            <td width="20" bgcolor="#efefef"></td>
                            <td width="300" bgcolor="#efefef" align="center">名称</td>
                            <td width="80" bgcolor="#efefef" align="center">状态</td>
                           
                        </tr>
                    </table>
                    <asp:GridView ID="gdvData" runat="server" AllowPaging="false" AutoGenerateColumns="False" Width="400"
                         DataKeyNames="nId" BorderStyle="Solid" BorderWidth="1px" PageSize="20"  CellPadding="0" CellSpacing="0" ShowHeader="False">
                        <HeaderStyle BackColor="#EFEFEF" Font-Size="14px" /><Columns>
                            <asp:TemplateField>
                                <ItemStyle Width="20px"/>
                                <ItemTemplate>
                                    <img src="images/cursor.gif">
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="vCorpName" ItemStyle-Width="300px" />
                           
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <asp:Label ID="lb2" runat="server" Text='<%#fun2(DataBinder.Eval(Container,"DataItem.nId")) %>'></asp:Label>
                                </ItemTemplate>
                                
                                <ControlStyle Width="80px" />
                                <ItemStyle Width="80px" />
                            </asp:TemplateField>
                           
                        </Columns>
                    </asp:GridView>
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
    </div>
    </form>
</body>
</html>



