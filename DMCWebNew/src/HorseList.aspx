<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HorseList.aspx.cs" Inherits="src_HorseList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link type="text/css" href="styles/common.css" rel="stylesheet" />

    <script type="text/javascript" language="javascript" src="inc/common.js"></script>

</head>
<body onload="showByOldDisplay();">
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
                                        <td width="350" rowspan="2" align="left">
                                            <asp:RadioButtonList ID="qrytypelist" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="4" Selected="True">当天</asp:ListItem>
                                                <asp:ListItem Value="5">昨天</asp:ListItem>
                                                <asp:ListItem Value="1">前三天</asp:ListItem>
                                                <asp:ListItem Value="2">前一周</asp:ListItem>
                                                <asp:ListItem Value="3">前一月</asp:ListItem>
                                                <asp:ListItem Value="0">选择时间</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="10">
                                            从</td>
                                        <td width="85px" colspan="2" rowspan="2">
                                            <igsch:WebDateChooser ID="sdate" runat="server" Width="85px" Value="2006-08-03" ClientSideEvents-OnBlur="SelectedChooseDate();">
                                                <CalendarLayout MaxDate="">
                                                </CalendarLayout>
                                            </igsch:WebDateChooser>
                                            <igsch:WebDateChooser ID="edate" runat="server" Width="85px" Value="2006-08-03" ClientSideEvents-OnBlur="SelectedChooseDate();">
                                                <CalendarLayout MaxDate="">
                                                </CalendarLayout>
                                            </igsch:WebDateChooser>
                                        </td>
                                        <td width="50" rowspan="2">
                                            <asp:RadioButtonList ID="rdlSort" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                <asp:ListItem Value="Asc">升序</asp:ListItem>
                                                <asp:ListItem Value="Desc" Selected="True">降序</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td width="180" rowspan="2">
                                            木马归属地
                                            <asp:DropDownList ID="ddlIpList" runat="server" Width="100px">
                                                <asp:ListItem Value="-1">请选择归属地</asp:ListItem>
                                                <asp:ListItem Value="0">中国大陆</asp:ListItem>
                                                <asp:ListItem Value="1">中国台湾</asp:ListItem>
                                                <asp:ListItem Value="2">香港澳门</asp:ListItem>
                                                <asp:ListItem Value="3">美国</asp:ListItem>
                                                <asp:ListItem Value="4">日本</asp:ListItem>
                                                <asp:ListItem Value="5">韩国</asp:ListItem>
                                                <asp:ListItem Value="254">未知</asp:ListItem>
                                                <asp:ListItem Value="255">其他国家</asp:ListItem>
                                                <asp:ListItem Value="253">保留地址</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td width="150" rowspan="2">
                                            <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnDelete" runat="server" Text="删除所选记录" OnClick="btnDelete_Click"
                                                OnClientClick="return confirm('是否真的要删除这些记录？')" Width="93px" />
                                            &nbsp;
                                            <asp:HiddenField ID="hidRow" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnParent" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10">
                                            到</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="17px">
                                <table width="100%" border="0" cellspacing="1" cellpadding="0" bgcolor="gainsboro">
                                    <tr>
                                        <td style="height: 17px;" align="right" bgcolor="#efefef">
                                            <img runat="server" id="imgTopCursor" src="images/up.gif" border="0" class="hand"
                                                onclick="ChangeDisplay();" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 100%; height: 100%; overflow: scroll; border-top: solid 1 black;
                                    border-bottom: solid 1 #000000;">
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" OnRowDataBound="GridView1_RowDataBound"
                                        DataKeyNames="id" Font-Size="12px" AutoGenerateColumns="False" CssClass="dd">
                                        <Columns>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label Width="0px" ID="lbState" runat="server" Visible="false" Text='<%#Eval("nState")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="0px" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbMail" runat="server" Checked="false" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <center>
                                                        <input id="Checkbox1" type="checkbox" onclick="selectAll(this);" /></center>
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <center>
                                                        <asp:Label ID="lblID" runat="server"></asp:Label></center>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Blue" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="时间">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("dCapture")%>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="木马名称">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("vSiteName")%>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="用户IP地址">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("vSrcAddr")%>
                                                        :<%#Eval("vSrcPort")%></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="用户MAC地址">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("vSrcMac")%>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="服务器IP地址">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("vDstAddr")%>
                                                        :<%#Eval("vDstPort")%></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="相关域名">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("vDnsName")%>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="流量比">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("vRate")%>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="类型">
                                                <ItemTemplate>
                                                    <center>
                                                        <%# common.FormatTrojanFlag(Convert.ToInt32(Eval("vFlag")))%>
                                                    </center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="IP归属地">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbFlag" Text='<%#Eval("ipnum")%>' Visible="false"></asp:Label>
                                                    <center>
                                                        <%# common.FormatIPString(Convert.ToInt32(Eval("ipnum") != DBNull.Value ? Eval("ipnum") : -1))%>
                                                    </center>
                                                </ItemTemplate>
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
                                        <td align="center" style="height: 19px">
                                            <webdiyer:AspNetPager ID="AspNetPager1" PageSize="5" runat="server" FirstPageText="首页"
                                                LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always"
                                                SubmitButtonText="Go" TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging">
                                            </webdiyer:AspNetPager>
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
                            <td id="trBottom" runat="server" height="120px">
                                <iframe runat="server" src="horseinfo.aspx" id="frmContent" name="frmContent" scrolling="no"
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
