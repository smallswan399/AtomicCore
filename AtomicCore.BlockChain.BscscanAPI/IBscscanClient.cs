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
        /// <returns></returns>
        BscscanSingleResult<BscGasOracleJson> GetGasOracle();

        #endregion
    }
}
