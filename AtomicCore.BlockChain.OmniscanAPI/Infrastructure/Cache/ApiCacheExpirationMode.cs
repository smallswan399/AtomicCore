namespace AtomicCore.BlockChain.OmniscanAPI
{
    /// <summary>
    /// cache expiration mode
    /// </summary>
    internal enum ApiCacheExpirationMode
    {
        /// <summary>
        /// none
        /// </summary>
        None = 0,

        /// <summary>
        /// absolute expired
        /// </summary>
        AbsoluteExpired = 1,

        /// <summary>
        /// slide expired
        /// </summary>
        SlideExpired = 2
    }
}
