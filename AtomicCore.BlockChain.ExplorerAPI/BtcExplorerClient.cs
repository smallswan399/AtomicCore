using System;
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
        /// https://blockchain.info
        /// </summary>
        private const string C_BLOCKCHAIN_INFOS = "https://blockchain.info";

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
        /// Get Block By Hash
        /// </summary>
        /// <param name="blockHash"></param>
        /// <param name="hex"></param>
        /// <returns></returns>
        public BtcSingleBlockResponse GetSingleBlock(string blockHash, bool hex = false)
        {
            if (string.IsNullOrEmpty(blockHash))
                throw new ArgumentNullException(nameof(blockHash));
            if (64 != blockHash.Length)
                throw new ArgumentException("illegal blockHash parameter format");

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetSingleBlock), blockHash);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out BtcSingleBlockResponse cacheData);
            if (!exists)
            {
                string url = $"{C_BLOCKCHAIN_INFOS}/rawblock/{blockHash}";
                if (hex)
                    url = $"{url}?format=hex";
                string resp = RestGet2(url);

                cacheData = ObjectParse<BtcSingleBlockResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Get Address Balance(BTC)
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public BtcAddressBalanceResponse GetAddressBalance(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            if (34 != address.Length)
                throw new ArgumentException("illegal address parameter format");

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressBalance), address);
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

        /// <summary>
        /// Get Address Txs
        /// </summary>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public BtcAddressTxsResponse GetAddressTxs(string address, int offset = 0, int limit = 0)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            if (34 != address.Length)
                throw new ArgumentException("illegal address parameter format");

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressTxs), address, offset.ToString(), limit.ToString());
            bool exists = ApiMsCacheProvider.Get(cacheKey, out BtcAddressTxsResponse cacheData);
            if (!exists)
            {
                StringBuilder urlBuilder = new StringBuilder($"{C_APIREST_BASEURL}/haskoin-store/btc/address/{address}/transactions/full");
                if (offset > 0)
                    urlBuilder.Append($"offset={offset}");
                if (limit > 0)
                    urlBuilder.Append($"limit={limit}");

                string url = urlBuilder.ToString();
                string resp = RestGet2(url);

                cacheData = ObjectParse<BtcAddressTxsResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        #endregion
    }
}
