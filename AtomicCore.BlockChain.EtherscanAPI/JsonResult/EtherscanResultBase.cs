using Newtonsoft.Json;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// Etherscan Result Base
    /// </summary>
    public abstract class EtherscanResultBase
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
    }
}
