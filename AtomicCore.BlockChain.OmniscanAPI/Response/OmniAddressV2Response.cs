using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.OmniscanAPI
{
    /// <summary>
    /// Omni Address V2 Response
    /// </summary>
    public class OmniAddressV2Response : OmniErrorResponse
    {
        #region Propertys

        [JsonProperty("balance")]
        public Dictionary<string, OmniAssetCollectionJson> AddressBalances { get; set; }

        #endregion
    }
}
