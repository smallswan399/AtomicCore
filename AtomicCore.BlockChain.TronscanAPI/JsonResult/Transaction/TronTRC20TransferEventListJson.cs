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
        /// data
        /// </summary>
        [JsonProperty("Data")]
        public List<TronTRC20TransferEventJson> MyProperty { get; set; }
    }
}
