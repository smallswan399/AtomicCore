using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Token Value Json
    /// </summary>
    public class TronTokenValueJson
    {
        /// <summary>
        /// call value
        /// </summary>
        [JsonProperty("call_value")]
        public string CallValue { get; set; }

        /// <summary>
        /// Token Info
        /// </summary>
        [JsonProperty("tokenInfo")]
        public TronTokenBasicJson TokenInfo { get; set; }
    }
}
