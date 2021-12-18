using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        /// <param name="module"></param>
        /// <param name="action">action</param>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetRestUrl(BscNetwork network, BscModule module, string action, string data)
        {
            string baseUrl = GetBaseUrl(network);
            return $"{baseUrl}/api?module={module}&action={action}".ToLower();
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

        #region Gas Tracker

        /// <summary>
        /// Returns the current Safe, Proposed and Fast gas prices. 
        /// </summary>
        /// <returns></returns>
        public BscscanSingleResult<BscGasOracleJson> GetGasOracle()
        {
            return null;
        }

        #endregion

    }
}
