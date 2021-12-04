using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.OMNINet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.OMNINet.Tests
{
    [TestClass()]
    public class CoinServiceTests
    {
        [TestMethod()]
        public void SendRawTransactionTest()
        {
            IOMNICoinService service = new OMNICoinService();
            string txid = service.SendRawTransaction("02000000000101ea7df99c92dad992f80488f408c9ef182f7aff77d779a9d70ba2903af943a7fc0100000017160014babc86ca0dcae4249a43cdcf71ed2f92b6c3ebf9ffffffff02d00700000000000017a91422e11bdd6cfb8d599cea5b6a658e617570f574a8870b264c000000000017a914b865a1c836063041f2745531722e232c4ef19d5c8702483045022100f02381a152d42e7f91c6a9f0b80c62713b62e62a13b99b74552ae1e9d43a64fa02204bb4a730208847210e2f8d0dad3bd5e0e1933b59e7d94e24c5cd993bfc09c2c1012103002b1077137aaf02aaa5089dd45dab137dbbadc2e75bf9f0a14982e6e0c7a0bd00000000");

            Assert.IsTrue(string.IsNullOrEmpty(txid));
        }
    }
}