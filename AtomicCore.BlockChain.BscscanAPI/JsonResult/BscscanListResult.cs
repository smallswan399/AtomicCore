using Newtonsoft.Json;
using System.Collections.Generic;

namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bsc list result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class BscscanListResult<T> : BscscanMsgResult
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public BscscanListResult()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        public BscscanListResult(BscscanJsonStatus status, string message)
        {
            this.Status = status;
            this.Message = message;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg"></param>
        public BscscanListResult(BscscanMsgResult msg)
        {
            this.Status = msg.Status;
            this.Message = msg.Message;

            if (!string.IsNullOrEmpty(msg.Result))
                this.Message = $"{this.Message} --> {msg.Result}";
        }

        #endregion

        #region Propertys

        /// <summary>
        /// data
        /// </summary>
        [JsonProperty("result")]
        public new List<T> Result { get; set; }

        #endregion
    }
}
