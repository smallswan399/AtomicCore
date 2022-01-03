using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bscscan client interface
    /// </summary>
    public interface IBscscanClient
    {
        #region Gas Tracker

        /// <summary>
        /// Returns the current Safe, Proposed and Fast gas prices. 
        /// </summary>
        /// <param name="apikey">apikey</param>
        /// <param name="network">network</param>
        /// <returns></returns>
        BscscanSingleResult<BscGasOracleJson> GetGasOracle(string apikey, BscNetwork network = BscNetwork.BscMainnet);

        #endregion
    }
}
