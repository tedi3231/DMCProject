using System;
using System.Collections.Generic;
using System.Text;
using helper;
using NUnit.Framework;
using System.IO;
namespace TestDll
{
    public class XMLOperatorTest
    {
        [Test]
        public void Test()
        {
            //string test = XmlTxtOperator.GetXMLContent(@"D:\Study1\templates\hotmail.xml");
            //NUnit.Framework.Assert.NotNull(test);
            //Console.WriteLine(test);
            //test = XmlTxtOperator.GetXMLContent(@"D:\Study1\templates\sogou.xml");
            //Assert.NotNull(test);
            //Console.WriteLine(test);
            //test = XmlTxtOperator.GetXMLContent(@"D:\Study1\templates\126.xml");
            //Assert.NotNull(test);
            //Console.WriteLine(test);
            string test = XmlTxtOperator.GetXMLContent(@"D:\DMCProject\Document\114622464_1_0.xml");
            Assert.IsNotNullOrEmpty(test);
            Console.WriteLine(test);
        }

        [Test]
        public void Testchinese()
        {
            /*string text = XmlTxtOperator.GetTXTString(@"D:\DMCProject\Document\1838940160_1_1.txt");

            string temp = XmlTxtOperator.ConvertChinese(text);

            Assert.IsNotNullOrEmpty(temp);

            text = XmlTxtOperator.GetTXTString(@"D:\DMCProject\Document\100859904_1_0.txt");
            temp = XmlTxtOperator.ConvertChinese(text);
            Assert.IsNotNullOrEmpty(temp);

            text = XmlTxtOperator.GetTXTString(@"D:\DMCProject\Document\636747776_1_0.txt");
            temp = XmlTxtOperator.ConvertChinese(text);
            Assert.IsNotNullOrEmpty(temp);

            text = XmlTxtOperator.GetTXTString(@"D:\DMCProject\Document\140640256_1_0.txt");
            temp = XmlTxtOperator.ConvertChinese(text);
            Assert.IsNotNullOrEmpty(temp);
            */
            string text = XmlTxtOperator.GetTXTString(@"D:\DMCProject\Document\1598858170_1_0.txt");
            string  temp = XmlTxtOperator.ConvertChinese(text);
            Assert.IsNotNullOrEmpty(temp);

            text = XmlTxtOperator.GetTXTString(@"D:\DMCProject\Document\1598858170_1_01.txt");
            temp = XmlTxtOperator.ConvertChinese(text);
            Assert.IsNotNullOrEmpty(temp);
        }
    }
}
