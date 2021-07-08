using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// Etherscan列表结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EtherscanListResult<T>
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
        /// 列表结果
        /// </summary>
        [JsonProperty("result")]
        public List<T> Result { get; set; }
    }
}
