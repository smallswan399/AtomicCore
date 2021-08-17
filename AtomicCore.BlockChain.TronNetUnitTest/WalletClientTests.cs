using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.TronNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace AtomicCore.BlockChain.TronNet.Tests
{
    [TestClass()]
    public class WalletClientTests
    {
        private readonly TronTestRecord _record;
        private readonly IWalletClient _wallet;
        private readonly Wallet.WalletClient _walletProtocol;

        public WalletClientTests()
        {
            _record = TronTestServiceExtension.GetMainRecord();
            _wallet = _record.TronClient.GetWallet();
            _walletProtocol = _wallet.GetProtocol();
        }

        /// <summary>
        /// Get Block By Number 
        /// </summary>
        [TestMethod()]
        public void GetBlockByNumber()
        {
            string blockID = "0000000001f618ad6a2d6492db91395ee6cb9c1ea8c4a38c456aa3aa57b592e5";
            long blockNumber = 32905389;

            Block block = _walletProtocol.GetBlockByNum(new NumberMessage()
            {
                Num = blockNumber
            }, headers: _wallet.GetHeaders());

            string currentBlockHash = block.GetBlockHash();

            Assert.IsTrue(blockID.Equals(currentBlockHash, StringComparison.OrdinalIgnoreCase));
        }
    }
}