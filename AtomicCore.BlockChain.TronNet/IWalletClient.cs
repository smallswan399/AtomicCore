﻿using Google.Protobuf;
using Grpc.Core;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Wallet Client Interface
    /// </summary>
    public interface IWalletClient
    {
        /// <summary>
        /// Get Protocol
        /// </summary>
        /// <returns></returns>
        Wallet.WalletClient GetProtocol();

        /// <summary>
        /// Get Solidity Protocol
        /// </summary>
        /// <returns></returns>
        WalletSolidity.WalletSolidityClient GetSolidityProtocol();

        /// <summary>
        /// Generatee Account
        /// </summary>
        /// <returns></returns>
        ITronAccount GenerateAccount();

        /// <summary>
        /// Get Account From PrivateKey HexString
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        ITronAccount GetAccount(string privateKey);

        /// <summary>
        /// Parse Address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        ByteString ParseAddress(string address);

        /// <summary>
        /// Get Metadata Header
        /// </summary>
        /// <returns></returns>
        Metadata GetHeaders();
    }
}
