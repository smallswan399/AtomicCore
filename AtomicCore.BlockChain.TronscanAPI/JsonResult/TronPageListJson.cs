using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Page List Json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TronPageListJson<T>
    {
        /// <summary>
        /// page limit
        /// </summary>
        [JsonProperty("total")]
        public int PageSize { get; set; }

        /// <summary>
        /// range total count
        /// </summary>
        [JsonProperty("rangeTotal")]
        public int TotalCount { get; set; }

        /// <summary>
        /// data list
        /// </summary>
        [JsonProperty("Data")]
        public List<T> Data { get; set; }
    }
}
