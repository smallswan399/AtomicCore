using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Chain Parmeter Overview Json
    /// </summary>
    public class TronNetChainParameterOverviewJson
    {
        /// <summary>
        /// Chain Parameters
        /// </summary>
        [JsonProperty("chainParameter")]
        public TronNetChainParameterJson[] ChainParameters { get; set; }
    }
}
