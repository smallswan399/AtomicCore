namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Account Rest
    /// https://cn.developers.tron.network/reference/get-account-info-by-address
    /// </summary>
    public interface ITronGridAccountRest
    {
        /// <summary>
        /// Get Account Info
        /// </summary>
        /// <param name="address"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        TronGridRestResult<TronGridAccountInfo> GetAccount(string address, TronGridRequestQuery query = null);

        /// <summary>
        /// Get the transfer records of an account history, including trc10 & trc20 transfers and TRX transfers 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        TronGridRestResult<TronGridTransactionInfo> GetTransactions(string address, TronGridRequestQuery query = null);
    }
}
