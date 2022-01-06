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
        private IBscscanClient client;

        public BscscanClientTests()
        {
            client = new BscscanClient();
            client.SetApiKeyToken("Y18IQ48GQSKKNDZGUN5TAUDEHD84VS8ZQY");
        }

        [TestMethod()]
        public void GetBalanceTest()
        {
            var result = client.GetBalance("0x0702383c8dd23081d1962c72EeDB72902c731940");

            Assert.IsTrue(result > decimal.Zero);
        }

        [TestMethod()]
        public void GetBalanceListTest()
        {
            var result = client.GetBalanceList(new string[]
            {
                "0x0000000000000000000000000000000000001004",
                "0x0702383c8dd23081d1962c72EeDB72902c731940"
            });

            Assert.IsTrue(result.Sum(d => d.Balance) > decimal.Zero);
        }
    }
}