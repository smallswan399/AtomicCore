using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.TronNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet.Tests
{
    [TestClass()]
    public class TronECKeyTests
    {
        [TestMethod()]
        public void GenerateKeyTest()
        {
            var privateKey = TronTestAccountCollection.TestMain.PirvateKey;

            TronECKey mainKey = new TronECKey(privateKey, TronNetwork.MainNet);
            string address = mainKey.GetPublicAddress();

            Assert.IsTrue(TronTestAccountCollection.TestMain.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }
    }
}