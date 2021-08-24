using Microsoft.Extensions.Options;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// ITronClient Interface Impl
    /// </summary>
    public class TronClient : ITronClient
    {
        #region Variables

        private readonly IOptions<TronNetOptions> _options;
        private readonly ITronNetRest _restApiClient;
        private readonly IGrpcChannelClient _channelClient;
        private readonly IWalletClient _walletClient;
        private readonly ITransactionClient _transactionClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="restApiClient"></param>
        /// <param name="channelClient"></param>
        /// <param name="walletClient"></param>
        /// <param name="transactionClient"></param>
        public TronClient(
            IOptions<TronNetOptions> options,
            ITronNetRest restApiClient,
            IGrpcChannelClient channelClient,
            IWalletClient walletClient,
            ITransactionClient transactionClient
        )
        {
            _options = options;
            _restApiClient = restApiClient;
            _channelClient = channelClient;
            _walletClient = walletClient;
            _transactionClient = transactionClient;
        }

        #endregion

        #region Propertys

        /// <summary>
        /// Tron Network Type Enum
        /// </summary>
        public TronNetwork TronNetwork => _options.Value.Network;

        #endregion

        #region Public Methods

        /// <summary>
        /// Grpc Channel Client
        /// </summary>
        /// <returns></returns>
        public IGrpcChannelClient GetChannel()
        {
            return _channelClient;
        }

        /// <summary>
        /// Get Wallet Interface Instance
        /// </summary>
        /// <returns></returns>
        public IWalletClient GetWallet()
        {
            return _walletClient;
        }

        /// <summary>
        /// Get Transaction Interface Instance
        /// </summary>
        /// <returns></returns>
        public ITransactionClient GetTransaction()
        {
            return _transactionClient;
        }

        /// <summary>
        /// Get Rest API
        /// </summary>
        /// <returns></returns>
        public ITronNetRest GetRestAPI()
        {
            return _restApiClient;
        }

        #endregion
    }
}
