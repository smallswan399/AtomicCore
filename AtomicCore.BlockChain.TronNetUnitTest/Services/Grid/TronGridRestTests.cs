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

        [TestMethod()]
        public void GetAccountTest()
        {
            var result = _gridApiClient.GetAccount("TK7XWSuRi5PxYDUQ53L43baio7ZBWukcGm");

            Assert.IsTrue(result.IsAvailable());
        }
    }
}