namespace AtomicCore.BlockChain.OMNINet
{
    /// <summary>
    /// ExtracoinConstants
    /// </summary>
    public class ExtracoinConstants
    {
        /// <summary>
        /// Constants
        /// </summary>
        public sealed class Constants : CoinConstants<Constants>
        {
            public readonly int OneBitcoinInSatoshis = 100000000;
            public readonly decimal OneSatoshiInBTC = 0.00000001M;
            public readonly int SatoshisPerBitcoin = 100000000;
            public readonly string Symbol = "฿";
        }
    }
}
