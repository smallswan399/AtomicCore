namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// IBscBlocks Interface
    /// </summary>
    public interface IBscBlocks
    {
        /// <summary>
        /// Returns the block reward awarded for validating a certain block. 
        /// </summary>
        /// <param name="blockNo">the integer block number to check block rewards for eg. 12697906</param>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        BscscanSingleResult<BscBlockRewardJson> GetBlockRewardByNumber(long blockNo, BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10);

        /// <summary>
        /// Returns the estimated time remaining, in seconds, until a certain block is validated.
        /// </summary>
        /// <param name="blockNo">the integer block number to check block rewards for eg. 12697906</param>
        /// <param name="network">network</param>
        /// <param name="cacheMode">cache mode</param>
        /// <param name="expiredSeconds">expired seconds</param>
        /// <returns></returns>
        BscscanSingleResult<BscBlockEstimatedJson> GetBlockEstimatedByNumber(long blockNo, BscNetwork network = BscNetwork.BscMainnet, BscscanCacheMode cacheMode = BscscanCacheMode.None, int expiredSeconds = 10);
    }
}
