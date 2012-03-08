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


        public static string GetXMLContent(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath) || !File.Exists(xmlFilePath))
            {
                return string.Empty;
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFilePath);
                XmlNode emp = xmlDoc.SelectSingleNode("/object/object/string[@name='content']");
                return emp.InnerText;
            }
            catch
            {
                return string.Empty;
            }

            //Console.WriteLine("The average cost of the books are {0}", nav.Evaluate(strExpression));
            //return nav.Evaluate(strExpression).ToString();
        }

        /// <summary>
        /// 获取TXT的内容
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetTXTString(string file)
        {
            //file = @"D:\1272381304_1_1.txt";

            if (string.IsNullOrEmpty(file) || !File.Exists(file))
            {
                return "文件不存在";
            }
            try
            {

                Encoding encode = Encoding.GetEncoding("GB2312");//转换中文




                using (StreamReader bf = new StreamReader(file, encode))
                {
                   
                    //byte[] bs = new byte[bf.Length];

                    //bf.Read(bs, 0, (int)bf.Length);

                    string str = bf.ReadToEnd(); // Encoding.GetEncoding("UTF-8").GetString(bs);

                    #region 解析文件，会有几种标注开始和结束的方式

                    int end = -1, start = -1;

                    start = str.IndexOf("Content-Disposition: form-data; name=\"msgtxt\"");

                    if (start < 0)
                    {
                        start = str.IndexOf("Content-Disposition: form-data; name=\"content__html\"");
                        if (start > 0)
                        {
                            start += 4; //前面有 htm"样的字符，需要这样去掉
                        }
                    }

                    end = str.LastIndexOf("Content-Disposition: form-data; name=\"snt\"");

                    if (end < 0)
                    {
                        end = str.LastIndexOf("Content-Disposition: form-data; name=\"savesendbox\"");
                    }
                    #endregion

                    if (start < 0)
                    {
                        return "文件解析失败";
                    }

                    if (end < 0)
                    {
                        end = str.Length - 1;
                    }

                    string strss = str.Substring(start + 48, end - start - 92);

                    return strss;
                }
            }
            catch (Exception ex)
            {
                return "打开文件时发生错误:" + ex.Message;
            }
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
