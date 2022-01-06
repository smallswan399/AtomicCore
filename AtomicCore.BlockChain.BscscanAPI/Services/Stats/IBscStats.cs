using System.Collections.Generic;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// IBscStats Interface
    /// </summary>
    public interface IBscStats
    {
        /// <summary>
        /// Returns the current amount of BNB in circulation.
        /// </summary>
        /// <returns></returns>
        decimal GetBNBTotalSupply();

        /// <summary>
        /// Returns the top 21 validators for the Binance Smart Chain.
        /// </summary>
        /// <returns></returns>
        List<string> GetBscValidatorList();

        /// <summary>
        /// Returns the latest price of 1 BNB.
        /// </summary>
        /// <returns></returns>
        decimal GetBNBLastPrice();
    }
}
