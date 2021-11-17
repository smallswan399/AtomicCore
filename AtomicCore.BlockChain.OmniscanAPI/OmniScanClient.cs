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
        /// Get Address V1
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public OmniAssetCollectionJson GetAddressV1(string address)
        {
            if (null == address || address.Length <= 0)
                throw new ArgumentNullException(nameof(address));

            string data = $"addr={address}";
            string url = this.CreateRestUrl(OmniRestVersion.V1, "address/addr/");
            string resp = this.RestPost(url, data);

            string error = HasResponseError(resp);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);

            OmniAssetCollectionJson result = ObjectParse<OmniAssetCollectionJson>(resp);

            return result;
        }

        /// <summary>
        /// Get Address V2
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Dictionary<string, OmniAssetCollectionJson> GetAddressV2(params string[] address)
        {
            if (null == address || address.Length <= 0)
                throw new ArgumentNullException(nameof(address));

            string data = string.Join("&", address.Select(s => $"addr={s}"));
            string url = this.CreateRestUrl(OmniRestVersion.V2, "address/addr/");
            string resp = this.RestPost(url, data);

            string error = HasResponseError(resp);
            if (!string.IsNullOrEmpty(error))
                throw new Exception(error);

            Dictionary<string, OmniAssetCollectionJson> result = ObjectParse<Dictionary<string, OmniAssetCollectionJson>>(resp);

            return result;
        }

        #endregion
    }
}
