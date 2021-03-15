using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.Tests
{
    [TestClass()]
    public class BufferExtensionsTests
    {
        [TestMethod()]
        public void ToBytesTest()
        {
            Uri uri = new Uri("http://www.baidu.com");

            byte[] buffer = uri.ToBytes();

            Uri secUri = buffer.ToObject<Uri>();

            Assert.IsTrue(null != buffer);
        }
    }
}