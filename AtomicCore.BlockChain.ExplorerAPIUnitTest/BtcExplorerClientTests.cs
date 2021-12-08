using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.ExplorerAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.ExplorerAPI.Tests
{
    [TestClass()]
    public class BtcExplorerClientTests
    {
        /// <summary>
        /// Get Address Balance 1ARjWDkZ7kT9fwjPrjcQyvbXDkEySzKHwu
        /// </summary>
        [TestMethod()]
        public void GetAddressBalanceTest()
        {
            IBtcExplorerClient cli = new BtcExplorerClient();

            var result = cli.GetAddressBalance("1ARjWDkZ7kT9fwjPrjcQyvbXDkEySzKHwu");

            Assert.IsTrue(null != result);
        }

        /// <summary>
        /// https://api.blockchain.info/haskoin-store/btc/address/1ARjWDkZ7kT9fwjPrjcQyvbXDkEySzKHwu/transactions/full
        /// </summary>
        [TestMethod()]
        public void GetAddressTxsTest()
        {
            IBtcExplorerClient cli = new BtcExplorerClient();

            var result = cli.GetAddressTxs("1ARjWDkZ7kT9fwjPrjcQyvbXDkEySzKHwu");

            Assert.IsTrue(null != result);
        }
    }
}