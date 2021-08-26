using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Transaction Rest Json
    /// </summary>
    public class TronNetTransactionRestJson : TronNetTransactionBaseJson
    {
        /// <summary>
        /// raw_data_hex
        /// </summary>
        [JsonProperty("raw_data_hex")]
        public string RawDataHex { get; set; }

        /// <summary>
        /// ref_block_bytes
        /// </summary>
        [JsonProperty("ref_block_bytes")]
        public string RefBlockBytes { get; set; }

        /// <summary>
        /// ref_block_bytes
        /// </summary>
        [JsonProperty("ref_block_hash")]
        public string RefBlockHash { get; set; }

        /// <summary>
        /// expiration
        /// </summary>
        [JsonProperty("expiration"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong Expiration { get; set; }

        /// <summary>
        /// fee_limit
        /// </summary>
        [JsonProperty("fee_limit"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong FeeLimit { get; set; }

        /// <summary>
        /// timestamp
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong Timestamp { get; set; }
    }
}
