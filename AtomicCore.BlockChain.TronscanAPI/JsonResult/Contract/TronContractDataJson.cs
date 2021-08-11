using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Contract Data Json
    /// </summary>
    public class TronContractDataJson
    {
        /// <summary>
        /// contract data
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Owner Address
        /// </summary>
        [JsonProperty("owner_address")]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// Contract Address
        /// </summary>
        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        /// <summary>
        /// call value
        /// </summary>
        [JsonProperty("call_value"), JsonConverter(typeof(BizTronULongJsonConverter))]
        public ulong CallValue { get; set; }
    }
}
