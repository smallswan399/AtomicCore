using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.TronNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Google.Protobuf;
using System.Buffers.Text;
using Newtonsoft.Json.Linq;

namespace AtomicCore.BlockChain.TronNet.Tests
{
    [TestClass()]
    public class TronNetWalletClientTests
    {
        #region Variables

        private readonly TronTestRecord _record;
        private readonly ITronNetWalletClient _wallet;
        Wallet.WalletClient _cli;

        #endregion

        #region Constructor

        public TronNetWalletClientTests()
        {
            _record = TronTestServiceExtension.GetMainRecord();
            _wallet = _record.TronClient.GetWallet();
            _cli = _wallet.GetProtocol();
        }

        #endregion

        [TestMethod()]
        public void GetHeadersTest()
        {
            Metadata headMetadata = _wallet.GetHeaders();

            Assert.IsTrue(headMetadata.Count > 0);
        }

        [TestMethod()]
        public void GetLastBlockTest()
        {
            Block block = _cli.GetNowBlock(new EmptyMessage(), headers: _wallet.GetHeaders());

            Assert.IsTrue(block.BlockHeader.RawData.Number > 0);
        }

        [TestMethod()]
        public void GetBlockByNumberTest()
        {
            Block block = _cli.GetBlockByNum(new NumberMessage()
            {
                Num = 1
            }, headers: _wallet.GetHeaders());

            string blockHash = block.GetBlockHash();

            Assert.IsTrue(!string.IsNullOrEmpty(blockHash));
            Assert.IsTrue(block.BlockHeader.RawData.Number > 0);
        }

        [TestMethod()]
        public void GetAccountBalanceTest1()
        {
            string ownerAddress = "TK7XWSuRi5PxYDUQ53L43baio7ZBWukcGm";

            Block block = _cli.GetNowBlock(new EmptyMessage(), headers: _wallet.GetHeaders());
            string blockHash = block.GetBlockHash();
            byte[] blockHashBytes = blockHash.HexToByteArray();

            //create tx
            AccountBalanceResponse balance = _cli.GetAccountBalance(new AccountBalanceRequest()
            {
                AccountIdentifier = new AccountIdentifier()
                {
                    Address = ByteString.CopyFrom(Base58Encoder.DecodeFromBase58Check(ownerAddress))
                },
                BlockIdentifier = new BlockBalanceTrace.Types.BlockIdentifier()
                {
                    Hash = ByteString.CopyFrom(blockHashBytes),
                    Number = block.BlockHeader.RawData.Number
                }
            }, headers: _wallet.GetHeaders());

            Assert.IsTrue(balance.Balance >= 0);
        }

        [TestMethod()]
        public void GetAccountBalanceTest2()
        {
            string ownerAddress = "TK7XWSuRi5PxYDUQ53L43baio7ZBWukcGm";

            BlockExtention block = _cli.GetNowBlock2(new EmptyMessage(), headers: _wallet.GetHeaders());

            //create tx
            AccountBalanceResponse balance = _cli.GetAccountBalance(new AccountBalanceRequest()
            {
                AccountIdentifier = new AccountIdentifier()
                {
                    Address = ByteString.CopyFrom(Base58Encoder.DecodeFromBase58Check(ownerAddress))
                },
                BlockIdentifier = new BlockBalanceTrace.Types.BlockIdentifier()
                {
                    Hash = block.Blockid,
                    Number = block.BlockHeader.RawData.Number
                }
            }, headers: _wallet.GetHeaders());

            Assert.IsTrue(balance.Balance >= 0);
        }

        [TestMethod()]
        public void GetTransactionByIdTest()
        {
            string txid = "31125a86fc2a1934a0fd9b1e9b238df23e29173745c11bb65741269dfb02690f";

            Transaction txInfo = _cli.GetTransactionById(new BytesMessage()
            {
                Value = ByteString.CopyFrom(txid.HexToByteArray())
            }, headers: _wallet.GetHeaders());

            string tx_id = txInfo.GetTxid();
            Assert.IsTrue(!string.IsNullOrEmpty(tx_id));

            //解析RawData数据
            long bh = txInfo.RawData.RefBlockNum;
            string ref_block_hash = txInfo.RawData.RefBlockHash.ToStringUtf8();
            long expTs = txInfo.RawData.Expiration;
            long timestamp = txInfo.RawData.Timestamp;
            var ref_block_byte = txInfo.RawData.RefBlockBytes.ToStringUtf8();

            //tx contract info
            Transaction.Types.Contract txContract = txInfo.RawData.Contract.FirstOrDefault();
            string contract_name = System.Text.Encoding.UTF8.GetString(txContract.ContractName.ToByteArray());
            string contract_type = txContract.Type.ToString();

            //解析Parameter.Value(谷歌协议数据格式)
            var contractTransfer = TransferContract.Parser.ParseFrom(txContract.Parameter.Value);
            string ownerAddress = contractTransfer.OwnerAddress.ToStringUtf8();
            string toAddress = contractTransfer.ToAddress.ToStringUtf8();
            long amount = contractTransfer.Amount;
        }
    }
}