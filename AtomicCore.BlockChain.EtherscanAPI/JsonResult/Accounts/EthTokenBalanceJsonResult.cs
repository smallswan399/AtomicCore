using Newtonsoft.Json;
using System.Numerics;

namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// 获取代币数量（最小单位的整数）
    /// </summary>
    public sealed class EthTokenBalanceJsonResult : EtherscanResultBase
    {
        /// <summary>
        /// 数据结果
        /// </summary>
        [JsonProperty("result")]
        public BigInteger Result { get; set; }
    }
}
