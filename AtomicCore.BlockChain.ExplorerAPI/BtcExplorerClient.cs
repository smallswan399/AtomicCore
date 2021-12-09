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
        /// <param name="cacheSeconds"></param>
        /// <param name="cacheMode"></param>
        /// <returns></returns>
        public BtcSingleBlockResponse GetSingleBlock(string blockHash, int cacheSeconds = 0, ExplorerAPICacheMode cacheMode = ExplorerAPICacheMode.None)
        {
            if (string.IsNullOrEmpty(blockHash))
                throw new ArgumentNullException(nameof(blockHash));
            if (64 != blockHash.Length)
                throw new ArgumentException("illegal blockHash parameter format");

            BtcSingleBlockResponse resultData = null;
            if (cacheSeconds <= 0 || ExplorerAPICacheMode.None == cacheMode)
            {
                string url = $"{C_BLOCKCHAIN_INFOS}/rawblock/{blockHash}";
                string resp = RestGet(url);

                resultData = ObjectParse<BtcSingleBlockResponse>(resp);
            }
            else
            {
                string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetSingleBlock), blockHash);
                bool exists = ApiMsCacheProvider.Get(cacheKey, out resultData);
                if (!exists)
                {
                    string url = $"{C_BLOCKCHAIN_INFOS}/rawblock/{blockHash}";
                    string resp = RestGet(url);

                    resultData = ObjectParse<BtcSingleBlockResponse>(resp);

                    ApiMsCacheProvider.Set(cacheKey, resultData, cacheMode, TimeSpan.FromSeconds(cacheSeconds));
                }
            }

            return resultData;
        }

        /// <summary>
        /// UnspentOutputs
        /// </summary>
        /// <param name="address"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="cacheMode"></param>
        /// <returns></returns>
        public BtcUnspentOutputResponse UnspentOutputs(string address, int cacheSeconds = 0, ExplorerAPICacheMode cacheMode = ExplorerAPICacheMode.None)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            if (34 != address.Length)
                throw new ArgumentException("illegal address parameter format");

            BtcUnspentOutputResponse resultData = null;
            if (cacheSeconds <= 0 || ExplorerAPICacheMode.None == cacheMode)
            {
                string url = $"{C_BLOCKCHAIN_INFOS}/unspent?active={address}";
                string resp = RestGet(url);

                resultData = ObjectParse<BtcUnspentOutputResponse>(resp);
            }
            else
            {
                string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(UnspentOutputs), address);
                bool exists = ApiMsCacheProvider.Get(cacheKey, out resultData);
                if (!exists)
                {
                    string url = $"{C_BLOCKCHAIN_INFOS}/unspent?active={address}";
                    string resp = RestGet(url);

                    resultData = ObjectParse<BtcUnspentOutputResponse>(resp);

                    ApiMsCacheProvider.Set(cacheKey, resultData, cacheMode, TimeSpan.FromSeconds(cacheSeconds));
                }
            }

            return resultData;
        }

        /// <summary>
        /// Get Address Balance(BTC)
        /// </summary>
        /// <param name="address"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="cacheMode"></param>
        /// <returns></returns>
        public BtcAddressBalanceResponse GetAddressBalance(string address, int cacheSeconds = 0, ExplorerAPICacheMode cacheMode = ExplorerAPICacheMode.None)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            if (34 != address.Length)
                throw new ArgumentException("illegal address parameter format");

            BtcAddressBalanceResponse resultData = null;
            if (cacheSeconds <= 0 || ExplorerAPICacheMode.None == cacheMode)
            {
                string url = $"{C_APIREST_BASEURL}/haskoin-store/btc/address/{address}/balance";
                string resp = RestGet(url);

                resultData = ObjectParse<BtcAddressBalanceResponse>(resp);
            }
            else
            {
                string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressBalance), address);
                bool exists = ApiMsCacheProvider.Get(cacheKey, out resultData);
                if (!exists)
                {
                    string url = $"{C_APIREST_BASEURL}/haskoin-store/btc/address/{address}/balance";
                    string resp = RestGet(url);

                    resultData = ObjectParse<BtcAddressBalanceResponse>(resp);

                    ApiMsCacheProvider.Set(cacheKey, resultData, cacheMode, TimeSpan.FromSeconds(cacheSeconds));
                }
            }

            return resultData;
        }

        /// <summary>
        /// Get Address Txs
        /// </summary>
        /// <param name="address"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="cacheSeconds"></param>
        /// <param name="cacheMode"></param>
        /// <returns></returns>
        public BtcAddressTxsResponse GetAddressTxs(string address, int offset = 0, int limit = 0, int cacheSeconds = 0, ExplorerAPICacheMode cacheMode = ExplorerAPICacheMode.None)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));
            if (34 != address.Length)
                throw new ArgumentException("illegal address parameter format");

            BtcAddressTxsResponse resultData = null;
            if (cacheSeconds <= 0 || ExplorerAPICacheMode.None == cacheMode)
            {
                StringBuilder urlBuilder = new StringBuilder($"{C_APIREST_BASEURL}/haskoin-store/btc/address/{address}/transactions/full");
                if (offset > 0)
                    urlBuilder.Append($"offset={offset}");
                if (limit > 0)
                    urlBuilder.Append($"limit={limit}");

                string url = urlBuilder.ToString();
                string resp = RestGet(url);

                resultData = ObjectParse<BtcAddressTxsResponse>(resp);
            }
            else
            {
                string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressTxs), address, offset.ToString(), limit.ToString());
                bool exists = ApiMsCacheProvider.Get(cacheKey, out resultData);
                if (!exists)
                {
                    StringBuilder urlBuilder = new StringBuilder($"{C_APIREST_BASEURL}/haskoin-store/btc/address/{address}/transactions/full");
                    if (offset > 0)
                        urlBuilder.Append($"offset={offset}");
                    if (limit > 0)
                        urlBuilder.Append($"limit={limit}");

                    string url = urlBuilder.ToString();
                    string resp = RestGet(url);

                    resultData = ObjectParse<BtcAddressTxsResponse>(resp);

                    ApiMsCacheProvider.Set(cacheKey, resultData, cacheMode, TimeSpan.FromSeconds(cacheSeconds));
                }
            }

            return resultData;
        }

        #endregion
    }
}
