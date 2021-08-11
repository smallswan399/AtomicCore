using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Block SR Confirm Json
    /// </summary>
    public class TronBlockSRConfirmJson
    {
        /// <summary>
        /// address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// name
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }

        /// <summary>
        /// block height
        /// </summary>
        [JsonProperty("block")]
        public ulong BlockHeight { get; set; }

        /// <summary>
        /// url
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
