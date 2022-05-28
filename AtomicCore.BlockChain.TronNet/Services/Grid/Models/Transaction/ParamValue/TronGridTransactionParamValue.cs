using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Transaction Param Value
    /// PS : The current data is the base class for all trading parameters
    /// </summary>
    public class TronGridTransactionParamValue : ITronGridTransactionParamValue
    {
        #region Propertys

        /// <summary>
        /// owner_address
        /// </summary>
        [JsonProperty("owner_address"), JsonConverter(typeof(TronGridAddressBase58JsonConverter))]
        public string OwnerAddress { get; set; }

        #endregion
    }
}
