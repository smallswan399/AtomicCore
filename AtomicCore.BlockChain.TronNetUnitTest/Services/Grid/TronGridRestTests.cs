﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace AtomicCore.BlockChain.TronNet.Tests
{
    [TestClass()]
    public class TronGridRestTests
    {
        #region Variables

        private readonly TronTestRecord _record;
        private readonly ITronGridRest _gridApiClient;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public TronGridRestTests()
        {
            _record = TronTestServiceExtension.GetMainRecord();
            _gridApiClient = _record.TronClient.GetGridAPI();
        }

        #endregion

        #region ITronGridAccountRest

        [TestMethod()]
        public void GetAccountTest()
        {
            var result = _gridApiClient.GetAccount("TK7XWSuRi5PxYDUQ53L43baio7ZBWukcGm");

            Assert.IsTrue(result.IsAvailable());
        }

        [TestMethod()]
        public void GetTransactionsTest()
        {
            var result = _gridApiClient.GetTransactions("TK7XWSuRi5PxYDUQ53L43baio7ZBWukcGm");

            var item = result.Data.FirstOrDefault(d => d.RawData.Contract.Any(d => d.Type == TronNetContractType.TriggerSmartContract));
            if (null != item)
            {
                var contract = item.RawData.Contract.FirstOrDefault(d => d.Type == TronNetContractType.TriggerSmartContract);
                var trc20Info = contract.Parameter.Value.ToObject<TronGridTriggerSmartContractInfo>();

                string toAddress = trc20Info.GetToAddress();
                var rawAmount = trc20Info.GetRawAmount();
                var amount = trc20Info.GetAmount(6);
            }

            Assert.IsTrue(result.IsAvailable());
        }

        #endregion


    }
}