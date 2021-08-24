using System;
using System.Text.RegularExpressions;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Untils
    /// </summary>
    public static class TronNetUntils
    {
        /// <summary>
        /// Remove Hex Zero
        /// </summary>
        /// <param name="hexStr"></param>
        /// <param name="strategy"></param>
        /// <param name="keepLen"></param>
        /// <param name="hexEncoding"></param>
        /// <returns></returns>
        public static string RemoveHexZero(string hexStr, TronNetHexCuteZeroStrategy strategy, int keepLen = 0, bool hexEncoding = true)
        {
            if (string.IsNullOrEmpty(hexStr))
                return string.Empty;
            if (hexStr.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                hexStr = hexStr.Substring(2);

            string tmp;
            if (TronNetHexCuteZeroStrategy.Left == strategy)
            {
                tmp = Regex.Replace(hexStr, @"^0+", string.Empty, RegexOptions.IgnoreCase);

                if (keepLen > 0)
                    tmp = tmp.PadLeft(keepLen, '0');
            }
            else if (TronNetHexCuteZeroStrategy.Right == strategy)
            {
                if (keepLen <= 0)
                    throw new Exception(string.Format("rigthLen must be greater than zero in right strategy mode,current value is '{0}'", keepLen));

                tmp = hexStr.Substring(0, keepLen);
            }
            else
                return string.Empty;

            if (hexEncoding)
                return string.Format("0x{0}", tmp);
            else
                return tmp;
        }
    }
}
