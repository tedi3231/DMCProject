using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Collections;
using Microsoft.Win32;
using System.Diagnostics;



/// <summary>
/// common 用于定义一些共用的方法
/// </summary>
public class common
{
    public common()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// HashString(string Value)用于将密码加密
    /// 将Value用MD5加密，返回加密后的字串
    /// </summary>
    public static string HashString(string Value)
    {
        byte[] bytValue;
        byte[] bytHash;

        MD5CryptoServiceProvider mhash;
        mhash = new MD5CryptoServiceProvider();

        // Convert the original string to array of Bytes
        bytValue = System.Text.Encoding.UTF8.GetBytes(Value);

        // Compute the Hash, returns an array of Bytes
        bytHash = mhash.ComputeHash(bytValue);

        mhash.Clear();

        // Return a base 64 encoded string of the Hash value
        return Convert.ToBase64String(bytHash);
    }

    public static string md5(string str)
    {
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
    }

    /// <summary>
    /// 判断字串是否可以转换成正整数
    /// </summary>
    public static bool isInt(string value)
    {
        return Regex.IsMatch(value, @"^[1-9]\d*$");
    }

    /// <summary>
    /// 验证Email地址 
    /// </summary>
    /// <param name="strIn"></param>
    /// <returns></returns>
    public static bool IsValidEmail(string strIn)
    {
        return Regex.IsMatch(strIn, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
    }

    /// <summary>
    /// 验证IP合法性
    /// </summary>
    /// <param name="strIn"></param>
    /// <returns></returns>
    public static bool IsValidIp(string strIn)
    {
        return Regex.IsMatch(strIn, @"^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$");
    }


    /// <summary>
    /// 判断字符串是否为IP地址
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool isIP(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        string[] strs = value.Split(new char[] { '.' });

        if (strs == null || strs.Length != 4)
        {
            return false;
        }

        int temp = 0;

        for (int i = 0; i < strs.Length; i++)
        {
            if (!int.TryParse(strs[i], out temp) || (temp < 0 || temp > 255))
                return false;
        }

        return true;

        //return Regex.IsMatch(value, @"\b(?:\d{1,3}\.){3}\d{1,3}\b");
    }

    /// <summary>
    /// 判断字符是否为合法DNS
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool isDns(string value)
    {
        return Regex.IsMatch(value, @"");
    }

    /// <summary>
    /// DateToStr(DateTime dtValue)用于日期转换成符合存储过程规范的日期格式字串
    /// </summary>
    public static string DateToStr(DateTime dtValue)
    {
        string strMonth = dtValue.Month.ToString();
        string strDay = dtValue.Day.ToString();
        string result = "";
        if (strMonth.Length < 2) strMonth = "0" + strMonth;
        if (strDay.Length < 2) strDay = "0" + strDay;
        result = dtValue.Year + strMonth + strDay;
        return result;
    }

    public static string FormatStr(string str, int length)
    {
        string strTemp = Regex.Replace(str, @"[^\x00-\xff]", "#$");
        int _length;
        if (strTemp.Length > (length + 3))
        {
            strTemp = strTemp.Substring(0, length);
            _length = strTemp.Replace("#$", "#").Length;
            strTemp = str.Substring(0, _length) + "...";
        }
        else
            strTemp = str;
        return strTemp;
    }

    public static string WriteScript(string strScript)
    {
        string _strScript = "";
        _strScript += "<script language='Javascript'>";
        _strScript += strScript;
        _strScript += "</script>";
        return _strScript;
    }

    public static string GetFileUrl(object FilePath, int pos)
    {
        string _s = "";
        string[] sArray = FilePath.ToString().Split('\\');
        for (int i = pos; i < sArray.Length - 1; i++)
            _s += sArray[i] + "/";
        _s += sArray[sArray.Length - 1];
        return _s;
    }

    //获取虚拟路径
    public static string GetFileUrl(string FilePath)
    {
        if (string.IsNullOrEmpty(FilePath))
        {
            return string.Empty;
        }

        string _s = string.Empty;

        int start = FilePath.IndexOf("src");

        if (start > 0)
        {
            _s = FilePath.Substring(start + 4);
            _s = _s.Replace("\\", "/");
        }        
        return _s;
    }

    public static string GetHttpHead(string URL)
    {
        string _s = "";
        string[] sArray = URL.Split('/');
        for (int i = 0; i < sArray.Length - 2; i++)
            _s += sArray[i] + "/";
        return _s;
    }

    //根据系统当前时间生成文件名
    public static string GetFileName()
    {
        string _filename = System.DateTime.Now.ToString();
        char[] delimiterChars = { ' ', '-', ':' };
        string[] words = _filename.Split(delimiterChars);
        string result = "";
        for (int i = 0; i < words.Length; i++)
            result += words[i];
        return result;
    }

    //过滤掉html中的标签
    public static string GetText(object s)
    {
        string _s = "";

        int html_start = 0;
        int html_end = 0;
        string html = s.ToString();
        string html1 = "";
        string html2 = "";
        while (html_start >= 0)
        {
            html_start = html.IndexOf("<");
            if (html_start >= 0)
            {
                html1 = html.Substring(0, html_start);
                html_end = html.IndexOf(">", html_start);
                if (html_end < 0) html_end = 0;
                html2 = html.Substring(html_end + 1, (html.Length - html_end - 1));
                html = html1 + html2;
            }
        }

        _s = html;

        return _s;
    }

    //public static void alert(Page page, string msg)
    //{
    //    page.RegisterStartupScript("message", "<script language='javascript' defer>alert('" + msg + "');</script>");
    //}

    //写日志
    public static void setLog(string UserID, string UserName, string IP, string Content)
    {
        //    string path = HttpContext.Current.Server.MapPath("log/") + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //    if (!File.Exists(path))
        //    {
        //        using (StreamWriter sw = File.CreateText(path))
        //        {
        //            sw.WriteLine("[" + states + "]　用户名：" + User + "　" + states + "时间：" + DateTime.Now.ToString() + "　用户IP：" + IP);
        //        }
        //    }
        //    else
        //    {
        //        using (StreamWriter wr = File.AppendText(path))
        //        {
        //            wr.WriteLine("[" + states + "]　用户名：" + User + "　" + states + "时间：" + DateTime.Now.ToString() + "　用户IP：" + IP);
        //        }
        //    }
        dbConfig DB = new dbConfig();
        DB.InsertLog(UserID, UserName, IP, Content);
    }

    //获取站点名称
    public static string getSiteName(string nId)
    {
        dbConfig dbSite = new dbConfig();
        DataSet ds = dbSite.getHostByID(nId);
        return ds.Tables[0].Rows[0]["vCorpName"].ToString();
    }

    //使用winrar压缩文件
    public static void CompressFiles(string rarPath, ArrayList fileArray)
    {
        string rar;
        RegistryKey reg;
        object obj;
        string info;
        ProcessStartInfo startInfo;
        Process rarProcess;
        try
        {
            reg = Registry.ClassesRoot.OpenSubKey("Applications\\WinRAR.exe\\Shell\\Open\\Command");
            obj = reg.GetValue("");
            rar = obj.ToString();
            reg.Close();
            rar = rar.Substring(1, rar.Length - 7);
            info = " a -as -r -EP1 " + rarPath;
            foreach (string filepath in fileArray)
                info += " " + filepath;
            startInfo = new ProcessStartInfo();
            startInfo.FileName = rar;
            startInfo.Arguments = info;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            rarProcess = new Process();
            rarProcess.StartInfo = startInfo;
            rarProcess.Start();
        }
        catch
        {

        }
    }

    /// <summary>
    /// 输出DNS类型的字符表示
    /// </summary>
    /// <param name="type">
    /// DNS类型
    /// 0 黑名单 1 动态域名;2 可疑域名;3 低风险异常域名;4 中风险异常域名;5 高风险异常域名;6 白名单
    /// </param>
    /// <returns>DNS类型的字符表示</returns>
    public static string FormatDnsType(int type)
    {
        string temp = string.Empty;
        switch (type)
        {
            case 0:
                temp = "黑名单";
                break;
            case 1:
                temp = "动态域名";
                break;
            case 2:
                temp = "可疑域名";
                break;
            case 3:
                temp = "低风险异常域名";
                break;
            case 4:
                temp = "中风险异常域名";
                break;
            case 5:
                temp = "高风险异常域名";
                break;
            case 6:
                temp = "白名单";
                break;
            case 7:
                temp = "普通黑名单";
                break;
            case 8:
                temp = "重要黑名单";
                break;
            case 9:
                temp = "紧急黑名单";
                break;
            default:
                temp = "未知类型";
                break;
        }
        return temp;
    }

    /// <summary>
    /// 输出值代表的一类地址的名称
    /// </summary>
    /// <param name="type">
    /// 0 中国大陆
    /// 1 中国台湾
    /// 2 香港澳门
    /// 3 美国
    /// 4 日本
    /// 5 韩国
    /// 254 未知
    /// 255 其他国家
    /// 253 保留地址
    /// </param>
    /// <returns>DNS类型的字符表示</returns>
    public static string FormatIPString(int type)
    {
        string temp = string.Empty;
        switch (type)
        {
            case 0:
                temp = "中国大陆";
                break;
            case 1:
                temp = "中国台湾";
                break;
            case 2:
                temp = "香港澳门";
                break;
            case 3:
                temp = "美国";
                break;
            case 4:
                temp = "日本";
                break;
            case 5:
                temp = "韩国";
                break;
            case 254:
                temp = "未知";
                break;
            case 255:
                temp = "其他国家";
                break;
            case 253:
                temp = "保留地址";
                break;
        }
        return temp;
    }

    /// <summary>
    /// 将整数木马方向转化为字符木马方向
    /// </summary>
    /// <param name="prc">
    /// 数字表示的 木马方向
    /// <p>0出1进2双向</p>
    /// </param>
    /// <returns>转换后的协议名称</returns>
    public static string FormatTrojanFlag(int vFlag)
    {

        switch (vFlag)
        {
            case 1:
                return "出";

            case 0:
                return "进";

            default:
                return "其它";
        }
    }

    /// <summary>
    /// 根据要求的长度取字符串部分内容
    /// </summary>
    /// <param name="content">源字符串</param>
    /// <param name="len">要取的长度</param>
    /// <returns>取得的部分内容</returns>     
    public static string SubContent(string content, int len)
    {
        if (string.IsNullOrEmpty(content))
            return string.Empty;
        if (content.Length > len)
        {
            return content.Substring(0, len) + "....";
        }
        else
        {
            return content;
        }
    }

    /// <summary>
    /// 根据用户ID返回用户能访问的所有节点,没有则返回NULL
    /// </summary>
    /// <param name="userID">用户ID</param>
    /// <returns>户能访问的所有节点,没有则返回NULL</returns>
    public static string GetHosrList(string userID)
    {
        dbConfig dbHost = new dbConfig();
        DataSet ds = dbHost.getSites(userID);

        StringBuilder bul = new StringBuilder("'0'");

        if (ds == null || ds.Tables.Count <= 0)
        {
            return null;
        }

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            bul.Append(",'");
            bul.Append(row["nId"].ToString());
            bul.Append("'");
        }

        return bul.ToString();
    }


    /// <summary>
    /// 将IPv4格式的字符串转换为int型表示
    /// </summary>
    /// <param name="strIPAddress">IPv4格式的字符</param>
    /// <returns></returns>
    public static int IPToNumber(object sIPAddress)
    {
        if (sIPAddress == DBNull.Value)
            return 0;

        string strIPAddress = sIPAddress.ToString();

        //将目标IP地址字符串strIPAddress转换为数字
        string[] arrayIP = strIPAddress.Split('.');
        int sip1 = Int32.Parse(arrayIP[0]);
        int sip2 = Int32.Parse(arrayIP[1]);
        int sip3 = Int32.Parse(arrayIP[2]);
        int sip4 = Int32.Parse(arrayIP[3]);
        int tmpIpNumber;
        tmpIpNumber = sip1 * 256 * 256 * 256 + sip2 * 256 * 256 + sip3 * 256 + sip4;
        return tmpIpNumber;
    }

    /// <summary>
    /// 将int型表示的IP还原成正常IPv4格式。
    /// </summary>
    /// <param name="intIPAddress">int型表示的IP</param>
    /// <returns></returns>
    public static string NumberToIP(object iIPAddress)
    {
        if (iIPAddress == DBNull.Value)
            return "";

        long tempIPAddress;
        long intIPAddress;

        intIPAddress = Convert.ToInt64(iIPAddress);

        //将目标整形数字intIPAddress转换为IP地址字符串
        //-1062731518 192.168.1.2 
        //-1062731517 192.168.1.3 
        if (intIPAddress >= 0)
        {
            tempIPAddress = intIPAddress;
        }
        else
        {
            tempIPAddress = intIPAddress + 1;
        }
        long s1 = tempIPAddress / 256 / 256 / 256;
        long s21 = s1 * 256 * 256 * 256;
        long s2 = (tempIPAddress - s21) / 256 / 256;
        long s31 = s2 * 256 * 256 + s21;
        long s3 = (tempIPAddress - s31) / 256;
        long s4 = tempIPAddress - s3 * 256 - s31;
        if (intIPAddress < 0)
        {
            s1 = 255 + s1;
            s2 = 255 + s2;
            s3 = 255 + s3;
            s4 = 255 + s4;
        }
        string strIPAddress = s1.ToString() + "." + s2.ToString() + "." + s3.ToString() + "." + s4.ToString();
        return strIPAddress;
    }

    /// <summary>
    /// 将秒数转换为时间
    /// </summary>
    /// <param name="second"></param>
    /// <returns></returns>
    public static string SecondToDate(long second)
    {
        DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0);
        dt = dt.AddSeconds(second);
        return dt.ToString();
    }


    /// <summary>
    /// 获取IP的具体地址信息
    /// </summary>
    /// <param name="ipnum">大地址</param>
    /// <param name="ip">IP值</param>
    /// <returns>IP的具体地址信息</returns>
    public static string GetIpAreaInfo(string ipnum, string ip)
    {
        if (string.IsNullOrEmpty(ip))
        {
            return string.Empty;
        }

        dbConfig db2 = new dbConfig();

        if (string.IsNullOrEmpty(ipnum))
        {
            return db2.GetIpArea(ip);
        }

        DataSet ds2 = db2.GetIpAddrInfo(ipnum, ip); ;

        if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        {
            return ds2.Tables[0].Rows[0][0].ToString();
        }

        return "尚无记录";
    }

    /// <summary>
    /// 返回CheckBoxList中返回的值
    /// </summary>
    /// <param name="CheckBoxList2">checkboxlist</param>
    /// <returns>返回的整数值</returns>
    public static int GetSenstiveCheck(CheckBoxList CheckBoxList2)
    {
        int result = 0;
        
        string[] temp = new string[]{ "0","0","0","0","0"};

        foreach (ListItem item in CheckBoxList2.Items)
        {
            if (item.Selected)
            {
                if (item.Value == "5")
                {
                    temp[4] = "1";
                }
                if (item.Value == "4")
                {
                    temp[3] = "1";
                }
                if (item.Value == "3")
                {
                    temp[2] = "1";
                }
                if (item.Value == "2")
                {
                    temp[1] = "1";
                }
                if (item.Value == "1")
                {
                    temp[0] = "1";
                }
            }
            else
            {
                if (item.Value == "5")
                {
                    temp[4] = "0";
                }
                if (item.Value == "4")
                {
                    temp[3] = "0";
                }
                if (item.Value == "3")
                {
                    temp[2] = "0";
                }
                if (item.Value == "2")
                {
                    temp[1] = "0";
                }
                if (item.Value == "1")
                {
                    temp[0] = "0";
                }
            }
        }

        string a = temp[4] + temp[3] + temp[2] + temp[1] + temp[0];
        result = Convert.ToInt32(a, 2);
        return result;
    }

    /// <summary>
    /// 根据期待的结果返回要显示的内容
    /// </summary>
    /// <param name="val"></param>
    /// <param name="exceptVal"></param>
    /// <returns></returns>
    public static string FormatVal(int val, int exceptVal)
    {
        if ((val & exceptVal) == exceptVal)
        {
            return "是";
        }
        else
        {
            return "否";
        }
    }
}
