using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace AtomicCore.IOStorage.Core.Tests
{
    [TestClass()]
    public class BizIOStorageGrcpClientTests
    {
        const string host = "192.168.1.22";
        const int http_port = 8277;
        const int grpc_port = 8278;
        static string baseUrl = $"http://{host}:{http_port}";
        const string apiKey = "a6e2f27ee1f544cc889898e4397f7b07";

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
            var client = new BizIOStorageGrcpClient(host, grpc_port, baseUrl, apiKey);

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(0, SeekOrigin.Begin);

                result = client.UploadFile("Test", null, "123.jpg", fs).Result;
            }

            Assert.IsTrue(result.Code == BizIOStateCode.Success);
        }

        [TestMethod()]
        public void DownLoadFileTest()
        {
            var client = new BizIOStorageGrcpClient(host, grpc_port, baseUrl, apiKey);

            BizIODownloadJsonResult result = client.DownLoadFile("/test/dog/test.jpg").Result;

            Assert.IsTrue(result.Code == BizIOStateCode.Success);
        }
    }
}