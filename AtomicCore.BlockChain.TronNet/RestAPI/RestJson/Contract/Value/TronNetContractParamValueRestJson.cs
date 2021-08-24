using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Contract Parameter Value Rest Json
    /// </summary>
    public class TronNetContractParamValueRestJson
    {
        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// owner_address
        /// </summary>
        [JsonProperty("owner_address"),JsonConverter(typeof(TronNetScriptAddressJsonConverter))]
        public string OwnerAddress { get; set; }

        /// <summary>
        /// to_address(Trx && Trc10)
        /// </summary>
        [JsonProperty("to_address")]
        public string ToAddress { get; set; }

        /// <summary>
        /// amount(Trx && Trc10)
        /// </summary>
        [JsonProperty("amount"),JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong Amount { get; set; }

        /// <summary>
        /// contract_address
        /// </summary>
        [JsonProperty("contract_address"), JsonConverter(typeof(TronNetScriptAddressJsonConverter))]
        public string ContractAddress { get; set; }

        /// <summary>
        /// Trc10 Asset Name
        /// </summary>
        [JsonProperty("asset_name")]
        public string Trc10AssetName { get; set; }
    }
}
