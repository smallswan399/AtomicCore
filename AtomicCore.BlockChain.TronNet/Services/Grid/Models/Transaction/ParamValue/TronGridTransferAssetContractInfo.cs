using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Transfer Asset Contract Info
    /// </summary>
    [TronNetParamValue(TronNetContractType.TransferAssetContract)]
    public class TronGridTransferAssetContractInfo : TronGridTransactionParamValue
    {
        #region Propertys

        /// <summary>
        /// contract_address
        /// </summary>
        [JsonProperty("asset_name")]
        public string AssetName { get; set; }

        /// <summary>
        /// contract_address
        /// </summary>
        [JsonProperty("to_address"), JsonConverter(typeof(TronGridAddressBase58JsonConverter))]
        public string ToAddress { get; set; }

        /// <summary>
        /// amount
        /// </summary>
        [JsonProperty("amount"), JsonConverter(typeof(TronNetTrxUnitJsonConverter))]
        public decimal Amount { get; set; }

        #endregion
    }
}
