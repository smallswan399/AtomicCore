using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Contract Transaction List Json
    /// </summary>
    public class TronContractTransactionListJson : TronPageListJson
    {
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

        /// <summary>
        /// transaction list data
        /// </summary>
        [JsonProperty("data")]
        public List<TronContractTransactionJson> Data { get; set; }
    }
}
