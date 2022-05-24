using System.Collections.Generic;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Request Query
    /// </summary>
    public class TronGridRequestQuery
    {
        #region Propertys

        /// <summary>
        /// null:null | true:only_confirmed | false:only_unconfirmed
        /// </summary>
        public bool? OnlyConfirmed { get; set; }

        /// <summary>
        /// null:null | only_from = true | only_from = false
        /// </summary>
        public bool? OnlyFrom { get; set; }

        /// <summary>
        /// null:null | only_to = true | only_to = false
        /// </summary>
        public bool? OnlyTo { get; set; }

        /// <summary>
        /// null:null | limit default is 20 
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// last page fingerprint
        /// </summary>
        public string FingerPrint { get; set; }

        /// <summary>
        /// order_by
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// min_timestamp
        /// </summary>
        public long? MinTimestamp { get; set; }

        /// <summary>
        /// max_timestamp
        /// </summary>
        public long? MaxTimestamp { get; set; }

        /// <summary>
        /// search_internal
        /// </summary>
        public bool? SearchInternal { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// get query parameters
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            var paramList = new List<string>();

            if (null != this.OnlyConfirmed)
                if (this.OnlyConfirmed.Value)
                    paramList.Add("only_confirmed=true");
                else
                    paramList.Add("only_unconfirmed=true");
            if (null != OnlyFrom)
                paramList.Add($"only_from={OnlyFrom.Value.ToString().ToLower()}");
            if (null != OnlyTo)
                paramList.Add($"only_to={OnlyTo.Value.ToString().ToLower()}");
            if (null != Limit)
                paramList.Add($"limit={Limit.Value}");
            if (!string.IsNullOrEmpty(FingerPrint))
                paramList.Add($"fingerprint={FingerPrint}");
            if (!string.IsNullOrEmpty(OrderBy))
                paramList.Add($"order_by={OrderBy}");
            if (null != MinTimestamp)
                paramList.Add($"min_timestamp={MinTimestamp.Value}");
            if (null != MaxTimestamp)
                paramList.Add($"max_timestamp={MaxTimestamp.Value}");
            if (null != SearchInternal)
                paramList.Add($"search_internal={SearchInternal.Value.ToString().ToLower()}");

            return string.Join("&", paramList);
        }

        #endregion
    }
}
