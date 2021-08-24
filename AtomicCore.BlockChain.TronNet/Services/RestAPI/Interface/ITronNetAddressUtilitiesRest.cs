using System;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Address Utilities Rest API
    /// </summary>
    public interface ITronNetAddressUtilitiesRest
    {
        /// <summary>
        /// Generates a random private key and address pair. 
        /// Risk Warning : there is a security risk. 
        /// This interface service has been shutdown by the Trongrid. 
        /// Please use the offline mode or the node deployed by yourself.
        /// </summary>
        /// <returns>Returns a private key, the corresponding address in hex,and base58</returns>
        [Obsolete("Remote service has been removed")]
        TronNetAddressKeyPairRestJson GenerateAddress();

        /// <summary>
        /// Create address from a specified password string (NOT PRIVATE KEY)
        /// Risk Warning : there is a security risk. 
        /// This interface service has been shutdown by the Trongrid. 
        /// Please use the offline mode or the node deployed by yourself.
        /// </summary>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        [Obsolete("Remote service has been removed")]
        TronNetAddressBase58CheckRestJson CreateAddress(string passphrase);

        /// <summary>
        /// Validates address, returns either true or false.
        /// </summary>
        /// <param name="address">Address should be in base58checksum, hexString, or base64 format.</param>
        /// <returns></returns>
        TronNetAddressValidRestJson ValidateAddress(string address);
    }
}
