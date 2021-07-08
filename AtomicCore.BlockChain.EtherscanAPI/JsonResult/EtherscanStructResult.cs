using Newtonsoft.Json;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// JSON解析返回结构体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EtherscanStructResult<T> : EtherscanResultBase
        where T : struct
    {
        /// <summary>
        /// 数据结果
        /// </summary>
        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
