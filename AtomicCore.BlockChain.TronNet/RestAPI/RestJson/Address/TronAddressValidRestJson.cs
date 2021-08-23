using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Address Valid Rest Json
    /// </summary>
    public class TronAddressValidRestJson
    {
        /// <summary>
        /// result
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
