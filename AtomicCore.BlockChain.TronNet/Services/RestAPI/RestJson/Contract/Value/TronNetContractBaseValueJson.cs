using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Contract Base Value Json
    /// </summary>
    public abstract class TronNetContractBaseValueJson
    {
        /// <summary>
        /// owner_address
        /// </summary>
        [JsonProperty("owner_address"), JsonConverter(typeof(TronNetHexAddressJsonConverter))]
        public virtual string OwnerAddress { get; set; }
    }
}
