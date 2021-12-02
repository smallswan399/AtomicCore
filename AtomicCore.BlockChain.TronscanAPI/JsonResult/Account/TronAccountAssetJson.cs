using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Numerics;

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

        /// <summary>
        /// transactions_out
        /// </summary>
        [JsonProperty("transactions_out")]
        public int TransactionsOut { get; set; }

        /// <summary>
        /// acquiredDelegateFrozenForBandWidth
        /// </summary>
        [JsonProperty("acquiredDelegateFrozenForBandWidth")]
        public ulong AcquiredDelegateFrozenForBandWidth { get; set; }

        /// <summary>
        /// rewardNum
        /// </summary>
        [JsonProperty("rewardNum")]
        public int RewardNum { get; set; }

        /// <summary>
        /// ownerPermission
        /// </summary>
        [JsonProperty("ownerPermission")]
        public TronAccountPermissionJson OwnerPermission { get; set; }

        /// <summary>
        /// tokenBalances
        /// </summary>
        [JsonProperty("tokenBalances")]
        public TronAccountTokenBalanceJson[] TokenBalances { get; set; }

        /// <summary>
        /// delegateFrozenForEnergy
        /// </summary>
        [JsonProperty("delegateFrozenForEnergy")]
        public ulong DelegateFrozenForEnergy { get; set; }

        /// <summary>
        /// balances
        /// </summary>
        [JsonProperty("balances")]
        public TronAccountTokenBalanceJson[] Balances { get; set; }

        /// <summary>
        /// trc721token_balances
        /// </summary>
        [JsonProperty("trc721token_balances")]
        public JObject TRC721TokenBalances { get; set; }

        /// <summary>
        /// TRX Balance
        /// </summary>
        [JsonProperty("balance")]
        public BigInteger Balance { get; set; }

        /// <summary>
        /// voteTotal
        /// </summary>
        [JsonProperty("voteTotal")]
        public ulong VoteTotal { get; set; }

        /// <summary>
        /// totalFrozen
        /// </summary>
        [JsonProperty("totalFrozen")]
        public BigInteger TotalFrozen { get; set; }

        /// <summary>
        /// tokens
        /// </summary>
        [JsonProperty("tokens")]
        public TronAccountTokenBalanceJson[] Tokens { get; set; }

        /// <summary>
        /// delegated
        /// </summary>
        [JsonProperty("delegated")]
        public JObject Delegated { get; set; }

        /// <summary>
        /// transactions_in
        /// </summary>
        [JsonProperty("transactions_in")]
        public ulong TransactionsIn { get; set; }

        /// <summary>
        /// totalTransactionCount
        /// </summary>
        [JsonProperty("totalTransactionCount")]
        public ulong TotalTransactionCount { get; set; }

        /// <summary>
        /// representative
        /// </summary>
        [JsonProperty("representative")]
        public JObject Representative { get; set; }

        /// <summary>
        /// frozenForBandWidth
        /// </summary>
        [JsonProperty("frozenForBandWidth")]
        public BigInteger FrozenForBandWidth { get; set; }

        /// <summary>
        /// reward
        /// </summary>
        [JsonProperty("reward")]
        public ulong Reward { get; set; }

        /// <summary>
        /// addressTagLogo
        /// </summary>
        [JsonProperty("addressTagLogo")]
        public JObject AddressTagLogo { get; set; }

        /// <summary>
        /// allowExchange
        /// </summary>
        [JsonProperty("allowExchange")]
        public JObject AllowExchange { get; set; }

        /// <summary>
        /// address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// frozen_supply
        /// </summary>
        [JsonProperty("frozen_supply")]
        public JObject FrozenSupply { get; set; }

        /// <summary>
        /// bandwidth
        /// </summary>
        [JsonProperty("bandwidth")]
        public JObject Bandwidth { get; set; }
    }
}
