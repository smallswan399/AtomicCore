using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.OmniscanAPI
{
    /// <summary>
    /// Omni scan client interface
    /// </summary>
    public interface IOmniScanClient
    {
        /// <summary>
        /// Get Address V2
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        OmniAddressV2Response GetAddressV2(params string[] address);
    }
}
