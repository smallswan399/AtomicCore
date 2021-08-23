using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Transaction Rest API
    /// </summary>
    public interface ITronTransactionsRestAPI
    {
        void GetTransactionSign();

        void BroadcastTransaction();

        void BroadcastHex();

        void EasyTransfer();

        void EasyTransferByPrivate();

        void CreateTransaction(string ownerAddress, string toAddress, ulong amount, int? permissionID, bool? visible);
    }
}
