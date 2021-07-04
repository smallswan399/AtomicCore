using Newtonsoft.Json;

namespace AtomicCore.BlockChain.EtherscanAPI.JsonResult
{
    /// <summary>
    /// Etherscan的统一返回结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EtherscanJsonResult<T>
        where T : class, new()
    {
        /// <summary>
        /// 消息状态(1 true)
        /// </summary>
        [JsonProperty("status")]
        public EtherscanJsonStatus Status { get; set; }

        /// <summary>
        /// 消息信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// 数据结果
        /// </summary>
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
