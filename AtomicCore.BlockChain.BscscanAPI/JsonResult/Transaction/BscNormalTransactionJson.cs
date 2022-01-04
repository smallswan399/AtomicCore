using Newtonsoft.Json;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// Bsc Normal Transaction Json
    /// </summary>
    public class BscNormalTransactionJson : BscTransactionJson
    {
        /// <summary>
        /// nonce
        /// </summary>
        [JsonProperty("nonce")]
        public int TxNonce { get; set; }

        /// <summary>
        /// transactionIndex
        /// </summary>
        [JsonProperty("transactionIndex")]
        public int TransactionIndex { get; set; }

        /// <summary>
        /// confirmations
        /// </summary>
        [JsonProperty("confirmations")]
        public long Confirmations { get; set; }

        /// <summary>
        /// gasPrice
        /// </summary>
        [JsonProperty("gasPrice")]
        public long TxGasPrice { get; set; }

        /// <summary>
        /// cumulativeGasUsed
        /// </summary>
        [JsonProperty("cumulativeGasUsed")]
        public long CumulativeGasUsed { get; set; }

        /// <summary>
        /// txreceipt_status
        /// </summary>
        [JsonProperty("txreceipt_status")]
        public int TxReceiptStatus { get; set; }

        /// <summary>
        /// blockHash
        /// </summary>
        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }
    }
}
