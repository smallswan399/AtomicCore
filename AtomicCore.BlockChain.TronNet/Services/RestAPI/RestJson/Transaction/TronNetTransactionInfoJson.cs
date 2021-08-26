using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Transaction Info Json
    /// </summary>
    public class TronNetTransactionInfoJson
    {
        /// <summary>
        /// txID
        /// </summary>
        [JsonProperty("id")]
        public string TxID { get; set; }

        /// <summary>
        /// fee
        /// </summary>
        [JsonProperty("fee")]
        public ulong TxFee { get; set; }

        /// <summary>
        /// block number
        /// </summary>
        [JsonProperty("blockNumber"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong BlockNumber { get; set; }

        /// <summary>
        /// block timestamp
        /// </summary>
        [JsonProperty("blockTimeStamp"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong BlockTimeStamp { get; set; }

        /// <summary>
        /// contract Result
        /// </summary>
        [JsonProperty("contractResult")]
        public string[] ContractResult { get; set; }

        /// <summary>
        /// Contract Address
        /// </summary>
        [JsonProperty("contract_address")]
        public string ContractAddress { get; set; }

        /// <summary>
        /// receipt
        /// </summary>
        [JsonProperty("receipt")]
        public TronNetTransactionReciptJson Receipt { get; set; }

        /// <summary>
        /// log
        /// </summary>
        [JsonProperty("log")]
        public TronNetContractLogJson[] Logs { get; set; }
    }
}
