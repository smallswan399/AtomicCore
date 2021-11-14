using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.OmniscanAPI
{
    /// <summary>
    /// omni asset balance json
    /// </summary>
    public class OmniAssetBalanceJson
    {
        public int id { get; set; }

        public string symbol { get; set; }

        public decimal value { get; set; }

        public decimal frozen { get; set; }

        public decimal reserved { get; set; }

        public bool divisible { get; set; }

        public string pendingpos { get; set; }

        public string pendingneg { get; set; }

        public bool error { get; set; }
    }
}
