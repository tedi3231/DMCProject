<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailSiteInfo.aspx.cs" Inherits="src_MailSiteInfo" %>

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="inc/common.js"></script>

    <script language="javascript">
    var EmailPath;
    function DoOnload()
    {
        EmailPath = "<%=getServerFile()%>";
        if (EmailPath != "")
        {
            document.all("showEmail").style.cursor = "hand";
            document.all("showEmail").style.visibility = "visible";
        }
        else
        {
            document.all("txtmapadd").style.width = "220";
        }
    }
    function OpenFile()
    {
    var WshSell;
        if (EmailPath != "")
        {
        //alert(EmailPath);
        //WshSell = new ActiveXObject("Wscript.Shell");
        //WshSell.Run(EmailPath,3,false);
        window.open(EmailPath,"_blank");
        }
    }
    </script>

</head>
<body onload="DoOnload();">
    <form id="frmMailSiteInfo" runat="server" target="frmEmail">
        <table border="0" width="100%" height="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" style="width: 357px">
                    <div style="width: 349px; height: 100%; overflow: auto;">
                        <table cellpadding="0" cellspacing="0" border="0" align="center">
                            <tr>
                                <td width="90">
                                    发件人：</td>
                                <td width="220">
                                    <asp:TextBox ID="txtLpFrom" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    时间：</td>
                                <td>
                                    <asp:TextBox ID="txtCapture" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    收件人：</td>
                                <td>
                                    <asp:TextBox ID="txtLpTo" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                                        BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    抄送：</td>
                                <td>
                                    <asp:TextBox ID="txtLpCc" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                                        BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    暗送：</td>
                                <td>
                                    <asp:TextBox ID="tbBcc" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                                        BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    用户名：</td>
                                <td>
                                    <asp:TextBox ID="txtLogin" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                                        BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    密码：</td>
                                <td>
                                    <asp:TextBox ID="txtPwd" runat="server" ReadOnly="true" BorderColor="#A9A9A9" BackColor="#efefef"
                                        BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    邮件物理地址：</td>
                                <td>
                                    <asp:TextBox ID="txtmapadd" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="200"></asp:TextBox><img
                                            id="showEmail" align="absmiddle" src="images/list1.gif" border="0" onclick="OpenFile();"
                                            style="visibility: hidden" /></td>
                            </tr>
                            <tr>
                                <td>
                                    邮件附件地址：</td>
                                <td>
                                    <asp:TextBox ID="txtWebFile" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox><asp:ImageButton
                                            ID="imbWebFile" ImageAlign="AbsMiddle" ImageUrl="~/src/images/list6.gif" runat="server"
                                            PostBackUrl="~/src/ListAttachFile.aspx" Visible="false" /></td>
                            </tr>
                            <tr>
                                <td>
                                    用户网卡地址：</td>
                                <td>
                                    <asp:TextBox ID="txtSrcMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    目的网卡地址：</td>
                                <td>
                                    <asp:TextBox ID="txtDstMac" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="220"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    用户IP地址：</td>
                                <td>
                                    <asp:TextBox ID="txtSrcAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150px"></asp:TextBox>
                                    <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="归属地" Width="65px" /></td>
                            </tr>
                            <tr>
                                <td style="height: 22px">
                                    目的IP地址：</td>
                                <td style="height: 22px">
                                    <asp:TextBox ID="txtDstAddr" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="150px"></asp:TextBox>
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="归属地" Width="65px" /></td>
                            </tr>
                            <tr>
                                <td>
                                    主题：</td>
                                <td>
                                    <asp:TextBox ID="txtLpTitle" runat="server" ReadOnly="true" BorderColor="#A9A9A9"
                                        BackColor="#efefef" BorderStyle="Solid" BorderWidth="1" Width="220" TextMode="MultiLine"
                                        Rows="5"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    IP地址归属地：</td>
                                <td>
                                    <%--<asp:Literal ID="ltArea" runat="server"></asp:Literal>--%>
                                    <asp:TextBox BorderColor="#A9A9A9" BackColor="#efefef" BorderStyle="Solid" BorderWidth="1"
                                        runat="server" ID="ltArea" Width="220" TextMode="multiLine" Height="70px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                    <iframe id="frmEmail" src="ShowXMLANDTXT.aspx?file=<%=emlFile2%>" name="frmEmail" frameborder="0" scrolling="auto"
                        width="100%" height="100%" style="border-left: solid 1 #c0c0c0; scrollbar-highlight-color: white;">
                    </iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
