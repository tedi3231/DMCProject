<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="src_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>数据管理中心----用户登陆</title>
     <link rel="stylesheet" href="styles/common.css" type="text/css" />
     
     <script language="javascript" type="text/javascript">
     		// 解决在框架内超时登陆问题
     		if( parent.frames.length>0 ) 
     		{
     			parent.location.href = self.document.location;
     		}
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="margin-top:100px;">
      <table width="567" height="325" border="0" cellpadding="0" cellspacing="0" >
        <tr>
          <td height="24"><img src="images/Login_01.gif" /></td>
        </tr>
        <tr>
          <td height="160"><img src="images/Login_02.gif" /></td>
        </tr>
        <tr>
          <td height="115" background="images/Login_04.gif"><table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%">
              <tr>
                <td rowspan="3" align="right" style="width: 280px"><img src="images/Login_03.gif" /></td>
                <td valign="bottom">&nbsp; &nbsp;用户名&nbsp;
                    <asp:TextBox ID="txtUser" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px"
                                                Height="22px" Width="160px"></asp:TextBox>
                  <br />
                  &nbsp; &nbsp;密&nbsp; 码&nbsp;
                  <asp:TextBox ID="txtPassword" runat="server" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" TextMode="Password"
                                                Width="160px" Height="22px"></asp:TextBox>
                  
                  <asp:HiddenField ID="hid" runat="server" />
                  <br />
                  &nbsp;&nbsp;
                  <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Visible="False" Width="206px"></asp:Label></td>
              </tr>
              <tr>
                <td align="center">				
                  <asp:ImageButton ID="imgLogin" runat="server" ImageUrl="images/Login_06.gif" OnClick="imgLogin_Click" />
                  <asp:ImageButton ID="imgCancel" runat="server" ImageUrl="~/src/images/Login_07.gif"  OnClick="imgCancel_Click" />
                </td>
              </tr>
          </table></td>
        </tr>
        <tr>
          <td height="26"><img src="images/Login_05.gif" alt="" /></td>
        </tr>
      </table>
      
    </div>
    </form>
</body>
</html>
