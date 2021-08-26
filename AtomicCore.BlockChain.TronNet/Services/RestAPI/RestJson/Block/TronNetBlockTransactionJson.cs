using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Block Transaction Json
    /// </summary>
    public class TronNetBlockTransactionJson
    {
        /// <summary>
        /// ret list result
        /// </summary>
        [JsonProperty("ret")]
        public TronNetReturnJson[] Returns { get; set; }

        /// <summary>
        /// signature
        /// </summary>
        [JsonProperty("signature")]
        public string[] Signature { get; set; }

        /// <summary>
        /// txID
        /// </summary>
        [JsonProperty("txID")]
        public string TxID { get; set; }

        /// <summary>
        /// raw_data
        /// </summary>
        [JsonProperty("raw_data")]
        public TronNetBlockTransactionRawDataJson RawData { get; set; }

        /// <summary>
        /// raw data hex
        /// </summary>
        [JsonProperty("raw_data_hex")]
        public string RawDataHex { get; set; }
    }
}
