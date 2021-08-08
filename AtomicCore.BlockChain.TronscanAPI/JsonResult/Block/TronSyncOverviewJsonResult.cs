using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// tron sync overview
    /// </summary>
    public class TronSyncOverviewJsonResult
    {
        /// <summary>
        /// sync progress
        /// </summary>
        [JsonProperty("progress")]
        public long Progress { get; set; }
    }
}
