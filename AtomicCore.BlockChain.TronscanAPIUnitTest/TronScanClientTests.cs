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
        public void GetChainTopAddressTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetChainTopAddress();

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
        public void GetLastBlocksTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetLastBlocks();

            Assert.IsTrue(null != result);
        }

        [TestMethod()]
        public void GetSRBlocksTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetSRBlocks("TMuA6YqfCeX8EhbfYEg5y7S4DqzSJireY9");

            Assert.IsTrue(null != result);
        }










        [TestMethod()]
        public void GetTRC10TransactionsTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetTRC10Transactions(
                "TSbhZijH2t7Qn1UAHAu7PBHQdVAvRwSyYr",
                0,
                20,
                "IGG",
                1529856000000,
                1552549912537
            );

            Assert.IsTrue(null != result);
        }

        [TestMethod()]
        public void GetTRC20TransactionsTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetTRC20Transactions(
                "TCN77KWWyUyi2A4Cu7vrh5dnmRyvUuME1E",
                0,
                20,
                1529856000000,
                1552550375474
            );

            Assert.IsTrue(null != result);
        }

        [TestMethod()]
        public void GetInternalTransactionTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetInternalTransactions(
                "TCN77KWWyUyi2A4Cu7vrh5dnmRyvUuME1E",
                0,
                20,
                1529856000000,
                1552550375474
            );

            Assert.IsTrue(null != result);
        }


    }
}