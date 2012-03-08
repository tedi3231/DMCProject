<%@ Page Language="C#" AutoEventWireup="true" CodeFile="top.aspx.cs" Inherits="src_top" %>

<html>
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
       var winQry;
       function checkouttime()
       {
           var iCount = form_refresh.all("timeCount").value;
           iCount=parseInt(iCount); 
           if (iCount < <%=TimeOutCount%>)
           {
             form_refresh.all("timeCount").value = iCount + 1;
             setTimeout(checkouttime,1000);
           }
           else
           {
             form_refresh.submit();
           }
       }
       function openWindow(sFile)
        {
        winQry = window.open(sFile,"winQry","width=" + (screen.availWidth-8) + ",height=" + (screen.availHeight-25) + ",top=0,left=0,scrollbars=no,location=no,menubar=no,resizable=no,status=no");
        }
       function closeWindow()
       {
           if(winQry) winQry.close();
       }
    </script>
</head>
<body topmargin="0" leftmargin="0" onbeforeunload="closeWindow();">
    <form id="form1" runat="server">
    <table width="100%" height="59" border="0" cellpadding="0" cellspacing="0" background="images/topback_2.gif">
        <tr>
            <td width="447" align="center" background="images/topback_1.gif" valign="top" style="background-repeat: no-repeat">
                <table width="350" border="0" cellpadding="0" cellspacing="0" style="padding-top: 5">
                    <tr>
                        <td align="center">
                            <a href="ContentSiteList.aspx" target="mainFrame">
                                <img src="images/btn_1.gif" width="58" height="44" alt="新建节点" border="0" /></a>
                        </td>
                        <td align="center">
                            <a onclick="openWindow('searchHttp.aspx');" style="cursor: hand">
                                <img src="images/btn_2.gif" alt="网站查询" border="0" /></a>
                        </td>
                        <td align="center">
                            <a onclick="openWindow('searchMsg.aspx');" style="cursor: hand">
                                <img src="images/btn_3.gif" alt="聊天查询" border="0" /></a>
                        </td>
                        <td align="center">
                            <a onclick="openWindow('searchMail.aspx');" style="cursor: hand">
                                <img src="images/btn_4.gif" alt="邮件查询" border="0" /></a>
                        </td>
                        <td align="center">
                            <a href="searchDNS.aspx" target="mainFrame" style="cursor: hand">
                                <img src="images/btn_13.gif" alt="域名查询" border="0" /></a>
                        </td>
                        <td align="center">
                            <a onclick="openWindow('searchHorse.aspx');" style="cursor: hand">
                                <img src="images/btn_14.gif" alt="木马查询" border="0" /></a>
                        </td>
                        <%--<td align="center">
                            <a onclick="openWindow('SearchSensitive.aspx')" target="mainFrame">
                                <img src="images/btn_5.gif" width="72" height="44" alt="敏感数据库" border="0"></a>
                        </td>--%>
                        <%--<td align="center"><a href="GarbageMail.aspx" target="mainFrame"><img src="images/btn_6.gif" border="0" alt="垃圾邮件"></a></td>--%>
                        <!-- 敏感数据库和垃圾邮件合成下面的一项 黑白名单 -->
                        <td align="center">
                            <a onclick="openWindow('searchinfo.aspx');" style="cursor: hand">
                                <img src="images/btn_12.gif" alt="其他查询" border="0" /></a>
                        </td>
                        <td align="center">
                            <a href="searchBlack.aspx" target="mainFrame">
                                <img src="images/btn_15.gif" alt="黑白名单" border="0" /></a>
                        </td>
                        <td align="center">
                            <a onclick="openWindow('searchClues.aspx');" target="mainFrame">
                                <img src="images/btn_16.gif" alt="线索分析" border="0" />
                            </a>
                        </td>
                        <td align="center">
                            <a href="MacIpUser.aspx" target="mainFrame">
                                <img src="images/btn_7.gif" border="0" alt="重点怀疑对象" /></a>
                        </td>
                        <td align="center" id="tdUser" runat="server">
                            <a onclick="openWindow('UpdateClient.aspx');;">
                                <img src="images/btn_11.gif" alt="其他作用，暂时不显示" border="0" /></a>
                        </td>
                        <td align="center">
                            <a onclick="openWindow('UpdateClient.aspx');" style="cursor: hand">
                                <img src="images/btn_17.gif" border="0" alt="更新程序" /></a>
                        </td>
                        <td align="center" id="tdAdmin" runat="server">
                            <a onclick="openWindow('datamanage.aspx');" style="cursor: hand">
                                <img alt="数据维护" src="images/btn_11.gif" border="0" /></a>
                        </td>
                        <td align="center">
                            <a onclick="openWindow('ManageUser.aspx');" style="cursor: hand">
                                <img src="images/btn_9.gif" border="0" alt="用户管理" /></a>
                        </td>
                        <td align="center">
                            <asp:ImageButton ID="imbLogout" runat="server" BorderWidth="0px" ImageUrl="images/btn_10.gif"
                                OnClick="imbLogout_Click" OnClientClick="return (confirm('您确认退出吗？'));" />
                        </td>
                    </tr>
                </table>
                <input id="topDisplay" type="hidden" />
                <input id="bottomDisplay" type="hidden" />
            </td>
        </tr>
    </table>
    </form>
    <form id="form_refresh" action="changeuserstate.aspx">
    <input id="timeCount" type="hidden" value="0" />
    </form>
</body>
</html>
