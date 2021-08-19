using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TRC10 Token
    /// </summary>
    public interface ITronTRC10TokenRestAPI
    {
        /// <summary>
        /// Get AssetIssue By ID
        /// </summary>
        /// <param name="assertID"></param>
        void GetAssetIssueById(int assertID);
    }
}
