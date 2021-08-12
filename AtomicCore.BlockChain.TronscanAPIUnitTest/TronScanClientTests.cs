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
        /// <summary>
        /// 1.Block Overview
        /// </summary>
        [TestMethod()]
        public void BlockOverviewTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.BlockOverview();

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 2.Get Last Block
        /// </summary>
        [TestMethod()]
        public void GetLastBlockTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetLastBlock();

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 3.List all the accounts in the blockchain
        /// </summary>
        [TestMethod()]
        public void GetChainTopAddressTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetChainTopAddress();

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 4.Get Account Assets
        /// </summary>
        [TestMethod()]
        public void GetAccountAssetsTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetAccountAssets("TWd4WrZ9wn84f5x1hZhL4DHvk738ns5jwb");

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 5.List the blocks in the blockchain
        /// </summary>
        [TestMethod()]
        public void GetLastBlocksTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetLastBlocks();

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 6.List all the blocks produced by the specified SR in the blockchain
        /// </summary>
        [TestMethod()]
        public void GetSRBlocksTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetSRBlocks("TMuA6YqfCeX8EhbfYEg5y7S4DqzSJireY9");

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 7.Get a single block's detail
        /// </summary>
        [TestMethod()]
        public void GetBlockByNumberTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetBlockByNumber(5987471);

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 8.Get Last Transaction List
        /// </summary>
        [TestMethod()]
        public void GetLastTransactionsTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetLastTransactions();

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 9.List the transactions related to a specified account
        /// </summary>
        [TestMethod()]
        public void GetNormalTransactionsTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetNormalTransactions("TMuA6YqfCeX8EhbfYEg5y7S4DqzSJireY9");

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// 10.List the transactions related to an smart contract
        /// </summary>
        [TestMethod()]
        public void GetContractTransactionsTest()
        {
            ITronScanClient client = new TronScanClient();
            var result = client.GetContractTransactions("TGfbkJww3x5cb9u4ekLtZ9hXvJo48nUSi4");

            Assert.IsTrue(null != result);
        }



        /// <summary>
        /// 40.List the internal transactions related to a specified account
        /// </summary>
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

        /// <summary>
        /// 41.List the transfers related to a specified TRC10 token(Order by Desc)
        /// </summary>
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

        /// <summary>
        /// 42.List the transfers related to a specified TRC20 token
        /// </summary>
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


    }
}