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
        public void UseWebAgent()
        {
            IEtherScanClient _client = new EtherScanClient("N4KR7R89K78AWKPBZY6WD27DDTTDB8YJ8W", EtherScanClient.c_eth_goerli, "http://129.226.189.12/Remote/Get?url={0}");

            var result = _client.GetBalance("0x29aAe16abfDC4C6F119E86D09ab8603D491c5d5F");

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetGasOracleTest()
        {
            EtherscanSingleResult<EthGasOracleJsonResult> result = this._client.GetGasOracle();

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GoerliTransactionTest()
        {
            IEtherScanClient _client = new EtherScanClient("N4KR7R89K78AWKPBZY6WD27DDTTDB8YJ8W", EtherScanClient.c_eth_goerli);
            var result = _client.GetNormalTransactions("0x29aAe16abfDC4C6F119E86D09ab8603D491c5d5F");

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }

        [TestMethod()]
        public void GetBalanceTest()
        {
            EtherscanSingleResult<decimal> result = this._client.GetBalance("0xcF62baF1237124d11740D4c89eF088C501FA102A");

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

        [TestMethod()]
        public void GetContractAbiTest()
        {
            EtherscanSingleResult<string> result = this._client.GetContractAbi("0xfeffbc959961b6e24cbaf8a91a6ca6abd1c3ffc5");

            Assert.IsTrue(result.Status == EtherscanJsonStatus.Success);
        }
    }
}