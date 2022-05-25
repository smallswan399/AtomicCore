﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// base url
        /// </summary>
        private readonly string _baseUrl;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public TronGridRest(IOptions<TronNetOptions> options)
        {
            _baseUrl = options.Value.TronGridSrvAPI;

            if (string.IsNullOrEmpty(_baseUrl))
                _baseUrl = "https://api.trongrid.io/";
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
        public TronGridRestResult<TronGridAccountInfo> GetAccount(string address, TronGridBaseQuery query = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            string query_str = string.Empty;
            if (null != query)
                query_str = query.GetQuery();

            string url = $"{_baseUrl}/v1/accounts/{address}{(string.IsNullOrEmpty(query_str) ? string.Empty : $"?{query_str}")}";
            string resp = RestGet(url);

            var result = ObjectParse<TronGridAccountInfo>(resp);

            return result;
        }

        /// <summary>
        /// Get the transfer records of an account history, including trc10 & trc20 transfers and TRX transfers 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public TronGridRestResult<TronGridTransactionInfo> GetTransactions(string address, TronGridTransactionQuery query = null)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            string query_str = string.Empty;
            if (null != query)
                query_str = query.GetQuery();

            string url = $"{_baseUrl}/v1/accounts/{address}/transactions{(string.IsNullOrEmpty(query_str) ? string.Empty : $"?{query_str}")}";
            string resp = RestGet(url);

            var result = ObjectParse<TronGridTransactionInfo>(resp);

            return result;
        }

        #endregion
    }
}
