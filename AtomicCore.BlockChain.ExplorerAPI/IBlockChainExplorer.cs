using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.ExplorerAPI
{
    /// <summary>
    /// IBlockChainExplorer
    /// </summary>
    public interface IBlockChainExplorer
    {
        /// <summary>
        /// Get Address Balance(BTC)
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        BtcAddressBalanceResponse GetAddressBTCBalance(string address);
    }
}
