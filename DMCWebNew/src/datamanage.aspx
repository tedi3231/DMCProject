<%@ Page Language="C#" AutoEventWireup="true" CodeFile="datamanage.aspx.cs" Inherits="src_datamanage" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics.WebUI.WebDateChooser.v5.3, Version=5.3.20053.50, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<html>
<head id="Head1" runat="server">
    <title>数据维护</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
    <script language="javascript">
    function DoChangeCheck()
    {
        var e = window.event.srcElement;
        var subid = e.id.substring(4);
        if (e.checked)
        {
            document.getElementById("txt_" + subid).value = 1;
            document.getElementById("ddl_" + subid).selectedIndex = 2;
        }
        else
        {
            document.getElementById("txt_" + subid).value = "";
            document.getElementById("ddl_" + subid).selectedIndex = 0;
        }
    }
    function chkValid()
    {
        var aObjects = document.body.all.tags("INPUT");
        var subid;
        var ddl;
        var txt;
        for (i = 0; i < aObjects.length; i++)
        {
            if (aObjects[i].id.substring(0,3) == "chk")
            {
                if (aObjects[i].checked)
                {
                    subid = aObjects[i].id.substring(4);
                    ddl = document.getElementById("ddl_" + subid);
                    txt = document.getElementById("txt_" + subid);
                    if (ddl.selectedIndex == 0)
                    {
                        alert("请设置数据自动覆盖周期！");
                        ddl.focus();
                        return false;
                    }
                    if (txt.value == "")
                    {
                        alert("请输入数据自动覆盖的周期数！");
                        txt.focus();
                        return false;
                    }
                    else
                    {
                        if (!isInteger(txt.value))
                        {
                            alert("请输入一个正整数！");
                            txt.focus();
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    </script>
</head>
<body topmargin=0 leftmargin=0>
    <form id="frmSetting" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" background="images/bg.jpg" class="bg">
    <tr>
        <td valign="top">
            <table width="100%" cellpadding="0" cellspacing="0">
            <tr><td class="title" height="40" align="center">数据维护</td></tr>
            <tr><td align="center">
                <table cellspacing="0" cellpadding="5">
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                Http网站内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="HttpStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="HttpEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnHttpDelete" runat="server" OnClick="btnHttpDelete_Click" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" />
                                <asp:Button ID="btnHttpExport" runat="server" OnClick="btnHttpExport_Click" Text="导出" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_Http" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_Http" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_Http" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--Http网站-->
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                MSN聊天记录：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="MsnStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="MsnEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnMSNDelete" runat="server" OnClick="btnMSNDelete_Click" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" />
                                <asp:Button ID="btnMSNExport" runat="server" OnClick="btnMSNExport_Click" Text="导出" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_Msn" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_Msn" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_Msn" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--Msn聊天-->
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                发送邮件内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="SmtpMailStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="SmtpMailEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnSmtpDelete" runat="server" OnClick="btnSmtpDelete_Click" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" />
                                <asp:Button ID="btnSmtpExport" runat="server" OnClick="btnSmtpExport_Click" Text="导出" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_SmtpMail" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_SmtpMail" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_SmtpMail" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--发送邮件-->
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                接收邮件内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="PopMailStartDate" runat="server" Width="140px" Value="2006-04-22">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="PopMailEndDate" runat="server" Width="140px" Value="2006-04-22">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnPopDelete" runat="server" OnClick="btnPopDelete_Click" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" />
                                <asp:Button ID="btnPopExport" runat="server" OnClick="btnPopExport_Click" Text="导出" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_PopMail" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_PopMail" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_PopMail" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--接收邮件-->
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                Ftp帐号内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="FtpStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="FtpEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnFtpDelete" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" OnClick="btnFtpDelete_Click" />
                                <asp:Button ID="btnFtpExport" runat="server" Text="导出" OnClick="btnFtpExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_Ftp" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_Ftp" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_Ftp" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--Ftp帐号-->
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                Web帐号内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="WebStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="WebEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnWebDelete" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" OnClick="btnWebDelete_Click" />
                                <asp:Button ID="btnWebExport" runat="server" Text="导出" OnClick="btnWebExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_Web" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_Web" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_Web" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--Web帐号-->
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                发送Web邮件内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="SendWebMailStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="SendWebMailEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnSendDelete" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" OnClick="btnSendDelete_Click" />
                                <asp:Button ID="btnSendExport" runat="server" Text="导出" OnClick="btnSendExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_SendWebMail" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_SendWebMail" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_SendWebMail" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--发送Web邮件-->
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                接收Web邮件内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="GetWebMailStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="GetWebMailEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnGetDelete" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" OnClick="btnGetDelete_Click" />
                                <asp:Button ID="btnGetExport" runat="server" Text="导出" OnClick="btnGetExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_GetWebMail" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_GetWebMail" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_GetWebMail" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--接收Web邮件-->
                    <tr><td>
                    <table border="0"  style="display:none" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                BBS帐号内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="BBSStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="BBSEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnBBSDelete" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" OnClick="btnBBSDelete_Click" />
                                <asp:Button ID="btnBBSExport" runat="server" Text="导出" OnClick="btnBBSExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_BBS" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_BBS" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_BBS" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--BBS帐号-->
                   
                    <tr><td>
                    <table   style="display:none" border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                QQ帐号内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="QQStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="QQEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnQQDelete" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" OnClick="btnQQDelete_Click" />
                                <asp:Button ID="btnQQExport" runat="server" Text="导出" OnClick="btnQQExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_QQ" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_QQ" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_QQ" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--QQ账号-->
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                Yahoo内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="YahooStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="YahooEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnYahooDelete" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" OnClick="btnYahooDelete_Click" />
                                <asp:Button ID="btnYahooExport" runat="server" Text="导出" OnClick="btnYahooExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_Yahoo" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_Yahoo" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_Yahoo" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--雅虎聊天-->
                    <tr><td>
                    <table  style="display:none" border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                突破封锁内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="UnlimitStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="UnlimitEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btnLimitDelete" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" OnClick="btnLimitDelete_Click" UseSubmitBehavior="False" />
                                <asp:Button ID="btnLimitExport" runat="server" Text="导出" OnClick="btnLimitExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox Checked="true" onclick="DoChangeCheck();" ID="chk_TC_Unlimit" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_Unlimit" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_TC_Unlimit" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--突破封锁-->
                    
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                DNS内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="dnsStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="dnsEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btDnsDel" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" UseSubmitBehavior="False" OnClick="btDnsDel_Click" />
                                <asp:Button ID="btDnsExport" runat="server" Text="导出" OnClick="btDnsExport_Click" UseSubmitBehavior="False" />
                                <asp:CheckBox onclick="DoChangeCheck();" ID="chk_TC_dns" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_dns" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_tc_dns" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--DNS内容-->
                    
                    <tr><td>
                    <table border="0" bgcolor="#efefef" cellpadding="5" cellspacing="0">
                        <tr>
                            <td width="110px">
                                木马内容：</td>
                            <td>从</td>
                            <td>
                                <igsch:webdatechooser id="horseStartDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
					                <CalendarLayout MaxDate=""></CalendarLayout>
						        </igsch:webdatechooser>
	                        </td>
                            <td>到</td>
                            <td>
                                <igsch:webdatechooser id="horseEndDate" runat="server" Width="140px" Value="2006-01-01" ClientSideEvents-OnBlur="SelectedChooseDate();">
						            <CalendarLayout MaxDate=""></CalendarLayout>
					            </igsch:webdatechooser>
                            </td>
                            <td>
                                <asp:Button ID="btHorseDel" runat="server" OnClientClick="return confirm('是否真的要删除这些内容？')" Text="删除" UseSubmitBehavior="False" OnClick="btHorseDel_Click" />
                                <asp:Button ID="btHorseExport" runat="server" Text="导出"  UseSubmitBehavior="False" OnClick="btHorseExport_Click" />
                                <asp:CheckBox onclick="DoChangeCheck();" ID="chk_TC_horse" runat="server" />自动覆盖<asp:TextBox ID="txt_TC_horse" runat="server" CssClass="txtNumber"  Text="1"></asp:TextBox><asp:DropDownList
                                    ID="ddl_tc_horse" runat="server">
                                    <asp:ListItem Value="">-</asp:ListItem>
                                    <asp:ListItem Value="d">日</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="m">月</asp:ListItem>
                                    <asp:ListItem Value="y">年</asp:ListItem>
                                </asp:DropDownList>前数据
                            </td>
                        </tr>
                    </table>
                    </td></tr> <!--木马内容-->
                    
                    <tr>
                        <td align = "center"><asp:Button ID="btnSave" runat="server" Text="保存设置" OnClientClick = "return chkValid()" OnClick="btnSave_Click" /></td>
                    </tr>
                </table>
            </td></tr>
            </table>
        </td>
        </tr>
    </table>
    </form>
</body>
</html>



