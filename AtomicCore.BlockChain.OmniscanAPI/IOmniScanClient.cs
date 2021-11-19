using System;
using System.Collections.Generic;
using System.Text;

namespace AtomicCore.BlockChain.OmniscanAPI
{
    /// <summary>
    /// Omni scan client interface
    /// </summary>
    public interface IOmniScanClient
    {
        /// <summary>
        /// Returns the balance information for a given address. 
        /// For multiple addresses in a single query use the v2 endpoint
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        OmniAssetCollectionJson GetAddressV1(string address);

        /// <summary>
        /// Returns the balance information for multiple addresses
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Dictionary<string, OmniAssetCollectionJson> GetAddressV2(params string[] address);

        /// <summary>
        /// Returns the balance information and transaction history list for a given address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        OmniAddressDetailsResponse GetAddressDetails(string address);

        /// <summary>
        /// Returns the Armory encoded version of an unsigned transaction for 
        /// use with Armory offline transactions. 
        /// Data: 
        ///     unsigned_hex : raw bitcoin hex formatted tx to be converted 
        ///     pubkey : pubkey of the sending address
        /// </summary>
        /// <param name="unsignedHex"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        OmniArmoryUnsignedResponse GetUnsigned(string unsignedHex, string publicKey);

        /// <summary>
        /// Decodes and returns the raw hex and signed status from an armory transaction. 
        /// Data: 
        ///     armory_tx : armory transaction in text format
        /// </summary>
        /// <param name="armoryTx"></param>
        /// <returns></returns>
        OmniRawTransactionResponse GetRawtransaction(string armoryTx);
    }
}
