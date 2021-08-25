using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Valid Rest Json
    /// </summary>
    public abstract class TronNetValidRestJson
    {
        /// <summary>
        /// Error Msg
        /// </summary>
        [JsonProperty("Error"), JsonIgnore]
        public string Error { get; set; }
    }
}
