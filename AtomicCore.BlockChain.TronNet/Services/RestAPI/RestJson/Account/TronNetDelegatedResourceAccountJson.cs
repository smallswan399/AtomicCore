using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Delegated Resource Account
    /// </summary>
    public class TronNetDelegatedResourceAccountJson : TronNetValidRestJson
    {
        /// <summary>
        /// account
        /// </summary>
        [JsonProperty("account"), JsonConverter(typeof(TronNetHexAddressJsonConverter))]
        public string Account { get; set; }

        /// <summary>
        /// toAccounts
        /// </summary>
        [JsonProperty("toAccounts"),JsonConverter(typeof(TronNetHexAddressArrayJsonConverter))]
        public string[] ToAccounts { get; set; }
    }
}
