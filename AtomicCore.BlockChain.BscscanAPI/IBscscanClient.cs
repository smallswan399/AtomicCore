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
        #region Public Methods

        /// <summary>
        /// set api key token
        /// </summary>
        /// <param name="apiKeyToken"></param>
        void SetApiKeyToken(string apiKeyToken);

        #endregion

        #region Gas Tracker

        /// <summary>
        /// Returns the current Safe, Proposed and Fast gas prices. 
        /// </summary>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        BscscanSingleResult<BscGasOracleJson> GetGasOracle(BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10);

        #endregion
    }
}
