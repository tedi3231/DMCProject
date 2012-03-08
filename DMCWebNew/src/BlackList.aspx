<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlackList.aspx.cs" Inherits="src_BlackList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>数据管理中心</title>
    <LINK href="styles/common.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="inc/common.js" type="text/javascript"></script>
    <script language="javascript">
        //验证输入内容的有效性
        function DoCheck()
        {
            if (event.srcElement.parentElement.parentElement.children(1).children(0).selectedIndex < 1)
            {
                alert("请选择关键字类别！");
                return false;
            }
            if (event.srcElement.parentElement.parentElement.children(2).children(0).value == "")
            {
                alert("请输入关键字！");
                event.srcElement.parentElement.parentElement.children(2).children(0).focus();
                return false;
            }
            return true;
        }
        
        function openWindow(sFile)
        {
        winSet = window.open(sFile,"winSet","width=700,height=500,scrollbars=no,location=no,menubar=no,resizable=no,status=no");
        }
       function closeWindow()
       {
           if(winSet) winSet.close();
       }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    </form>
</body>
</html>
