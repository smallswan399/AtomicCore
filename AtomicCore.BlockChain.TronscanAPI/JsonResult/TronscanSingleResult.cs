using Newtonsoft.Json;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tronscan Single Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class TronscanSingleResult<T> : TronscanResultBase
    {
        /// <summary>
        /// 数据结果
        /// </summary>
        [JsonProperty("data")]
        public T Result { get; set; }
    }
}
