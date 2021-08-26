using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.TronNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet.Tests
{
    [TestClass()]
    public class TronNetRestTests
    {
        #region Variables

        private readonly TronTestRecord _record;
        private readonly ITronNetRest _restAPI;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public TronNetRestTests()
        {
            _record = TronTestServiceExtension.GetMainRecord();
            _restAPI = _record.TronClient.GetRestAPI();
        }

        #endregion

        #region ITronAddressUtilitiesRestAPI

        [TestMethod()]
        public void ValidateAddressTest()
        {
            var result = _restAPI.ValidateAddress("TEfiVcH2MF43NDXLpxmy6wRpaMxnZuc4iX");

            Assert.IsTrue(result.Result);
        }

        #endregion

        #region ITronTransactionsRest

        [TestMethod()]
        public void CreateTransactionTest()
        {
            TronTestRecord shatasnet = TronTestServiceExtension.GetTestRecord();
            ITronNetRest testRestAPI = shatasnet.TronClient.GetRestAPI();

            TronNetCreateTransactionRestJson result = testRestAPI.CreateTransaction(
                TronTestAccountCollection.TestMain.Address,
                TronTestAccountCollection.TestA.Address,
                1
            );

            Assert.IsTrue(!string.IsNullOrEmpty(result.TxID));
        }

        [TestMethod()]
        public void GetTransactionSignTest()
        {
            TronTestRecord shatasnet = TronTestServiceExtension.GetTestRecord();
            ITronNetRest testRestAPI = shatasnet.TronClient.GetRestAPI();

            TronNetCreateTransactionRestJson createTransaction = testRestAPI.CreateTransaction(
                TronTestAccountCollection.TestMain.Address,
                TronTestAccountCollection.TestA.Address,
                1
            );
            Assert.IsTrue(!string.IsNullOrEmpty(createTransaction.TxID));

            var result = testRestAPI.GetTransactionSign(TronTestAccountCollection.TestMain.PirvateKey, createTransaction);

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// API ERROR
        /// </summary>
        [TestMethod()]
        public void BroadcastTransactionTest()
        {
            TronTestRecord shatasnet = TronTestServiceExtension.GetTestRecord();
            ITronNetRest testRestAPI = shatasnet.TronClient.GetRestAPI();

            TronNetCreateTransactionRestJson createTransaction = testRestAPI.CreateTransaction(
                TronTestAccountCollection.TestMain.Address,
                TronTestAccountCollection.TestA.Address,
                1
            );
            Assert.IsTrue(!string.IsNullOrEmpty(createTransaction.TxID));

            TronNetSignedTransactionRestJson signTransaction = testRestAPI.GetTransactionSign(TronTestAccountCollection.TestMain.PirvateKey, createTransaction);

            TronNetResultJson result = testRestAPI.BroadcastTransaction(signTransaction);

            Assert.IsTrue(result.Result);
        }

        #endregion

        #region ITronQueryNetworkRestAPI

        [TestMethod()]
        public void GetBlockByNumTest()
        {
            TronNetBlockJson result = _restAPI.GetBlockByNum(200);

            Assert.IsTrue(!string.IsNullOrEmpty(result.BlockID));
        }

        [TestMethod()]
        public void GetBlockByIdTest()
        {
            TronNetBlockJson result = _restAPI.GetBlockById("0000000001f9f486548b36b7a54732ffc070b4311247cb88999cf7cef5c1bfa2");

            Assert.IsTrue(!string.IsNullOrEmpty(result.BlockID));
        }

        [TestMethod()]
        public void GetBlockByLatestNumTest()
        {
            TronNetBlockListJson result = _restAPI.GetBlockByLatestNum(10);

            Assert.IsTrue(result.Blocks != null);
        }

        [TestMethod()]
        public void GetBlockByLimitNextTest()
        {
            TronNetBlockListJson result = _restAPI.GetBlockByLimitNext(10, 11);

            Assert.IsTrue(result.Blocks != null);
        }

        [TestMethod()]
        public void GetNowBlockTest()
        {
            TronNetBlockDetailsJson result = _restAPI.GetNowBlock();

            Assert.IsTrue(result != null);
        }

        [TestMethod()]
        public void GetTransactionByIDTest()
        {
            string txid = "ca8d10f2b141a3a8d8e31453ff50716258d873c89fd189f6abce92effaa1960d";

            TronNetTransactionRestJson rest_txInfo = _restAPI.GetTransactionByID(txid);

            TronNetContractJson contractJson = rest_txInfo.RawData.Contract.FirstOrDefault();
            Assert.IsNotNull(contractJson);

            string ownerAddress = contractJson.Parameter.Value.GetOwnerTronAddress();
            Assert.IsTrue(!string.IsNullOrEmpty(ownerAddress));

            TronNetTriggerSmartContractJson valueJson = contractJson.Parameter.Value.ToContractValue<TronNetTriggerSmartContractJson>();
            Assert.IsNotNull(valueJson);

            string toEthAddress = valueJson.GetToEthAddress();
            Assert.IsTrue("0x10b6bb9e59f3e7b139a3e23c340eabc841817976".Equals(toEthAddress, StringComparison.OrdinalIgnoreCase));

            string toTronAddress = valueJson.GetToTronAddress();
            Assert.IsTrue("TBVaidbMvnXovzHJV7TTxeZ5Tkehxrx5UW".Equals(toTronAddress, StringComparison.OrdinalIgnoreCase));

            ulong amount = valueJson.GetOriginalAmount();
            Assert.IsTrue(amount > 0);

            Assert.IsTrue(!string.IsNullOrEmpty(rest_txInfo.TxID));
        }

        [TestMethod()]
        public void GetTransactionInfoByIdTest()
        {
            //TRX => f337385642d56a981fe8938049e3765e6abcca53ac9412a327b1906df272bdc1
            //TRC10 => c43d19f4517ce1a4c31c66eb4d9b41409caddc3be1cb733e28e941b3220e1b2d
            //TRC20 => f2eb864b3058b708d082b4aecf6573bc5606c741b51cf8870da30dd56e4aae40

            TronNetTransactionInfoJson result = _restAPI.GetTransactionInfoById("f337385642d56a981fe8938049e3765e6abcca53ac9412a327b1906df272bdc1");

            Assert.IsTrue(!string.IsNullOrEmpty(result.TxID));
        }


        #endregion


    }
}