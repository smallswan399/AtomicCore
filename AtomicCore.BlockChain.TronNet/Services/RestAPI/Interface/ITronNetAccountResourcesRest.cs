namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// To manipulate account resources. Bandwidth or energy.
    /// </summary>
    public interface ITronNetAccountResourcesRest
    {
        /// <summary>
        /// Query the resource information of an account(bandwidth,energy,etc)
        /// </summary>
        /// <param name="address"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        TronNetAccountResourceJson GetAccountResource(string address, bool? visible = null);

        /// <summary>
        /// Query bandwidth information.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        TronNetAccountNetResourceJson GetAccountNet(string address, bool? visible = null);

        /// <summary>
        /// Stake an amount of TRX to obtain bandwidth OR Energy and TRON Power (voting rights) .
        /// Optionally, user can stake TRX to grant Energy or Bandwidth to others. 
        /// Balance amount in the denomination of sun.s
        /// </summary>
        /// <param name="ownerAddress">Owner address</param>
        /// <param name="frozenBalance">TRX stake amount,Trx</param>
        /// <param name="frozenDuration">TRX stake duration, only be specified as 3 days</param>
        /// <param name="resource">TRX stake type, 'BANDWIDTH' or 'ENERGY'</param>
        /// <param name="receiverAddress"></param>
        /// <param name="permissionID"></param>
        /// <param name="visible"></param>
        TronNetCreateTransactionRestJson FreezeBalance(string ownerAddress, decimal frozenBalance, int frozenDuration, TronNetResourceType resource, string receiverAddress = null, int? permissionID = null, bool? visible = null);
    }
}
