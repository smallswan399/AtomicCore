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

            Assert.IsTrue(result.Length >= 0);
        }

        [TestMethod()]
        public void GetBEP20TransactionByAddressTest()
        {
            var result = client.GetBEP20TransactionByAddress("0x0702383c8dd23081d1962c72EeDB72902c731940", "0xe9e7cea3dedca5984780bafc599bd69add087d56");

            Assert.IsTrue(result.Length >= 0);
        }

        #endregion


    }
}