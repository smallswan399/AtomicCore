namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Rest API
    /// </summary>
    public interface ITronRestAPI
    {
        /// <summary>
        /// Get Transaction By Txid
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        TronTransactionRestJson GetTransactionByID(string txid);
    }
}
