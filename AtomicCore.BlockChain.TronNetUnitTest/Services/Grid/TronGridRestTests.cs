using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var result = _gridApiClient.GetTransactions("TK7XWSuRi5PxYDUQ53L43baio7ZBWukcGm", new TronGridRequestQuery()
            {
                Limit = 1
            });

            Assert.IsTrue(result.IsAvailable());
        }

        #endregion


    }
}