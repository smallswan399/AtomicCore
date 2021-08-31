using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Account Json
    /// </summary>
    public class TronNetAccountInfoJson : TronNetValidRestJson
    {
        /// <summary>
        /// address
        /// </summary>
        [JsonProperty("address"), JsonConverter(typeof(TronNetHexAddressJsonConverter))]
        public string Address { get; set; }

        /// <summary>
        /// account_name
        /// </summary>
        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        /// <summary>
        /// trx balance
        /// </summary>
        [JsonProperty("balance"), JsonConverter(typeof(TronNetTrxUnitJsonConverter))]
        public decimal TrxBalance { get; set; }

        /// <summary>
        /// latest_opration_time
        /// </summary>
        [JsonProperty("latest_opration_time")]
        public ulong LatestOprationTime { get; set; }

        /// <summary>
        /// latest_consume_free_time
        /// </summary>
        [JsonProperty("latest_consume_free_time")]
        public ulong LatestConsumeFreeTime { get; set; }

        /// <summary>
        /// account_resource
        /// </summary>
        [JsonProperty("account_resource")]
        public TronNetAccountResourceJson AccountResource { get; set; }

        /// <summary>
        /// owner_permission
        /// </summary>
        [JsonProperty("owner_permission")]
        public TronNetOwnerPermissionJson OwnerPermission { get; set; }

        /// <summary>
        /// active_permission
        /// </summary>
        [JsonProperty("active_permission")]
        public TronNetActivePermissionJson[] ActivePermission { get; set; }

        /// <summary>
        /// trc10 token asset list
        /// </summary>
        [JsonProperty("assetV2")]
        public TronNetAssetV2Json[] AssetV2 { get; set; }
    }
}
