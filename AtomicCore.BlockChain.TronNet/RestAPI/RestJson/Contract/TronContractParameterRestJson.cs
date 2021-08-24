using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Contract Parameter Rest Json
    /// </summary>
    public class TronContractParameterRestJson
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
        public TronContractValueBaseRestJson Value { get; set; }
    }
}
