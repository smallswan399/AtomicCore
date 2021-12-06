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
    public class BlockChainExplorer : IBlockChainExplorer
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
        public BlockChainExplorer(string agentGetTmp = null, string agentPostTmp = null)
        {
            _agentGetTmp = agentGetTmp;
            _agentPostTmp = agentPostTmp;
        }

        #endregion

        #region Private Methods

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
        /// rest get request(Using HttpClient)
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        private string RestGet2(string url)
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

                using (HttpClient cli = new HttpClient())
                {
                    HttpResponseMessage respMessage = cli.GetAsync(remoteUrl).Result;
                    if (!respMessage.IsSuccessStatusCode)
                        throw new HttpRequestException($"StatusCode -> {respMessage.StatusCode}, ");

                    resp = respMessage.Content.ReadAsStringAsync().Result;
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
                if (string.IsNullOrEmpty(this._agentPostTmp))
                    resp = HttpProtocol.HttpPost(url, data, HttpProtocol.XWWWFORMURLENCODED);
                else
                {
                    string encodeUrl = UrlEncoder.UrlEncode(url);
                    string remoteUrl = string.Format(this._agentPostTmp, url, encodeUrl);

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

        #region IBlockChainExplorer

        /// <summary>
        /// Get Address Balance(BTC)
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public BtcAddressBalanceResponse GetAddressBTCBalance(string address)
        {
            return null;
        }

        #endregion
    }
}
