namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Account Interfacr Implementation Class
    /// </summary>
    public class TronAccount : ITronAccount
    {
        #region Variables

        /// <summary>
        /// Tron ECKEY
        /// </summary>
        private TronECKey _key = null;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="network"></param>
        public TronAccount(string privateKey, TronNetwork network = TronNetwork.MainNet)
        {
            Initialise(new TronECKey(privateKey, network));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key"></param>
        public TronAccount(TronECKey key)
        {
            Initialise(key);
        }

        #endregion

        #region Propertys

        /// <summary>
        /// Current Address
        /// </summary>
        public string Address { get; protected set; }

        /// <summary>
        /// Public Key
        /// </summary>
        public string PublicKey { get; private set; }

        /// <summary>
        /// Private Key
        /// </summary>
        public string PrivateKey { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get Address Prefix
        /// </summary>
        /// <returns></returns>
        public byte GetAddressPrefix()
        {
            return _key.GetPublicAddressPrefix();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialise Instance
        /// </summary>
        /// <param name="key"></param>
        private void Initialise(TronECKey key)
        {
            this._key = key;
            this.PrivateKey = key.GetPrivateKey();
            this.Address = key.GetPublicAddress();
            this.PublicKey = key.GetPubKey().ToHex();
        }

        #endregion 
    }
}
