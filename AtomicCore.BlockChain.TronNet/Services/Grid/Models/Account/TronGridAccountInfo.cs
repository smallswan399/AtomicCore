using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Account Info
    /// </summary>
    public class TronGridAccountInfo
    {
        /// <summary>
        /// address
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// trx balance
        /// </summary>
        [JsonProperty("balance"), JsonConverter(typeof(TronNetTrxUnitJsonConverter))]
        public decimal Balance { get; set; }

        /// <summary>
        /// owner_permission
        /// </summary>
        [JsonProperty("owner_permission")]
        public TronGridPermissionInfo OwnerPermission { get; set; }

        /// <summary>
        /// active_permission
        /// </summary>
        [JsonProperty("active_permission")]
        public TronGridActivePermissionInfo ActivePermission { get; set; }

        /// <summary>
        /// create time # UTC-TIMESTAMP
        /// </summary>
        [JsonProperty("create_time")]
        public ulong CreateTime { get; set; }

        /// <summary>
        /// latest opration time
        /// </summary>
        [JsonProperty("latest_opration_time")]
        public ulong LatestOprationTime { get; set; }

        /// <summary>
        /// latest consume free time # UTC-TIMESTAMP
        /// </summary>
        [JsonProperty("latest_consume_free_time")]
        public ulong LatestConsumeFreeTime { get; set; }

        /// <summary>
        /// trc10 # trc10 asset list
        /// </summary>
        [JsonProperty("assetV2")]
        public List<TronGridKVInfo> AssetV2 { get; set; }

        /// <summary>
        /// free_asset_net_usageV2
        /// </summary>
        [JsonProperty("free_asset_net_usageV2")]
        public List<TronGridKVInfo> FreeAssetNetUsageV2 { get; set; }

        /// <summary>
        /// trc20 # trc20 asset list
        /// </summary>
        [JsonProperty("trc20")]
        public Dictionary<string, System.Numerics.BigInteger> TRC20 { get; set; }
    }
}
