﻿namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bscscan client interface
    /// </summary>
    public interface IBscscanClient : IBscAccounts, IBscGasTracker
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
