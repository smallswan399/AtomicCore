using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Delegated Json
    /// </summary>
    public class TronNetDelegatedJson
    {
        /// <summary>
        /// from address
        /// </summary>
        [JsonProperty("from"),JsonConverter(typeof(TronNetHexAddressJsonConverter))]
        public string From { get; set; }

        /// <summary>
        /// to address
        /// </summary>
        [JsonProperty("to"), JsonConverter(typeof(TronNetHexAddressJsonConverter))]
        public string To { get; set; }

        /// <summary>
        /// frozen_balance_for_energy
        /// </summary>
        [JsonProperty("frozen_balance_for_energy"),JsonConverter(typeof(TronNetTrxUnitJsonConverter))]
        public decimal FrozenBalanceForEnergy { get; set; }

        /// <summary>
        /// expire_time_for_energy
        /// </summary>
        [JsonProperty("expire_time_for_energy")]
        public ulong ExpireTimeForEnergy { get; set; }
    }
}
