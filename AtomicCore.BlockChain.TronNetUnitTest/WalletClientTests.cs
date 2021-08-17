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
        private const string blk_hash_mainnet = "0000000001f618ad6a2d6492db91395ee6cb9c1ea8c4a38c456aa3aa57b592e5";
        private const long bh_mainnet = 32905389;
        private const string txid_mainnet = "bfe65fb20e26225345ab6e20c9852ce5af411e5b18245b8a6abbeafb3582d802";

        private readonly TronTestRecord _record;
        private readonly ITronRestAPI _restAPI;
        private readonly IWalletClient _wallet;
        private readonly Wallet.WalletClient _walletProtocol;

        public WalletClientTests()
        {
            _record = TronTestServiceExtension.GetMainRecord();
            _restAPI = _record.TronClient.GetRestAPI();
            _wallet = _record.TronClient.GetWallet();
            _walletProtocol = _wallet.GetProtocol();
        }

        /// <summary>
        /// Get Current Block
        /// </summary>
        [TestMethod()]
        public void GetCurrentBlock()
        {
            BlockExtention block = _walletProtocol.GetNowBlock2(new EmptyMessage());

            string blockHash = block.Blockid.ToByteArray().ToHex();

            Assert.IsTrue(!string.IsNullOrEmpty(blockHash));
        }

        /// <summary>
        /// Get Block By Number 
        /// </summary>
        [TestMethod()]
        public void GetBlockByNumber()
        {
            BlockExtention block = _walletProtocol.GetBlockByNum2(new NumberMessage()
            {
                Num = bh_mainnet
            }, headers: _wallet.GetHeaders());

            string currentBlockHash = block.Blockid.ToByteArray().ToHex();

            Assert.IsTrue(blk_hash_mainnet.Equals(currentBlockHash, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Get Block By Hash
        /// </summary>
        [TestMethod()]
        public void GetBlockByHash()
        {
            Block block = _walletProtocol.GetBlockById(new BytesMessage()
            {
                Value = ByteString.CopyFrom(blk_hash_mainnet.HexToByteArray())
            });

            Assert.IsTrue(block.BlockHeader.RawData.Number == bh_mainnet);
        }

        /// <summary>
        /// Get Transaction
        /// GRPC not available, use REST API
        /// </summary>
        [TestMethod()]
        public void GetTransaction()
        {
            TronTransactionRestJson txInfo = _restAPI.GetTransactionByID(txid_mainnet);

            Assert.IsTrue(txInfo.TxID.Equals(txid_mainnet, StringComparison.OrdinalIgnoreCase));
        }
    }
}