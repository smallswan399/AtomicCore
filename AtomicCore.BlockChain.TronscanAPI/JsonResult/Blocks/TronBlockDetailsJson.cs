using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tron Block Details Json
    /// </summary>
    public class TronBlockDetailsJson : TronBlockBasicJson
    {
        /// <summary>
        /// Witness Name
        /// </summary>
        [JsonProperty("witnessName")]
        public string WitnessName { get; set; }

        /// <summary>
        /// version
        /// </summary>
        [JsonProperty("version")]
        public string Version { get; set; }

        /// <summary>
        /// transferCount
        /// </summary>
        [JsonProperty("transferCount")]
        public int TransferCount { get; set; }

        /// <summary>
        /// confirmations
        /// </summary>
        [JsonProperty("confirmations")]
        public int Confirmations { get; set; }

        /// <summary>
        /// netUsage
        /// </summary>
        [JsonProperty("netUsage")]
        public int NetUsage { get; set; }

        /// <summary>
        /// energyUsage
        /// </summary>
        [JsonProperty("energyUsage")]
        public int EnergyUsage { get; set; }

        /// <summary>
        /// blockReward
        /// </summary>
        [JsonProperty("blockReward")]
        public int BlockReward { get; set; }

        /// <summary>
        /// revert
        /// </summary>
        [JsonProperty("revert")]
        public bool Revert { get; set; }

        /// <summary>
        /// srConfirmList
        /// </summary>
        [JsonProperty("srConfirmList")]
        public List<TronBlockSRConfirmJson> SRConfirmList { get; set; }
    }
}
