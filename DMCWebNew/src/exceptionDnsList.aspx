﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="exceptionDnsList.aspx.cs" Inherits="src_exceptionDnsList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>异常域名查看</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="inc/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="95%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th colspan="8">
                    <div align="left">
                      异常域名查看
                    </div>                </th>
            </tr>
            <tr>
                <td width="83">
                    <div align="right">
                        服务器:</div>                </td>
                <td width="195">
                    <div align="left">
                        <input type="checkbox" id="chkHost" onclick="SelectAllHost();" />全选
                        <asp:CheckBoxList ID="cblHost" runat="server" DataTextField="vCorpName" DataValueField="nId"
                            RepeatColumns="6" RepeatDirection="Horizontal" RepeatLayout="Table">                        </asp:CheckBoxList>
                    </div>               </td>
                <td width="104">
                    <div align="right">
                        域名性质:</div>                </td>
                <td width="204"><asp:DropDownList ID="ddDnsType" runat="server" Width="150px">
                    <asp:ListItem>异常域名</asp:ListItem>
                </asp:DropDownList></td>
                <td width="104">
                    <div align="right">
                        &nbsp;</div>                </td>
                <td width="288">
                    <div align="left">
                        &nbsp;</div>                </td>
            </tr>
			<tr>
                <td width="83">
                <div align="right">域名:</div>                </td>
                <td>
                    <div align="left">
                        <asp:TextBox ID="tbDns" runat="server" Width="150px"></asp:TextBox>&nbsp;</div>                </td>
                <td width="104">
                    <div align="right">
                        域名IP:</div>                </td>
                <td>
                    <div align="left">
                        <asp:TextBox ID="tbDnsIp" runat="server" Width="150px"></asp:TextBox>&nbsp;</div>                </td>
                <td align="right" valign="middle">
                    <div align="right">内网IP:</div>
                </td>
                <td align="left"><div align="left">
                    <asp:TextBox ID="tbInIp" runat="server" Width="150px"></asp:TextBox></div></td>
			</tr>
            <tr>
                <td width="83">
                    <div align="right">
                        开始时间:</div>                </td>
                <td>
                    <div align="left">
                        <TW:DateTextBox ID="dtStart" runat="server" Width="150px"></TW:DateTextBox></div>                </td>
                <td width="104">
                    <div align="right">
                        结束时间:</div>                </td>
                <td>
                    <div align="left">
                        <TW:DateTextBox ID="dtEnd" runat="server" Width="150px"></TW:DateTextBox></div>                </td>
                <td colspan="2">
                    <div align="right">                    </div>
                    <div align="left">
                        <asp:Button ID="Button1" runat="server" Text="查   询" OnClick="Button1_Click" /></div>                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" >
            <Columns>
                <asp:TemplateField HeaderText="域名">
                    <ItemTemplate>
                        <%#Eval("vName")%>                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="域名IP">
                    <ItemTemplate>
                        <%#Eval("vDstIp")%>
                        :<%#Eval("vDstPort")%>
                    </ItemTemplate>
                </asp:TemplateField>
               <%-- <asp:TemplateField HeaderText="内网IP">
                    <ItemTemplate>
                        <%#Eval("vSrcIp")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="所在地域">
                    <ItemTemplate>
                        <asp:Label ID="lbArea" runat="server" Text='<%#Eval("vDstIp")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="域名性质">
                    <ItemTemplate>
                        <%# common.FormatDnsType( Convert.ToInt32( Eval("vType")))%>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="详情"></asp:TemplateField>
            </Columns>
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
            NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always" SubmitButtonText="Go"
            TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>