using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using helper;

namespace helperTest
{
    [TestClass]
    public class SystemSourceTest
    {
        [TestMethod]
        public void Test()
        {
            int val = 0xF;

            int a = val & 0x1;
            int b = val & 0x2;
            int c = val & 0x4;
            int d = val & 0x8;
            Assert.AreEqual(1,a);
            Assert.AreEqual(2, b);
            Assert.AreEqual(4, c);
            Assert.AreEqual(8, d);
        }

        [TestMethod]
        public void GetDiskUsePercentTest()
        {
            ServerResource sr = new ServerResource();
            Assert.AreEqual(sr.GetDiskUsePercent(), 0);
        }

        [TestMethod]
        public void GetVersion_test()
        {
           string version =  VersionUpdate.GetVersion(2);
           VersionUpdate.UpdateFile(2, Convert.ToInt32(version));

            Assert.IsNotNull(version);
        }
    }
}
