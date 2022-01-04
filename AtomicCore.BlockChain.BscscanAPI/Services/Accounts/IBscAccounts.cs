namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// IBscAccounts Interface
    /// </summary>
    public interface IBscAccounts
    {
        /// <summary>
        /// Get Balance
        /// </summary>
        /// <param name="address"></param>
        /// <param name="network"></param>
        /// <param name="cacheMode"></param>
        /// <param name="expiredSeconds"></param>
        /// <returns></returns>
        BscscanSingleResult<decimal> GetBalance(string address, BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10);

        /// <summary>
        /// Get Balance
        /// </summary>
        /// <param name="address"></param>
        /// <param name="network"></param>
        /// <param name="cacheMode"></param>
        /// <param name="expiredSeconds"></param>
        /// <returns></returns>
        BscscanListResult<BscAccountBalanceJson> GetBalanceList(string[] address, BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10);
    }
}
