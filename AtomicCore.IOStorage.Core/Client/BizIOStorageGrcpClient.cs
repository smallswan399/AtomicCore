using Google.Protobuf;
using Grpc.Net.Client;
using System.IO;
using System.Threading.Tasks;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// IOStorage Grcp Client
    /// </summary>
    public class BizIOStorageGrcpClient
    {
        private const string c_head_token = "token";
        private readonly string _host;
        private readonly int _port;
        private readonly string _apiKey;

        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="apiKey"></param>
        public BizIOStorageGrcpClient(string host, int port, string apiKey)
        {
            _host = host;
            _port = port;
            _apiKey = apiKey;
        }

        /// <summary>
        /// grpc upload file
        /// </summary>
        /// <param name="bizFolder"></param>
        /// <param name="indexFolder"></param>
        /// <param name="fileName"></param>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        public async Task<BizIOSingleUploadJsonResult> UploadFile(string bizFolder, string indexFolder, string fileName, Stream fileStream)
        {
            //基础判断
            if (string.IsNullOrEmpty(bizFolder))
                return new BizIOSingleUploadJsonResult("请指定业务文件夹");
            if (string.IsNullOrEmpty(fileName))
                return new BizIOSingleUploadJsonResult("文件名称不允许为空");
            if (null == fileStream || fileStream.Length <= 0)
                return new BizIOSingleUploadJsonResult("文件流不允许为空");

            // 重置文件流
            fileStream.Seek(0, SeekOrigin.Begin);

            // 设置GRPC头
            var grpcHeads = new Grpc.Core.Metadata();
            grpcHeads.Add(c_head_token, _apiKey);

            // 变量定义
            var sended = 0;
            var eachLength = 1024 * 1024;                   // 每次最多发送 1M 的文件内容
            var totalLength = fileStream.Length;            // 文件流长度
            var buffer = new byte[eachLength];

            UploadFileRequest request;
            UploadFileReply reply;
            string url = $"http://{_host}:{_port}";
            using (var channel = GrpcChannel.ForAddress(url))
            {
                var client = new FileService.FileServiceClient(channel);
                var uploadResult = client.UploadFile(grpcHeads);

                while (sended < totalLength)
                {
                    int length;
                    if ((totalLength - sended) > eachLength)
                        length = await fileStream.ReadAsync(buffer, sended, eachLength);
                    else
                        length = await fileStream.ReadAsync(buffer, sended, (int)(totalLength - sended));
                    sended += length;

                    request = new UploadFileRequest()
                    {
                        BizFolder = bizFolder,
                        IndexFolder = indexFolder,
                        FileName = fileName,
                        FileExt = string.Empty,
                        FileBytes = ByteString.CopyFrom(buffer)
                    };

                    await uploadResult.RequestStream.WriteAsync(request);
                }

                await uploadResult.RequestStream.CompleteAsync();

                reply = await uploadResult.ResponseAsync;
            }

            return new BizIOSingleUploadJsonResult()
            {
                Code = reply.Result ? BizIOStateCode.Success : BizIOStateCode.Failure,
                Message = reply.Message,
                RelativePath = reply.RelativePath,
                Url = reply.Url
            };
        }
    }
}
