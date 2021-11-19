using Microsoft.VisualStudio.TestTools.UnitTesting;
using AtomicCore.BlockChain.OmniscanAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.OmniscanAPI.Tests
{
    [TestClass()]
    public class OmniScanClientTests
    {
        [TestMethod()]
        public void GetAddressV1Test()
        {
            IOmniScanClient client = new OmniScanClient();
            var result = client.GetAddressV1("1KYiKJEfdJtap9QX2v9BXJMpz2SfU4pgZw");

            Assert.IsTrue(null != result);
        }

        [TestMethod()]
        public void GetAddressV2Test()
        {
            IOmniScanClient client = new OmniScanClient();
            var result = client.GetAddressV2("1KYiKJEfdJtap9QX2v9BXJMpz2SfU4pgZw", "1FoWyxwPXuj4C6abqwhjDWdz6D4PZgYRjA");

            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod()]
        public void GetAddressDetailsTest()
        {
            IOmniScanClient client = new OmniScanClient();
            var result = client.GetAddressDetails("1KYiKJEfdJtap9QX2v9BXJMpz2SfU4pgZw");

            Assert.IsTrue(!string.IsNullOrEmpty(result.Address));
        }
    }
}