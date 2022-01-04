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
        /// <returns></returns>
        BscscanSingleResult<decimal> GetBalance(string address);

        /// <summary>
        /// Get Balance
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        BscscanListResult<BscAccountBalanceJson> GetBalanceList(params string[] address);
    }
}
