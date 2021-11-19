using Newtonsoft.Json;

namespace AtomicCore.BlockChain.OmniscanAPI
{
    /// <summary>
    /// omni tx history resposne
    /// </summary>
    public class OmniTxHistoryResponse : OmniPagerResponse
    {
        /// <summary>
        /// Transaction list
        /// </summary>
        [JsonProperty("transactions")]
        public OmniHistoryTxInfoJson[] Transactions { get; set; }
    }
}
