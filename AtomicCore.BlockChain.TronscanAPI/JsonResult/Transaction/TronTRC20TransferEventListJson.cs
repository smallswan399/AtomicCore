using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron TRC20 Transfer Event List
    /// </summary>
    public class TronTRC20TransferEventListJson : TronPageListJson
    {
        /// <summary>
        /// TRC20 Transfer Event List Data
        /// </summary>
        [JsonProperty("Data")]
        public List<TronTRC20TransferEventJson> Data { get; set; }
    }
}
