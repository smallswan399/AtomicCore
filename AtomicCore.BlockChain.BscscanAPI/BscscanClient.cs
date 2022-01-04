using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bscscan client service
    /// </summary>
    public class BscscanClient : IBscscanClient
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

        /// <summary>
        /// api key token 
        /// </summary>
        private string _apiKeyToken = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="agentGetTmp"></param>
        /// <param name="agentPostTmp"></param>
        public BscscanClient(string agentGetTmp = null, string agentPostTmp = null)
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
        private string GetBaseUrl(BscNetwork network)
        {
            string baseUrl = network switch
            {
                BscNetwork.BscMainnet => "https://api.bscscan.com",
                BscNetwork.BscTestnet => "https://api-testnet.bscscan.com",
                _ => throw new NotImplementedException(),
            };

            return baseUrl;
        }

        /// <summary>
        /// create rest url
        /// </summary>
        /// <param name="network">version</param>
        /// <param name="module">module</param>
        /// <param name="action">action</param>
        /// <param name="data">query data</param>
        /// <returns></returns>
        private string GetRestUrl(BscNetwork network, BscModule module, string action, Dictionary<string, string> data = null)
        {
            string baseUrl = GetBaseUrl(network);

            if (null == data || data.Count <= 0)
                return $"{baseUrl}/api?module={module}&action={action}&apikey={_apiKeyToken}".ToLower();
            else
                return $"{baseUrl}/api?module={module}&action={action}&apikey={_apiKeyToken}&{string.Join("&", data.Select(s => $"{s.Key}={s.Value}"))}".ToLower();
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

        #region No Cache Methods

        /// <summary>
        /// Get Balance
        /// </summary>
        /// <param name="address">the string representing the address to check for balance</param>
        /// <param name="tag">the string pre-defined block parameter, either earliest, pending or latest</param>
        /// <param name="network">network</param>
        /// <returns></returns>
        private BscscanSingleResult<decimal> GetBalance(string address, BscBlockTag tag, BscNetwork network = BscNetwork.BscMainnet)
        {
            //拼接URL
            string url = this.GetRestUrl(network, BscModule.Accounts, "balance", new Dictionary<string, string>()
            {
                { "address",address },
                { "tag",tag.ToString().ToLower() }
            });

            //请求API
            string resp = this.RestGet(url);

            //解析JSON
            BscscanSingleResult<decimal> jsonResult = ObjectParse<BscscanSingleResult<decimal>>(resp);

            return jsonResult;
        }

        /// <summary>
        /// Get Balance
        /// </summary>
        /// <param name="address"></param>
        /// <param name="tag">the string pre-defined block parameter, either earliest, pending or latest</param>
        /// <param name="network">network</param>
        /// <returns></returns>
        private BscscanListResult<BscAccountBalanceJson> GetBalanceList(string[] address, BscBlockTag tag = BscBlockTag.Latest, BscNetwork network = BscNetwork.BscMainnet)
        {
            //拼接URL
            string url = this.GetRestUrl(network, BscModule.Accounts, "balance", new Dictionary<string, string>()
            {
                { "address",string.Join(",",address) },
                { "tag",tag.ToString().ToLower() }
            });

            //请求API
            string resp = this.RestGet(url);

            //解析JSON
            BscscanListResult<BscAccountBalanceJson> jsonResult = ObjectParse<BscscanListResult<BscAccountBalanceJson>>(resp);

            return jsonResult;
        }

        /// <summary>
        /// Returns the list of transactions performed by an address, with optional pagination.
        ///     Note : This API endpoint returns a maximum of 10000 records only.
        ///     Tip: Specify a smaller startblock and endblock range for faster search results.
        /// </summary>
        /// <param name="address">the string representing the addresses to check for balance</param>
        /// <param name="startblock">the integer block number to start searching for transactions</param>
        /// <param name="endblock">the integer block number to stop searching for transactions</param>
        /// <param name="page">the integer page number, if pagination is enabled</param>
        /// <param name="offset">the number of transactions displayed per page</param>
        /// <param name="sort">the sorting preference, use asc to sort by ascending and desc to sort by descending</param>
        /// <param name="network">network</param>
        /// <returns></returns>
        private BscscanListResult<BscTransactionJson> GetNormalTransactionByAddress(string address, int startblock = 0, int endblock = int.MaxValue, int page = 1, int offset = 10000, BscSort sort = BscSort.Desc, BscNetwork network = BscNetwork.BscMainnet)
        {
            //拼接URL
            string url = this.GetRestUrl(network, BscModule.Accounts, "txlist", new Dictionary<string, string>()
            {
                { "address",address },
                { "startblock",startblock.ToString() },
                { "endblock",endblock.ToString() },
                { "page",page.ToString() },
                { "offset",offset.ToString() },
                { "sort",sort.ToString().ToLower() }
            });

            //请求API
            string resp = this.RestGet(url);

            //解析JSON
            BscscanListResult<BscTransactionJson> jsonResult = ObjectParse<BscscanListResult<BscTransactionJson>>(resp);

            return jsonResult;
        }

        /// <summary>
        /// Returns the current Safe, Proposed and Fast gas prices. 
        /// </summary>
        /// <param name="network">network</param>
        /// <returns></returns>
        private BscscanSingleResult<BscGasOracleJson> GetGasOracle(BscNetwork network = BscNetwork.BscMainnet)
        {
            //拼接URL
            string url = this.GetRestUrl(network, BscModule.GasTracker, "gasoracle");

            //请求API
            string resp = this.RestGet(url);

            //解析JSON
            BscscanSingleResult<BscGasOracleJson> jsonResult = ObjectParse<BscscanSingleResult<BscGasOracleJson>>(resp);

            return jsonResult;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// set api key token
        /// </summary>
        /// <param name="apiKeyToken"></param>
        public void SetApiKeyToken(string apiKeyToken)
        {
            _apiKeyToken = apiKeyToken;
        }

        #endregion

        #region IBscAccounts

        /// <summary>
        /// Get Balance
        /// </summary>
        /// <param name="address">the string representing the address to check for balance</param>
        /// <param name="tag">the string pre-defined block parameter, either earliest, pending or latest</param>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        public BscscanSingleResult<decimal> GetBalance(string address, BscBlockTag tag = BscBlockTag.Latest, BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10)
        {
            if (cacheMode == BscscanCacheMode.None)
                return GetBalance(address, tag, network);
            else
            {
                string cacheKey = BscscanCacheProvider.GenerateCacheKey(nameof(GetBalance), network.ToString());
                bool exists = BscscanCacheProvider.Get(cacheKey, out BscscanSingleResult<decimal> cacheData);
                if (!exists)
                {
                    cacheData = GetBalance(address, tag, network);
                    BscscanCacheProvider.Set(cacheKey, cacheData, cacheMode, TimeSpan.FromSeconds(expiredSeconds));
                }

                return cacheData;
            }
        }

        /// <summary>
        /// Get Balance List
        /// </summary>
        /// <param name="address"></param>
        /// <param name="tag">the string pre-defined block parameter, either earliest, pending or latest</param>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        public BscscanListResult<BscAccountBalanceJson> GetBalanceList(string[] address, BscBlockTag tag = BscBlockTag.Latest, BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10)
        {
            if (cacheMode == BscscanCacheMode.None)
                return GetBalanceList(address, tag, network);
            else
            {
                string cacheKey = BscscanCacheProvider.GenerateCacheKey(nameof(GetBalanceList), network.ToString());
                bool exists = BscscanCacheProvider.Get(cacheKey, out BscscanListResult<BscAccountBalanceJson> cacheData);
                if (!exists)
                {
                    cacheData = GetBalanceList(address, tag, network);
                    BscscanCacheProvider.Set(cacheKey, cacheData, cacheMode, TimeSpan.FromSeconds(expiredSeconds));
                }

                return cacheData;
            }
        }

        /// <summary>
        /// Returns the list of transactions performed by an address, with optional pagination.
        ///     Note : This API endpoint returns a maximum of 10000 records only.
        ///     Tip: Specify a smaller startblock and endblock range for faster search results.
        /// </summary>
        /// <param name="address">the string representing the addresses to check for balance</param>
        /// <param name="startblock">the integer block number to start searching for transactions</param>
        /// <param name="endblock">the integer block number to stop searching for transactions</param>
        /// <param name="page">the integer page number, if pagination is enabled</param>
        /// <param name="offset">the number of transactions displayed per page</param>
        /// <param name="sort">the sorting preference, use asc to sort by ascending and desc to sort by descending</param>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        public BscscanListResult<BscTransactionJson> GetNormalTransactionByAddress(string address, int startblock = 0, int endblock = int.MaxValue, int page = 1, int offset = 10000, BscSort sort = BscSort.Desc, BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10)
        {
            if (cacheMode == BscscanCacheMode.None)
                return GetNormalTransactionByAddress(address, startblock, endblock, page, offset, sort, network);
            else
            {
                string cacheKey = BscscanCacheProvider.GenerateCacheKey(nameof(GetNormalTransactionByAddress), network.ToString());
                bool exists = BscscanCacheProvider.Get(cacheKey, out BscscanListResult<BscTransactionJson> cacheData);
                if (!exists)
                {
                    cacheData = GetNormalTransactionByAddress(address, startblock, endblock, page, offset, sort, network);
                    BscscanCacheProvider.Set(cacheKey, cacheData, cacheMode, TimeSpan.FromSeconds(expiredSeconds));
                }

                return cacheData;
            }
        }

        #endregion

        #region IBscGasTracker

        /// <summary>
        /// Returns the current Safe, Proposed and Fast gas prices. 
        /// </summary>
        /// <param name="apikey">apikey</param>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        public BscscanSingleResult<BscGasOracleJson> GetGasOracle(BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10)
        {
            if (cacheMode == BscscanCacheMode.None)
                return GetGasOracle(network);
            else
            {
                string cacheKey = BscscanCacheProvider.GenerateCacheKey(nameof(GetGasOracle), network.ToString());
                bool exists = BscscanCacheProvider.Get(cacheKey, out BscscanSingleResult<BscGasOracleJson> cacheData);
                if (!exists)
                {
                    cacheData = GetGasOracle(network);
                    BscscanCacheProvider.Set(cacheKey, cacheData, cacheMode, TimeSpan.FromSeconds(expiredSeconds));
                }

                return cacheData;
            }
        }

        #endregion

    }
}
