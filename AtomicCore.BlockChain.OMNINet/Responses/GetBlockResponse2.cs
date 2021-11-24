using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtomicCore.BlockChain.OMNINet
{
    public class GetBlockResponse2
    {
        public string hash { get; set; }

        public uint confirmations { get; set; }

        public uint size { get; set; }

        public uint height { get; set; }

        public uint version { get; set; }

        public string merkleroot { get; set; }

        public double mint { get; set; }

        public ulong time { get; set; }

        public string nonce { get; set; }

        public string bits { get; set; }

        public double difficulty { get; set; }

        public string previousblockhash { get; set; }

        public string nextblockhash { get; set; }

        public string flags { get; set; }

        public string proofhash { get; set; }

        public int entropybit { get; set; }

        public string modifier { get; set; }

        public string modifierchecksum { get; set; }

        public List<string> tx { get; set; }

        public string signature { get; set; }
    }
}
