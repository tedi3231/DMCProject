<%@ Page Language="C#" AutoEventWireup="true" CodeFile="left.aspx.cs" Inherits="src_left" %>

<html>
<head id="Head1" runat="server">
<title>数据管理中心</title>
<link rel="stylesheet" href="styles/common.css" type="text/css">
<script language="javascript" src="inc/common.js"></script>
</head>

<body>

<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#DDECF9">
  <tr>
    <td align="left" valign="top">
		<div style="width:100%; height:100%; overflow:auto" class="scrollbar">
            <form id="form1" runat="server">
                <asp:TreeView ID="tvwMenu" SelectedNodeStyle-BackColor="Navy" SelectedNodeStyle-ForeColor="white" ShowLines="true" runat="server" Height="100%" Width="148px" MaxDataBindDepth="3" OnTreeNodePopulate="tvwMenu_TreeNodePopulate">
                    <Nodes>
                        <asp:TreeNode Value="tndSite" Text="控制中心" PopulateOnDemand="True" ImageUrl="~/src/images/control2.gif" SelectAction="Expand">
                        </asp:TreeNode>
                        <asp:TreeNode Value="tndMIU" Text="重点怀疑对象" PopulateOnDemand="True" ImageUrl="~/src/images/icontrol.gif" SelectAction="Expand">
                        </asp:TreeNode>
                        <asp:TreeNode Value="tndApp" Text="数据管理中心" PopulateOnDemand="false" ImageUrl="~/src/images/app-1.png" SelectAction="Expand" NavigateUrl="~/src/AppList.aspx" Target="_blank">
                        </asp:TreeNode>
                    </Nodes>
                    <SelectedNodeStyle BackColor="Gray" ForeColor="White" />
                </asp:TreeView>
                <asp:Label ID="labelStatus" runat="server"></asp:Label>
            </form>
		</div>
	</td>
  </tr>
</table>
</body>
</html>


