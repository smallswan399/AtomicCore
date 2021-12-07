using Newtonsoft.Json;

namespace AtomicCore.BlockChain.ExplorerAPI
{
    /// <summary>
    /// https://api.blockchain.info/haskoin-store/btc/address/1ARjWDkZ7kT9fwjPrjcQyvbXDkEySzKHwu/transactions/full
    /// </summary>
    public class BtcTransactionJson
    {
        /// <summary>
        /// txid
        /// </summary>
        [JsonProperty("txid")]
        public string TxId { get; set; }

        /// <summary>
        /// size
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// version
        /// </summary>
        [JsonProperty("version")]
        public int Version { get; set; }

        /// <summary>
        /// locktime
        /// </summary>
        [JsonProperty("locktime")]
        public ulong Locktime { get; set; }

        /// <summary>
        /// fee
        /// </summary>
        [JsonProperty("fee")]
        public ulong Fee { get; set; }

        /// <summary>
        /// inputs
        /// </summary>
        [JsonProperty("inputs")]
        public BtcTransactionInputJson[] Inputs { get; set; }

        /// <summary>
        /// outputs
        /// </summary>
        [JsonProperty("outputs")]
        public object[] Outputs { get; set; }

        /// <summary>
        /// block
        /// </summary>
        [JsonProperty("block")]
        public object Block { get; set; }

        /// <summary>
        /// deleted
        /// </summary>
        [JsonProperty("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// time
        /// </summary>
        [JsonProperty("time")]
        public ulong Time { get; set; }

        /// <summary>
        /// rbf
        /// </summary>
        [JsonProperty("rbf")]
        public bool Rbf { get; set; }

        /// <summary>
        /// weight
        /// </summary>
        [JsonProperty("weight")]
        public int Weight { get; set; }
    }
}
