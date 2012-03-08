<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchDNS2.aspx.cs" Inherits="src_searchDNS2"
    EnableEventValidation="false" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>域名查询</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
</head>
<body topmargin="0" leftmargin="0">
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
        <tr>
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                    <tr id="trTop" runat="server">
                        <td height="165px">
                            <table width="100%" border="0" cellspacing="5" cellpadding="0">
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
                                    <td width="230" valign="top" bgcolor="#CEE9F2" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0">
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
                                            </tr>
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
                                        <table border="0" cellpadding="0" cellspacing="0">
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
                                                <td style="text-align: right">
                                                    用户IP地址：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbUserIp" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                                <td style="text-align: right">
                                                    用户MAC地址：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbUserMac" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    域名服务器IP：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtvDstIp" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    网关MAC地址：
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtvDstMac" runat="server" Width="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="340" valign="middle" bgcolor="#CEE9F2" align="center">
                                        <table border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td>
                                                    <div align="right">
                                                        域名名称：</div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtvName" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:CheckBox ID="fromeq" runat="server" Text="等于" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div align="right">
                                                        域名类型：</div>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlDnsTypelist" runat="server" Width="150px">
                                                        <asp:ListItem Value="-1">所有域名</asp:ListItem>
                                                        <asp:ListItem Value="0">所有黑域名</asp:ListItem>
                                                        <asp:ListItem Value="7">普通黑域名</asp:ListItem>
                                                        <asp:ListItem Value="8">重要黑域名</asp:ListItem>
                                                        <asp:ListItem Value="9">紧急黑域名</asp:ListItem>
                                                        <asp:ListItem Value="1">动态域名</asp:ListItem>
                                                        <asp:ListItem Value="2">可疑域名</asp:ListItem>
                                                        <asp:ListItem Value="10">所有异常域名</asp:ListItem>
                                                        <asp:ListItem Value="3">异常域名(低风险)</asp:ListItem>
                                                        <asp:ListItem Value="4">异常域名(中风险)</asp:ListItem>
                                                        <asp:ListItem Value="5">异常域名(高风险)</asp:ListItem>
                                                        <asp:ListItem Value="6">白域名</asp:ListItem>
                                                        <asp:ListItem Value="11">其他域名</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div align="right">
                                                        IP归属地：</div>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlIpList" runat="server" Width="150px">
                                                        <asp:ListItem Value="-1">
                                                       请选择归属地
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="0">
                                                       中国大陆
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="1">
                                                       中国台湾
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="2">
                                                       香港澳门
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="3">
                                                       美国
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="4">
                                                       日本
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="5">
                                                       韩国
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="254">
                                                       未知
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="255">
                                                       其他国家
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="253">
                                                       保留地址
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div align="right">
                                                        域名解析IP：</div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtvAddr" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    静默与否：
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStateFlag" runat="server" Height="16px" Width="106px">
                                                        <asp:ListItem Value="-1">所有</asp:ListItem>
                                                        <asp:ListItem Value="0">活动</asp:ListItem>
                                                        <asp:ListItem Value="1">静默</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RadioButtonList ID="rdlSort" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                        <asp:ListItem Value="Asc">升序</asp:ListItem>
                                                        <asp:ListItem Value="Desc" Selected="True">降序</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    敏感类型：
                                                </td>
                                                <td>
                                                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">IP黑名单</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="25px">
                            <table width="100%" height="25px" cellpadding="0" cellspacing="0" bgcolor="gainsboro">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblUser" runat="server" Visible="False"></asp:Label><asp:Button ID="btnSearch"
                                            runat="server" Text="查询" OnClick="btnSearch_Click" Width="55px" />
                                        <asp:Button ID="btnDelete" runat="server" Text="删除所选记录" OnClick="btnDelete_Click"
                                            OnClientClick="return confirm('是否真的要删除这些记录？')" Width="100px" />
                                        <asp:Button ID="btDelAll" runat="server" Text="删除查询结果" OnClientClick="return confirm('是否真的要删除这些记录？')"
                                            Width="100px" OnClick="btDelAll_Click" />
                                        <asp:Button ID="btExportAll" runat="server" Text="导出所选记录" Width="100px" OnClick="btExportAll_Click" />
                                        <asp:Button ID="btnExport" runat="server" Text="导出查询结果" OnClick="btnExport_Click"
                                            Width="100px" /><asp:HiddenField ID="hdDnsType" runat="server" />
                                    </td>
                                    <td width="15" align="center">
                                        <img id="imgTopCursor" src="images/up.gif" border="0" class="hand" onclick="ChangeDisplay();"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trContent" runat="server" visible="true">
                        <td>
                            <div style="width: 100%; height: 100%; overflow: scroll; border-top: solid 1 black;
                                border-bottom: solid 1 #000000;">
                                <asp:GridView ID="dtgData" runat="server" Width="100%" PageSize="2" OnRowDataBound="dtgData_RowDataBound"
                                    Font-Size="12px" DataKeyNames="ID" AutoGenerateColumns="False" CssClass="dd">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbMail" runat="server" Checked="false" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <input id="Checkbox1" type="checkbox" onclick="selectAll(this);" />
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="15px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblID" runat="server"></asp:Label></center>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Blue" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="时间">
                                            <ItemTemplate>
                                                <center>
                                                    <%#Eval("dCapture")%></center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="域名">
                                            <ItemTemplate>
                                                <center>
                                                    <%#common.SubContent( Eval("vName").ToString(),25) %></center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="域名服务器IP">
                                            <ItemTemplate>
                                                <center>
                                                    <%#Eval("vDstAddr")%></center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="网关MAC地址">
                                            <ItemTemplate>
                                                <center>
                                                    <%#Eval("vDstMac")%>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="用户IP地址">
                                            <ItemTemplate>
                                                <center>
                                                    <%#Eval("vSrcAddr")%></center>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="用户MAC地址">
                                            <ItemTemplate>
                                                <center>
                                                    <%#Eval("vSrcMac")%>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IP归属地">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label runat="server" ID="lbFlag" Text='<%#Eval("ipnum")%>' Visible="false"></asp:Label>
                                                    <%# common.FormatIPString(Convert.ToInt32(Eval("ipnum") != DBNull.Value ? Eval("ipnum") : -1))%></center>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="风险级别">
                                            <ItemTemplate>
                                                <center>
                                                    <%# common.FormatDnsType( Convert.ToInt32(Eval("vType"))) %></center>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="静默与否">
                                            <ItemTemplate>
                                                <center>
                                                    <%# Eval("vStateFlag").ToString() == "1" ? "<font color=#009900>静默</font>" : "活动"%>
                                                </center>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IP黑名单">
                                            <ItemTemplate>
                                                 <asp:HiddenField runat="server" ID="hdKey" Value='<%#Eval("nKey") %>' />
                                                 <asp:Label ID="lbIp" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="25px">
                            <!--翻页起始-->
                            <table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="gainsboro">
                                <tr>
                                    <td align="center">
                                        <webdiyer:AspNetPager ID="AspNetPager1" PageSize="5" runat="server" FirstPageText="首页"
                                            LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always"
                                            SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging">
                                        </webdiyer:AspNetPager>
                                    </td>
                                    <td align="right">
                                        <img alt="" id="imgBottomCursor" src="images/up.gif" border="0" class="hand" onclick="ChangeDisplay();"
                                            runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <!--翻页结束-->
                        </td>
                    </tr>
                    <tr>
                        <td id="trBottom" runat="server" height="120px">
                            <iframe runat="server" src="dnsInfo.aspx" id="frmContent" name="frmContent" scrolling="no"
                                contenteditable="true" frameborder="no" width="100%" height="100%"></iframe>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
