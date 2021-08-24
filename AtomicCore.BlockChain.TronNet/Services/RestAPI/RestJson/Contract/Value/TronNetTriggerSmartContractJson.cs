using Newtonsoft.Json;
using System;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Trigger Smart Contract Json
    /// Type 31,eg:trc20 && trc721
    /// </summary>
    public class TronNetTriggerSmartContractJson : TronNetContractBaseValueJson
    {
        #region Variables

        /// <summary>
        /// TRC20 - Transfer Method top 4 Bytes Hex
        /// </summary>
        private const string c_trc20Transfer = "a9059cbb";

        #endregion

        #region Propertys

        /// <summary>
        /// contract_address
        /// </summary>
        [JsonProperty("contract_address"), JsonConverter(typeof(TronNetHexAddressJsonConverter))]
        public string ContractAddress { get; set; }

        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get ToAddress From Data（TRC20 - Transfer）
        /// </summary>
        /// <param name="network"></param>
        /// <returns></returns>
        public string GetToTronAddress(TronNetwork network = TronNetwork.MainNet)
        {
            if (string.IsNullOrEmpty(this.Data))
                return string.Empty;
            if (!this.Data.StartsWith(c_trc20Transfer, StringComparison.OrdinalIgnoreCase))
                return string.Empty;

            string hexAddress = this.Data.Substring(30, 42);

            return TronNetECKey.ConvertToTronAddressFromHexAddress(hexAddress, network);
        }

        /// <summary>
        /// Get To Address From Data（TRC20 - Transfer）
        /// <param name="isUpper"></param>
        /// </summary>
        /// <returns></returns>
        public string GetToEthAddress(bool isUpper = false)
        {
            if (string.IsNullOrEmpty(this.Data))
                return string.Empty;
            if (!this.Data.StartsWith(c_trc20Transfer, StringComparison.OrdinalIgnoreCase))
                return string.Empty;

            string hexAddress = this.Data.Substring(30, 42);

            return TronNetECKey.ConvertToEthAddressFromHexAddress(hexAddress, isUpper);
        }

        #endregion
    }
}
