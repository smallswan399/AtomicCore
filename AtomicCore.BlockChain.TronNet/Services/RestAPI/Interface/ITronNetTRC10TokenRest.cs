namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TRC10 Token
    /// </summary>
    public interface ITronNetTRC10TokenRest
    {
        /// <summary>
        /// Get AssetIssue By Account
        /// </summary>
        /// <param name="tronAddress">tron address</param>
        /// <param name="visible"></param>
        /// <returns></returns>
        TronNetAssetCollectionJson GetAssetIssueByAccount(string tronAddress, bool? visible = null);

        /// <summary>
        /// Get AssetIssue By Id
        /// </summary>
        /// <param name="assertID"></param>
        /// <returns></returns>
        TronNetAssetInfoJson GetAssetIssueById(int assertID);

        /// <summary>
        /// Get AssetIssue List
        /// </summary>
        /// <returns></returns>
        TronNetAssetCollectionJson GetAssetIssueList();

        /// <summary>
        /// Get Paginated AssentIssue LIst
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        TronNetAssetCollectionJson GetPaginatedAssetIssueList(int offset, int limit);

        //void TransferAsset(string owner_address, string to_address, string asset_name, ulong amount, int? permission_id, bool? visible);

        //void CreateAssetIssue(string owner_address, string name, int precision, string abbr, ulong total_supply, ulong trx_num, ulong num, DateTime start_time, DateTime end_time, string description, string url, ulong free_asset_net_limit, ulong public_free_asset_net_limit, string frozen_supply);

        //void ParticipateAssetIssue(string to_address, string owner_address, ulong amount, string asset_name, bool? visible);

        //void UnfreezeAsset(string owner_address, int? permission_id, bool? visible);

        //void UpdateAsset(string owner_address, string description, string url, int new_limit, int new_public_limit, int? permission_id, bool? visible);

        //void EasyTransferAsset(string passPhrase, string toAddress, string assetId, ulong amount, bool? visible);

        //void EasyTransferAssetByPrivate(string privateKey,string toAddress, string assetId, ulong amount, bool? visible);
    }
}
