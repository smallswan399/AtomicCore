using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Block Info By Number Json
    /// </summary>
    public class TronBlockInfoByNumberJson : TronPageListJson
    {
        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("data")]
        public List<TronBlockDetailsJson> Data { get; set; }
    }
}
