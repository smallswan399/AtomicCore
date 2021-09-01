namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Rest API
    /// </summary>
    public interface ITronNetRest : ITronNetAddressUtilitiesRest, ITronNetAccountRest, ITronTransactionsRest, ITronNetQueryNetworkRest, ITronNetTRC10TokenRest
    {

    }
}
