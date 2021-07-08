using System.Collections.Generic;

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
        EtherscanJsonResult<EthGasOracleJsonResult> GetGasOracle();

        /// <summary>
        /// 获取交易列表（根据地址）
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="fromBlock">起始区块</param>
        /// <param name="toBlock">结束区块</param>
        /// <param name="sort">排序规则</param>
        /// <param name="page">当前页码</param>
        /// <param name="limit">每页容量</param>
        /// <returns></returns>
        EtherscanListResult<EthTransactionJsonResult> GetTransactions(string address, ulong? fromBlock = null, ulong? toBlock = null, EtherscanSort sort = EtherscanSort.Asc, int? page = 1, int? limit = 1000);
    }
}
