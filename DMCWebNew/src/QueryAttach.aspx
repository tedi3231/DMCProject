<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryAttach.aspx.cs" Inherits="src_QueryAttach" %>
<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet">
    <script language="javascript" src="inc/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr>
                <td style="width: 100px">
                    查询编录</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtIndex" runat="server" Text="<%$ AppSettings:FileIndexName %>"></asp:TextBox></td>
                <td style="width: 100px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 100px">
                    查询词条</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtQueryText" runat="server"></asp:TextBox></td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
        </table>
    <asp:Button ID="btnQuery" runat="server" OnClick="btnQuery_Click" Text="查询" /><br />
    </div>
        
        <asp:GridView ID="gdvFiles" runat="server" AutoGenerateColumns="False" Width="100%" DataKeyNames="Path" OnRowCommand="gdvFiles_RowCommand1">
            <Columns>
                <asp:BoundField DataField="RANK" HeaderText="匹配度" />
                <asp:BoundField DataField="FILENAME" HeaderText="文件名" />                
                <asp:BoundField DataField="SIZE" HeaderText="大小(byte)" />
                <asp:BoundField DataField="WRITE" HeaderText="最后更新时间" />
                <asp:BoundField DataField="HITCOUNT" HeaderText="匹配字数" />
                <asp:BoundField DataField="DocKeywords" HeaderText="路径" />
                <asp:ButtonField HeaderText="" Text="下载" CommandName="download" />
            </Columns>
        </asp:GridView> 
    </form>
</body>
</html>
