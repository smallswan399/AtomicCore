using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Owner PermissionKey Json
    /// </summary>
    public class TronNetAccountPermissionKeyValueJson
    {
        /// <summary>
        /// address
        /// </summary>
        [JsonProperty("address"), JsonConverter(typeof(TronNetHexAddressJsonConverter))]
        public string Address { get; set; }

        /// <summary>
        /// weight
        /// </summary>
        [JsonProperty("weight")]
        public int Weight { get; set; }
    }
}
