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
    public class TronRestAPITests
    {
        #region Variables

        private readonly TronTestRecord _record;
        private readonly ITronNetRest _restAPI;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        public TronRestAPITests()
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


    }
}