using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tronscan Result Base
    /// </summary>
    public abstract class TronscanResultBase
    {
        /// <summary>
        /// 消息状态
        /// </summary>
        [JsonProperty("code"), JsonConverter(typeof(BizTronJsonStatusConverter))]
        public TronscanJsonStatus Status { get; set; }

        /// <summary>
        /// 消息信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
