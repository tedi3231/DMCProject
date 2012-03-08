<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="WebCalendar" Namespace="Titan.WebForm" TagPrefix="TW" %>

<%@ Register Assembly="Infragistics.WebUI.WebDateChooser.v5.3" Namespace="Infragistics.WebUI.WebSchedule"
    TagPrefix="igsch" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <igsch:WebCalendar ID="WebCalendar1" runat="server">
        </igsch:WebCalendar>
    
    </div>
        <igsch:WebDateChooser ID="WebDateChooser1" runat="server">
        </igsch:WebDateChooser>
        <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black"
            BorderStyle="Solid" CellSpacing="1" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black"
            Height="250px" NextPrevFormat="ShortMonth" Width="330px">
            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
            <TodayDayStyle BackColor="#999999" ForeColor="White" />
            <DayStyle BackColor="#CCCCCC" />
            <OtherMonthDayStyle ForeColor="#999999" />
            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
            <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt"
                ForeColor="White" Height="12pt" />
        </asp:Calendar>
        &nbsp;
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
            NextPageText="下一页" PrevPageText="上一页" ShowPageIndexBox="Always" SubmitButtonText="Go"
            TextAfterPageIndexBox="页" TextBeforePageIndexBox="转到">
        </webdiyer:AspNetPager>
    </form>
</body>
</html>
