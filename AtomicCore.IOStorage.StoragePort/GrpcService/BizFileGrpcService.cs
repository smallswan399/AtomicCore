using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AtomicCore.IOStorage.StoragePort.GrpcService
{
    /// <summary>
    /// File Grpc Service
    /// </summary>
    public class BizFileGrpcService : FileService.FileServiceBase
    {
        /// <summary>
        /// 头部信息Token
        /// </summary>
        private const string c_head_token = "token";

        /// <summary>
        /// 日志对象
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public BizFileGrpcService(ILogger<BizFileGrpcService> logger)
        {
            _logger = logger;
        }

        public override async Task<UploadFileReply> UploadFile(IAsyncStreamReader<UploadFileRequest> requestStream, ServerCallContext context)
        {
            // 判断权限
            if (!HasPremission(context))
                return new UploadFileReply()
                {
                    Result = false,
                    Message = "illegal request, insufficient permission to request"
                };

            // 参数接收
            var requests = new Queue<UploadFileRequest>();
            while (await requestStream.MoveNext())
            {
                var request = requestStream.Current;
                requests.Enqueue(request);
            }

            // 参数转化
            var first = requests.Peek();

            // 基础判断
            //if(requestStream.f)

            return new UploadFileReply()
            {
                Result = true,
                Message = "success"
            };
        }

        public override Task DownloadFile(DownloadFileRequest request, IServerStreamWriter<DownloadFileReply> responseStream, ServerCallContext context)
        {
            return base.DownloadFile(request, responseStream, context);
        }

        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool HasPremission(ServerCallContext context)
        {
            var requestContext = context.GetHttpContext();
            IBizPathSrvProvider pathSrvProvider = requestContext.RequestServices.GetRequiredService<IBizPathSrvProvider>();
            if (null == pathSrvProvider)
            {
                _logger.LogError($"[Grpc | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] --> '{nameof(IBizPathSrvProvider)}' is null, are you register the interface of '{nameof(IBizPathSrvProvider)}' in startup?");
                return false;
            }
            if (string.IsNullOrEmpty(pathSrvProvider.AppToken))
            {
                _logger.LogError($"[Grpc | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] --> '{nameof(pathSrvProvider.AppToken)}' is null, are you setting the env or appsetting?");
                return false;
            }

            // 判断头部是否包含token
            bool hasHeadToken = requestContext.Request.Headers.TryGetValue(c_head_token, out StringValues headTK);
            if (!hasHeadToken)
            {
                _logger.LogError($"[Grpc | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] --> illegal request, insufficient permission to request");
                return false;
            }

            // 判断Token是否匹配
            if (!pathSrvProvider.AppToken.Equals(headTK.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError($"[Grpc | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] --> app token is illegal, current request token is '{headTK}'");
                return false;
            }

            return true;
        }
    }
}
