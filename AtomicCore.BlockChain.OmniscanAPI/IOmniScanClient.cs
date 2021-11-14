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
        Dictionary<string, OmniAssetCollectionJson> GetAddressV2(params string[] address);
    }
}
