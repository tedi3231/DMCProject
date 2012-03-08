<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailBoxList.aspx.cs" Inherits="src_MailBoxList" %>

<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="inc/common.js"></script>

</head>
<body onload="DoWinOnload();">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
            <tr>
                <td style="width: 2px; cursor: e-resize; background-color: #6D9EDB">
                </td>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                        <tr id="trTop" runat="server">
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
                                        <td>
                                            从</td>
                                        <td>
                                            <igsch:WebDateChooser ID="sdate" runat="server" Width="85px">
                                                <CalendarLayout MaxDate="">
                                                </CalendarLayout>
                                            </igsch:WebDateChooser>
                                        </td>
                                        <td rowspan="2">
                                            <asp:RadioButtonList ID="rdlMailType" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                <asp:ListItem Value="Pop">收信</asp:ListItem>
                                                <asp:ListItem Value="Smtp" Selected="True">发信</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td rowspan="2">
                                            <asp:RadioButtonList ID="rdlSort" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                <asp:ListItem Value="Asc">升序</asp:ListItem>
                                                <asp:ListItem Value="Desc" Selected="True">降序</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td rowspan="2" style="display:none;">
                                            <asp:RadioButtonList ID="rdlMode" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                <asp:ListItem Value="0" Selected="True">普通模式</asp:ListItem>
                                                <%--<asp:ListItem Value="1">敏感模式</asp:ListItem>--%>
                                            </asp:RadioButtonList>&nbsp;
                                        </td>
                                        <td rowspan="2" class="btquery">
                                            <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" OnClientClick="SaveDateRange();" />
                                            <asp:Button ID="btnDelete" runat="server" Text="删除所选" OnClick="btnDelete_Click" OnClientClick="return confirm('是否真的要删除这些记录？')" /></td>
                                        <td>
                                            &nbsp;<asp:Button ID="btnPrev" runat="server" Text="上一封" OnClick="btnPrev_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            到</td>
                                        <td>
                                            <igsch:WebDateChooser ID="edate" runat="server" Width="85px">
                                                <CalendarLayout MaxDate="">
                                                </CalendarLayout>
                                            </igsch:WebDateChooser>
                                        </td>
                                        <td>
                                            &nbsp;<asp:Button ID="btnNext" runat="server" Text="下一封" OnClick="btnNext_Click" />
                                            <asp:HiddenField ID="hidRow" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="25px">
                                <table width="100%" height="100%" border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                                    <tr>
                                        <td width="20" bgcolor="#efefef">
                                            <input type="checkbox" id="CheckAll" onclick="DoSelectAll();" title="全选/全取消"></td>
                                        <td width="30" bgcolor="#efefef">
                                        </td>
                                        <td width="30" bgcolor="#efefef" align="center">
                                            附件</td>
                                        <td bgcolor="#efefef" align="center">
                                            主题</td>
                                        <td width="120" bgcolor="#efefef" align="center">
                                            时间</td>
                                        <td width="100" bgcolor="#efefef" align="center">
                                            用户IP地址</td>
                                        <td width="140" bgcolor="#efefef" align="center">
                                            发件人</td>
                                        <td width="120" bgcolor="#efefef" align="center">
                                            收件人</td>
                                        <td width="15" bgcolor="#efefef" align="center">
                                            <img runat="server" id="imgTopCursor" src="images/up.gif" border="0" class="hand"
                                                onclick="ChangeDisplay();" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trContent" runat="server">
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
                                            <asp:TemplateColumn HeaderText="附件">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelAttach" runat="server"><%# formatAttach(DataBinder.Eval(Container.DataItem, "nAttach"))%></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="red" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="主题">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelTitle" runat="server" CssClass="hidoverflow" Width="250px"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "vLpTitle").ToString())%></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="vLpTitle" HeaderText="具体内容" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="dCapture" HeaderText="时间">
                                                <ItemStyle Width="120px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="vSrcAddr" HeaderText="用户IP地址">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="发件人">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelMailFrom" runat="server" Width="140px" CssClass="hidoverflow"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "vLpFrom").ToString())%></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="140px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="收件人">
                                                <ItemTemplate>
                                                    <asp:Label ID="LabelMailTo" runat="server" Width="120px" CssClass="hidoverflow"><%# Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "vLpTo").ToString())%></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="120px" />
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
                                            </asp:ListBox>页&nbsp;
                                        </td>
                                        <td width="15" align="center">
                                            <img id="imgBottomCursor" src="images/up.gif" border="0" class="hand" onclick="ChangeDisplay();"
                                                runat="server" /></td>
                                    </tr>
                                </table>
                                <!--翻页结束-->
                            </td>
                        </tr>
                        <tr>
                            <td id="trBottom" runat="server" height="250">
                                <iframe id="frmContent" runat="server" contenteditable="true" frameborder="no" height="100%"
                                    name="frmContent" scrolling="no" src="mailboxinfo.aspx" width="100%"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
