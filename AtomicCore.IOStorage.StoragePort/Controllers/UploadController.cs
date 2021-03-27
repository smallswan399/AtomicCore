using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AtomicCore.IOStorage.StoragePort.Controllers
{
    /// <summary>
    /// 上传控制器
    /// https://www.jb51.net/article/174873.htm
    /// </summary>
    public class UploadController : Controller
    {
        /// <summary>
        /// 文件存储根目录
        /// </summary>
        private const string c_rootDir = "Uploads";

        /// <summary>
        /// 当前WEB路径
        /// </summary>
        private IBizPathSrvProvider _pathProvider = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pathProvider"></param>
        public UploadController(IBizPathSrvProvider pathProvider)
        {
            this._pathProvider = pathProvider;
        }

        /// <summary>
        /// 流式文件上传
        /// 直接读取请求体装载后的Section 对应的stream 直接操作strem即可。无需把整个请求体读入内存
        /// </summary>
        /// <remarks>
        /// 从多部分请求收到文件，然后应用直接处理或保存它。流式传输无法显著提高性能。流式传输可降低上传文件时对内存或磁盘空间的需求。
        /// </remarks>
        /// <returns></returns>
        [HttpPost("UploadingStream")]
        public async Task<IActionResult> UploadingStream()
        {
            //获取boundary
            StringSegment boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(Request.ContentType).Boundary).Value;
            //得到reader
            MultipartReader reader = new MultipartReader(boundary.Value, HttpContext.Request.Body);
            //{ BodyLengthLimit = 2000 };//
            var section = await reader.ReadNextSectionAsync();

            //存储路径
            string trustedFileNameForFileStorage = Path.GetRandomFileName();
            string savePath = this.GetSavePath("Test", null, trustedFileNameForFileStorage);

            //读取section
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out _);
                if (hasContentDispositionHeader)
                {
                    await WriteFileAsync(section.Body, savePath);
                }
                section = await reader.ReadNextSectionAsync();
            }
            return Created(nameof(UploadController), null);
        }

        /// <summary>
        /// 缓存式文件上传
        /// 通过模型绑定先把整个文件保存到内存，然后我们通过IFormFile得到stream，优点是效率高，缺点对内存要求大。文件不宜过大
        /// </summary>
        /// <param name=""></param>
        /// <remarks>
        /// 整个文件读入 IFormFile，它是文件的 C# 表示形式，用于处理或保存文件。文件上传所用的资源（磁盘、内存）取决于并发文件上传的数量和大小。 
        /// 如果应用尝试缓冲过多上传，站点就会在内存或磁盘空间不足时崩溃。如果文件上传的大小或频率会消耗应用资源，请使用流式传输
        /// </remarks>
        /// <returns></returns>
        [HttpPost("UploadingFormFile")]
        public async Task<IActionResult> UploadingFormFile(IFormFile file)
        {
            //存储路径
            string trustedFileNameForFileStorage = Path.GetRandomFileName();
            string savePath = this.GetSavePath("Test", null, trustedFileNameForFileStorage);

            using (var stream = file.OpenReadStream())
            {
                await WriteFileAsync(stream, savePath);
            }
            return Created(nameof(UploadController), null);
        }

        /// <summary>
        /// 获取存储IO路径
        /// </summary>
        /// <param name="bizFolder"></param>
        /// <param name="indexFolder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetSavePath(string bizFolder, string indexFolder, string fileName)
        {
            if (string.IsNullOrEmpty(bizFolder))
                throw new ArgumentNullException(nameof(bizFolder));

            string io_bizFolder = this._pathProvider.MapPath(bizFolder);
            if (!Directory.Exists(io_bizFolder))
                Directory.CreateDirectory(io_bizFolder);

            string io_indexFolder;
            if (string.IsNullOrEmpty(indexFolder))
                io_indexFolder = io_bizFolder;
            else
            {
                io_indexFolder = Path.Combine(io_bizFolder, indexFolder);
                if (!Directory.Exists(io_indexFolder))
                    Directory.CreateDirectory(io_indexFolder);
            }

            return Path.Combine(io_indexFolder, fileName);
        }

        /// <summary>
        /// 写文件导到磁盘
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="path">文件保存路径</param>
        /// <returns></returns>
        private async Task<int> WriteFileAsync(System.IO.Stream stream, string path)
        {
            const int FILE_WRITE_SIZE = 84975;//写出缓冲区大小
            int writeCount = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, FILE_WRITE_SIZE, true))
            {
                byte[] byteArr = new byte[FILE_WRITE_SIZE];
                int readCount = 0;
                while ((readCount = await stream.ReadAsync(byteArr, 0, byteArr.Length)) > 0)
                {
                    await fileStream.WriteAsync(byteArr, 0, readCount);
                    writeCount += readCount;
                }
            }
            return writeCount;
        }
    }
}
