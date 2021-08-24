﻿using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Transfer Contract Value Rest Json
    /// </summary>
    public class TronNetTransferContractVauleJson : TronNetContractValueBaseJson
    {
        /// <summary>
        /// toAddress
        /// </summary>
        [JsonProperty("to_address"), JsonConverter(typeof(TronNetScriptAddressJsonConverter))]
        public string ToAddress { get; set; }

        /// <summary>
        /// amount,unit is sun
        /// </summary>
        [JsonProperty("amount"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong Amount { get; set; }
    }
}
