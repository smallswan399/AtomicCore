using System;
using Microsoft.Extensions.DependencyInjection;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Net Service Extension
    /// </summary>
    public static class TronNetServiceExtension
    {
        /// <summary>
        /// Register TronNet Interface
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddTronNet(this IServiceCollection services, Action<TronNetOptions> setupAction)
        {
            //Load options
            TronNetOptions options = new TronNetOptions();
            setupAction(options);

            //Register Interface
            services.AddTransient<ITronRestAPI, TronRestAPI>();
            services.AddTransient<ITransactionClient, TransactionClient>();
            services.AddTransient<IGrpcChannelClient, GrpcChannelClient>();
            services.AddTransient<ITronClient, TronClient>();
            services.AddTransient<IWalletClient, WalletClient>();
            services.AddSingleton<IContractClientFactory, ContractClientFactory>();
            services.AddTransient<TRC20ContractClient>();
            services.Configure(setupAction);

            return services;
        }
    }
}
