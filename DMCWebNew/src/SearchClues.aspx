<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchClues.aspx.cs" Inherits="src_SearchClues" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>关联分析</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />

    <script src="inc/jquery-1.4.2.js" type="text/javascript"></script>

    <script language="javascript" src="inc/common.js" type="text/javascript"></script>

    <style type="text/css">
        .col
        {
            font: bold 11px "Trebuchet MS" , Verdana, Arial, Helvetica, sans-serif;
            color: #4f6b72;
            border-right: 1px solid #C1DAD7;
            border-bottom: 1px solid #C1DAD7;
            border-top: 1px solid #C1DAD7;
            letter-spacing: 2px;
            text-transform: uppercase;
            text-align: left;
            padding: 6px 6px 6px 12px;
            background: #CAE8EA no-repeat;
        }
        
#mytable {
width: 100%;
padding-left:5px;
padding-right:5px;
margin:0;
}

.row {
border-right: 1px solid #C1DAD7;
border-bottom: 1px solid #C1DAD7;
background: #fff;
font-size:12px;
padding: 6px 6px 6px 12px;
color: #4f6b72;
}

.cap {
padding: 5px 0 5px 0;
font: BILD  15px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
text-align: left;
 background: #CAE8EA no-repeat;
}

        .style1
        {
            width: 47px;
        }
        .style2
        {
            width: 81px;
        }

        .style3
        {
            width: 409px;
        }

    </style>

    <script language="javascript" type="text/javascript">
        $(document).ready(function(){
        
            $('#CheckBoxList1 :checkbox').attr('checked','checked');      
        
            $('#chkHost_11').change(function(){
            
                var temp = $(this).attr('checked');
                         
                if( temp )
                {
                     $('#CheckBoxList1 :checkbox').attr('checked','checked');                     
                }
                else
                {
                     $('#CheckBoxList1 :checkbox').attr('checked','');                  
                }                          
            });
            
            $('#btnSearch').click(function(){
                var text = $('#tbIp').val();
                //alert(text);
                if( text=='' || text==null )
                {
                    alert('IP不能为空');
                    return false;
                }
                return true;
            });
        });
    </script>

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
                                    <td align="center" bgcolor="#CEE9F2" class="style3">
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
                                                       <%-- <asp:ListItem Value="4" Selected="True">当天</asp:ListItem>--%>
                                                        <asp:ListItem Value="5" Selected="True">昨天</asp:ListItem>
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
                                    <td valign="top" bgcolor="#CEE9F2" align="center" class="style3">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="style2">
                                                    服 务 器：</td>
                                                <td valign="middle" rowspan="2">
                                                    <div style="width: 100%; height: 57px; overflow: auto; border: inset 2 #c0c0c0; background-color: White;margin: 3px 0 2px 1;">
                                                        <asp:CheckBoxList ID="cblHost" runat="server" DataTextField="vCorpName" DataValueField="nId"
                                                            RepeatColumns="4" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <input type="checkbox" id="chkHost" onclick="SelectAllHost();" />全选</td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: left" class="style2">
                                                    选择范围：
                                                </td>
                                                <td rowspan="2">
                                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                                                        RepeatDirection="Horizontal" RepeatColumns="4">
                                                        <asp:ListItem Value="1">HTTP网站</asp:ListItem>
                                                        <asp:ListItem Value="2">MSN聊天</asp:ListItem>
                                                        <asp:ListItem Value="3">雅虎聊天</asp:ListItem>
                                                        <asp:ListItem Value="4">邮件分析</asp:ListItem>
                                                        <asp:ListItem Value="5">WEB账号</asp:ListItem>
                                                        <asp:ListItem Value="6">WEB邮件</asp:ListItem>
                                                        <asp:ListItem Value="7">实时域名</asp:ListItem>
                                                        <asp:ListItem Value="8">木马分析</asp:ListItem>
                                                        <asp:ListItem Value="9">FTP分析</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    <input type="checkbox" id="chkHost_11"" checked="checked" />全选&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <span style="color: Red;">关联分析比较耗资源，选择的时间段越长，消耗的时间越长，请耐心等候！</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="340" valign="middle" bgcolor="#CEE9F2" align="center">
                                        <table border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                                <td class="style1">
                                                    <div align="right">
                                                        IP：</div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbIp" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="display: none;">
                                                <td class="style1">
                                                    <div align="right">
                                                        &nbsp;返回数量：</div>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="tbCount" runat="server" Text="100" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" colspan="2" style="padding-left:10px;">
                                                    <asp:Button ID="btnSearch" runat="server" Text="分 析" Width="63px" 
                                                        OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <table width="100%" id="mytable">
                                <caption class="cap">
                                    统计信息
                                </caption>
                                <tr>
                                    <td width="100px" class="col">
                                        HTTP网站
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltHttp" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100px"  class="col">
                                        MSN聊天
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltMsn" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="col">
                                        雅虎聊天
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltYahoo" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="col">
                                        POP3邮件
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltPop" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="col">
                                        SMTP邮件
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltSmtp" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td  class="col">
                                        WEB账号
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltWeb" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="col">
                                        WEB发送邮件
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltSend" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="col">
                                        WEB接收邮件
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltRev" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td  class="col">
                                        实时域名
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltDns" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="col">
                                        木马分析
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltTrojan" runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td  class="col">
                                        FTP分析
                                    </td>
                                    <td class="row">
                                        <asp:Literal ID="ltFtp" runat="server"></asp:Literal>
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
