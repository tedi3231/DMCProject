<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTrojan.aspx.cs" Inherits="AddTrojan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>新建木马特征码</title>
    <link href="styles/common.css" type="text/css" rel="stylesheet" />
    <link href="images/tablecloth.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width:500px;">
             <tr>
                    <th colspan="2">新建木马特征码</th>
                </tr>
                <tr>
                    <td style="width:80px;">木马名称:</td>
                    <td>
                        <asp:TextBox ID="tbName" runat="server" Width="200px"></asp:TextBox>                        
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td style="width: 80px">特征码:</td>                                  
                    <td colspan="2">
                        <asp:TextBox ID="tbKey" runat="server" Height="120px" TextMode="MultiLine" Width="490px"></asp:TextBox>
                        <br />
                        特征码:多个特征码请使用英文逗号隔开,如果是16进制请用0X开头
                        </td>                   
                </tr>
                
                 <tr>
                    <td style="width: 80px">木马描述:</td>                                  
                    <td colspan="2">
                        <asp:TextBox ID="tbContent" runat="server" Height="120px" TextMode="MultiLine" Width="490px"></asp:TextBox>                       
                        </td>                   
                </tr>
                
                <tr>
                    <td style="width: 80px">协议类型:</td>                                  
                    <td colspan="2">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Width="214px">
                            <asp:ListItem Selected="True" Value="0">所有</asp:ListItem>
                            <asp:ListItem Value="1">TCP</asp:ListItem>
                            <asp:ListItem Value="2">UDP</asp:ListItem>
                            <asp:ListItem Value="3">其他</asp:ListItem>
                        </asp:RadioButtonList>
                        </td>                   
                </tr>
                
                 <tr>
                    <td style="width: 80px">端口列表:</td>                                  
                    <td colspan="2">
                        <asp:TextBox ID="tbPort" runat="server" Height="20px" Width="490px"></asp:TextBox>
                        多个端口号请英文逗号隔开，空表示所有
                        </td>                   
                </tr>
                
                <tr>
                    <td style="width: 80px">木马方向:</td>                                  
                    <td colspan="2">
                        <asp:RadioButtonList ID="rblFlag" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">出</asp:ListItem>
                            <asp:ListItem Value="0">进</asp:ListItem>
                            <asp:ListItem Value="2" Selected="true">双向</asp:ListItem>
                        </asp:RadioButtonList>
                        </td>                   
                </tr>
                
                <tr>
                    <td>
                        <asp:Button ID="btSave" runat="server" Text="保   存" OnClick="btSave_Click" /></td>
                    <td><asp:Button ID="btExit" runat="server"  OnClientClick="this.window.close();" Text="退   出" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
