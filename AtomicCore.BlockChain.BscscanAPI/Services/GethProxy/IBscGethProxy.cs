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
        long GetBlockNumber(BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10);

        /// <summary>
        /// Returns information about a block by block number.
        /// </summary>
        /// <param name="blockNumber">the block number</param>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        BscBlockSimpleJson GetBlockSimple(long blockNumber, BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10);
    }
}
