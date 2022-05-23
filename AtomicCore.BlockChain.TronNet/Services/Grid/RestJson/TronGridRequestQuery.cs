using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron
    /// </summary>
    public class TronGridRequestQuery
    {
        #region Propertys

        /// <summary>
        /// null:null | true:only_confirmed | false:only_unconfirmed
        /// </summary>
        public bool? OnlyConfirmed { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// get query parameters
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            StringBuilder paramBuilder = new StringBuilder();

            if (null != this.OnlyConfirmed)
                if (this.OnlyConfirmed.Value)
                    paramBuilder.Append("only_confirmed=true");
                else
                    paramBuilder.Append("only_unconfirmed=true");

            return paramBuilder.ToString();
        }

        #endregion
    }
}
