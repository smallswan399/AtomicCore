using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Address Asset Json
    /// </summary>
    public class TronNetAddressAssetJson : TronNetValidRestJson
    {
        /// <summary>
        /// AssetIssue List
        /// </summary>
        [JsonProperty("assetIssue")]
        public TronNetAssetJson[] AssetIssue { get; set; }
    }
}
