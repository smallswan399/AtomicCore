using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.BscscanAPI.JsonResult
{
    /// <summary>
    /// bsc返回列表结果集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BscscanListResult<T> : BscscanBaseResult
    {
        /// <summary>
        /// 数据结果
        /// </summary>
        [JsonProperty("result")]
        public List<T> Result { get; set; }
    }
}
