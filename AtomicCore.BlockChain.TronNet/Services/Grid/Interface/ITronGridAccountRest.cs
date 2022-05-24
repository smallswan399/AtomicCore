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
        TronGridAccountInfo GetAccount(string address, TronGridRequestQuery query = null);


    }
}
