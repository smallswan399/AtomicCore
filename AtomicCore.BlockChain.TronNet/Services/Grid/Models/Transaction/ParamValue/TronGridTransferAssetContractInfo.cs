using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Transfer Asset Contract Info
    /// </summary>
    [TronNetParamValue(TronNetContractType.TransferAssetContract)]
    public class TronGridTransferAssetContractInfo : TronGridTransferContractInfo
    {
        #region Propertys

        /// <summary>
        /// contract_address
        /// </summary>
        [JsonProperty("asset_name")]
        public string AssetName { get; set; }

        #endregion
    }
}
