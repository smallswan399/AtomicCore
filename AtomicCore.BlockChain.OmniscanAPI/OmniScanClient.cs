﻿using Newtonsoft.Json.Linq;
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
        /// <param name="address"></param>
        /// <returns></returns>
        public Dictionary<string, OmniAssetCollectionJson> GetAddressV2(params string[] address)
        {
            if (null == address || address.Length <= 0)
                throw new ArgumentNullException(nameof(address));

            string cacheKey = ApiMsCacheProvider.GenerateCacheKey(nameof(GetAddressV2), address);
            bool exists = ApiMsCacheProvider.Get(cacheKey, out Dictionary<string, OmniAssetCollectionJson> cacheData);
            if (!exists)
            {
                string data = string.Join("&", address.Select(s => $"addr={s}"));
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

        #endregion
    }
}
