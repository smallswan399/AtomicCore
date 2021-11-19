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

        [TestMethod()]
        public void GetUnsignedTest()
        {
            IOmniScanClient client = new OmniScanClient();
            var result = client.GetUnsigned("01000000011c2d89d15d4853194e0e617e5d6d6d151752b09f72bf62a34453643270ebc763000000001976a914946cb2e08075bcbaf157e47bcb67eb2b2339d24288acffffffff01d8d60000000000001976a914946cb2e08075bcbaf157e47bcb67eb2b2339d24288ac00000000", "04ad90e5b6bc86b3ec7fac2c5fbda7423fc8ef0d58df594c773fa05e2c281b2bfe877677c668bd13603944e34f4818ee03cadd81a88542b8b4d5431264180e2c28");

            Assert.IsTrue(null != result);
        }

        [TestMethod()]
        public void GetRawtransactionTest()
        {
            IOmniScanClient client = new OmniScanClient();
            var result = client.GetRawtransaction("armory_tx======TXSIGCOLLECT-7fK9x5wF======================================AQAAAPm+tNkAAAAAAf2gAQEAAAD5vrTZHC2J0V1IUxlODmF+XW1tFRdSsJ9yv2KjRFNkMnDrx2MAAAAA/SUBAQAAAAHb6cvxUOY3xjmumbvja4ws2Y91+lIUyjV3kswGZmxHNAAAAABqRzBEAiAWH+zK7+MqKy/RpARlwAxlkGmB2oI5jwXCzwXlrgoDdwIgRIt5APjkheiGF64yCgGDXHpLdlB7cYRgvuEePdRvee0BIQIx3eVJDpRTFPeTFz1zD/sd7M8z0/5dZUFpNFHpPFrinv////8EYOoAAAAAAAAZdqkUlGyy4IB1vLrxV+R7y2frKyM50kKIrJB3dAEAAAAAGXapFJHmmj4F756OJPTktKHCmlUL83kWiKxg6gAAAAAAABl2qRSCgUFaaIQzk5laBF7zFYAlb2gEyYisYOoAAAAAAAAZdqkUgQAAAAAAAAACAAAAAAX14QAAAACIrAAAAAAAAAD/////AUEErZDltryGs+x/rCxfvadCP8jvDVjfWUx3P6BeLCgbK/6HdnfGaL0TYDlE409IGO4Dyt2BqIVCuLTVQxJkGA4sKAAAATQBAAAA+b602Rl2qRSUbLLggHW8uvFX5HvLZ+srIznSQois2NYAAAAAAAAAAAROT05FAAAAAAAAAA==================================================================");

            Assert.IsTrue(null != result);
        }
    }
}