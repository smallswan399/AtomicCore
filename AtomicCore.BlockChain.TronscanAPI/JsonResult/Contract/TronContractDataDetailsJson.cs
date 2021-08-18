using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Contract Data Details Json
    /// </summary>
    public class TronContractDataDetailsJson : TronContractDataJson
    {
        /// <summary>
        /// tokenInfo
        /// </summary>
        [JsonProperty("tokenInfo")]
        public TronTokenBasicJson TokenInfo { get; set; }
    }
}
