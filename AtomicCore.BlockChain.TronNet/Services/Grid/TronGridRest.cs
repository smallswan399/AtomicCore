using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronGrid Rest
    /// </summary>
    public class TronGridRest : ITronGridRest
    {
        #region Variables

        private readonly IOptions<TronNetOptions> _options;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public TronGridRest(IOptions<TronNetOptions> options)
        {
            _options = options;
        }

        #endregion

        #region Private Methods




        #endregion

        #region ITronGridAccountRest




        #endregion
    }
}
