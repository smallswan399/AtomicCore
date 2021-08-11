using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Block Info List Json
    /// </summary>
    public class TronBlockInfoListJson : TronPageListJson
    {
        /// <summary>
        /// block info list
        /// </summary>
        [JsonProperty("data")]
        public List<TronBlockInfoJson> Data { get; set; }
    }
}
