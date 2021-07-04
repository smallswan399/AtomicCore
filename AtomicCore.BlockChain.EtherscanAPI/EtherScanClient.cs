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

        #region IEtherScanClient Methods

        /// <summary>
        /// 获取网络手续费（三档）
        /// https://api-cn.etherscan.com/api?module=gastracker&action=gasoracle
        /// </summary>
        /// <returns></returns>
        public EtherscanJsonResult<GasOracleJsonResult> GetGasOracle()
        {
            //拼接URL
            string url = string.Format(
                "{0}/api?apikey={1}&module=gastracker&action=gasoracle", 
                c_baseUrl,
                this._apiKey
            );

            //请求API
            string resp;
            try
            {
                resp = HttpProtocol.HttpGet(url);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //解析JSON
            EtherscanJsonResult<GasOracleJsonResult> jsonResult;
            try
            {
                jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject<EtherscanJsonResult<GasOracleJsonResult>>(resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonResult;
        }

        #endregion
    }
}
