using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// IEtherScanClient interface definition
    /// </summary>
    public interface IEtherScanClient
    {
        /// <summary>
        /// 获取网络手续费（三档）
        /// </summary>
        /// <returns></returns>
        GasOracleJsonResult GetGasOracle();
    }
}
