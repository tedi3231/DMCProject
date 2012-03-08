<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchSensitive.aspx.cs"
    Inherits="src_SearchSensitive" %>

<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="inc/common.js"></script>
    <script src="inc/jquery-1.4.2.js" type="text/javascript"></script>
   <%-- <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            var type = $('#DropDownList1').val();

        });
    </script>--%>
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
        <tr>
            <td style="width: 2px; cursor: e-resize; background-color: #6D9EDB">
            </td>
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                    <tr id="trTop">
                        <td height="190px">
                            <table width="100%" height="165px" border="0" cellspacing="5" cellpadding="0">
                                <tr>
                                    <td align="center" bgcolor="#CEE9F2">
                                        选择时间
                                    </td>
                                    <td align="center" bgcolor="#CEE9F2">
                                        选择服务器及地址
                                    </td>
                                    <td align="center" bgcolor="#CEE9F2">
                                        其他条件
                                    </td>
                                </tr>
                                <tr>
                                    <td width="210" valign="top" bgcolor="#CEE9F2" align="center">
                                        <table>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RadioButtonList ID="qrytypelist" runat="server" RepeatColumns="3" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="4" Selected="True">当天</asp:ListItem>
                                                        <asp:ListItem Value="5">昨天</asp:ListItem>
                                                        <asp:ListItem Value="1">前三天</asp:ListItem>
                                                        <asp:ListItem Value="2">前一周</asp:ListItem>
                                                        <asp:ListItem Value="3">前一月</asp:ListItem>
                                                        <asp:ListItem Value="0">选择时间</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <tr>
                                                    <td width="60" align="center">
                                                        起始时间：
                                                    </td>
                                                    <td width="140">
                                                        <igsch:WebDateChooser ID="sdate" runat="server" Width="100px" Value="2006-01-01"
                                                            ClientSideEvents-OnBlur="SelectedChooseDate();">
                                                            <CalendarLayout MaxDate="">
                                                            </CalendarLayout>
                                                        </igsch:WebDateChooser>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="60" align="center">
                                                        结束时间：
                                                    </td>
                                                    <td>
                                                        <igsch:WebDateChooser ID="edate" runat="server" Width="100px" Value="2006-01-01"
                                                            ClientSideEvents-OnBlur="SelectedChooseDate();">
                                                            <CalendarLayout MaxDate="">
                                                            </CalendarLayout>
                                                        </igsch:WebDateChooser>
                                                    </td>
                                                </tr>
                                        </table>
                                    </td>
                                    <td valign="top" bgcolor="#CEE9F2" align="center">
                                        <table>
                                            <tr>
                                                <td>
                                                    服务器：<br />
                                                    <input type="checkbox" id="chkHost" onclick="SelectAllHost();" />全选&nbsp;&nbsp;
                                                </td>
                                                <td valign="middle" colspan="3">
                                                    <div style="width: 100%; height: 75; overflow: auto; border: inset 2 #c0c0c0; background-color: White;
                                                        margin: 3px 0 2px 1;">
                                                        <asp:CheckBoxList ID="cblHost" runat="server" DataTextField="vCorpName" DataValueField="nId"
                                                            RepeatColumns="4" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    用户IP：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSrcAddr" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    网卡地址：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSrcMac" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    目的IP：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDstAddr" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp; 目的MAC：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDstMac" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                敏感类型:
                                                </td>
                                                <td colspan="3">
                                                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" 
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">IP黑名单</asp:ListItem>
                                                        <asp:ListItem Value="2">邮箱黑名单</asp:ListItem>
                                                        <asp:ListItem Value="3">关键字</asp:ListItem>
                                                        <asp:ListItem Value="4">垃圾邮件关键字</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="300" align="center" valign="top" bgcolor="#CEE9F2">
                                        <table border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td style="width: 65px">
                                                    数据类型：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" Height="18px" Width="148px">
                                                        <asp:ListItem Value="1">Http网站</asp:ListItem>
                                                        <asp:ListItem Value="2">MSN聊天</asp:ListItem>
                                                        <asp:ListItem Value="3">雅虎聊天</asp:ListItem>
                                                        <asp:ListItem Value="4">P0P3邮件分析</asp:ListItem>
                                                        <asp:ListItem Value="5">SMTP邮件分析</asp:ListItem>
                                                        <asp:ListItem Value="6">收Web邮件</asp:ListItem>
                                                        <asp:ListItem Value="7">发Web邮件</asp:ListItem>
                                                        <asp:ListItem Value="8">Web帐号</asp:ListItem>
                                                        <asp:ListItem Value="9">实时域名</asp:ListItem>
                                                        <asp:ListItem Value="10">木马分析</asp:ListItem>
                                                        <asp:ListItem Value="11">FTP分析 </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 65px">
                                                    <%--   敏感模块：--%>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" Visible="False">
                                                        <asp:ListItem>IP黑名单</asp:ListItem>
                                                        <asp:ListItem>邮箱黑名单</asp:ListItem>
                                                        <asp:ListItem>关键字</asp:ListItem>
                                                        <asp:ListItem>垃圾邮件关键字</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" height="25px" cellpadding="0" cellspacing="0" bgcolor="gainsboro">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnDelete" runat="server" Text="删除所选记录" OnClick="btnDelete_Click"
                                            OnClientClick="return confirm('是否真的要删除这些记录？')" />&nbsp;
                                        <asp:Button ID="btDelAll" runat="server" OnClick="btDelAll_Click" OnClientClick="return confirm('是否真的要删除这些记录？')"
                                            Text="删除查询结果" Width="100px" />
                                        &nbsp;<asp:Button ID="btExportAll" runat="server" OnClick="btExportAll_Click" Text="导出所选记录"
                                            Width="100px" />
                                        <asp:Button ID="btnExport" runat="server" Text="导出查询结果" 
                                            OnClick="btnExport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="25px">
                            <table width="100%" height="100%" border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                                <tr>
                                    <td width="30px" bgcolor="#efefef">
                                        <input type="checkbox" id="CheckAll" onclick="DoSelectAll();" title="全选/全取消" />
                                    </td>
                                    <td align="center" bgcolor="#efefef" width="30">
                                        编号
                                    </td>
                                    <td width="120px" bgcolor="#efefef" align="center">
                                        时间
                                    </td>
                                    <td width="90" bgcolor="#efefef" align="center">
                                        用户网卡地址
                                    </td>
                                    <td width="100" bgcolor="#efefef" align="center">
                                        用户IP
                                    </td>
                                    <td width="100" bgcolor="#efefef" align="center">
                                        网站IP地址
                                    </td>
                                    <%-- <td bgcolor="#efefef" align="center">被访问网站</td>--%>
                                    <td width="80" bgcolor="#efefef" align="center">
                                        所属节点
                                    </td>
                                    <td width="60px" bgcolor="#efefef" align="center">
                                        IP黑名单
                                    </td>
                                    <td width="60px" bgcolor="#efefef" align="center">
                                        邮箱黑名单
                                    </td>
                                    <td width="60px" bgcolor="#efefef" align="center">
                                        关键字
                                    </td>
                                    <td width="90px" bgcolor="#efefef" align="center">
                                        垃圾邮件关键字
                                    </td>
                                    <td width="15" bgcolor="#efefef" align="center">
                                        <img id="imgTopCursor" src="images/up.gif" border="0" class="hand" onclick="ChangeDisplay();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="width: 100%; height: 100%; overflow: scroll; border-top: solid 1 black;
                                border-bottom: solid 1 #000000;">
                                <asp:DataGrid ID="dtgData" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#EFEFEF" BorderStyle="Solid" BorderWidth="1px" CellPadding="0" DataKeyField="ID"
                                    PageSize="250" Width="100%" OnItemDataBound="dtgData_ItemDataBound" ShowHeader="False">
                                    <ItemStyle BackColor="White" ForeColor="Black" />
                                    <HeaderStyle BackColor="#EFEFEF" Font-Bold="False" ForeColor="Black" />
                                    <Columns>
                                        <asp:BoundColumn DataField="nState" HeaderText="查看状态" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="全选/全不选">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkData" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="编号" Visible="true">
                                            <ItemTemplate>
                                                <%# this.dtgData.CurrentPageIndex * this.dtgData.PageSize + Container.ItemIndex + 1%>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="blue" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="dCapture" HeaderText="时间">
                                            <ItemStyle Width="120px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="vSrcMac" HeaderText="用户网卡地址">
                                            <ItemStyle Width="90px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="vSrcAddr" HeaderText="用户IP">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="vDstAddr" HeaderText="网站IP地址">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundColumn>
                                        <%--  <asp:TemplateColumn HeaderText="被访问网站">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelURL" runat="server" Width="450" CssClass="hidoverflow"><%# DataBinder.Eval(Container.DataItem, "vURL").ToString() %></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>--%>
                                        <asp:TemplateColumn HeaderText="所属节点">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdKey" Value='<%#Eval("nKey") %>' />
                                                <asp:Label ID="LabelParent" runat="server"><%# getSiteName(DataBinder.Eval(Container.DataItem, "nParent"))%></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateColumn>
                                        <%-- 9--%>
                                        <asp:TemplateColumn HeaderText="IP黑名单">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="邮箱黑名单">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="关键字">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="垃圾邮件关键字">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <ItemStyle Width="90px" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle Visible="False" />
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="25px">
                            <!--翻页起始-->
                            <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="gainsboro">
                                <tr>
                                    <td align="center" style="height: 19px">
                                        <asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_Click" Visible="False">上一页</asp:LinkButton>&nbsp;
                                        第<asp:Label ID="lblPageIndex" runat="server" Text="0"></asp:Label>页/共<asp:Label ID="lblPageCount"
                                            runat="server" Text="0"></asp:Label>页（共<asp:Label ID="lblRecordCount" runat="server"
                                                Text="0"></asp:Label>条记录）&nbsp;
                                        <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" Visible="False">下一页</asp:LinkButton>&nbsp;
                                        跳转到第<asp:ListBox ID="lsbPage" runat="server" Rows="1" AutoPostBack="True" OnSelectedIndexChanged="lsbPage_SelectedIndexChanged">
                                        </asp:ListBox>
                                        页&nbsp;
                                    </td>
                                    <td width="15" align="center">
                                        <img id="imgBottomCursor" src="images/up.gif" border="0" class="hand" onclick="ChangeDisplay();"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <!--翻页结束-->
                        </td>
                    </tr>
                     <tr id="trBottom">
                        <td height="140px">
                            <iframe src="" id="frmContent" name="frmContent" scrolling="no" frameborder="0"
                                width="100%" height="140px"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
