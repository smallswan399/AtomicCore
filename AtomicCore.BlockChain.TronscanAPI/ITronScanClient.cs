using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// TRON SCAN INTERFACE
    /// </summary>
    public interface ITronScanClient
    {
        /// <summary>
        /// 1.Block Overview
        /// </summary>
        /// <returns></returns>
        TronChainOverviewJson BlockOverview();

        /// <summary>
        /// 2.Get Last Block
        /// </summary>
        /// <returns></returns>
        TronBlockBasicJson GetLastBlock();

        /// <summary>
        /// Get Account Assets list
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        TronAccountAssetJson GetAccountAssets(string address);

        //void GetAddressAssets(string address);

        //void GetNormalTransactions();
    }
}
