using Newtonsoft.Json;
using System.Numerics;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bsc transaction json
    /// </summary>
    public class BscTransactionJson
    {
        /// <summary>
        /// isError
        /// </summary>
        [JsonProperty("isError")]
        public int IsError { get; set; }

        /// <summary>
        /// hash
        /// </summary>
        [JsonProperty("hash")]
        public string TxHash { get; set; }

        /// <summary>
        /// timeStamp
        /// </summary>
        [JsonProperty("timeStamp")]
        public long TimeStamp { get; set; }

        /// <summary>
        /// contractAddress
        /// </summary>
        [JsonProperty("contractAddress")]
        public string ContractAddress { get; set; }

        /// <summary>
        /// from
        /// </summary>
        [JsonProperty("from")]
        public string TxFrom { get; set; }

        /// <summary>
        /// to
        /// </summary>
        [JsonProperty("to")]
        public string TxTo { get; set; }

        /// <summary>
        /// input
        /// </summary>
        [JsonProperty("input")]
        public string TxInput { get; set; }

        /// <summary>
        /// value
        /// </summary>
        [JsonProperty("value")]
        public BigInteger TxValue { get; set; }

        /// <summary>
        /// gas
        /// </summary>
        [JsonProperty("gas")]
        public long TxGas { get; set; }

        /// <summary>
        /// gasUsed
        /// </summary>
        [JsonProperty("gasUsed")]
        public long GasUsed { get; set; }

        /// <summary>
        /// blockNumber
        /// </summary>
        [JsonProperty("blockNumber")]
        public long BlockNumber { get; set; }
    }
}
