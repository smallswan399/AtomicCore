using Newtonsoft.Json;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bsc返回对象结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class BscscanSingleResult<T> : BscscanBaseResult
    {
        /// <summary>
        /// 数据结果
        /// </summary>
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
