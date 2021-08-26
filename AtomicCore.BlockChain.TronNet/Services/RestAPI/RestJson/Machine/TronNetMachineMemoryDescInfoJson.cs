using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Machine Memory Desc Info Json
    /// </summary>
    public class TronNetMachineMemoryDescInfoJson
    {
        public ulong initSize { get; set; }

        public ulong maxSize { get; set; }

        public string name { get; set; }

        public decimal useRate { get; set; }

        public ulong useSize { get; set; }
    }
}
