<%@ Page Language="C#" AutoEventWireup="true" CodeFile="searchBlack.aspx.cs" Inherits="src_searchBlack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>黑白名单</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
</head>
<body style="text-align: center" class="bg" background="images/bg.jpg">
    <form id="form1" runat="server">
    <div>
        
       
        <table width="545" height="282" border="0" cellpadding="0" cellspacing="0" style="margin-top: 80px;">
            <tr>
                <td colspan="4"><h2>黑白名单管理</h2></td>
            </tr>
            <tr><td colspan="4">
            <hr />
            </td></tr>
            <tr>
                <td colspan="4">&nbsp;</td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <a href="KeyUser.aspx">
                        <img src="../src/images/b_1.png" alt="敏感关键字管理" />
                    </a>
                </td>
                <td align="center" valign="middle">
                    <a href="BlackWhiteIPKeyWord.aspx">
                        <img src="../src/images/b_2.png" alt="黑IP管理" /></a>
                </td>
                <td align="center" valign="middle">
                    <%--<a href="BlackWhiteMacKeyWord.aspx">
                        <img src="../src/images/b_3.png" alt="黑白MAC管理" /></a>--%>
                         <a href="BlackHttpKeyWord.aspx"><img src="../src/images/b_3.png" alt="HTTP黑名单管理" /></a>
                        <%--<img src="../src/images/b_3.png" alt="黑白MAC管理" /></a>--%>
                </td>
                <td align="center" valign="middle">
                    <a href="BlackWhiteEmailKeyWord.aspx">
                        <img src="../src/images/b_4.png" alt="黑邮箱管理" /></a>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    <a href="GarbageMail.aspx?type=1">
                        <img src="../src/images/b_8.png" alt="垃圾邮件关键字" />
                    </a>
                </td>
                <td align="center" valign="middle">
                    <a href="TrojanKeyWordList.aspx">
                        <img src="../src/images/b_5.png" alt="木马管理" />
                    </a>
                </td>
                <td align="center" valign="middle">
                    <a href="BlackWhiteDNSKeyWord.aspx">
                        <img src="../src/images/b_6.png" alt="黑白域名管理" />
                    </a>
                </td>
                <td align="center" valign="middle">
                  <div style="display:none;">  <a href="BlackWhiteDNSKeyWord.aspx?type=4">
                        <img src="../src/images/b_7.png" alt="动态域名管理" />
                    </a>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
