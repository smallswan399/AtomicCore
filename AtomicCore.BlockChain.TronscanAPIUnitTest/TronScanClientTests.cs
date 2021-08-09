using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.TronscanAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronscanAPI.Tests
{
    [TestClass()]
    public class TronScanClientTests
    {
        [TestMethod()]
        public void BlockOverviewTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.BlockOverview();

            Assert.IsTrue(null != result);
        }

        [TestMethod()]
        public void GetLastBlockTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetLastBlock();

            Assert.IsTrue(null != result);
        }

        [TestMethod()]
        public void GetAccountAssetsTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetAccountAssets("TWd4WrZ9wn84f5x1hZhL4DHvk738ns5jwb");

            Assert.IsTrue(null != result);
        }

        [TestMethod()]
        public void GetTRC10TransactionsTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetTRC10Transactions("TSbhZijH2t7Qn1UAHAu7PBHQdVAvRwSyYr");

            Assert.IsTrue(null != result);
        }
    }
}