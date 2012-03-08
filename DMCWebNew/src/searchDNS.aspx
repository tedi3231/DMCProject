<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchDNS.aspx.cs" Inherits="src_searchDNS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>域名查询</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet">

    <script language="javascript" src="inc/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
       var winQry;       
       function openWindow(sFile)
        {
            winQry = window.open(sFile,"winQry","width=" + (screen.availWidth-8) + ",height=" + (screen.availHeight-25) + ",top=0,left=0,scrollbars=no,location=no,menubar=no,resizable=no,status=no");
        }     
    </script>

</head>
<body style="text-align: center" class="bg" background="images/bg.jpg">
    <form id="form1" runat="server">
    <div>
        <table width="596" height="395" border="0" cellpadding="0" cellspacing="0" style="margin-top: 80px;">
            <tr>
                <td width="120" align="center" valign="middle">
                    <a href="#" onclick="openWindow('searchDNS2.aspx');">
                        <img src="../src/images/dns_1.png" alt="实时域名查看" width="109" height="100" /></a>
                </td>               
                <td width="120" align="center" valign="middle">
                    <a href="#" onclick="openWindow('DynamicRank.aspx')">
                        <img src="../src/images/dns_4.png" alt="动态域名排名" width="109" height="100" /></a>
                </td>
                <td width="120" align="center" valign="middle">
                    <a href="#" onclick="openWindow('DynamicRank.aspx?type=2')">
                        <img src="../src/images/dns_5.png" alt="动态域名排名(内网IP)" /></a>
                </td>
                <td width="120" align="center" valign="middle">
                    <a href="#" onclick="openWindow('changeDNSDnsList.aspx')">
                        <img src="../src/images/dns_6.png" alt="多域名对同一IP" width="109" height="100" /></a>
                </td>
            </tr>
            <tr>
                <%--onclick="openWindow('searchDNS2.aspx?type=2')"--%>
                <td width="120" align="center" valign="middle">
                    <a href="#">
                        <img src="../src/images/dns_7.png" alt="区域变化域名" width="109" height="100" /></a><a
                            href="#" onclick="openWindow('searchDNS2.aspx?type=3')"></a>
                </td>
                <td width="120" align="center" valign="middle">
                    <a href="#" onclick="openWindow('changeIpDnsList.aspx')">
                        <img src="../src/images/dns_8.png" alt="IP变化频繁域名" width="109" height="100" /></a>
                </td>
                <td width="120" align="center" valign="middle">
                </td>
                <td width="120" align="center" valign="middle">
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
