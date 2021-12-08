using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.ExplorerAPI
{
    /// <summary>
    /// IBlockChainExplorer
    /// </summary>
    /// <remarks>
    /// https://www.blockchain.com/api/blockchain_api
    /// </remarks>
    public interface IBtcExplorerClient
    {
        /// <summary>
        /// Get Single Block By Hash
        /// </summary>
        /// <param name="blockHash"></param>
        /// <param name="hex"></param>
        /// <returns></returns>
        BtcSingleBlockResponse GetSingleBlock(string blockHash, bool hex = false);

        /// <summary>
        /// Unspent Outputs
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        BtcUnspentOutputResponse UnspentOutputs(string address);

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
