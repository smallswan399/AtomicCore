using Newtonsoft.Json;

namespace AtomicCore.BlockChain.OmniscanAPI
{
    /// <summary>
    /// Omni Json Result
    /// </summary>
    public class OmniBaseResponse
    {
        #region Propertys

        /// <summary>
        /// error
        /// </summary>
        [JsonProperty("error")]
        public bool Error { get; set; }

        /// <summary>
        /// msg
        /// </summary>
        [JsonProperty("msg")]
        public string Message { get; set; }

        #endregion
    }
}
