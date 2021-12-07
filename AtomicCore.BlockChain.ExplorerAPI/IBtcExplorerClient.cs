using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.ExplorerAPI
{
    /// <summary>
    /// IBlockChainExplorer
    /// </summary>
    public interface IBtcExplorerClient
    {
        /// <summary>
        /// Get Address Balance(BTC)
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        BtcAddressBalanceResponse GetAddressBalance(string address);

        /// <summary>
        /// Get Address Txs
        /// </summary>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        BtcAddressTxsResponse GetAddressTxs(string address, int offset = 0, int limit = 0);
    }
}
