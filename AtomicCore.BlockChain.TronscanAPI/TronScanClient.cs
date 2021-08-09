using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// TRON SCAN CLIENT
    /// </summary>
    public class TronScanClient : ITronScanClient
    {
        #region Variables

        /// <summary>
        /// tron mainnet
        /// </summary>
        public const string c_tron_main = "https://apilist.tronscan.org";

        /// <summary>
        /// tron shstanet
        /// </summary>
        public const string c_tron_shasta = "https://shastapi.tronscan.org";

        /// <summary>
        /// base url
        /// </summary>
        private readonly string _baseUrl;

        /// <summary>
        /// agent url tmp
        /// </summary>
        private readonly string _agentGetTmp;

        #endregion

        #region Constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseUrl">基础URL</param>
        /// <param name="agentGetTmp">代理模版</param>
        public TronScanClient(string baseUrl = c_tron_main, string agentGetTmp = null)
        {
            this._baseUrl = baseUrl;
            this._agentGetTmp = agentGetTmp;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 创建Rest Url
        /// </summary>
        /// <param name="actionUrl">接口URL</param>
        /// <returns></returns>
        private string CreateRestUrl(string actionUrl)
        {
            return string.Format(
                "{0}/api/{1}",
                this._baseUrl,
                actionUrl
            );
        }

        /// <summary>
        /// Rest Get Request
        /// </summary>
        /// <param name="url">请求URL</param>
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
        /// JSON解析OBJECT
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

        /// <summary>
        /// JSON解析单模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resp"></param>
        /// <returns></returns>
        private TronscanSingleResult<T> SingleParse<T>(string resp)
        {
            TronscanSingleResult<T> jsonResult;
            try
            {
                jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TronscanSingleResult<T>>(resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonResult;
        }

        /// <summary>
        /// JSON解析列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resp"></param>
        /// <returns></returns>
        private TronscanListResult<T> ListParse<T>(string resp)
            where T : class, new()
        {
            TronscanListResult<T> jsonResult;
            try
            {
                jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TronscanListResult<T>>(resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonResult;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Block Overview
        /// </summary>
        /// <returns></returns>
        public TronChainOverviewJson BlockOverview()
        {
            string url = this.CreateRestUrl("system/status");
            string resp = this.RestGet(url);
            TronChainOverviewJson jsonResult = ObjectParse<TronChainOverviewJson>(resp);

            return jsonResult;
        }

        /// <summary>
        /// Get Last Block
        /// </summary>
        /// <returns></returns>
        public TronBlockBasicJson GetLastBlock()
        {
            string url = this.CreateRestUrl("block/latest");
            string resp = this.RestGet(url);
            TronBlockBasicJson jsonResult = ObjectParse<TronBlockBasicJson>(resp);

            return jsonResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public TronAccountAssetJson GetAccountAssets(string address)
        {
            string url = this.CreateRestUrl(string.Format("account?address={0}", address));
            string resp = this.RestGet(url);
            TronAccountAssetJson jsonResult = ObjectParse<TronAccountAssetJson>(resp);

            return jsonResult;
        }

        #endregion
    }
}
