using System;
using System.Collections.Generic;
using System.Text;



using NUnit.Framework;
using helper;
using System.IO;

namespace TestDll
{
    public class VersionUpdateTest
    {
        [Test]
        public void GetVersion_SimpleTest()
        {
            VersionUpdate up = new VersionUpdate();

            string result = VersionUpdate.GetVersion(1);
            Assert.IsTrue(!string.IsNullOrEmpty(result));
        }

        [Test]
        public void UpdateFile_SimpleTest()
        {
            string file = VersionUpdate.UpdateFile(0, 222);
            Assert.IsNotNull(file);
            Assert.IsTrue(File.Exists(file));
        }
    }
}
