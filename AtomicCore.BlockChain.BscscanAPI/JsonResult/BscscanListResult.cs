using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bsc list result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BscscanListResult<T> : BscscanBaseResult
    {
        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("result")]
        public List<T> Result { get; set; }
    }
}
