namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// IBscGethProxy Interface
    /// </summary>
    public interface IBscGethProxy
    {
        /// <summary>
        /// Returns the number of most recent block
        /// </summary>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        BscscanSingleResult<string> GetBlockNumber(BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10);


    }
}
