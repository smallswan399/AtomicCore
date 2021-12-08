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
        /// cache seconds short(10 seconds)
        /// </summary>
        private const int c_cacheSeconds_short = 10;

        /// <summary>
        /// cache seconds of new blocks(10 minutes a new block,half is 5 minutes)
        /// </summary>
        private const int c_cacheSeconds_halfNewBlockTimes = 300;

        /// <summary>
        /// Blockchain Data API, eg : https://blockchain.info
        /// </summary>
        private const string C_BLOCKCHAIN_INFOS = "https://blockchain.info";

        /// <summary>
        /// api rest base url, eg : https://api.blockchain.info
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
        /// <returns></returns>
        public BtcSingleBlockResponse GetSingleBlock(string blockHash)
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
                string resp = RestGet(url);

                cacheData = ObjectParse<BtcSingleBlockResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds_short));
            }

            return cacheData;
        }

        /// <summary>
        /// Unspent Outputs
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public BtcUnspentOutputResponse UnspentOutputs(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            if (34 != address.Length)
                throw new ArgumentException("illegal address parameter format");

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(UnspentOutputs), address);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out BtcUnspentOutputResponse cacheData);
            if (!exists)
            {
                string url = $"{C_BLOCKCHAIN_INFOS}/unspent?active={address}";
                string resp = RestGet(url);

                cacheData = ObjectParse<BtcUnspentOutputResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds_halfNewBlockTimes));
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
                string resp = RestGet(url);

                cacheData = ObjectParse<BtcAddressBalanceResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds_short));
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
                string resp = RestGet(url);

                cacheData = ObjectParse<BtcAddressTxsResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds_short));
            }

            return cacheData;
        }

        #endregion
    }
}
