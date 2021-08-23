using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Address Utilities Rest API
    /// </summary>
    public interface ITronAddressUtilitiesRestAPI
    {
        void GenerateAddress();

        void CreateAddress();

        void ValidateAddress();
    }
}
