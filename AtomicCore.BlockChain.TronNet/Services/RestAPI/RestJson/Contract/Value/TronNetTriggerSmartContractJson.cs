using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Trigger Smart Contract Json
    /// Type 31,eg:trc20 && trc721
    /// </summary>
    public class TronNetTriggerSmartContractJson : TronNetContractBaseValueJson
    {
        /// <summary>
        /// contract_address
        /// </summary>
        [JsonProperty("contract_address"), JsonConverter(typeof(TronNetScriptAddressJsonConverter))]
        public string ContractAddress { get; set; }

        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
