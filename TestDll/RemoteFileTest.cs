using System;
using System.Collections.Generic;
using System.Text;

 

using NUnit.Framework;

namespace TestDll
{
    public class RemoteFileTest
    {
        RemoteFile rf = new RemoteFile();
        string url = "http://localhost/src/default.htm";
        [Test]
        public void RemoteFileExists_SimpleTest()
        {           
            Assert.IsTrue( rf.RemoteFileExists(url) );
        }

        [Test]
        public void SaveRemoteFile_SimpleTest()
        {
            Assert.IsTrue( rf.SaveRemoteFile(url) );
        }

        [Test]
        public void GetRemoteFileByteContent_SimpleTest()
        {
            Assert.IsNotNull( rf.GetRemoteFileByteContent(url) );
            Assert.IsTrue(rf.GetRemoteFileByteContent(url).Length > 0);
        }
    }
}
