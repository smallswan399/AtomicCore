using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Token Asset Json
    /// </summary>
    public class TronAccountAssetJson
    {
        /// <summary>
        /// trc20 token balances
        /// </summary>
        [JsonProperty("trc20token_balances")]
        public TronAssetBalanceJson[] Trc20Balances { get; set; }
    }
}
