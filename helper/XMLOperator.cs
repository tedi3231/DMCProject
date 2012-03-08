using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace helper
{
    public class XmlTxtOperator
    {

        /// <summary>
        /// 获取XML和TXT文件中的邮件正文，无则返回空字符
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>邮件正文</returns>
        public static string GetContentFromFile(string file)
        {
            if (string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                return "文件不存在 ";
            }

            if (file.Trim().EndsWith(".txt"))
            {
                return GetTXTString(file);
            }
            else if (file.Trim().EndsWith(".xml"))
            {
                return GetXMLContent(file);
            }

            return string.Empty;
        }

        /// <summary>
        /// 从XML邮件中获取邮件正文
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public static string GetXMLContent(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath) || !File.Exists(xmlFilePath))
            {
                return string.Empty;
            }

            XmlDocument xmlDoc = new XmlDocument();

            string content = string.Empty;

            try
            {
                xmlDoc.Load(xmlFilePath);
                XmlNode emp = xmlDoc.SelectSingleNode("/object/object/string[@name='content']");

                if (emp == null)
                {
                    content = GetXMLContent2(xmlDoc, null);
                }
                else
                {
                    content = emp.FirstChild.InnerText;
                }

                return content;
            }
            catch
            {
            }
            return content;
            //Console.WriteLine("The average cost of the books are {0}", nav.Evaluate(strExpression));
            //return nav.Evaluate(strExpression).ToString();
        }

        /// <summary>
        /// 获取特殊邮件正文
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string GetXMLContent2(XmlDocument xmlDoc, string nodeName)
        {
            XmlNode emp = xmlDoc.SelectSingleNode("/MailBox/GeneralPart/MailboxName");
            string mailServer = emp.InnerText.Trim().ToLower();

            //if (mailServer == "hotmail.com")
            {
                emp = xmlDoc.SelectSingleNode("/MailBox/MailSendPart/MailSendSending/MailContentTemplate/TemplateElement[Name='CONTENT']/Prefix");
            }

            return emp.InnerText;
        }

        public static string GetTXTString(string file, string endcode)
        {
            //file = @"D:\1272381304_1_1.txt";

            if (string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                return "文件不存在";
            }
            try
            {

                Encoding encode = Encoding.GetEncoding(endcode);//转换中文

                //是否需要转换为中文
                bool needTranslate = false;

                using (StreamReader bf = new StreamReader(file, encode))
                {

                    //byte[] bs = new byte[bf.Length];

                    //bf.Read(bs, 0, (int)bf.Length);

                    string str = bf.ReadToEnd(); // Encoding.GetEncoding("UTF-8").GetString(bs);

                    #region 解析文件，会有几种标注开始和结束的方式

                    int end = -1, start = -1;

                    //sina.com
                    start = str.IndexOf("Content-Disposition: form-data; name=\"msgtxt\"");

                    if (start >= 0)
                    {
                        start = start + 45+4;
                        end = str.IndexOf("-----------------------------", start);
                        end = end - 2;
                    }


                    if(start<0)
                    {
                        start = str.IndexOf("Content-Disposition: form-data; name=\"content__html\"");
                        if (start > 0)
                        {
                            start += 52; //前面有 htm"样的字符，需要这样去掉
                            end = str.IndexOf("Content-Disposition: form-data; name=\"savesendbox\"", start);
                        }
                    }

                    if (start < 0)
                    {
                        start = str.IndexOf("Content-Disposition: form-data; name=\"content\"");
                        if (start >= 0)
                        {
                            start = start + 46 + 4;
                            end = str.IndexOf("-----------------------------", start);
                            end = end - 2;
                            //end = str.LastIndexOf("Content-Disposition: form-data; name=\"sendmailname\"");
                        }
                    }


                    if (start < 0)
                    {
                        //hotmail.com
                        start = str.IndexOf("Content-Disposition: form-data; name=\"fMessageBody\"");
                        if (start >= 0)
                        {
                            start = start +51 + 4;
                            end = str.IndexOf("-----------------------------", start);
                            end = end - 2;
                        }

                        needTranslate = true;
                    }
                    #endregion

                    if (start < 0)
                    {
                        return "没有正文内容";
                    }

                    if (end < 0)
                    {
                        end = str.Length - 1;
                    }
                    

                    string strss = str.Substring(start, end - start);

                    //对新浪的邮件特殊处理，转换为UTF编码
                    if (str.IndexOf("name=\"sinafriends\"") >= 0)
                    {
                        needTranslate = true;
                    }

                    if (needTranslate)
                    {
                        strss = ConvertChinese(strss);
                    }

                    return strss;
                }
            }
            catch (Exception ex)
            {
                return "打开文件时发生错误:" + ex.Message;
            }
        }

        /// <summary>
        /// 获取TXT的内容
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetTXTString(string file)
        {
            return GetTXTString(file, "GB2312");
        }

        public static string ConvertChinese(string utf8String)
        {
            Encoding utf8 = Encoding.UTF8;
            Encoding defaultCode = Encoding.Default;

            // Convert the string into a byte[].
            byte[] utf8Bytes = defaultCode.GetBytes(utf8String);

            // Perform the conversion from one encoding to the other.
            byte[] defaultBytes = Encoding.Convert(utf8, defaultCode, utf8Bytes);


            byte[] buffer1 = Encoding.Default.GetBytes(utf8String);
            byte[] buffer2 = Encoding.Convert(Encoding.UTF8, Encoding.Default, buffer1, 0, buffer1.Length);
            string strBuffer = Encoding.Default.GetString(buffer2, 0, buffer2.Length);

            return strBuffer;
        }
    }
}
