using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bscscan client interface
    /// </summary>
    public interface IBscscanClient : IBscGasTracker
    {
        #region Public Methods

        /// <summary>
        /// set api key token
        /// </summary>
        /// <param name="apiKeyToken"></param>
        void SetApiKeyToken(string apiKeyToken);

        #endregion
    }
}
