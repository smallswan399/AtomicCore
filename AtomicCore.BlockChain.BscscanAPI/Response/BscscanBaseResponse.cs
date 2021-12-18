using Newtonsoft.Json;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bsc api response
    /// </summary>
    public class BscscanBaseResponse
    {
        /// <summary>
        /// 消息状态(1 true)
        /// </summary>
        [JsonProperty("status")]
        public BscscanJsonStatus Status { get; set; }

        /// <summary>
        /// 消息信息
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
