namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Grpc Channel Option
    /// </summary>
    public class GrpcChannelOption
    {
        /// <summary>
        /// Host
        /// </summary>
        public string Host { get; set; } = "grpc.shasta.trongrid.io";

        /// <summary>
        /// Ports
        /// </summary>
        public int Port { get; set; } = 50051;
    }
}
