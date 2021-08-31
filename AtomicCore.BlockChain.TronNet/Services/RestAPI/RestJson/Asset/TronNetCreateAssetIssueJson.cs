using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Create Asset Issue Json
    /// </summary>
    public class TronNetCreateAssetIssueJson : TronNetValidRestJson
    {
        /// <summary>
        /// visible
        /// </summary>
        [JsonProperty("visible")]
        public bool Visible { get; set; }

        /// <summary>
        /// TXID
        /// </summary>
        [JsonProperty("txID")]
        public string TxID { get; set; }

        /// <summary>
        /// Raw Data
        /// </summary>
        [JsonProperty("raw_data")]
        public TronNetCreateAssetIssueRawDataJson RawData { get; set; }

        /// <summary>
        /// Raw Data Hex
        /// </summary>
        [JsonProperty("raw_data_hex")]
        public string RawDataHex { get; set; }
    }
}
