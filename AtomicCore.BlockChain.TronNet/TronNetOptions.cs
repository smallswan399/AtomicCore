using Grpc.Core;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet Options
    /// </summary>
    public class TronNetOptions
    {
        /// <summary>
        /// Network Type Enum
        /// </summary>
        public TronNetwork Network { get; set; }

        /// <summary>
        /// Grpc Channel
        /// </summary>
        public GrpcChannelOption Channel { get; set; }

        /// <summary>
        /// Solidity Channel
        /// </summary>
        public GrpcChannelOption SolidityChannel { get; set; }

        /// <summary>
        /// ApiKey
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Tron-Pro-Api-Key
        /// </summary>
        /// <returns></returns>
        internal Metadata GetgRPCHeaders()
        {
            return new Metadata
            {
                { "TRON-PRO-API-KEY", ApiKey }
            };
        }
    }
}
