using Newtonsoft.Json.Linq;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet JObject Extension
    /// </summary>
    public static class TronNetJObjectExtension
    {
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
    }
}
