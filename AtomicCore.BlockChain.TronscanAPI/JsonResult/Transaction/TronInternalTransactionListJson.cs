using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Internal Transaction List Json
    /// </summary>
    public class TronInternalTransactionListJson : TronPageListJson
    {
        /// <summary>
        /// Contract Map
        /// </summary>
        [JsonProperty("contractMap")]
        public Dictionary<string, string> ContractMap { get; set; }

        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("data")]
        public List<object> Data { get; set; }
    }
}
