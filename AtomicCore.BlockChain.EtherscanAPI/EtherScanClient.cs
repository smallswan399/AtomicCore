using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// IEtherScanClient interface implementation
    /// </summary>
    public class EtherScanClient : IEtherScanClient
    {
        /// <summary>
        /// 获取网络手续费（三档）
        /// </summary>
        /// <returns></returns>
        public GasOracleJsonResult GetGasOracle()
        {
            throw new NotImplementedException();
        }
    }
}
