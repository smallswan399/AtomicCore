using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Chain Top Account List Json
    /// </summary>
    public class TronChainTopAddressListJson : TronPageListJson
    {
        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("data")]
        public List<TronChainTopAddressJson> Data { get; set; }

        /// <summary>
        /// contract maps
        /// </summary>
        [JsonProperty("contractMap")]
        public Dictionary<string, bool> ContractMap { get; set; }

        /// <summary>
        /// Contract Info
        /// </summary>
        public Dictionary<string, TronContractTagJson> ContractInfo { get; set; }
    }
}
