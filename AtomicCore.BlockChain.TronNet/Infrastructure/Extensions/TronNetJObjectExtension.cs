using Newtonsoft.Json.Linq;
using System;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet JObject Extension
    /// </summary>
    public static class TronNetJObjectExtension
    {
        #region Public Methods

        /// <summary>
        /// JObject => Contract Type Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jobject"></param>
        /// <returns></returns>
        public static T ToContractValue<T>(this JObject jobject)
            where T : TronNetContractBaseValueJson, new()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jobject.ToString());
        }

        #endregion

        #region Basic Property Getter

        /// <summary>
        /// Get Owner EthAddress
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="isUpper"></param>
        /// <returns></returns>
        public static string GetOwnerEthAddress(this JObject jobject, bool isUpper = false)
        {
            bool flag = jobject.TryGetValue("owner_address", out JToken token);
            if (!flag)
                return string.Empty;

            string hexAddress = token.ToString();

            return TronNetECKey.ConvertToEthAddressFromHexAddress(hexAddress, isUpper);
        }

        #endregion

        #region Trx && Trc10 Property Getter

        /// <summary>
        /// Get Owner TronAddress
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        public static string GetOwnerTronAddress(this JObject jobject, TronNetwork network = TronNetwork.MainNet)
        {
            bool flag = jobject.TryGetValue("owner_address", out JToken token);
            if (!flag)
                return string.Empty;

            string hexAddress = token.ToString();

            return TronNetECKey.ConvertToTronAddressFromHexAddress(hexAddress, network);
        }

        /// <summary>
        /// Get TRC10 ToEthAddress
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="isUpper"></param>
        /// <returns></returns>
        public static string GetTrc10ToEthAddress(this JObject jobject, bool isUpper = false)
        {
            bool flag = jobject.TryGetValue("to_address", out JToken token);
            if (!flag)
                return string.Empty;

            string hexAddress = token.ToString();

            return TronNetECKey.ConvertToEthAddressFromHexAddress(hexAddress, isUpper);
        }

        /// <summary>
        /// Get TRC10 ToTronAddress
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        public static string GetTrc10ToTronAddress(this JObject jobject, TronNetwork network = TronNetwork.MainNet)
        {
            bool flag = jobject.TryGetValue("to_address", out JToken token);
            if (!flag)
                return string.Empty;

            string hexAddress = token.ToString();

            return TronNetECKey.ConvertToTronAddressFromHexAddress(hexAddress, network);
        }

        /// <summary>
        /// Get Trc10 Asset Name
        /// </summary>
        /// <param name="jobject"></param>
        /// <returns></returns>
        public static string GetTrc10AssetName(this JObject jobject)
        {
            bool flag = jobject.TryGetValue("asset_name", out JToken token);
            if (!flag)
                return string.Empty;

            return token.ToString();
        }

        /// <summary>
        /// Get Trc10 OrigAmount
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static ulong GetTrc10Amount(this JObject jobject)
        {
            bool flag = jobject.TryGetValue("amount", out JToken token);
            if (!flag)
                return 0UL;

            return Convert.ToUInt64(token.ToString());
        }

        /// <summary>
        /// Get Trc10 Amount
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal GetTrc10Amount(this JObject jobject, int decimals = 6)
        {
            ulong origAmount = GetTrc10Amount(jobject);

            if (decimals <= 0)
                return origAmount;
            else
                return origAmount / (decimal)Math.Pow(10, decimals);
        }

        #endregion

        #region Trc20 Property Getter

        /// <summary>
        /// Get Contract EthAddress
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="isUpper"></param>
        /// <returns></returns>
        public static string GetContractEthAddress(this JObject jobject, bool isUpper = false)
        {
            bool flag = jobject.TryGetValue("contract_address", out JToken token);
            if (!flag)
                return string.Empty;

            string hexAddress = token.ToString();

            return TronNetECKey.ConvertToEthAddressFromHexAddress(hexAddress, isUpper);
        }

        /// <summary>
        /// Get Contract TronAddress
        /// </summary>
        /// <param name="jobject"></param>
        /// <param name="network"></param>
        /// <returns></returns>
        public static string GetContractTronAddress(this JObject jobject, TronNetwork network = TronNetwork.MainNet)
        {
            bool flag = jobject.TryGetValue("contract_address", out JToken token);
            if (!flag)
                return string.Empty;

            string hexAddress = token.ToString();

            return TronNetECKey.ConvertToTronAddressFromHexAddress(hexAddress, network);
        }

        #endregion
    }
}
