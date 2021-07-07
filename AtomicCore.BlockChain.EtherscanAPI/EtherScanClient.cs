using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// IEtherScanClient interface implementation
    /// </summary>
    public class EtherScanClient : IEtherScanClient
    {
        #region Variables

        /// <summary>
        /// 基础URL模版
        /// </summary>
        private const string c_baseUrl = "https://api.etherscan.io";

        /// <summary>
        /// ApiKey Temp Append To End,eg => apikey={0}
        /// </summary>
        private const string c_apiKeyTemp = "&apikey={0}";

        /// <summary>
        /// API KEY
        /// </summary>
        private readonly string _apiKey;

        #endregion

        #region Constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="apiKey"></param>
        public EtherScanClient(string apiKey)
        {
            this._apiKey = apiKey;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 创建Rest Url
        /// </summary>
        /// <param name="module">模块名称</param>
        /// <param name="action">行为名称</param>
        /// <returns></returns>
        private string CreateRestUrl(string module, string action)
        {
            return string.Format(
                "{0}/api?module={1}&action={2}{3}",
                c_baseUrl,
                module,
                action,
                string.IsNullOrEmpty(this._apiKey) ?
                    string.Empty :
                    string.Format(c_apiKeyTemp, this._apiKey)
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
                resp = HttpProtocol.HttpGet(url);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// JSON解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resp"></param>
        /// <returns></returns>
        private EtherscanJsonResult<T> JsonParse<T>(string resp)
            where T : class, new()
        {
            EtherscanJsonResult<T> jsonResult;
            try
            {
                jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EtherscanJsonResult<T>>(resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonResult;
        }

        #endregion

        #region IEtherScanClient Methods

        /// <summary>
        /// 获取网络手续费（三档）
        /// https://api-cn.etherscan.com/api?module=gastracker&action=gasoracle
        /// </summary>
        /// <returns></returns>
        public EtherscanJsonResult<EthGasOracleJsonResult> GetGasOracle()
        {
            //拼接URL
            string url = this.CreateRestUrl("gastracker", "gasoracle");

            //请求API
            string resp = this.RestGet(url);

            //解析JSON
            EtherscanJsonResult<EthGasOracleJsonResult> jsonResult = JsonParse<EthGasOracleJsonResult>(resp);

            return jsonResult;
        }

        /// <summary>
        /// 获取交易记录列表
        /// </summary>
        /// <param name="address">钱包地址</param>
        /// <param name="startBlock">起始区块高度</param>
        /// <param name="endBlock">截止区块高度</param>
        /// <param name="sort">排序规则</param>
        /// <param name="page">当前页码</param>
        /// <param name="limit">每页多少条数据</param>
        /// <returns></returns>
        public EtherscanJsonResult<EthTransactionJsonResult> GetTransactions(string address, ulong? startBlock = null, ulong? endBlock = null, EtherscanSort sort = EtherscanSort.Asc, int? page = 1, int? limit = 1000)
        {
            //拼接URL
            string url = this.CreateRestUrl("account", "txlist");

            //请求参数拼接
            StringBuilder urlBuilder = new StringBuilder(url);
            urlBuilder.AppendFormat("&address={0}", address);
            urlBuilder.AppendFormat("&sort={0}", sort.ToString());
            if (null != startBlock && startBlock > 0)
                urlBuilder.AppendFormat("&startblock={0}", startBlock);
            if (null != endBlock && endBlock > 0)
                urlBuilder.AppendFormat("&endblock={0}", endBlock);
            if (null != page && page > 0)
                urlBuilder.AppendFormat("&page={0}", page);
            if (null != limit && limit > 0)
                urlBuilder.AppendFormat("&offset={0}", limit);

            //请求API
            string resp = this.RestGet(url);

            //解析JSON
            EtherscanJsonResult<EthTransactionJsonResult> jsonResult = JsonParse<EthTransactionJsonResult>(resp);

            return jsonResult;
        }

        #endregion
    }
}
