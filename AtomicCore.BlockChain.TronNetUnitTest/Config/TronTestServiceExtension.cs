using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Options;

namespace AtomicCore.BlockChain.TronNet.Tests
{
    public record TronTestRecord(IServiceProvider ServiceProvider, ITronClient TronClient, IOptions<TronNetOptions> Options);

    public static class TronTestServiceExtension
    {
        private static IServiceProvider AddTronMainNet()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTronNet(x =>
            {
                x.Network = TronNetwork.MainNet;
                x.FullNodeRestAPI = "https://api.trongrid.io";
                x.SolidityNodeRestAPI = "https://api.trongrid.io";
                x.EventSrvAPI = "https://api.trongrid.io";
                x.Channel = new GrpcChannelOption 
                { 
                    Host = "grpc.trongrid.io", 
                    Port = 50051 
                };
                x.SolidityChannel = new GrpcChannelOption 
                { 
                    Host = "grpc.trongrid.io", 
                    Port = 50052 
                };
                x.ApiKey = "30213a7e-bc62-4f79-9a78-f651d3234047";
            });
            services.AddLogging();
            return services.BuildServiceProvider();
        }

        private static IServiceProvider AddTronTestNet()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTronNet(x =>
            {
                x.Network = TronNetwork.MainNet;
                x.FullNodeRestAPI = "https://api.shasta.trongrid.io";
                x.SolidityNodeRestAPI = "https://api.shasta.trongrid.io";
                x.EventSrvAPI = "https://api.shasta.trongrid.io";
                x.Channel = new GrpcChannelOption 
                { 
                    Host = "grpc.shasta.trongrid.io", 
                    Port = 50051 
                };
                x.SolidityChannel = new GrpcChannelOption 
                { 
                    Host = "grpc.shasta.trongrid.io", 
                    Port = 50052 
                };
                x.ApiKey = "";
            });
            services.AddLogging();
            return services.BuildServiceProvider();
        }

        public static TronTestRecord GetMainRecord()
        {
            IServiceProvider provider = AddTronMainNet();
            var client = provider.GetService<ITronClient>();
            var options = provider.GetService<IOptions<TronNetOptions>>();

            return new TronTestRecord(provider, client, options);
        }

        public static TronTestRecord GetTestRecord()
        {
            IServiceProvider provider = AddTronTestNet();
            var client = provider.GetService<ITronClient>();
            var options = provider.GetService<IOptions<TronNetOptions>>();

            return new TronTestRecord(provider, client, options);
        }
    }

}
