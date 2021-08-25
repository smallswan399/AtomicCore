using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Create Transaction Rest Json
    /// </summary>
    public class TronNetCreateTransactionRestJson : TronNetValidRestJson
    {
        /// <summary>
        /// TXID
        /// </summary>
        [JsonProperty("txID")]
        public string TxID { get; set; }

        /// <summary>
        /// Raw Data
        /// </summary>
        [JsonProperty("raw_data")]
        public TronNetCreateTransactionRawDataJson RawData { get; set; }
    }
}
