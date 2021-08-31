﻿using System;
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

            //Register REST Interface
            services.AddTransient<ITronNetAddressUtilitiesRest, TronNetRest>();
            services.AddTransient<ITronNetQueryNetworkRest, TronNetRest>();
            services.AddTransient<ITronNetRest, TronNetRest>();
            services.AddTransient<ITronNetTRC10TokenRest, TronNetRest>();

            //Register Other Interface
            services.AddTransient<ITronNetTransactionClient, TronNetTransactionClient>();
            services.AddTransient<IGrpcChannelClient, GrpcChannelClient>();
            services.AddTransient<ITronNetClient, TronNetClient>();
            services.AddTransient<ITronNetWalletClient, TronNetWalletClient>();
            services.AddSingleton<IContractClientFactory, ContractClientFactory>();
            services.AddTransient<TRC20ContractClient>();
            services.Configure(setupAction);

            return services;
        }
    }
}
