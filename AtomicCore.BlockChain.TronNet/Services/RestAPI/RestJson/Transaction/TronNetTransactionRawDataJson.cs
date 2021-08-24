using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Transaction Raw Data Rest Json
    /// </summary>
    public class TronNetTransactionRawDataJson
    {
        /// <summary>
        /// contract
        /// </summary>
        [JsonProperty("contract")]
        public TronNetContractRestJson[] Contract { get; set; }
    }
}
