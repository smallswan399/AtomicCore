namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Query Network
    /// </summary>
    public interface ITronNetQueryNetworkRest
    {
        /// <summary>
        /// Get Block By Number
        /// </summary>
        /// <param name="blockHeight"></param>
        /// <returns></returns>
        TronNetBlockJson GetBlockByNum(ulong blockHeight);

        /// <summary>
        /// Get Block By Hash(ID)
        /// </summary>
        /// <param name="blockID"></param>
        /// <returns></returns>
        TronNetBlockJson GetBlockById(string blockID);

        /// <summary>
        /// Get a list of block objects by last blocks
        /// </summary>
        /// <param name="lastNum"></param>
        /// <returns></returns>
        TronNetBlockListJson GetBlockByLatestNum(ulong lastNum);

        /// <summary>
        /// Returns the list of Block Objects included in the 'Block Height' range specified.
        /// </summary>
        /// <param name="startNum">Starting block height, including this block.</param>
        /// <param name="endNum">Ending block height, excluding that block.</param>
        /// <returns></returns>
        TronNetBlockListJson GetBlockByLimitNext(ulong startNum, ulong endNum);

        /// <summary>
        /// Query the latest block information
        /// </summary>
        /// <returns></returns>
        TronNetBlockDetailsJson GetNowBlock();

        /// <summary>
        /// Get Transaction By Txid
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        TronNetTransactionRestJson GetTransactionByID(string txid);

        /// <summary>
        /// Get Transaction Info By Txid
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        TronNetTransactionInfoJson GetTransactionInfoById(string txid);
    }
}
