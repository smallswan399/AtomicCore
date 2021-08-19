using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Transaction Raw Data Rest Json
    /// </summary>
    public class TronTransactionRawDataRestJson
    {
        /// <summary>
        /// contract
        /// </summary>
        [JsonProperty("contract")]
        public TronContractRestJson[] Contract { get; set; }
    }
}
