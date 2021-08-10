using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron TRC20 Transaction List Json
    /// </summary>
    public class TronTRC20TransactionListJson : TronPageListJson
    {
        /// <summary>
        /// Contract Info
        /// </summary>
        [JsonProperty("contractInfo")]
        public Dictionary<string, TronContractTagJson> ContractInfo { get; set; }

        /// <summary>
        /// Token Transfers
        /// </summary>
        [JsonProperty("token_transfers")]
        public List<TronTRC20TransferJson> TokenTransfers { get; set; }
    }
}
