using System;

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

        /// <summary>
        /// Create AssetIssue
        /// </summary>
        /// <param name="ownerAddress">Owner Address</param>
        /// <param name="tokenName">Token Name</param>
        /// <param name="tokenPrecision">Token Precision</param>
        /// <param name="tokenAbbr">Token Abbr</param>
        /// <param name="totalSupply">Token Total Supply</param>
        /// <param name="trxNum">
        /// Define the price by the ratio of trx_num/num(The unit of 'trx_num' is SUN)
        /// </param>
        /// <param name="num">
        /// Define the price by the ratio of trx_num/num
        /// </param>
        /// <param name="startTime">ICO StartTime</param>
        /// <param name="endTime">ICO EndTime</param>
        /// <param name="tokenDescription">Token Description</param>
        /// <param name="tokenUrl">Token Official Website Url</param>
        /// <param name="freeAssetNetLimit">Token Free Asset Net Limit</param>
        /// <param name="publicFreeAssetNetLimit">Token Public Free Asset Net Limit</param>
        /// <param name="frozenSupply">Token Frozen Supply</param>
        /// <returns></returns>
        string CreateAssetIssue(string ownerAddress, string tokenName, int tokenPrecision, string tokenAbbr, ulong totalSupply, ulong trxNum, ulong num, DateTime startTime, DateTime endTime, string tokenDescription, string tokenUrl, ulong freeAssetNetLimit, ulong publicFreeAssetNetLimit, TronNetFrozenSupplyJson frozenSupply);

        //void ParticipateAssetIssue(string to_address, string owner_address, ulong amount, string asset_name, bool? visible);

        //void UnfreezeAsset(string owner_address, int? permission_id, bool? visible);

        //void UpdateAsset(string owner_address, string description, string url, int new_limit, int new_public_limit, int? permission_id, bool? visible);

        //void EasyTransferAsset(string passPhrase, string toAddress, string assetId, ulong amount, bool? visible);

        //void EasyTransferAssetByPrivate(string privateKey,string toAddress, string assetId, ulong amount, bool? visible);
    }
}
