using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGridTransactionParameter
    /// </summary>
    public class TronGridTransactionParameter
    {
        /// <summary>
        /// type_url
        /// </summary>
        [JsonProperty("type_url")]
        public string TypeUrl { get; set; }

        /// <summary>
        /// value
        /// </summary>
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}
