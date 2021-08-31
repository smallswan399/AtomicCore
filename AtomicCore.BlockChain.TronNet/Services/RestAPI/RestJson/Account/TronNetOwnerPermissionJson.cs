using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Owner Permission Json
    /// </summary>
    public class TronNetOwnerPermissionJson
    {
        /// <summary>
        /// permission_name
        /// </summary>
        [JsonProperty("permission_name")]
        public string PermissionName { get; set; }

        /// <summary>
        /// accouthresholdnt_name
        /// </summary>
        [JsonProperty("threshold")]
        public int Threshold { get; set; }

        /// <summary>
        /// keys
        /// </summary>
        [JsonProperty("keys")]
        public TronNetOwnerPermissionKeyJson[] Keys { get; set; }
    }
}
