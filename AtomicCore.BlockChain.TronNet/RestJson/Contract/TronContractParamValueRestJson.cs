using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Contract Parameter Value Rest Json
    /// </summary>
    public class TronContractParamValueRestJson
    {
        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// owner_address
        /// </summary>
        [JsonProperty("owner_address")]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// contract_address
        /// </summary>
        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }
    }
}
