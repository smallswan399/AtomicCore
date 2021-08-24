namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Query Network
    /// </summary>
    public interface ITronNetQueryNetworkRest
    {
        /// <summary>
        /// Get Transaction By Txid
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        TronTransactionRestJson GetTransactionByID(string txid);
    }
}
