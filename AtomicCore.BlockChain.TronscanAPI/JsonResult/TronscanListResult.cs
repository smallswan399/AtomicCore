using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tronscan List Result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class TronscanListResult<T> : TronscanResultBase
    {
        /// <summary>
        /// 列表结果
        /// </summary>
        [JsonProperty("data")]
        public List<T> Result { get; set; }
    }
}
