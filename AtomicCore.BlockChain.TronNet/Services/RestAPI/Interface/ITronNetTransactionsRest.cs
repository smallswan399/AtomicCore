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

        /// <summary>
        /// Create a TRX transfer transaction. 
        /// If to_address does not exist, then create the account on the blockchain.
        /// </summary>
        /// <param name="ownerAddress">To_address is the transfer address</param>
        /// <param name="toAddress">Owner_address is the transfer address</param>
        /// <param name="amount">Amount is the transfer amount,the unit is trx</param>
        /// <param name="permissionID">Optional, for multi-signature use</param>
        /// <param name="visible">Optional.Whehter the address is in base58 format</param>
        /// <returns></returns>
        TronNetCreateTransactionRestJson CreateTransaction(string ownerAddress, string toAddress, decimal amount, int? permissionID, bool? visible);
    }
}
