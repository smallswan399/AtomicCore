using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Transfer Contract Value Json
    /// TRX Transfer,
    /// </summary>
    public class TronNetTransferContractVauleJson : TronNetContractBaseValueJson
    {
        /// <summary>
        /// toAddress
        /// </summary>
        [JsonProperty("to_address"), JsonConverter(typeof(TronNetHexAddressJsonConverter))]
        public string ToAddress { get; set; }

        /// <summary>
        /// amount,unit is sun
        /// </summary>
        [JsonProperty("amount"), JsonConverter(typeof(TronNetULongJsonConverter))]
        public ulong Amount { get; set; }
    }
}
