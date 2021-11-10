using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomicCore.BlockChain.OMNINet
{
    public class GetBlockByNumberResponse
    {
        /// <summary>
        /// 区块哈希值
        /// </summary>
        public string hash { get; set; }

        /// <summary>
        /// 确认次数
        /// </summary>
        public int confirmations { get; set; }

        /// <summary>
        /// 区块的大小
        /// </summary>
        public string size { get; set; }

        /// <summary>
        /// 区块索引
        /// </summary>
        public int height { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string version { get; set; }

        public string merkleroot { get; set; }

        /// <summary>
        /// 区块货币价值
        /// </summary>
        public double mint { get; set; }

        /// <summary>
        /// 从1970-01-01的时间戳 单位秒
        /// </summary>
        public int time { get; set; }

        public string nonce { get; set; }

        public string bits { get; set; }

        /// <summary>
        /// 难度
        /// </summary>
        public double difficulty { get; set; }

        public string previousblockhash { get; set; }

        public string nextblockhash { get; set; }

        public string flags { get; set; }

        public string proofhash { get; set; }

        public int entropybit { get; set; }

        public string modifier { get; set; }

        public string modifierchecksum { get; set; }

        public string[] tx { get; set; }

        public string signature { get; set; }
    }
}
