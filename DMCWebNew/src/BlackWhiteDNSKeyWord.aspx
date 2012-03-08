<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlackWhiteDNSKeyWord.aspx.cs"
    Inherits="BlackWhiteDNSKeyWord" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register src="control/IssuedControl.ascx" tagname="IssuedControl" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ڰ���������</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <!-- <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />-->

    <script language="javascript" src="inc/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">  
var oWindow;   
var listen_started= false;   
function showParentWin()
{   
    var url="";//���嵯�����ڵ�URL   
    var model="";//���嵯��������ʽ   
    oWindow = window.open('AddBlackDNS.aspx','newwindow','height=460, width=500, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');   
    //ע����������ص�   
     if(!listen_started)
     {   
        setTimeout(refreshSelf,1000);   
    }
    else
    {   
        listen_started=true;    
    }   
}
 
//
function showUpdateWin(id)
{   
    var url="";//���嵯�����ڵ�URL   
    var model="";//���嵯��������ʽ   
    oWindow = window.open('UpdateBlackDNS.aspx?id='+id,'newwindow','height=260, width=500, top=100, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=n o, status=no');   
    //ע����������ص�   
     if(!listen_started)
     {   
        setTimeout(refreshSelf,1000);   
    }
    else
    {   
        listen_started=true;    
    }   
} 
//ˢ�±�ҳ��   
function refreshSelf(){   
   if (!oWindow.closed)
   {   
       setTimeout(refreshSelf,500);   
   } 
   else 
   {   
       listen_started = false;   
       var form = document.forms[0];  
       //form.action = "ˢ�±�ҳ���URL";   
       form.action="BlackWhiteDNSKeyWord.aspx";
       form.submit();   
   }   
}   
    </script>

</head>
<body onload="showByOldDisplay();" class="bg">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" class="aa">
            <tr>
                <td>
                </td>
                <td align="center" valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" background="images/bg.jpg"
                        class="bg">
                        <tr>
                            <td>
                            </td>
                            <td valign="top" align="center" style="height: 546px">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="center" valign="middle" class="title" height="40">
                                            �������� <a href='searchBlack.aspx'>����</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" height="330">
                                            <hr width="90%" />
                                            <table border="0" cellpadding="0" cellspacing="1" width="80%">
                                                <tr>
                                                    <td width="50" valign="middle">
                                                        ������</td>
                                                    <td width="100" valign="middle">
                                                        <asp:TextBox ID="tbDns" Width="100px" runat="server"></asp:TextBox></td>
                                                    <td width="50" valign="middle" style="width: 50px; text-align: right; margin-right: 5px;">
                                                        ���ͣ�</td>
                                                    <td width="150" valign="middle">
                                                        <asp:DropDownList ID="DropDownList1" runat="server" Width="150px">
                                                            <asp:ListItem Value="-1">��������</asp:ListItem>
                                                            <asp:ListItem Value="0">��ͨ������</asp:ListItem>
                                                            <asp:ListItem Value="1">��Ҫ������</asp:ListItem>
                                                            <asp:ListItem Value="2">����������</asp:ListItem>
                                                            <asp:ListItem Value="3">������</asp:ListItem>
                                                            <asp:ListItem Value="4">��̬����</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td width="50" valign="middle">
                                                        <asp:Button ID="btQuery" runat="server" Text="����" OnClick="btQuery_Click" /></td>
                                                    <td width="50" align="center" valign="middle">
                                                        <asp:Button ID="btNew" runat="server" OnClientClick="showParentWin();" Text="�½�" /></td>
                                                    <td width="50" align="center" valign="middle">
                                                        <asp:Button ID="btDel" OnClientClick="return confirm('��ȷ��Ҫɾ��ѡ�е�����');" OnClick="btDel_Click"
                                                            runat="server" Width="80px" Text="ɾ��ѡ����"></asp:Button></td>
                                                    <td width="50" valign="middle">
                                                        <asp:Button ID="btImport" runat="server" OnClientClick="window.open('importFileForBlack.aspx?type=4');"
                                                            Text="���뵼��" /></td>
                                                            <td>
                                                            
                                                                <uc1:IssuedControl ID="IssuedControl1" runat="server" DataType="6" />
                                                            
                                                            </td>
                                                    <td valign="middle">
                                                        <asp:LinkButton ID="lbClearWhite" runat="server" CommandArgument="2" OnClick="lbClearWhite_Click"
                                                            Visible="False">��հ�����</asp:LinkButton>
                                                    </td>
                                                    <td valign="middle">
                                                        <asp:LinkButton ID="lbClearBlack" runat="server" CommandArgument="0" OnClick="lbClearBlack_Click"
                                                            Visible="False">��պ�����</asp:LinkButton></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="11">
                                                    <span class="warningmsg">�������޸����ݺ��������·������������·���ǰ�ˣ������������޸���Ч</span>
                                                    </td>
                                                </tr>
                                            </table>
                                             
                                            <asp:GridView ID="GridView1" runat="server" Width="80%" AutoGenerateColumns="False"
                                                OnRowCommand="GridView1_RowCommand" DataKeyNames="nid" CssClass="tt">
                                                
                                                <Columns>                                                    
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbMail" runat="server" Checked="false" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <input name="checkbox" type="checkbox" id="Checkbox1" onclick="selectAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="����">
                                                        <ItemTemplate>
                                                            <%#Eval("vName")%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="����">
                                                        <ItemTemplate>
                                                            <%# FormatLevel(Convert.ToInt32(Eval("vLevel") ))%>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="ctenertd" Width="15%"  />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="��ע">
                                                        <ItemTemplate>
                                                            <%# common.SubContent(Eval("vContent").ToString(),25)%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="50%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="����">
                                                        <ItemTemplate>
                                                            <asp:LinkButton OnClientClick="return confirm('��ȷ��Ҫɾ��ѡ�е�����');" Visible="true" ID="btDelete"
                                                                CommandArgument='<%#Eval("nid")%>' CommandName="del" runat="server" Text="ɾ��" />
                                                            <a href="#" onclick="showUpdateWin('<%#Eval("nid")%>');">�޸�</a>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="ctenertd" Width="90px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle CssClass="tt" />
                                            </asp:GridView>
                                            <!--��ҳ��ʼ-->
                                            <table width="100%" height="25" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="center" style="height: 19px">
                                                        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" OnPageChanging="AspNetPager1_PageChanging"
                                                            ShowPageIndexBox="Always" SubmitButtonText="Go" TextAfterPageIndexBox="ҳ" TextBeforePageIndexBox="ת��"
                                                            PageSize="20" FirstPageText="��ҳ" LastPageText="βҳ" NextPageText="��һҳ" PrevPageText="��һҳ">
                                                        </webdiyer:AspNetPager>
                                                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal></td>
                                                </tr>
                                            </table>
                                            <!--��ҳ����-->
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
