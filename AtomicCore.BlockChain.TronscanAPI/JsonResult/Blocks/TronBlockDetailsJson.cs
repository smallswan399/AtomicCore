using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Block Details Json
    /// </summary>
    public class TronBlockDetailsJson : TronBlockInfoJson
    {
        /// <summary>
        /// transferCount
        /// </summary>
        [JsonProperty("transferCount")]
        public int TransferCount { get; set; }

        /// <summary>
        /// srConfirmList
        /// </summary>
        [JsonProperty("srConfirmList")]
        public List<TronBlockSRConfirmJson> SRConfirmList { get; set; }
    }
}
