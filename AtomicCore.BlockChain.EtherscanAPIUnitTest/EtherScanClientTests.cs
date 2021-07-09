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
            EtherscanSingleResult<EthGasOracleJsonResult> result = this._client.GetGasOracle();

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetBalanceTest()
        {
            EtherscanStructResult<decimal> result = this._client.GetBalance("0xcF62baF1237124d11740D4c89eF088C501FA102A");

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);

            result = this._client.GetBalance("0xcF62baF1237124d11740D4c89eF088C501FA102A", "0xA2b4C0Af19cC16a6CfAcCe81F192B024d625817D", 9);

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetTransactionsTest()
        {
            EtherscanListResult<EthNormalTransactionJsonResult> result = this._client.GetNormalTransactions("0xcF62baF1237124d11740D4c89eF088C501FA102A");

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetInternalTransactionsTest()
        {
            EtherscanListResult<EthInternalTransactionJsonResult> result = this._client.GetInternalTransactions("0x2c1ba59d6f58433fb1eaee7d20b26ed83bda51a3");

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetERC20TransactionsTest()
        {
            EtherscanListResult<EthErc20TransactionJsonResult> result = this._client.GetERC20Transactions("0xcF62baF1237124d11740D4c89eF088C501FA102A", "0xA2b4C0Af19cC16a6CfAcCe81F192B024d625817D");

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }
    }
}