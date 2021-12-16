using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AtomicCore.BlockChain.BlockCypherAPI
{
    /// <summary>
    /// BlockCypher Service
    /// </summary>
    public class BlockCypherSrvAPI : IBlockCypherAPI
    {
        #region Variables

        /// <summary>
        /// application/json
        /// </summary>
        private const string APPLICATIONJSON = "application/json";

        /// <summary>
        /// agent url tmp
        /// </summary>
        private readonly string _agentGetTmp;

        /// <summary>
        /// agent url tmp
        /// </summary>
        private readonly string _agentPostTmp;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="agentGetTmp"></param>
        /// <param name="agentPostTmp"></param>
        public BlockCypherSrvAPI(string agentGetTmp = null, string agentPostTmp = null)
        {
            _agentGetTmp = agentGetTmp;
            _agentPostTmp = agentPostTmp;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 根据网络获取基础请求地址
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        private string GetBaseUrl(BlockCypherNetwork network)
        {
            string baseUrl;
            switch (network)
            {
                case BlockCypherNetwork.BtcMainnet:
                    baseUrl = "http://api.blockcypher.com/v1/btc/main";
                    break;
                case BlockCypherNetwork.BtcTestnet:
                    baseUrl = "http://api.blockcypher.com/v1/btc/test3";
                    break;
                case BlockCypherNetwork.DashMainnet:
                    baseUrl = "http://api.blockcypher.com/v1/dash/main";
                    break;
                case BlockCypherNetwork.DogeMainnet:
                    baseUrl = "http://api.blockcypher.com/v1/doge/main";
                    break;
                case BlockCypherNetwork.LiteMainnet:
                    baseUrl = "http://api.blockcypher.com/v1/ltc/main";
                    break;
                case BlockCypherNetwork.BcyMainnet:
                    baseUrl = "http://api.blockcypher.com/v1/bcy/test";
                    break;
                default:
                    throw new NotImplementedException();
            }

            return baseUrl;
        }

        /// <summary>
        /// create rest url
        /// </summary>
        /// <param name="network">version</param>
        /// <param name="actionUrl">action url</param>
        /// <returns></returns>
        private string GetRestUrl(BlockCypherNetwork network, string actionUrl)
        {
            string baseUrl = GetBaseUrl(network);
            return $"{baseUrl}/{actionUrl}".ToLower();
        }

        /// <summary>
        /// rest get request(Using HttpClient)
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        private string RestGet(string url)
        {
            string resp;
            try
            {
                string remoteUrl;
                if (string.IsNullOrEmpty(this._agentGetTmp))
                    remoteUrl = url;
                else
                {
                    string encodeUrl = UrlEncoder.UrlEncode(url);
                    remoteUrl = string.Format(this._agentGetTmp, encodeUrl);
                }

                using HttpClient cli = new HttpClient();
                HttpResponseMessage response = cli.GetAsync(remoteUrl).Result;
                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"StatusCode -> {response.StatusCode}, ");

                resp = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// rest post request(Using HttpClient)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string RestPost<T>(string url, T data)
        {
            string resp;
            try
            {
                string remoteUrl;
                if (string.IsNullOrEmpty(this._agentGetTmp))
                    remoteUrl = url;
                else
                {
                    string encodeUrl = UrlEncoder.UrlEncode(url);
                    remoteUrl = string.Format(this._agentGetTmp, encodeUrl);
                }

                using HttpClient cli = new HttpClient();
                HttpResponseMessage response = cli.PostAsync(remoteUrl, new StringContent(
                    Newtonsoft.Json.JsonConvert.SerializeObject(data),
                    Encoding.UTF8,
                    APPLICATIONJSON
                )).Result;

                if (!response.IsSuccessStatusCode)
                    throw new HttpRequestException($"StatusCode -> {response.StatusCode}, ");

                resp = response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// json -> check error propertys
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        private string HasResponseError(string resp)
        {
            JObject json_obj;
            try
            {
                json_obj = JObject.Parse(resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (json_obj.TryGetValue("error", StringComparison.OrdinalIgnoreCase, out JToken error_json))
                return error_json.ToString();

            return null;
        }

        /// <summary>
        /// json -> object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resp"></param>
        /// <returns></returns>
        private T ObjectParse<T>(string resp)
            where T : class, new()
        {
            T jsonResult;
            try
            {
                jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonResult;
        }

        #endregion

        #region NoCache Methods

        /// <summary>
        /// General information about a blockchain is available by GET-ing the base resource.
        /// https://www.blockcypher.com/dev/bitcoin/?shell#blockchain-api
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        private ChainEndpointResponse ChainEndpointNoCache(BlockCypherNetwork network)
        {
            string url = GetBaseUrl(network);
            string resp = RestGet(url);

            return ObjectParse<ChainEndpointResponse>(resp);
        }

        /// <summary>
        /// If you want more data on a particular block, you can use the Block Hash endpoint.
        /// https://www.blockcypher.com/dev/bitcoin/?shell#block-hash-endpoint
        /// </summary>
        /// <param name="network"></param>
        /// <param name="blockHash"></param>
        private BlockCypherBlockResponse BlockHashEndpointNoCache(BlockCypherNetwork network, string blockHash)
        {
            if (string.IsNullOrEmpty(blockHash))
                throw new ArgumentNullException(nameof(blockHash));

            string url = GetRestUrl(network, $"blocks/{blockHash}");
            string resp = RestGet(url);

            return ObjectParse<BlockCypherBlockResponse>(resp);
        }

        #endregion

        #region IBlockCypherAPI Methods

        /// <summary>
        /// General information about a blockchain is available by GET-ing the base resource.
        /// https://www.blockcypher.com/dev/bitcoin/?shell#blockchain-api
        /// </summary>
        /// <param name="network"></param>
        /// <param name="cacheMode"></param>
        /// <param name="cacheSeconds"></param>
        /// <returns></returns>
        public ChainEndpointResponse ChainEndpoint(BlockCypherNetwork network, BlockCypherCacheMode cacheMode = BlockCypherCacheMode.AbsoluteExpired, int cacheSeconds = 10)
        {
            if (cacheMode == BlockCypherCacheMode.None)
                return ChainEndpointNoCache(network);
            else
            {
                string cacheKey = BlockCypherCacheProvider.GenerateCacheKey(nameof(ChainEndpoint), network.ToString());
                bool exists = BlockCypherCacheProvider.Get(cacheKey, out ChainEndpointResponse cacheData);
                if (!exists)
                {
                    cacheData = ChainEndpointNoCache(network);

                    BlockCypherCacheProvider.Set(cacheKey, cacheData, cacheMode, TimeSpan.FromSeconds(cacheSeconds));
                }

                return cacheData;
            }
        }

        /// <summary>
        /// If you want more data on a particular block, you can use the Block Hash endpoint.
        /// https://www.blockcypher.com/dev/bitcoin/?shell#block-hash-endpoint
        /// </summary>
        /// <param name="network"></param>
        /// <param name="blockHash"></param>
        /// <param name="cacheMode"></param>
        /// <param name="cacheSeconds"></param>
        /// <returns></returns>
        public BlockCypherBlockResponse BlockHashEndpoint(BlockCypherNetwork network, string blockHash, BlockCypherCacheMode cacheMode = BlockCypherCacheMode.AbsoluteExpired, int cacheSeconds = 10)
        {
            if (cacheMode == BlockCypherCacheMode.None)
                return BlockHashEndpointNoCache(network, blockHash);
            else
            {
                string cacheKey = BlockCypherCacheProvider.GenerateCacheKey(nameof(BlockHashEndpoint), blockHash);
                bool exists = BlockCypherCacheProvider.Get(cacheKey, out BlockCypherBlockResponse cacheData);
                if (!exists)
                {
                    cacheData = BlockHashEndpointNoCache(network, blockHash);

                    BlockCypherCacheProvider.Set(cacheKey, cacheData, cacheMode, TimeSpan.FromSeconds(cacheSeconds));
                }

                return cacheData;
            }
        }

        #endregion
    }
}
