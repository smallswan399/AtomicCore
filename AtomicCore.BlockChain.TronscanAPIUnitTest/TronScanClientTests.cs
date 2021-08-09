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
        [TestMethod()]
        public void BlockOverviewTest()
        {
            ITronScanClient client = new TronScanClient();
            TronBlockOverviewJsonResult result = client.BlockOverview();

            Assert.IsTrue(null != result);
        }
    }
}