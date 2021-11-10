#if NET472_OR_GREATER
using System.Configuration;
#else
using Microsoft.Extensions.Configuration;
#endif
using System;
using System.IO;

namespace AtomicCore.BlockChain.OMNINet
{
    /// <summary>
    /// coin rpc setting
    /// </summary>
    public class CoinRpcSetting
    {
        #region Variable

        /// <summary>
        /// rpc-url
        /// </summary>
        private const string key_rpc_Url = "rpc_url";

        /// <summary>
        /// rpc-url-test
        /// </summary>
        private const string key_rpc_Url_test = "rpc_url_test";

        /// <summary>
        /// rpc-username
        /// </summary>
        private const string key_rpc_username = "rpc_username";

        /// <summary>
        /// rpc-password
        /// </summary>
        private const string key_rpc_password = "rpc_password";

        /// <summary>
        /// rpc-timeout
        /// </summary>
        private const string key_rpc_timeout = "rpc_timeout";

        /// <summary>
        /// wallet-password
        /// </summary>
        private const string key_rpc_wallet_password = "wallet_password";

        #endregion

        #region Propertys

        /// <summary>
        /// rpc url
        /// </summary>
        public string RpcUrl { get; set; }

        /// <summary>
        /// rpc userName
        /// </summary>
        public string RpcUserName { get; set; }

        /// <summary>
        /// rpc password
        /// </summary>
        public string RpcPassword { get; set; }

        /// <summary>
        /// rpc timeout (seconds)
        /// </summary>
        public int RpcTimeout { get; set; } = 60;

        /// <summary>
        /// wallet password
        /// </summary>
        public string WalletPassword { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// load from config
        /// </summary>
        /// <returns></returns>
        public static CoinRpcSetting LoadFromConfig()
        {
            CoinRpcSetting cfg = new CoinRpcSetting();

#if NET472_OR_GREATER

#else
using Microsoft.Extensions.Configuration;
#endif

            return cfg;
        }

        #endregion
    }
}
