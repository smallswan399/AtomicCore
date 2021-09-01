﻿namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Account Rest Inteface
    /// </summary>
    public interface ITronNetAccountRest
    {
        /// <summary>
        /// Create an account. Uses an already activated account to create a new account
        /// </summary>
        /// <param name="ownerAddress">Owner_address is an activated account，converted to a hex String.If the owner_address has enough bandwidth obtained by freezing TRX, then creating an account will only consume bandwidth , otherwise, 0.1 TRX will be burned to pay for bandwidth, and at the same time, 1 TRX will be required to be created.</param>
        /// <param name="accountAddress">account_address is the address of the new account, converted to a hex string, this address needs to be calculated in advance</param>
        /// <param name="permissionID">Optional,whether the address is in base58 format</param>
        /// <param name="visible">Optional,for multi-signature use</param>
        /// <returns></returns>
        TronNetCreateTransactionRestJson CreateAccount(string ownerAddress, string accountAddress, int? permissionID = null, bool? visible = null);

        /// <summary>
        /// Query information about an account,Including balances, stake, votes and time, etc.
        /// </summary>
        /// <param name="address">address should be converted to a hex string</param>
        /// <param name="visible">Optional,whether the address is in base58 format</param>
        TronNetAccountInfoJson GetAccount(string address, bool? visible = null);

        /// <summary>
        /// Modify account name
        /// </summary>
        /// <param name="accountName">Account_name is the name of the account</param>
        /// <param name="ownerAddress">Owner_address is the account address to be modified</param>
        /// <param name="permissionID">Optional,for multi-signature use</param>
        /// <param name="visible">Optional,whether the address is in base58 format</param>
        /// <returns></returns>
        TronNetCreateTransactionRestJson UpdateAccount(string accountName, string ownerAddress, int? permissionID = null, bool? visible = null);

        /// <summary>
        /// Update the account's permission.
        /// </summary>
        /// <param name="ownerAddress"></param>
        /// <param name="actives"></param>
        /// <param name="owner"></param>
        /// <param name="witness"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        TronNetCreateTransactionRestJson AccountPermissionUpdate(string ownerAddress, TronNetAccountOperatePermissionJson actives, TronNetAccountOperatePermissionJson owner, TronNetAccountOperatePermissionJson witness, bool? visible = null);

        //void GetAccountBalance();
    }
}
