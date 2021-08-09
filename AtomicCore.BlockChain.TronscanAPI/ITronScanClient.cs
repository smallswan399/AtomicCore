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
        /// Block Overview
        /// </summary>
        /// <returns></returns>
        TronscanSingleResult<TronBlockOverviewJsonResult> BlockOverview();

        //void GetAddressAssets(string address);

        //void GetNormalTransactions();
    }
}
