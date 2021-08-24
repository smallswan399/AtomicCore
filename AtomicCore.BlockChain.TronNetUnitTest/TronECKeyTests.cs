using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.TronNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AtomicCore.BlockChain.TronNet.Tests
{
    [TestClass()]
    public class TronECKeyTests
    {
        [TestMethod()]
        public void GenerateKeyTest()
        {
            var privateKey = TronTestAccountCollection.TestMain.PirvateKey;

            TronNetECKey mainKey = new TronECKey(privateKey, TronNetwork.MainNet);
            string address = mainKey.GetPublicAddress();

            Assert.IsTrue(TronTestAccountCollection.TestMain.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
        }

        [TestMethod()]
        public void ConvertToTronAddressTest()
        {
            string date = "a9059cbb0000000000000000000000000e36621fbc6c3df109e8125282c84343f056420e00000000000000000000000000000000000000000000000000000000004c4b40";

            string eth_address = string.Format("0x{0}", Regex.Replace(date.Substring(8, 64), @"^0*", string.Empty));

            string tronAddress = TronNetECKey.ConvertToTronAddress(eth_address);

            Assert.IsTrue("TBGMcMy84rxZijiQyjMDEJddMGgGoDQGH5".Equals(tronAddress, StringComparison.OrdinalIgnoreCase));
        }
    }
}