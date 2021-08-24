using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Contract ValueBase Rest Json
    /// </summary>
    public abstract class TronNetContractValueBaseJson
    {
        /// <summary>
        /// owner_address
        /// </summary>
        [JsonProperty("owner_address"), JsonConverter(typeof(TronNetScriptAddressJsonConverter))]
        public string OwnerAddress { get; set; }
    }
}
