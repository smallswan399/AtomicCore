namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// TRON SCAN INTERFACE
    /// </summary>
    public interface ITronScanClient
    {
        /// <summary>
        /// 1.Block Overview
        /// </summary>
        /// <returns></returns>
        TronChainOverviewJson BlockOverview();

        /// <summary>
        /// 2.Get Last Block
        /// </summary>
        /// <returns></returns>
        TronBlockBasicJson GetLastBlock();

        /// <summary>
        /// 4.Get Account Assets list
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        TronAccountAssetJson GetAccountAssets(string address);

        /// <summary>
        /// List the transfers related to a specified TRC10 token(Order by Desc)
        /// ps:only display the latest 10,000 data records in the query time range
        /// </summary>
        /// <param name="address">token creation address</param>
        /// <param name="start">query index for pagination</param>
        /// <param name="limit">page size for pagination</param>
        /// <param name="name">token name</param>
        /// <param name="start_timestamp">query date range</param>
        /// <param name="end_timestamp">query date range</param>
        /// <returns>TRC10 token transfers list</returns>
        TronTRC10TransactionListJson GetTRC10Transactions(string address, int start = 0, int limit = 20, string name = null, ulong? start_timestamp = null, ulong? end_timestamp = null);


    }
}
