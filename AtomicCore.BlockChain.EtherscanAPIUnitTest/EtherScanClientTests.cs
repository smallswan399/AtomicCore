using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.EtherscanAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.EtherscanAPI.Tests
{
    [TestClass()]
    public class EtherScanClientTests
    {
        private IEtherScanClient _client = new EtherScanClient("N4KR7R89K78AWKPBZY6WD27DDTTDB8YJ8W");

        [TestMethod()]
        public void GetGasOracleTest()
        {
            EtherscanJsonResult<GasOracleJsonResult> result = this._client.GetGasOracle();

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }
    }
}