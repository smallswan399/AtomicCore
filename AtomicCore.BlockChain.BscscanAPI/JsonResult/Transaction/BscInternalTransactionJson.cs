using Newtonsoft.Json;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// Bsc Internal Transaction Json
    /// </summary>
    public class BscInternalTransactionJson : BscTransactionJson
    {
        /// <summary>
        /// type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// traceId
        /// </summary>
        [JsonProperty("traceId")]
        public string TraceId { get; set; }

        /// <summary>
        /// errCode
        /// </summary>
        [JsonProperty("errCode")]
        public string ErrCode { get; set; }
    }
}
