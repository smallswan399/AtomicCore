namespace AtomicCore.BlockChain.EtherscanAPI
{
    /// <summary>
    /// https://docs.etherscan.io/api-endpoints/geth-parity-proxy
    /// </summary>
    public interface IEtherProxy
    {
        /// <summary>
        /// Returns the number of most recent block
        /// </summary>
        /// <returns></returns>
        long GetBlockNumber();

        /// <summary>
        /// Returns the number of transactions performed by an address.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        long GetTransactionCount(string address);
    }
}
