<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="src_Logon" %>

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <link rel="stylesheet" href="styles/common.css" type="text/css" />

    <script language="javascript" type="text/javascript" src="inc/md5.js" />

</head>
<body>
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" valign="middle">
                <form id="frmLogin" runat="server">
                    <table width="567" height="331" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="24">
                                <img src="images/Login_01.gif" /></td>
                        </tr>
                        <tr>
                            <td height="160">
                                <img src="images/Login_02.gif" /></td>
                        </tr>
                        <tr>
                            <td background="images/Login_04.gif">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
                                    <tr>
                                        <td rowspan="3" align="right" style="width: 280px">
                                            <img src="images/Login_03.gif" /></td>
                                        <td valign="bottom">
                                            &nbsp; &nbsp;用户名&nbsp;
                                            <asp:TextBox ID="txtUser" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                                                Height="22px" Width="160px"></asp:TextBox><br>
                                            &nbsp; &nbsp;密&nbsp; 码&nbsp;
                                            <asp:TextBox ID="txtPassword" runat="server" BorderWidth="1px" TextMode="Password"
                                                Width="160px"></asp:TextBox>&nbsp;
                                            <asp:HiddenField ID="hid" runat="server" />
                                            <br>
                                            &nbsp;&nbsp;
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False" Width="206px"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />                                            
                                            &nbsp;
                                            <asp:ImageButton ID="imgLogin" runat="server" ImageUrl="images/Login_06.gif" OnClick="imgLogin_Click" />
                                            <asp:ImageButton ID="imgCancel" runat="server" ImageUrl="~/src/images/Login_07.gif"  OnClick="imgCancel_Click" />                                      </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="26">
                                <img src="images/Login_05.gif" /></td>
                        </tr>
                    </table>
                </form>
          </td>
        </tr>
    </table>
</body>
</html>
