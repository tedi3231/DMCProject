<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchMsg.aspx.cs" Inherits="src_searchMsg" %>

<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>聊天查询</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
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
                                                    <input type="checkbox" id="chkHost" onclick="SelectAllHost();">全选&nbsp;&nbsp;
                                                </td>
                                                <td valign="middle" colspan="5">
                                                    <div style="width: 100%; height: 75; overflow: auto; border: inset 2 #c0c0c0; background-color: White;
                                                        margin: 3px 0 2px 1;">
                                                        <asp:CheckBoxList ID="cblHost" runat="server" DataTextField="vCorpName" DataValueField="nId"
                                                            RepeatColumns="5" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    用户IP：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSrcAddr" runat="server" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    网卡地址：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSrcMac" runat="server" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    目标IP：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDstAddr" runat="server" Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="300" align="center" valign="top" bgcolor="#CEE9F2">
                                        <table border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td width="80px"> 邮箱：</td>
                                                <td>
                                                    <asp:TextBox ID="txtMailFrom" runat="server"></asp:TextBox>
                                                    <asp:CheckBox ID="fromeq" runat="server" Text="等于" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>类型：</td>
                                                <td>                                                    
                                                    <asp:RadioButtonList ID="chkTalkMode" runat="server" RepeatDirection="Horizontal"
                                                        RepeatLayout="Flow">
                                                        <asp:ListItem Value="0">发送方</asp:ListItem>
                                                        <asp:ListItem Value="1">接收方</asp:ListItem>
                                                        <asp:ListItem Value="2" Selected="True">对话模式</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                            <td>内容：</td>
                                                <td>
                                                    <asp:TextBox ID="txtMessage" runat="server" Width="230"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>消息类型：</td>
                                                <td>
                                                    <table border="0" cellspacing="0" cellpadding="2">
                                                        <tr>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlCategory" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                                    <asp:ListItem Selected="True" Value="Msn">MSN</asp:ListItem>
                                                                    <asp:ListItem Value="Yahoo">YAHOO</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="rdlSort" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="Asc">升序</asp:ListItem>
                                                                    <asp:ListItem Value="Desc" Selected="True">降序</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td style="display: none;">
                                                                <asp:RadioButtonList ID="rdlMode" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="0" Selected="True">普通模式</asp:ListItem>
                                                                    <asp:ListItem Value="1">敏感模式</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>敏感类型：</td>
                                                <td>
                                                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">IP黑名单</asp:ListItem>
                                                        <asp:ListItem Value="2">邮箱黑名单</asp:ListItem>
                                                        <asp:ListItem Value="3">关键字</asp:ListItem>
                                                    </asp:CheckBoxList>
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
                                            OnClientClick="return confirm('是否真的要删除这些记录？')" />&nbsp;<asp:Button ID="btDelAll"
                                                runat="server" OnClick="btDelAll_Click" OnClientClick="return confirm('是否真的要删除这些记录？')"
                                                Text="删除查询结果" Width="100px" />
                                        <asp:Button ID="btExportAll" runat="server" OnClick="btExportAll_Click" Text="导出所选记录"
                                            Width="100px" />
                                        <asp:Button ID="btnExport" runat="server" Text="导出查询结果" OnClick="btnExport_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="25px">
                            <table width="100%" height="100%" border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                                <tr>
                                    <td width="20" bgcolor="#efefef" align="center">
                                        <input type="checkbox" id="CheckAll" onclick="DoSelectAll();" title="全选/全取消">
                                    </td>
                                    <td width="30" bgcolor="#efefef" align="center">
                                        编号
                                    </td>
                                    <td bgcolor="#efefef" align="center">
                                        内容
                                    </td>
                                    <td width="120" bgcolor="#efefef" align="center">
                                        时间
                                    </td>
                                    <td width="150" bgcolor="#efefef" align="center">
                                        发送方邮箱
                                    </td>
                                    <td width="150" bgcolor="#efefef" align="center">
                                        接收方邮箱
                                    </td>
                                    <td width="30" bgcolor="#efefef" align="center">
                                        类型
                                    </td>
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
                                            <ItemStyle Width="20px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="编号" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server">                                </asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="blue" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="内容">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelMessage" runat="server" Width="150px" CssClass="hidoverflow"><%# DataBinder.Eval(Container.DataItem, "vMessage").ToString() %></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="vMessage" HeaderText="具体内容" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="dCapture" HeaderText="时间">
                                            <ItemStyle Width="120px" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="发送方邮箱">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelMailFrom" runat="server" Width="150px" CssClass="hidoverflow"><%# DataBinder.Eval(Container.DataItem, "vMailFrom").ToString() %></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="接收方邮箱">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelMailTo" runat="server" Width="150px" CssClass="hidoverflow"><%# DataBinder.Eval(Container.DataItem, "vMailTo").ToString() %></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="类型">
                                            <ItemTemplate>
                                                <asp:Label ID="Labeltype" runat="server"><%# getState(DataBinder.Eval(Container.DataItem, "nType"))%></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="所属节点">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hdKey" Value='<%#Eval("nKey") %>' />
                                                <asp:Label ID="LabelParent" runat="server"><%# getSiteName(DataBinder.Eval(Container.DataItem, "nParent"))%></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateColumn>
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
                        <td height="120px">
                            <iframe src="msninfo.aspx" id="frmContent" name="frmContent" scrolling="no" frameborder="0"
                                width="100%" height="120px"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
