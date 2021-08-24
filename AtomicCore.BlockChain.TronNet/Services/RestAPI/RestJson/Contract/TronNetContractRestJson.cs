using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Contract Rest Json
    /// </summary>
    public class TronNetContractRestJson
    {
        /// <summary>
        /// type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// parameter
        /// </summary>
        [JsonProperty("parameter")]
        public TronNetContractParameterJson Parameter { get; set; }
    }
}
