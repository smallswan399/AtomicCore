using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Rest API Implementation Class
    /// </summary>
    public class TronNetRest : ITronNetRest
    {
        #region Variables

        private const string c_apiKeyName = "TRON-PRO-API-KEY";
        private readonly IOptions<TronNetOptions> _options;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public TronNetRest(IOptions<TronNetOptions> options)
        {
            _options = options;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create Full Node Rest Url
        /// </summary>
        /// <param name="actionUrl">接口URL</param>
        /// <returns></returns>
        private string CreateFullNodeRestUrl(string actionUrl)
        {
            return string.Format(
                "{0}{1}",
                this._options.Value.FullNodeRestAPI,
                actionUrl
            );
        }

        /// <summary>
        /// Rest Get Json Result
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string RestGetJson(string url)
        {
            string resp;
            try
            {
                //head api key
                Dictionary<string, string> heads = new Dictionary<string, string>()
                {
                    { "Accept","application/json"}
                };

                string apiKey = this._options.Value.ApiKey;
                if (!string.IsNullOrEmpty(apiKey))
                    heads.Add(c_apiKeyName, apiKey);

                resp = HttpProtocol.HttpGet(url, heads: heads);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// Rest Post Json Request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string RestPostJson(string url)
        {
            string resp;
            try
            {
                //head api key
                Dictionary<string, string> heads = null;
                string apiKey = this._options.Value.ApiKey;
                if (!string.IsNullOrEmpty(apiKey))
                    heads = new Dictionary<string, string>()
                    {
                        { c_apiKeyName,apiKey}
                    };

                resp = HttpProtocol.HttpPost(url, null, heads: heads);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// Rest Post Json Request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">rest url</param>
        /// <param name="json">json data</param>
        /// <returns></returns>
        private string RestPostJson<T>(string url, T json)
        {
            string resp;
            try
            {
                //post data
                string post_data = Newtonsoft.Json.JsonConvert.SerializeObject(json);

                //head api key
                Dictionary<string, string> heads = null;
                string apiKey = this._options.Value.ApiKey;
                if (!string.IsNullOrEmpty(apiKey))
                    heads = new Dictionary<string, string>()
                    {
                        { c_apiKeyName,apiKey}
                    };

                resp = HttpProtocol.HttpPost(url, post_data, heads: heads);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resp;
        }

        /// <summary>
        /// StringJson Parse to Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resp"></param>
        /// <returns></returns>
        private T ObjectParse<T>(string resp)
            where T : class, new()
        {
            T jsonResult;
            try
            {
                jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return jsonResult;
        }

        #endregion

        #region ITronAddressUtilitiesRest

        /// <summary>
        /// Generates a random private key and address pair. 
        /// Risk Warning : there is a security risk. 
        /// This interface service has been shutdown by the Trongrid. 
        /// Please use the offline mode or the node deployed by yourself.
        /// </summary>
        /// <returns>Returns a private key, the corresponding address in hex,and base58</returns>
        [Obsolete("Remote service has been removed")]
        public TronNetAddressKeyPairRestJson GenerateAddress()
        {
            string url = CreateFullNodeRestUrl("/wallet/generateaddress");
            string resp = this.RestGetJson(url);
            TronNetAddressKeyPairRestJson restJson = ObjectParse<TronNetAddressKeyPairRestJson>(resp);

            return restJson;
        }

        /// <summary>
        /// Create address from a specified password string (NOT PRIVATE KEY)
        /// Risk Warning : there is a security risk. 
        /// This interface service has been shutdown by the Trongrid. 
        /// Please use the offline mode or the node deployed by yourself.
        /// </summary>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        [Obsolete("Remote service has been removed")]
        public TronNetAddressBase58CheckRestJson CreateAddress(string passphrase)
        {
            if (string.IsNullOrEmpty(passphrase))
                throw new ArgumentNullException(nameof(passphrase));

            string passphraseHex = Encoding.UTF8.GetBytes(passphrase).ToHex();
            string url = CreateFullNodeRestUrl("/wallet/createaddress");
            string resp = this.RestPostJson(url, new { value = passphraseHex });
            TronNetAddressBase58CheckRestJson restJson = ObjectParse<TronNetAddressBase58CheckRestJson>(resp);

            return restJson;
        }

        /// <summary>
        /// Validates address, returns either true or false.
        /// </summary>
        /// <param name="address">Tron Address</param>
        /// <returns></returns>
        public TronNetAddressValidRestJson ValidateAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            string addressHex = Base58Encoder.DecodeFromBase58Check(address).ToHex();
            string url = CreateFullNodeRestUrl("/wallet/validateaddress");
            string resp = this.RestPostJson(url, new { address = addressHex });
            TronNetAddressValidRestJson restJson = ObjectParse<TronNetAddressValidRestJson>(resp);

            return restJson;
        }

        #endregion

        #region ITronTransactionsRest

        /// <summary>
        /// Create a TRX transfer transaction. 
        /// If to_address does not exist, then create the account on the blockchain.
        /// </summary>
        /// <param name="ownerAddress">To_address is the transfer address</param>
        /// <param name="toAddress">Owner_address is the transfer address</param>
        /// <param name="amount">Amount is the transfer amount,the unit is trx</param>
        /// <param name="permissionID">Optional, for multi-signature use</param>
        /// <param name="visible">Optional.Whehter the address is in base58 format</param>
        /// <returns></returns>
        public TronNetCreateTransactionRestJson CreateTransaction(string ownerAddress, string toAddress, decimal amount, int? permissionID = null, bool? visible = null)
        {
            if (string.IsNullOrEmpty(ownerAddress))
                throw new ArgumentNullException(nameof(ownerAddress));
            if (string.IsNullOrEmpty(toAddress))
                throw new ArgumentNullException(nameof(toAddress));
            if (amount <= decimal.Zero)
                throw new ArgumentException("amount must be greater than zero");

            //address to hex
            string hex_owner_address = TronNetECKey.ConvertToHexAddress(ownerAddress);
            string hex_to_address = TronNetECKey.ConvertToHexAddress(toAddress);
            long trx_of_sun = (long)(amount * 1000000);

            //create request data
            dynamic reqData = new
            {
                owner_address = hex_owner_address,
                to_address = hex_to_address,
                amount = trx_of_sun
            };
            if (null != permissionID)
                reqData.permission_id = permissionID.Value;
            if (null != visible)
                reqData.visible = visible.Value;

            string url = CreateFullNodeRestUrl("/wallet/createtransaction");
            string resp = this.RestPostJson(url, reqData);
            TronNetCreateTransactionRestJson restJson = ObjectParse<TronNetCreateTransactionRestJson>(resp);

            return restJson;
        }

        #endregion

        #region ITronQueryNetworkRestAPI

        /// <summary>
        /// Get Transaction By Txid
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        public TronNetTransactionRestJson GetTransactionByID(string txid)
        {
            if (string.IsNullOrEmpty(txid))
                throw new ArgumentNullException(nameof(txid));

            string url = CreateFullNodeRestUrl("/wallet/gettransactionbyid");
            string resp = this.RestPostJson(url, new { value = txid });
            TronNetTransactionRestJson restJson = ObjectParse<TronNetTransactionRestJson>(resp);

            return restJson;
        }



        #endregion
    }
}
