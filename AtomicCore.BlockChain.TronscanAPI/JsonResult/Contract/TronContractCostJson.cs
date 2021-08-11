using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Cotract Cost Json
    /// </summary>
    public class TronContractCostJson
    {
        /// <summary>
        /// NetFee
        /// </summary>
        [JsonProperty("net_fee")]
        public int NetFee { get; set; }

        /// <summary>
        /// EnergyUsage
        /// </summary>
        [JsonProperty("energy_usage")]
        public int EnergyUsage { get; set; }

        /// <summary>
        /// EnergyFee
        /// </summary>
        [JsonProperty("energy_fee")]
        public int EnergyFee { get; set; }

        /// <summary>
        /// EnergyUsageTotal
        /// </summary>
        [JsonProperty("energy_usage_total")]
        public int EnergyUsageTotal { get; set; }

        /// <summary>
        /// OriginEnergyUsage
        /// </summary>
        [JsonProperty("origin_energy_usage")]
        public int OriginEnergyUsage { get; set; }

        /// <summary>
        /// NetUsage
        /// </summary>
        [JsonProperty("net_usage")]
        public int NetUsage { get; set; }
    }
}
