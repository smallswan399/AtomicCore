using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace AtomicCore.BlockChain.TronNet.Tests
{
    [TestClass()]
    public class TRC20ContractClientTest
    {
        private TronTestRecord _record;
        private IWalletClient _wallet;
        private IContractClientFactory _contractClientFactory;

        public TRC20ContractClientTest()
        {
            _record = TronTestServiceExtension.GetTestRecord();
            _wallet = _record.ServiceProvider.GetService<IWalletClient>();
            _contractClientFactory = _record.ServiceProvider.GetService<IContractClientFactory>();
        }

        [TestMethod()]
        public async Task TestTransferAsync()
        {
            string privateKey = "1bf5134ffaedae943b8d2b2d5a19fd067210dd7ebf9ead392681a651b53eef75";
            ITronAccount account = _wallet.GetAccount(privateKey);
            //Assert.True(!string.IsNullOrEmpty(account.Address));

            string contractAddress = "TR7NHqjeKQxGTCi8q8ZY4pL8otSzgjLj6t"; //USDT Contract Address
            string to = "TYBp938TjQyndAcmmHVoq6eJBXoqi1yDuZ";
            decimal amount = 10M; //USDT Amount
            long feeAmount = 5L * 1000000L;

            var contractClient = _contractClientFactory.CreateClient(ContractProtocol.TRC20);

            var result = await contractClient.TransferAsync(contractAddress, account, to, amount, string.Empty, feeAmount);

            Assert.IsNotNull(result);
        }
    }
}
