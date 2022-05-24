using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Transaction Contract
    /// </summary>
    public class TronGridTransactionContract
    {
        /// <summary>
        /// contract type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// parameter
        /// </summary>
        [JsonProperty("parameter")]
        public TronGridTransactionParameter Parameter { get; set; }
    }
}
