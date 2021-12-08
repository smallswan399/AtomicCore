using Newtonsoft.Json;

namespace AtomicCore.BlockChain.ExplorerAPI
{
    /// <summary>
    /// btc block transaction json
    /// </summary>
    public class BtcBlockTransactionJson
    {
        /// <summary>
        /// transaction hash
        /// </summary>
        [JsonProperty("hash")]
        public string TxHash { get; set; }

        /// <summary>
        /// ver
        /// </summary>
        [JsonProperty("ver")]
        public int TxVersion { get; set; }

        public int vin_sz { get; set; }

        public int vout_sz { get; set; }

        public int size { get; set; }

        public ulong fee { get; set; }

        /////// <summary>
        /////// relayed_by
        /////// </summary>
        ////[JsonProperty("relayed_by")]
        ////public string RelayedBy { get; set; }

        public ulong lock_time { get; set; }

        public ulong tx_index { get; set; }

        public bool double_spend { get; set; }

        public ulong times { get; set; }

        public ulong block_index { get; set; }

        public ulong block_height { get; set; }

        /// <summary>
        /// tx vins
        /// </summary>
        [JsonProperty("inputs")]
        public BtcBlockTxVinJson[] TxVins { get; set; }
    }
}
