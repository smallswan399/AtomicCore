using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AtomicCore.BlockChain.ExplorerAPI
{
    /// <summary>
    /// BlockChain Explorer
    /// </summary>
    public class BtcExplorerClient : BaseExplorerClient, IBtcExplorerClient
    {
        #region Variables

        /// <summary>
        /// cache seconds
        /// </summary>
        private const int c_cacheSeconds = 10;

        /// <summary>
        /// api rest base url
        /// </summary>
        private const string C_APIREST_BASEURL = "https://api.blockchain.info";

        #endregion

        #region Constructor

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="agentGetTmp"></param>
        /// <param name="agentPostTmp"></param>
        public BtcExplorerClient(string agentGetTmp = null, string agentPostTmp = null) 
            : base(agentGetTmp, agentPostTmp)
        {

        }

        #endregion

        #region IBlockChainExplorer

        /// <summary>
        /// Get Address Balance(BTC)
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public BtcAddressBalanceResponse GetAddressBTCBalance(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            if (34 != address.Length)
                throw new ArgumentException("illegal address parameter format");

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressBTCBalance), address);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out BtcAddressBalanceResponse cacheData);
            if (!exists)
            {
                string url = $"{C_APIREST_BASEURL}/haskoin-store/btc/address/{address}/balance";
                string resp = RestGet2(url);

                cacheData = ObjectParse<BtcAddressBalanceResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        #endregion
    }
}
