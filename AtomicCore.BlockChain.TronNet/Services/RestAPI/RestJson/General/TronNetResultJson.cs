using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Result Json
    /// </summary>
    public class TronNetResultJson : TronNetValidRestJson
    {
        /// <summary>
        /// ContractRet
        /// </summary>
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
