<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DNSList.aspx.cs" Inherits="DNSList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="inc/common.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
            <tr>
                <td style="width: 2px; cursor: e-resize; background-color: #6D9EDB">
                </td>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                        <tr id="trTop" runat="server">
                            <td bgcolor="gainsboro" height="50px">
                                <table width="840" height="45" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="350" rowspan="2">
                                            <asp:RadioButtonList ID="qrytypelist" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="4" Selected="True">当天</asp:ListItem>
                                                <asp:ListItem Value="5">昨天</asp:ListItem>
                                                <asp:ListItem Value="1">前三天</asp:ListItem>
                                                <asp:ListItem Value="2">前一周</asp:ListItem>
                                                <asp:ListItem Value="3">前一月</asp:ListItem>
                                                <asp:ListItem Value="0">选择时间</asp:ListItem>
                                      </asp:RadioButtonList></td>
                                        <td width="14">
                                            从</td>
                                        <td width="35">
                                            <igsch:WebDateChooser ID="sdate" runat="server" Width="85px" Value="2006-08-03" ClientSideEvents-OnBlur="SelectedChooseDate();">
                                                <CalendarLayout MaxDate="">                                                </CalendarLayout>
                                            </igsch:WebDateChooser>                                      </td>
                                        <td width="66" rowspan="2" align="center">
                                            <asp:RadioButtonList ID="rdlSort" runat="server" RepeatDirection="Vertical" RepeatLayout="Flow">
                                                <asp:ListItem Value="Asc">升序</asp:ListItem>
                                                <asp:ListItem Value="Desc" Selected="True">降序</asp:ListItem>
                                      </asp:RadioButtonList></td>
                                        <td width="70">
                                            <div align="right">域名类型:</div></td>
                                        <td width="127">
                                            <asp:DropDownList ID="ddlDnsType" runat="server" Width="100px">
                                                <asp:ListItem Value="-1">
                                                  所有域名
                                                </asp:ListItem>
                                                <asp:ListItem Value="0">
                                                  黑名单
                                                </asp:ListItem>
                                                <asp:ListItem Value="7">
                                                  普通黑名单
                                                </asp:ListItem>
                                                <asp:ListItem Value="8">
                                                  重要黑名单
                                                </asp:ListItem>
                                                <asp:ListItem Value="9">
                                                  紧急黑名单
                                                </asp:ListItem>
                                                <asp:ListItem Value="1">
                                                  动态域名
                                                </asp:ListItem>
                                                <asp:ListItem Value="2">
                                                  可疑域名
                                                </asp:ListItem>
                                                <asp:ListItem Value="3">
                                                  异常域名(低风险)
                                                </asp:ListItem>
                                                <asp:ListItem Value="4">
                                                 异常域名(中风险)
                                                </asp:ListItem>
                                                <asp:ListItem Value="5">
                                                  异常域名(高风险)
                                                </asp:ListItem>
                                                <asp:ListItem Value="6">
                                                  白域名
                                                </asp:ListItem>
                                      </asp:DropDownList></td>
                                        <td width="200" rowspan="2" valign="middle">
                                            <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnDelete" runat="server" Text="删除所选记录" OnClick="btnDelete_Click"
                                                OnClientClick="return confirm('是否真的要删除这些记录？')" Width="93px" />
                                           
                                            <asp:HiddenField ID="hidRow" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnParent" runat="server" Value="0" />                                      <div align="left"></div></td>
                                    </tr>
                                    <tr>
                                        <td width="14">
                                            到</td>
                                        <td>
                                            <igsch:WebDateChooser ID="edate" runat="server" Width="85px" Value="2006-08-03" ClientSideEvents-OnBlur="SelectedChooseDate();">
                                                <CalendarLayout MaxDate="">                                                </CalendarLayout>
                                            </igsch:WebDateChooser>                                        </td>
                                        <td>
                                            <div align="right">
                                                IP归属地:</div>                                        </td>
                                        <td  valign="right">
                                            <asp:DropDownList ID="ddlIpList" runat="server" Width="101px">
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
                                      </asp:DropDownList></td>
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
                        <tr id="trContent" runat="server">
                            <td>
                                <div style="width: 100%; height: 100%; overflow: scroll; border-top: solid 1 black;
                                    border-bottom: solid 1 #000000;">
                                    <asp:GridView ID="dtgData" runat="server" Width="100%" PageSize="2" OnRowDataBound="dtgData_RowDataBound"
                                        Font-Size="12px" DataKeyNames="ID"
                                        AutoGenerateColumns="False" CssClass="dd">
                                        <Columns>
                                            <asp:TemplateField Visible="false" >
                                                <ItemTemplate>
                                                    <asp:Label Width="0px" ID="lbState" runat="server" Visible="false" Text='<%#Eval("nState")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="0px"  />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                   <center><asp:CheckBox ID="cbMail" runat="server" Checked="false" /></center>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <input id="Checkbox1" type="checkbox" onClick="selectAll(this);" />
                                                </HeaderTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="15px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <center><asp:Label ID="lblID" runat="server"></asp:Label></center>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" HorizontalAlign="Center" ForeColor="Blue" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="时间">
                                                <ItemTemplate>
                                                     <center><%#Eval("dCapture")%></center>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="域名名称">
                                                <ItemTemplate>
                                                     <center><%#common.SubContent( Eval("vName").ToString(),25) %></center>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="用户IP地址">
                                                <ItemTemplate>
                                                     <center><%#Eval("vSrcAddr")%></center>
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
                                            
                                            <asp:TemplateField HeaderText="域名服务器IP">
                                                <ItemTemplate>
                                                     <center><%#Eval("vDstAddr")%></center>
                                                </ItemTemplate>                                             
                                            </asp:TemplateField>
                                            
                                            <%--<asp:TemplateField HeaderText="网关MAC地址">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("vDstMac")%>
                                                    </center>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>--%>
                                            
                                            <asp:TemplateField HeaderText="域名解析IP">
                                                <ItemTemplate>
                                                    <center>
                                                        <%#Eval("vAddr")%>
                                                    </center>
                                                </ItemTemplate>                                                
                                            </asp:TemplateField>
                                                                                                                                   
                                            <asp:TemplateField HeaderText="域名类型">
                                                <ItemTemplate>
                                                    <center> <%# common.FormatDnsType( Convert.ToInt32(Eval("vType"))) %></center>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                              
                                              <asp:TemplateField HeaderText="状态">
                                                <ItemTemplate>
                                                    <center> <%# Eval("vStateFlag").ToString() == "1" ? "<font color=#009900>静默</font>" : "活动"%> </center>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="IP归属地">
                                                <ItemTemplate>
                                                   <center> <asp:Label runat="server" ID="lbFlag" Text='<%#Eval("ipnum")%>' Visible="false"></asp:Label>
                                                     <%# common.FormatIPString(Convert.ToInt32(Eval("ipnum") != DBNull.Value ? Eval("ipnum") : -1))%></center>
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
                                        <td align="center" style="height: 19px">
                                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
                                                NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always" SubmitButtonText="Go"
                                                TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging">
                                            </webdiyer:AspNetPager>
                                        </td>
                                        <td width="15" align="center">
                                            <img id="imgBottomCursor" src="images/up.gif" border="0" class="hand" onClick="ChangeDisplay();"
                                                runat="server" /></td>
                                    </tr>
                                </table>
                                <!--翻页结束-->
                            </td>
                        </tr>
                        <tr>
                            <td id="trBottom" runat="server" height="120px">
                                <iframe runat="server" src="dnsinfo.aspx" id="frmContent" name="frmContent" scrolling="no"
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
