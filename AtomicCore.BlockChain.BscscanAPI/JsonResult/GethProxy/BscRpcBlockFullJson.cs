using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bsc block full json
    /// </summary>
    public class BscRpcBlockFullJson
    {
        /// <summary>
        /// baseFeePerGas
        /// </summary>
        [JsonProperty("baseFeePerGas")]
        public string BaseFeePerGas { get; set; }

        /// <summary>
        /// difficulty
        /// </summary>
        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        /// <summary>
        /// extraData
        /// </summary>
        [JsonProperty("extraData")]
        public string ExtraData { get; set; }

        /// <summary>
        /// gasLimit
        /// </summary>
        [JsonProperty("gasLimit"), JsonConverter(typeof(BscHexLongJsonConverter))]
        public long GasLimit { get; set; }

        /// <summary>
        /// gasUsed
        /// </summary>
        [JsonProperty("gasUsed"), JsonConverter(typeof(BscHexLongJsonConverter))]
        public long GasUsed { get; set; }

        /// <summary>
        /// hash
        /// </summary>
        [JsonProperty("hash")]
        public string BlockHash { get; set; }

        /// <summary>
        /// logsBloom
        /// </summary>
        [JsonProperty("logsBloom")]
        public string LogsBloom { get; set; }

        /// <summary>
        /// miner
        /// </summary>
        [JsonProperty("miner")]
        public string Miner { get; set; }

        /// <summary>
        /// mixHash
        /// </summary>
        [JsonProperty("mixHash")]
        public string MixHash { get; set; }

        /// <summary>
        /// nonce
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// number
        /// </summary>
        [JsonProperty("number")]
        public long BlockNumber { get; set; }

        /// <summary>
        /// parentHash
        /// </summary>
        [JsonProperty("parentHash")]
        public string ParentHash { get; set; }

        /// <summary>
        /// receiptsRoot
        /// </summary>
        [JsonProperty("receiptsRoot")]
        public string ReceiptsRoot { get; set; }

        /// <summary>
        /// sha3Uncles
        /// </summary>
        [JsonProperty("sha3Uncles")]
        public string Sha3Uncles { get; set; }

        /// <summary>
        /// size
        /// </summary>
        [JsonProperty("size"), JsonConverter(typeof(BscHexLongJsonConverter))]
        public long BlockSize { get; set; }

        /// <summary>
        /// stateRoot
        /// </summary>
        [JsonProperty("stateRoot")]
        public string StateRoot { get; set; }

        /// <summary>
        /// timestamp
        /// </summary>
        [JsonProperty("timestamp"), JsonConverter(typeof(BscHexLongJsonConverter))]
        public long Timestamp { get; set; }

        /// <summary>
        /// totalDifficulty
        /// </summary>
        [JsonProperty("totalDifficulty")]
        public string TotalDifficulty { get; set; }

        /// <summary>
        /// transactions
        /// </summary>
        [JsonProperty("transactions")]
        public JArray[] Transactions { get; set; }

        /// <summary>
        /// transactionsRoot
        /// </summary>
        [JsonProperty("transactionsRoot")]
        public string TransactionsRoot { get; set; }

        /// <summary>
        /// uncles
        /// </summary>
        [JsonProperty("uncles")]
        public string[] Uncles { get; set; }
    }
}
