using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Last Transaction List Json
    /// </summary>
    public class TronNormalTransactionListJson : TronPageListJson
    {
        /// <summary>
        /// transaction data list
        /// </summary>
        [JsonProperty("data")]
        public List<TronNormalTransactionJson> Data { get; set; }

        /// <summary>
        /// Whole Chain Tx Count Total
        /// </summary>
        [JsonProperty("wholeChainTxCount"), JsonConverter(typeof(BizTronULongJsonConverter))]
        public ulong WholeChainTxCount { get; set; }

        /// <summary>
        /// Contract Map
        /// </summary>
        [JsonProperty("contractMap")]
        public Dictionary<string, string> ContractMap { get; set; }

        /// <summary>
        /// Contract Info
        /// </summary>
        [JsonProperty("contractInfo")]
        public Dictionary<string, TronContractTagJson> ContractInfo { get; set; }
    }
}
