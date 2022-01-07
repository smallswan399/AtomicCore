using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.BscscanAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.BscscanAPI.Tests
{
    [TestClass()]
    public class BscscanClientTests
    {
        #region Variables

        private readonly IBscscanClient client;

        #endregion

        #region Constructor

        public BscscanClientTests()
        {
            client = new BscscanClient();
            client.SetApiKeyToken("Y18IQ48GQSKKNDZGUN5TAUDEHD84VS8ZQY");
        }

        #endregion

        #region IBscAccounts

        [TestMethod()]
        public void GetBalanceTest()
        {
            var result = client.GetBalance("0x0702383c8dd23081d1962c72EeDB72902c731940");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetBalanceListTest()
        {
            var result = client.GetBalanceList(new string[]
            {
                "0x0000000000000000000000000000000000001004",
                "0x0702383c8dd23081d1962c72EeDB72902c731940"
            });

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetNormalTransactionByAddressTest()
        {
            var result = client.GetNormalTransactionByAddress("0x0702383c8dd23081d1962c72EeDB72902c731940");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetInternalTransactionByAddressTest()
        {
            var result = client.GetInternalTransactionByAddress("0x33350dd80773DEB379D79ceb035b49E5E79E3615");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetInternalTransactionByHashTest()
        {
            var result = client.GetInternalTransactionByHash("0xe03b3199a41733cf167201f3b31cf77076689944d42f7a4a37f8b4d377ed1336");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetBEP20TransactionByAddressTest()
        {
            var result = client.GetBEP20TransactionByAddress("0x0702383c8dd23081d1962c72EeDB72902c731940", "0xe9e7cea3dedca5984780bafc599bd69add087d56");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetBEP721TransactionByAddressTest()
        {
            var result = client.GetBEP721TransactionByAddress("0x785D43bd5Bd506ca3B28b017394f14Ec04F9CCC9", "0xf51fb8de65f85cb18a2558c1d3769835f526f36c");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetMinedBlockListByAddressTest()
        {
            var result = client.GetMinedBlockListByAddress("0x78f3adfc719c99674c072166708589033e2d9afe", "blocks");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        #endregion

        #region IBscBlocks

        [TestMethod()]
        public void GetContractABITest()
        {
            var result = client.GetContractABI("0xe9e7cea3dedca5984780bafc599bd69add087d56");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        #endregion

        #region IBscTransactions

        [TestMethod()]
        public void GetTransactionReceiptStatusTest()
        {
            var result = client.GetTransactionReceiptStatus("0xd0d1d4b745a221527ef38a5fbf22509ed5a5fb104efbab28d924b2fb9c19e93b");

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        #endregion

        #region IBscBlocks

        [TestMethod()]
        public void GetBlockRewardByNumberTest()
        {
            var result = client.GetBlockRewardByNumber(13467768);

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetBlockEstimatedByNumberTest()
        {
            var result = client.GetBlockEstimatedByNumber(14154145);

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetBlockNumberByTimestampTest()
        {
            var result = client.GetBlockNumberByTimestamp(1601510400, BscClosest.Before);

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }




        #endregion

        #region IBscGasTracker

        [TestMethod()]
        public void GetGasOracleTest()
        {
            var result = client.GetGasOracle();

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        #endregion

        #region IBscGethProxy

        [TestMethod()]
        public void GetBlockNumberTest()
        {
            var result = client.GetBlockNumber();

            Assert.IsTrue(result.Status == BscscanJsonStatus.Success);
        }

        #endregion


    }
}