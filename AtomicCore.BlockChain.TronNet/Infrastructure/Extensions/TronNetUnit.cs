using System;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Unit
    /// </summary>
    public static class TronNetUnit
    {
        /// <summary>
        /// tron sun value
        /// </summary>
        private static long _sun_unit = 1_000_000L;

        /// <summary>
        /// Trx to Sun
        /// </summary>
        /// <param name="trx"></param>
        /// <returns></returns>
        public static long TRXToSun(decimal trx)
        {
            return Convert.ToInt64(trx * _sun_unit);
        }

        /// <summary>
        /// Sun to Trx
        /// </summary>
        /// <param name="sun"></param>
        /// <returns></returns>
        public static decimal SunToTRX(long sun)
        {
            return Convert.ToDecimal(sun) / _sun_unit;
        }
    }
}
