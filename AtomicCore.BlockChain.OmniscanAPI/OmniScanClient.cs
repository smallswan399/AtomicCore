using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomicCore.BlockChain.OmniscanAPI
{
    /// <summary>
    /// omni scan client service
    /// </summary>
    public class OmniScanClient : IOmniScanClient
    {
        #region Variables

        /// <summary>
        /// cache seconds
        /// </summary>
        private const int c_cacheSeconds = 10;

        /// <summary>
        /// api rest base url
        /// </summary>
        private const string C_APIREST_BASEURL = "https://api.omniwallet.org";

        /// <summary>
        /// agent url tmp
        /// </summary>
        private readonly string _agentGetTmp;

        #endregion

        #region Constructor

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="agentGetTmp"></param>
        public OmniScanClient(string agentGetTmp = null)
        {
            this._agentGetTmp = agentGetTmp;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// create rest url
        /// </summary>
        /// <param name="version">version</param>
        /// <param name="actionUrl">action url</param>
        /// <returns></returns>
        private string CreateRestUrl(OmniRestVersion version, string actionUrl)
        {
            return $"{C_APIREST_BASEURL}/{version}/{actionUrl}".ToLower();
        }

        /// <summary>
        /// rest get request
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        private string RestGet(string url)
        {
            string resp;
            try
            {
                if (string.IsNullOrEmpty(this._agentGetTmp))
                    resp = HttpProtocol.HttpGet(url);
                else
                {
                    string encodeUrl = UrlEncoder.UrlEncode(url);
                    string remoteUrl = string.Format(this._agentGetTmp, encodeUrl);

                    resp = HttpProtocol.HttpGet(remoteUrl);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// rest post request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string RestPost(string url, string data)
        {
            string resp;
            try
            {
                if (string.IsNullOrEmpty(this._agentGetTmp))
                    resp = HttpProtocol.HttpPost(url, data, HttpProtocol.XWWWFORMURLENCODED);
                else
                {
                    string encodeUrl = UrlEncoder.UrlEncode(url);
                    string remoteUrl = string.Format(this._agentGetTmp, encodeUrl);

                    resp = HttpProtocol.HttpPost(url, data, HttpProtocol.XWWWFORMURLENCODED);
                }
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

            JToken error_json;
            if (json_obj.TryGetValue("error", StringComparison.OrdinalIgnoreCase, out error_json))
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

        #region IOmniScanClient Methods

        /// <summary>
        /// Returns the balance information for a given address. 
        /// For multiple addresses in a single query use the v2 endpoint
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public OmniAssetCollectionJson GetAddressV1(string address)
        {
            if (null == address || address.Length <= 0)
                throw new ArgumentNullException(nameof(address));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressV1), address);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniAssetCollectionJson cacheData);
            if (!exists)
            {
                string data = $"addr={address}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "address/addr/");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniAssetCollectionJson>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Returns the balance information for multiple addresses
        /// </summary>
        /// <param name="addresses"></param>
        /// <returns></returns>
        public Dictionary<string, OmniAssetCollectionJson> GetAddressV2(params string[] addresses)
        {
            if (null == addresses || addresses.Length <= 0)
                throw new ArgumentNullException(nameof(addresses));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressV2), addresses);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out Dictionary<string, OmniAssetCollectionJson> cacheData);
            if (!exists)
            {
                string data = string.Join("&", addresses.Select(s => $"addr={s}"));
                string url = this.CreateRestUrl(OmniRestVersion.V2, "address/addr/");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<Dictionary<string, OmniAssetCollectionJson>>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Returns the balance information and transaction history list for a given address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public OmniAddressDetailsResponse GetAddressDetails(string address)
        {
            if (null == address || address.Length <= 0)
                throw new ArgumentNullException(nameof(address));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressDetails), address);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniAddressDetailsResponse cacheData);
            if (!exists)
            {
                string data = $"addr={address}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "address/addr/details/");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniAddressDetailsResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Returns the Armory encoded version of an unsigned transaction for 
        /// use with Armory offline transactions. 
        /// Data: 
        ///     unsigned_hex : raw bitcoin hex formatted tx to be converted 
        ///     pubkey : pubkey of the sending address
        /// </summary>
        /// <param name="unsignedHex"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        [Obsolete("The remote server returned an error: (502) Bad Gateway.")]
        public OmniArmoryUnsignedResponse GetUnsigned(string unsignedHex, string publicKey)
        {
            if (string.IsNullOrEmpty(unsignedHex))
                throw new ArgumentNullException(nameof(unsignedHex));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetUnsigned), unsignedHex, publicKey);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniArmoryUnsignedResponse cacheData);
            if (!exists)
            {
                string data = $"unsigned_hex={unsignedHex}&pubkey={publicKey}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "armory/getunsigned");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniArmoryUnsignedResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Decodes and returns the raw hex and signed status from an armory transaction. 
        /// Data: 
        ///     armory_tx : armory transaction in text format
        /// </summary>
        /// <param name="armoryTx"></param>
        /// <returns></returns>
        [Obsolete("The remote server returned an error: (502) Bad Gateway.")]
        public OmniRawTransactionResponse GetRawtransaction(string armoryTx)
        {
            if (string.IsNullOrEmpty(armoryTx))
                throw new ArgumentNullException(nameof(armoryTx));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetRawtransaction), armoryTx);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniRawTransactionResponse cacheData);
            if (!exists)
            {
                string data = $"armory_tx={armoryTx}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "armory/getrawtransaction");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniRawTransactionResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Decodes raw hex returning Omni and Bitcoin transaction information
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        [Obsolete("The remote server returned an error: (502) Bad Gateway.")]
        public OmniDecodeResponse Decode(string hex)
        {
            if (string.IsNullOrEmpty(hex))
                throw new ArgumentNullException(nameof(hex));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(Decode), hex);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniDecodeResponse cacheData);
            if (!exists)
            {
                string data = $"hex={hex}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "decode/");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniDecodeResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Return a list of currently active/available base currencies the omnidex 
        /// has open orders against. 
        /// Data: 
        ///     ecosystem : 
        ///         1 for main / production ecosystem 
        ///         or 
        ///         2 for test/development ecosystem
        /// </summary>
        /// <param name="ecosystem"></param>
        /// <returns></returns>
        public OmniDesignatingCurrenciesResponse DesignatingCurrencies(int ecosystem)
        {
            if (ecosystem != 1 && ecosystem != 2)
                throw new ArgumentOutOfRangeException("1 for main / production ecosystem or 2 for test/development ecosystem");

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(DesignatingCurrencies), ecosystem.ToString());
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniDesignatingCurrenciesResponse cacheData);
            if (!exists)
            {
                string data = $"ecosystem={ecosystem}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "omnidex/designatingcurrencies");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniDesignatingCurrenciesResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Returns list of transactions (up to 10 per page) relevant to queried Property ID. 
        /// Returned transaction types include: 
        /// Creation Tx, Change issuer txs, Grant Txs, Revoke Txs, Crowdsale Participation Txs, 
        /// Close Crowdsale earlier tx
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public OmniTxHistoryResponse GetHistory(int propertyId, int page = 0)
        {
            if (propertyId <= 0)
                throw new ArgumentOutOfRangeException(nameof(propertyId));
            if (page < 0)
                page = 0;

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetHistory), propertyId.ToString(), page.ToString());
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniTxHistoryResponse cacheData);
            if (!exists)
            {
                string data = $"page={page}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, $"properties/gethistory/{propertyId}");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniTxHistoryResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Return list of properties created by a queried address.
        /// </summary>
        /// <param name="addresses"></param>
        /// <returns></returns>
        public OmniListByOwnerResponse ListByOwner(params string[] addresses)
        {
            if (null == addresses || addresses.Length <= 0)
                throw new ArgumentNullException(nameof(addresses));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(ListByOwner), addresses);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniListByOwnerResponse cacheData);
            if (!exists)
            {
                string data = string.Join("&", addresses.Select(s => $"addresses={s}"));
                string url = this.CreateRestUrl(OmniRestVersion.V1, $"properties/listbyowner");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniListByOwnerResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Returns list of currently active crowdsales. 
        /// Data: 
        ///     ecosystem : 
        ///         1 for production/main ecosystem. 
        ///         2 for test/dev ecosystem
        /// </summary>
        /// <param name="ecosystem"></param>
        /// <returns></returns>
        public OmniCrowdSalesResponse ListActiveCrowdSales(int ecosystem)
        {
            if (ecosystem != 1 && ecosystem != 2)
                throw new ArgumentOutOfRangeException("1 for main / production ecosystem or 2 for test/development ecosystem");

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(ListActiveCrowdSales), ecosystem.ToString());
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniCrowdSalesResponse cacheData);
            if (!exists)
            {
                string data = $"ecosystem={ecosystem}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "properties/listactivecrowdsales");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniCrowdSalesResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// returns list of created properties filtered by ecosystem. 
        /// Data: 
        ///     ecosystem : 
        ///         1 for production/main ecosystem. 
        ///         2 for test/dev ecosystem
        /// </summary>
        /// <param name="ecosystem"></param>
        /// <returns></returns>
        public OmniListByEcosystemResponse ListByEcosystem(int ecosystem)
        {
            if (ecosystem != 1 && ecosystem != 2)
                throw new ArgumentOutOfRangeException("1 for main / production ecosystem or 2 for test/development ecosystem");

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(ListByEcosystem), ecosystem.ToString());
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniListByEcosystemResponse cacheData);
            if (!exists)
            {
                string data = $"ecosystem={ecosystem}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "properties/listbyecosystem");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniListByEcosystemResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Returns list of all created properties.
        /// </summary>
        /// <returns></returns>
        public OmniCoinListResponse PropertyList()
        {
            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(PropertyList));
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniCoinListResponse cacheData);
            if (!exists)
            {
                string url = this.CreateRestUrl(OmniRestVersion.V1, "properties/list");
                string resp = this.RestGet(url);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniCoinListResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        /// <summary>
        /// Search by transaction id, address or property id. 
        /// Data: 
        ///     query : 
        ///         text string of either Transaction ID, Address, or property id to search for
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public OmniSearchResponse Search(string query)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentNullException(nameof(query));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(Search), query);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out OmniSearchResponse cacheData);
            if (!exists)
            {
                string data = $"query={query}";
                string url = this.CreateRestUrl(OmniRestVersion.V1, "search");
                string resp = this.RestPost(url, data);

                string error = HasResponseError(resp);
                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                cacheData = ObjectParse<OmniSearchResponse>(resp);

                ApiMsCacheProvider.Set(cacheKey, cacheData, ApiCacheExpirationMode.SlideExpired, TimeSpan.FromSeconds(c_cacheSeconds));
            }

            return cacheData;
        }

        #endregion
    }
}
