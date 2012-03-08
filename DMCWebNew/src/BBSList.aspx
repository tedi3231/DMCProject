<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BBSList.aspx.cs" Inherits="src_BBSList" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body onload="showByOldDisplay();">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
        <tr>
            <td style="width:2px;cursor:e-resize; background-color:#6D9EDB"></td>
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                <tr id="trTop">
                    <td bgcolor="gainsboro" height="50px">
                    <table border="0" cellpadding="0" cellspacing="0" height="100%">
                        <tr>
                            <td align="left" rowspan="2">
                                <asp:RadioButtonList ID="qrytypelist" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="4" Selected="True">当天</asp:ListItem>
                                    <asp:ListItem Value="5">昨天</asp:ListItem>
                                    <asp:ListItem Value="1">前三天</asp:ListItem>
                                    <asp:ListItem Value="2">前一周</asp:ListItem>
                                    <asp:ListItem Value="3">前一月</asp:ListItem>
                                    <asp:ListItem Value="0">选择时间</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="sdate" runat="server" Width="85px" Value="2006-08-03" ClientSideEvents-OnBlur="SelectedChooseDate();">
                                    <CalendarLayout MaxDate=""></CalendarLayout>
                                </igsch:webdatechooser>
                            </td>
                            <td rowspan="2">
                                <asp:RadioButtonList ID="rdlSort" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                    <asp:ListItem Value="Asc">升序</asp:ListItem>
                                    <asp:ListItem Value="Desc" Selected="True">降序</asp:ListItem>
                                </asp:RadioButtonList>&nbsp;
                            </td>
                            <td rowspan="2" style="display:none;">
                                <asp:RadioButtonList ID="rdlMode" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                    <asp:ListItem Value="0" Selected="True">普通模式</asp:ListItem>
                                    <asp:ListItem Value="1">敏感模式</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td rowspan="2"><asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="删除所选记录" OnClick="btnDelete_Click" OnClientClick="return confirm('是否真的要删除这些记录？')" />
                            </td>
                        </tr>
                        <tr>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="edate" runat="server" Width="85px" Value="2006-08-03" ClientSideEvents-OnBlur="SelectedChooseDate();">
                                    <CalendarLayout MaxDate=""></CalendarLayout>
                                </igsch:webdatechooser>
                            </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td height="25px">
                    <table width="100%" height="100%" border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                        <tr>
                            <td width="20" bgcolor="#efefef"><input type="checkbox" id="CheckAll" onclick="DoSelectAll();" title="全选/全取消"></td>
                            <td width="30" bgcolor="#efefef"></td>
                            <td width="120" bgcolor="#efefef" align="center">时间</td>
                            <td bgcolor="#efefef" align="center">内容</td>
                            <td width="100" bgcolor="#efefef" align="center">用户网卡地址</td>
                            <td width="100" bgcolor="#efefef" align="center">用户IP</td>
                            <td width="100" bgcolor="#efefef" align="center">服务器IP</td>
                            <td width="15" bgcolor="#efefef" align="center"><img id="imgTopCursor" src="images/up.gif" border="0" class="hand" onclick="ChangeDisplay();"/></td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="width:100%; height:100%; overflow:scroll; border-top:solid 1 black; border-bottom:solid 1 #000000;">
                        <asp:DataGrid ID="dtgData" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#EFEFEF" BorderStyle="Solid" BorderWidth="1px" CellPadding="0" DataKeyField="ID" PageSize="250" Width="100%" OnItemDataBound="dtgData_ItemDataBound" ShowHeader="False">
                            <ItemStyle BackColor="White" ForeColor="Black" />
                            <HeaderStyle BackColor="#EFEFEF" Font-Bold="False" ForeColor="Black" />
                            <Columns>
                            <asp:BoundColumn DataField="nState" HeaderText="查看状态" Visible=False></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="全选/全不选">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkData" Runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="编号" Visible="true">
                                <ItemTemplate>
                                    <asp:Label id="lblID" runat="server">                                </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="blue" />
                            </asp:TemplateColumn> 
                                <asp:BoundColumn DataField="dCapture" HeaderText="时间">
                                    <ItemStyle Width="120px" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="用户名&密码">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelContent" runat="server">用户名：<%# DataBinder.Eval(Container.DataItem, "vLogin")%> | 密码：<%# DataBinder.Eval(Container.DataItem, "vPwd")%></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="用户网卡地址">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelMailFrom" runat="server"><%# DataBinder.Eval(Container.DataItem, "vSrcMac")%></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="用户IP地址">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelMailTo" runat="server"><%# DataBinder.Eval(Container.DataItem, "vSrcAddr")%></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="服务器Ip">
                                    <ItemTemplate>
                                        <asp:Label ID="Labeltype" runat="server"><%# DataBinder.Eval(Container.DataItem, "vDstAddr")%></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
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
                                第<asp:Label id="lblPageIndex" runat="server" Text="0"></asp:Label>页/共<asp:Label id="lblPageCount" runat="server" Text="0"></asp:Label>页（共<asp:Label id="lblRecordCount" runat="server" Text="0"></asp:Label>条记录）&nbsp;
                                <asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click" Visible="False">下一页</asp:LinkButton>&nbsp;
                                跳转到第<asp:ListBox ID="lsbPage" runat="server" Rows="1" AutoPostBack="True" OnSelectedIndexChanged="lsbPage_SelectedIndexChanged"></asp:ListBox>页&nbsp;
                            </td>
                            <td width="15" align="center"><img id="imgBottomCursor" src="images/up.gif" border="0" class="hand" onclick="ChangeDisplay();" runat="server"/></td>
                        </tr>
                    </table>
                    <!--翻页结束-->
                    </td>
                </tr>
                <tr id="trBottom">
                    <td height="100px">
                    <iframe src="BBSInfo.aspx" id="frmContent" name="frmContent" scrolling="no" frameborder="0" width="100%" height="80px">
                    </iframe>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>



