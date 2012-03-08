// JScript 文件
var curObject="";
var curValue="";

function isInteger(val_num)//判断是否为正整数
{
    if (val_num.match("^[1-9]\d*$") == null)
        return false;
    else
        return true;
} 

function ShowDetail(FileName,TableType,RecordID)
{
    var sel;
    sel = window.event.srcElement;
    if (sel.tagName != "INPUT")
    {
        if (sel.tagName != "TD")
            sel=sel.parentElement;
        //清除旧的选择标记
        if (curObject != "")
            document.all(curObject).innerText = curValue;
        //保存当前选中记录的编号
        if (curObject != sel.parentElement.cells(1).children(0).id)
        {
            curObject = sel.parentElement.cells(1).children(0).id;
            curValue = sel.parentElement.cells(1).children(0).innerText;
        }
        //将当前选中记录前的编号改为图片
        sel.parentElement.cells(1).children(0).innerText = "◆";
        sel.parentElement.style.color = "#FF9000";
        strHref = FileName + "?type=" + TableType + "&id=" + RecordID;
        frmContent.location.href = strHref;
    }
}

//跟上地址的标识
function ShowDetail2(FileName,TableType,vFlag,RecordID)
{
    var sel;
    sel = window.event.srcElement;
    if (sel.tagName != "INPUT")
    {
        if (sel.tagName != "TD")
            sel=sel.parentElement;
        //清除旧的选择标记
        if (curObject != "")
            document.all(curObject).innerText = curValue;
        //保存当前选中记录的编号
        if (curObject != sel.parentElement.cells(1).children(0).id)
        {
            curObject = sel.parentElement.cells(1).children(0).id;
            curValue = sel.parentElement.cells(1).children(0).innerText;
        }
        //将当前选中记录前的编号改为图片
        sel.parentElement.cells(1).children(0).innerText = "◆";
        sel.parentElement.style.color = "#FF9000";
        strHref = FileName + "?type=" + TableType + "&vFlag=" + vFlag + "&id=" + RecordID;
        frmContent.location.href = strHref;
    }
}

//显示DNS的内容
function ShowDnsDetail(FileName,vType,TableType,RecordID)
{
    var sel;
    sel = window.event.srcElement;
    if (sel.tagName != "INPUT")
    {
        if (sel.tagName != "TD")
            sel=sel.parentElement;
        //清除旧的选择标记
        if (curObject != "")
            document.all(curObject).innerText = curValue;
        //保存当前选中记录的编号
        if (curObject != sel.parentElement.cells(1).children(0).id)
        {
            curObject = sel.parentElement.cells(1).children(0).id;
            curValue = sel.parentElement.cells(1).children(0).innerText;
        }
        //将当前选中记录前的编号改为图片
        sel.parentElement.cells(1).children(0).innerText = "◆";
        sel.parentElement.style.color = "#FF9000";
        strHref = FileName + "?vType=" + vType + "&type=" + TableType + "&id=" + RecordID;
        frmContent.location.href = strHref;
    }
}

function ShowInfoDetail(FileName,InfoType,TableType,RecordID)
{
    var sel;
    sel = window.event.srcElement;
    if (sel.tagName != "INPUT")
    {
        if (sel.tagName != "TD")
            sel=sel.parentElement;
        //清除旧的选择标记
        if (curObject != "")
            document.all(curObject).innerText = curValue;
        //保存当前选中记录的编号
        if (curObject != sel.parentElement.cells(1).children(0).id)
        {
            curObject = sel.parentElement.cells(1).children(0).id;
            curValue = sel.parentElement.cells(1).children(0).innerText;
        }
        //将当前选中记录前的编号改为图片
        sel.parentElement.cells(1).children(0).innerText = "◆";
        sel.parentElement.style.color = "#FF9000";
        strHref = FileName + "?infotype="+InfoType+"&type=" + TableType + "&id=" + RecordID;
        frmContent.location.href = strHref;
    }
}

function ShowMail(FileName,MailType,TableType,RecordID)
{
    var sel;
    sel = window.event.srcElement;
    if (sel.tagName != "INPUT")
    {
        if (sel.tagName != "TD")
            sel=sel.parentElement;
        //清除旧的选择标记
        if (curObject != "")
            document.all(curObject).innerText = curValue;
        //保存当前选中记录的编号
        if (curObject != sel.parentElement.cells(1).children(0).id)
        {
            curObject = sel.parentElement.cells(1).children(0).id;
            curValue = sel.parentElement.cells(1).children(0).innerText;
        }
        //将当前选中记录前的编号改为图片
        sel.parentElement.cells(1).children(0).innerText = "◆";
        sel.parentElement.style.color = "#FF9000";
        strHref = FileName + "?mail=" + MailType + "&type=" + TableType + "&id=" + RecordID;
        frmContent.location.href = strHref;
    }
}

function GetMacIpUserInfo(ItemIndex)
{
    frmContent.location.href = "MacIpUser.aspx?index="+ItemIndex;
}

function DoMouseOver()
{
    var sel;
    sel = window.event.srcElement;
    if (sel.tagName != "TD")
        sel=sel.parentElement;
    sel.parentElement.style.backgroundColor = "#FFEE8F";
}
function DoMouseOut()
{
    var sel;
    sel = window.event.srcElement;
    if (sel.tagName != "TD")
        sel=sel.parentElement;
    sel.parentElement.style.backgroundColor = "#FFFFFF";
}
function DoSelectAll()
{
    var e = window.event.srcElement;
    for (var i=0; i<dtgData.rows.length; i++)
    {
        dtgData.rows(i).cells(0).children(0).checked = e.checked;
    }
}

    function selectAll(obj)
    {
        var theTable  = obj.parentElement.parentElement.parentElement;
        var i;
        var j = obj.parentElement.cellIndex;
        
        for(i=0;i<theTable.rows.length;i++)
        {
            var objCheckBox = theTable.rows[i].cells[j].firstChild;
            if(objCheckBox.checked!=null)objCheckBox.checked = obj.checked;
        }
    }

function SelectAllHost()
{
    var e = window.event.srcElement;
    for (var i=0; i<cblHost.rows.length; i++)
    {
        for (var j=0; j<cblHost.rows[i].cells.length; j++)
        {
            cblHost.rows(i).cells(j).children(0).checked = e.checked;
        }
    }
}

function ChangeDisplay()
{
    var e = window.event.srcElement;
    if (e.id == "imgTopCursor")//顶部箭头
    {
        if (e.src.indexOf("up.gif") >= 0)
        {
            eval("trTop").style.display = "none";
            e.src = e.src.replace("up.gif","down.gif");
        }
        else
        {
            eval("trTop").style.display = "block";
            e.src = e.src.replace("down.gif","up.gif");
        }
        top.frames[0].document.all["topDisplay"].value=eval("trTop").style.display;
    }
    else//底部箭头
    {
        if (e.src.indexOf("down.gif") >= 0)
        {
            eval("trContent").style.display = "block";
            eval("trBottom").height = "250";
            //eval("tblPage").style.display = "block";
            e.src = e.src.replace("down.gif","up.gif");
        }
        else
        {
            eval("trContent").style.display = "none";
            eval("trBottom").height = "100%";
            //eval("tblPage").style.display = "none";
            e.src = e.src.replace("up.gif","down.gif");
        }
        top.frames[0].document.all["bottomDisplay"].value=eval("trBottom").style.display; 
    }
}
//PostBack时保留原来的显示模式
function showByOldDisplay()
{
/*    var topStyle = top.frames[0].document.all["topDisplay"].value;
    var bottomSytyle = top.frames[0].document.all["bottomDisplay"].value;
    var imgTopSrc = document.all["imgTopCursor"].src;
    var imgBottomSrc = document.all["imgBottomCursor"].src;
    eval("trTop").style.display = topStyle;
    if ((topStyle == "none") && (imgTopSrc.indexOf("up.gif")) >= 0)
        document.all["imgTopCursor"].src = imgTopSrc.replace("up.gif","down.gif");
    eval("trBottom").style.display = bottomSytyle;
    if ((bottomSytyle == "none") && (imgBottomSrc.indexOf("down.gif")) >= 0)
        document.all["imgBottomCursor"].src = imgBottomSrc.replace("down.gif","up.gif");
*/
}
function writeTime()
{
    if (document.all)
    {
        if (self.name == "winQry")
        {
            self.opener.form_refresh.all("timeCount").value = "0";   
        }
        else
            top.frames[0].form_refresh.all("timeCount").value = "0";   
    }
}

function DoWinOnload()
{
    //document.all("sdate_input").value = top.frames[0].document.all("hidFromDate").value;
    //document.all("edate_input").value = top.frames[0].document.all("hidToDate").value;
}

function SaveDateRange()
{
    //top.frames[0].document.all("hidFromDate").value = document.all("sdate_input").value;
    //top.frames[0].document.all("hidToDate").value = document.all("edate_input").value;
}
//document.onkeydown = writeTime;
//document.onmousemove = writeTime;

function SelectedChooseDate()
{
    var lastRowIndex = qrytypelist.rows.length - 1;
    var lastCellIndex = qrytypelist.rows(lastRowIndex).cells.length - 1;
    qrytypelist.rows(lastRowIndex).cells(lastCellIndex).firstChild.checked = true;
}

/////关闭窗口时，刷新父窗口
function reLoadParentPage()
{
  window.opener.location.href=window.opener.location.href;
  window.opener.location.reload; 
  window.close();
}
   