using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.OMNINet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.OMNINet.Tests
{
    [TestClass()]
    public class USDTCoinServiceTests
    {
        [TestMethod()]
        public void GetOMNIInfoTest()
        {
            IUSDTCoinService usdtHandler = new USDTCoinService("http://127.0.0.1:8332", "root", "123456", string.Empty);

            var info = usdtHandler.GetOMNIInfo();

            Assert.IsTrue(null != info);
        }
    }
}