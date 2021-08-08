using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// tron block sync json result
    /// </summary>
    public class TronBlockOverviewJsonResult
    {
        /// <summary>
        /// database object
        /// </summary>
        [JsonProperty("database")]
        public TronDatabaseOverviewJsonResult DatabaseOverview { get; set; }

        /// <summary>
        /// sync object
        /// </summary>
        [JsonProperty("sync")]
        public TronSyncOverviewJsonResult SyncOverview { get; set; }

        /// <summary>
        /// network object
        /// </summary>
        [JsonProperty("network")]
        public object NetworkOverview { get; set; }

        /// <summary>
        /// full object
        /// </summary>
        [JsonProperty("full")]
        public object FullOverview { get; set; }

        /// <summary>
        /// solidity object
        /// </summary>
        [JsonProperty("solidity")]
        public object SolidityOverview { get; set; }
    }
}
