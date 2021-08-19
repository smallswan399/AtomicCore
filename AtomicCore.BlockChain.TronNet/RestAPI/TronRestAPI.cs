using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Rest API Implementation Class
    /// </summary>
    public class TronRestAPI : ITronRestAPI
    {
        #region Variables

        private const string c_apiKeyName = "TRON-PRO-API-KEY";
        private readonly IOptions<TronNetOptions> _options;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public TronRestAPI(IOptions<TronNetOptions> options)
        {
            _options = options;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create Full Node Rest Url
        /// </summary>
        /// <param name="actionUrl">接口URL</param>
        /// <returns></returns>
        private string CreateFullNodeRestUrl(string actionUrl)
        {
            return string.Format(
                "{0}{1}",
                this._options.Value.FullNodeRestAPI,
                actionUrl
            );
        }

        /// <summary>
        /// Rest Post Json Request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">rest url</param>
        /// <param name="json">json data</param>
        /// <returns></returns>
        private string RestPostJson<T>(string url, T json)
        {
            string resp;
            try
            {
                //post data
                string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(json);

                //head api key
                Dictionary<string, string> heads = null;
                string apiKey = this._options.Value.ApiKey;
                if (!string.IsNullOrEmpty(apiKey))
                {
                    heads = new Dictionary<string, string>()
                    {
                        { c_apiKeyName,apiKey}
                    };
                }

                resp = HttpProtocol.HttpPost(url, post_data, heads: heads);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// StringJson Parse to Object
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

        #region ITronQueryNetworkRestAPI Methods

        /// <summary>
        /// Get Transaction By Txid
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        public TronTransactionRestJson GetTransactionByID(string txid)
        {
            if (string.IsNullOrEmpty(txid))
                throw new ArgumentNullException(nameof(txid));

            string url = CreateFullNodeRestUrl("/wallet/gettransactionbyid");
            string resp = this.RestPostJson(url, new { value = txid });
            TronTransactionRestJson restJson = ObjectParse<TronTransactionRestJson>(resp);

            return restJson;
        }

        #endregion
    }
}
