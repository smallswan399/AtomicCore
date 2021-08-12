using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Transaction Cost Json
    /// </summary>
    public class TronTransactionCostJson : TronContractCostJson
    {
        /// <summary>
        /// MultiSignFee
        /// </summary>
        [JsonProperty("multi_sign_fee")]
        public int MultiSignFee { get; set; }

        /// <summary>
        /// NetFeeCost
        /// </summary>
        [JsonProperty("net_fee_cost")]
        public int NetFeeCost { get; set; }

        /// <summary>
        /// EnergyFeeCost
        /// </summary>
        [JsonProperty("energy_fee_cost")]
        public int EnergyFeeCost { get; set; }

        /// <summary>
        /// fee
        /// </summary>
        [JsonProperty("fee")]
        public int Fee { get; set; }
    }
}
