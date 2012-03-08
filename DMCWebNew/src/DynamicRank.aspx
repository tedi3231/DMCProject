<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DynamicRank.aspx.cs" Inherits="src_DynamicRank" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>动态域名排名</title>
     <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />
   <script language="javascript" src="inc/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="95%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <th colspan="8">
                    <div align="left">
                        <asp:Literal ID="ltTitle" runat="server"></asp:Literal>
                        <asp:HiddenField ID="hdType" runat="server" />
                    </div>                </th>
					
            </tr>
        </table>
        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" HorizontalAlign="center" >
            <Columns>
                <asp:TemplateField HeaderText="域名">
                    <ItemTemplate>
                       <center><%#Eval("vDstIp")%></center>
                    </ItemTemplate>
                    <ItemStyle CssClass="ctenertd" Width="30%" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DNS个数">
                    <ItemTemplate>
                       <center> <%#Eval("orderCount")%></center>
                    </ItemTemplate>
                     <ItemStyle  CssClass="ctenertd" Width="30%" />
                </asp:TemplateField>
                      <asp:TemplateField HeaderText="排名数">
                    <ItemTemplate>
                       <center> <%#Eval("rank")%></center>
                    </ItemTemplate>
                     <ItemStyle CssClass="ctenertd" Width="30%" />
                </asp:TemplateField>
                        
            </Columns>
        </asp:GridView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
            NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always" SubmitButtonText="Go"
            TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到" OnPageChanging="AspNetPager1_PageChanging">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
