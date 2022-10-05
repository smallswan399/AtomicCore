using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AtomicCore.IOStorage.Core.Tests
{
    [TestClass()]
    public class BizIOStorageGrcpClientTests
    {
        static BizIOStorageGrcpClientTests()
        {
            AtomicCore.AtomicKernel.Initialize();
        }

        [TestMethod()]
        public void UploadFileTest()
        {
            string basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            string path = string.Format("{0}test.jpg", basePath);

            BizIOSingleUploadJsonResult result;
            var client = new BizIOStorageGrcpClient("127.0.0.1", 8778, "a6e2f27ee1f544cc889898e4397f7b07");

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(0, SeekOrigin.Begin);

                result = client.UploadFile("Test", "dog", "test.jpg", fs).Result;
            }

            Assert.IsTrue(result.Code == BizIOStateCode.Success);
        }
    }
}