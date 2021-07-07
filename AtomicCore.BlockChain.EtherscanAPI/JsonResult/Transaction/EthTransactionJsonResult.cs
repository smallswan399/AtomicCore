using Newtonsoft.Json;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// 交易信息JSON结果集
    /// </summary>
    public class EthTransactionJsonResult
    {
        /// <summary>
        /// 是否正常
        /// </summary>
        [JsonProperty("isError")]
        public int IsError { get; set; }

        /// <summary>
        /// 交易HASH
        /// </summary>
        [JsonProperty("hash")]
        public string TransactionHash { get; set; }

        /// <summary>
        /// 发起地址
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// 到账地址
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [JsonProperty("value")]
        public decimal Value { get; set; }

        /// <summary>
        /// 燃气价
        /// </summary>
        [JsonProperty("gasPrice")]
        public int GasPrice { get; set; }

        /// <summary>
        /// 燃气量
        /// </summary>
        [JsonProperty("gas")]
        public int Gas { get; set; }

        /// <summary>
        /// 已使用燃料量
        /// </summary>
        [JsonProperty("gasUsed")]
        public int GasUsed { get; set; }

        /// <summary>
        /// 交易拓展数据
        /// </summary>
        [JsonProperty("input")]
        public string Input { get; set; }

        /// <summary>
        /// 合约地址
        /// </summary>
        [JsonProperty("contractAddress")]
        public string ContractAddress { get; set; }

        /// <summary>
        /// 交易确认状态
        /// </summary>
        [JsonProperty("txreceipt_status")]
        public int TxReceiptStatus { get; set; }

        /// <summary>
        ///  交易所在区块的索引位
        /// </summary>
        [JsonProperty("transactionIndex")]
        public int TransactionIndex { get; set; }

        /// <summary>
        /// 地址Nonce
        /// </summary>
        [JsonProperty("nonce")]
        public int Nonce { get; set; }

        /// <summary>
        /// 区块哈希
        /// </summary>
        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }

        /// <summary>
        /// 区块高度
        /// </summary>
        [JsonProperty("blockNumber")]
        public string BlockNumber { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [JsonProperty("timeStamp")]
        public string TimeStamp { get; set; }
    }
}
