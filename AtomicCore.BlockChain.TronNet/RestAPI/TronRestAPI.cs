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
    public class TronRestAPI : ITronRestAPI
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
        public TronRestAPI(IOptions<TronNetOptions> options)
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

        #region ITronAddressUtilitiesRestAPI

        /// <summary>
        /// Generates a random private key and address pair. 
        /// Risk Warning : there is a security risk. 
        /// This interface service has been shutdown by the Trongrid. 
        /// Please use the offline mode or the node deployed by yourself.
        /// </summary>
        /// <returns>Returns a private key, the corresponding address in hex,and base58</returns>
        [Obsolete("Remote service has been removed")]
        public TronAddressKeyPairRestJson GenerateAddress()
        {
            string url = CreateFullNodeRestUrl("/wallet/generateaddress");
            string resp = this.RestGetJson(url);
            TronAddressKeyPairRestJson restJson = ObjectParse<TronAddressKeyPairRestJson>(resp);

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
        public TronAddressBase58CheckRestJson CreateAddress(string passphrase)
        {
            if (string.IsNullOrEmpty(passphrase))
                throw new ArgumentNullException(nameof(passphrase));

            string passphraseHex = Encoding.UTF8.GetBytes(passphrase).ToHex();
            string url = CreateFullNodeRestUrl("/wallet/createaddress");
            string resp = this.RestPostJson(url, new { value = passphraseHex });
            TronAddressBase58CheckRestJson restJson = ObjectParse<TronAddressBase58CheckRestJson>(resp);

            return restJson;
        }

        /// <summary>
        /// Validates address, returns either true or false.
        /// </summary>
        /// <param name="address">Tron Address</param>
        /// <returns></returns>
        public TronAddressValidRestJson ValidateAddress(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException(nameof(address));

            string addressHex = Base58Encoder.DecodeFromBase58Check(address).ToHex();
            string url = CreateFullNodeRestUrl("/wallet/validateaddress");
            string resp = this.RestPostJson(url, new { address = addressHex });
            TronAddressValidRestJson restJson = ObjectParse<TronAddressValidRestJson>(resp);

            return restJson;
        }

        #endregion

        #region ITronQueryNetworkRestAPI

        /// <summary>
        /// Get Transaction By Txid
        /// </summary>
        /// <param name="txid"></param>
        /// <returns></returns>
        public TronTransactionRestJson GetTransactionByID(string txid)
        {
            if (string.IsNullOrEmpty(txid))
                throw new ArgumentNullException(nameof(txid));

            string url = CreateFullNodeRestUrl("/wallet/gettransactionbyid");
            string resp = this.RestPostJson(url, new { value = txid });
            TronTransactionRestJson restJson = ObjectParse<TronTransactionRestJson>(resp);

            return restJson;
        }



        #endregion
    }
}
