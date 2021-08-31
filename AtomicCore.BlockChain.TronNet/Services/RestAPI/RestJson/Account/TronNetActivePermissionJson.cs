using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Active Permission Json
    /// </summary>
    public class TronNetActivePermissionJson : TronNetOwnerPermissionJson
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("id")]
        public int ID { get; set; }

        /// <summary>
        /// type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// operations
        /// </summary>
        [JsonProperty("operations")]
        public string Operations { get; set; }
    }
}
