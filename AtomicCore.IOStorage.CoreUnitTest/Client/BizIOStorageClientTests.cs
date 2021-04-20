using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.IOStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AtomicCore.IOStorage.Core.Tests
{
    [TestClass()]
    public class BizIOStorageClientTests
    {
        static BizIOStorageClientTests()
        {
            AtomicCore.AtomicKernel.Initialize();
        }

        [TestMethod()]
        public void UploadFileTest()
        {
            string basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            string path = string.Format("{0}test.jpg", basePath);

            BizIOSingleUploadJsonResult result;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(0, SeekOrigin.Begin);

                BizIOStorageClient client = new BizIOStorageClient("http://127.0.0.1:8777");
                result = client.UploadFile(new BizIOUploadFileInput()
                {
                    BizFolder = "Test",
                    FileStream = fs,
                    FileName = "test.jpg"
                });
            }

            Assert.IsTrue(null != result && result.Code == BizIOStateCode.Success);
        }
    }
}