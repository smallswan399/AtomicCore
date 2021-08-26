using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Node PeerInfo Json
    /// </summary>
    public class TronNetNodePeerInfoJson : TronNetNodeHostJson
    {
        public bool active { get; set; }

        public decimal avgLatency { get; set; }

        public int blockInPorcSize { get; set; }

        public ulong connectTime { get; set; }

        public ulong disconnectTimes { get; set; }

        public ulong headBlockTimeWeBothHave { get; set; }

        public string headBlockWeBothHave { get; set; }

        public int inFlow { get; set; }

        public ulong lastBlockUpdateTime { get; set; }

        public string lastSyncBlock { get; set; }

        public string localDisconnectReason { get; set; }

        public bool needSyncFromPeer { get; set; }

        public bool needSyncFromUs { get; set; }

        public int nodeCount { get; set; }

        public string nodeId { get; set; }

        public int remainNum { get; set; }

        public string remoteDisconnectReason { get; set; }

        public decimal score { get; set; }

        public int syncBlockRequestedSize { get; set; }

        public bool syncFlag { get; set; }

        public int syncToFetchSize { get; set; }

        public int syncToFetchSizePeekNum { get; set; }

        public int unFetchSynNum { get; set; }
    }
}
