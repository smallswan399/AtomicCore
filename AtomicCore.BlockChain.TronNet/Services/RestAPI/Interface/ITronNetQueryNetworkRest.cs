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
        /// Get Transaction By Txid
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        TronNetTransactionRestJson GetTransactionByID(string txid);
    }
}
