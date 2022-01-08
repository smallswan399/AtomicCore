using Newtonsoft.Json;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bsc rpc transaction json
    /// </summary>
    public class BscRpcTransactionJson
    {
        /// <summary>
        /// hash
        /// </summary>
        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }

        /// <summary>
        /// number
        /// </summary>
        [JsonProperty("blockNumber"), JsonConverter(typeof(BscHexLongJsonConverter))]
        public long BlockNumber { get; set; }

        /// <summary>
        /// from
        /// </summary>
        [JsonProperty("from")]
        public string TxFrom { get; set; }

        /// <summary>
        /// gas
        /// </summary>
        [JsonProperty("gas"), JsonConverter(typeof(BscHexLongJsonConverter))]
        public long TxGas { get; set; }

        /// <summary>
        /// gasPrice
        /// </summary>
        [JsonProperty("gasPrice"), JsonConverter(typeof(BscHexLongJsonConverter))]
        public long TxGasPrice { get; set; }

        /// <summary>
        /// hash
        /// </summary>
        [JsonProperty("hash"),JsonConverter(typeof(BscBNBConverter))]
        public string TxHash { get; set; }

        /// <summary>
        /// input
        /// </summary>
        [JsonProperty("input")]
        public string TxInput { get; set; }

        /// <summary>
        /// nonce
        /// </summary>
        [JsonProperty("nonce")]
        public int TxNonce { get; set; }

        /// <summary>
        /// to
        /// </summary>
        [JsonProperty("to")]
        public string TxTo { get; set; }

        /// <summary>
        /// transactionIndex
        /// </summary>
        [JsonProperty("transactionIndex")]
        public int TransactionIndex { get; set; }

        /// <summary>
        /// value
        /// </summary>
        [JsonProperty("value")]
        public decimal TxValue { get; set; }

        /// <summary>
        /// type
        /// </summary>
        [JsonProperty("type")]
        public int Type { get; set; }

        /// <summary>
        /// v
        /// </summary>
        [JsonProperty("v")]
        public string V { get; set; }

        /// <summary>
        /// r
        /// </summary>
        [JsonProperty("r")]
        public string R { get; set; }

        /// <summary>
        /// s
        /// </summary>
        [JsonProperty("s")]
        public string S { get; set; }
    }
}
