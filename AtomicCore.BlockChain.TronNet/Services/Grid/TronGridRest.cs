using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Rest
    /// </summary>
    public class TronGridRest : ITronGridRest
    {
        #region Variables

        private readonly IOptions<TronNetOptions> _options;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public TronGridRest(IOptions<TronNetOptions> options)
        {
            _options = options;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Rest Get Json Result
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string RestGet(string url)
        {
            string resp;

            using (var client = new HttpClient())
            {
                try
                {
                    resp = client.GetStringAsync(url).Result;
                }
                catch (Exception ex)
                {
                    client.Dispose();
                    throw ex;
                }
            }

            return resp;
        }

        /// <summary>
        /// StringJson Parse to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resp"></param>
        /// <returns></returns>
        private TronGridRestResult<T> ObjectParse<T>(string resp)
            where T : new()
        {
            TronGridRestResult<T> jsonResult;
            try
            {
                jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject<TronGridRestResult<T>>(resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonResult;
        }

        #endregion

        #region ITronGridAccountRest

        /// <summary>
        /// Get Account Info
        /// </summary>
        /// <param name="address"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public TronGridRestResult<TronGridAccountInfo> GetAccount(string address, TronGridRequestQuery query = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            string query_str = string.Empty;
            if (null != query)
                query_str = query.GetQuery();

            string url = $"{this._options.Value.FullNodeRestAPI}/v1/accounts/{address}{(string.IsNullOrEmpty(query_str) ? string.Empty : $"?{query_str}")}";
            string resp = this.RestGet(url);

            var result = ObjectParse<TronGridAccountInfo>(resp);

            return result;
        }

        #endregion
    }
}
