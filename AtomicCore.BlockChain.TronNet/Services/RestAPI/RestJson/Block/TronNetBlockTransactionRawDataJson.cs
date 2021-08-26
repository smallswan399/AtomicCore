﻿using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Block Transaction RawData Json
    /// </summary>
    public class TronNetBlockTransactionRawDataJson : TronNetTransactionRawDataJson
    {
        /// <summary>
        /// ref_block_bytes
        /// </summary>
        [JsonProperty("ref_block_bytes")]
        public string RefBlockBytes { get; set; }

        /// <summary>
        /// ref_block_hash
        /// </summary>
        [JsonProperty("ref_block_hash")]
        public string RefBlockHash { get; set; }

        /// <summary>
        /// expiration
        /// </summary>
        [JsonProperty("expiration"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong Expiration { get; set; }

        /// <summary>
        /// timestamp
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong Timestamp { get; set; }
    }
}
