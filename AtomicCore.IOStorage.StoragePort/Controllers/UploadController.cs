using AtomicCore.IOStorage.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AtomicCore.IOStorage.StoragePort.Controllers
{
    /// <summary>
    /// 上传控制器
    /// https://www.jb51.net/article/174873.htm
    /// </summary>
    public class UploadController : Controller
    {
        #region Variable

        /// <summary>
        /// 文件存储根目录
        /// </summary>
        private const string c_rootDir = "Uploads";

        /// <summary>
        /// 允许的后缀
        /// </summary>
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        /// <summary>
        /// 文件最大限制
        /// </summary>
        private readonly long _fileSizeLimit = 2097152;

        /// <summary>
        /// Get the default form options so that we can use them to set the default 
        /// limits for request body data.
        /// </summary>
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        /// <summary>
        /// 当前WEB路径
        /// </summary>
        private readonly IBizPathSrvProvider _pathProvider = null;

        #endregion

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pathProvider"></param>
        public UploadController(IBizPathSrvProvider pathProvider)
        {
            this._pathProvider = pathProvider;
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// 流式文件上传（分片多文件上传）
        /// 直接读取请求体装载后的Section 对应的stream 直接操作strem即可。无需把整个请求体读入内存
        /// </summary>
        /// <param name="bizFolder">业务文件夹</param>
        /// <param name="indexFolder">数据索引文件夹</param>
        /// <remarks>
        /// 从多部分请求收到文件，然后应用直接处理或保存它。流式传输无法显著提高性能。流式传输可降低上传文件时对内存或磁盘空间的需求。
        /// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0
        /// https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/mvc/models/file-uploads/samples/2.x/SampleApp/Controllers/StreamingController.cs
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadingStream(string bizFolder, string indexFolder)
        {
            //基础判断
            if (string.IsNullOrEmpty(bizFolder))
                return Ok(new BizIOBatchUploadJsonResult("业务文件夹不允许为空"));

            //判断内容类型
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                return Ok(new BizIOBatchUploadJsonResult("contentType must be 'multipart' type..."));

            //上传文件集合
            List<string> relativePathList = new List<string>();

            //获取boundary
            MediaTypeHeaderValue mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(Request.ContentType);
            StringSegment stringSegment = HeaderUtilities.RemoveQuotes(mediaTypeHeaderValue.Boundary);
            StringSegment boundary = stringSegment.Value;
            MultipartReader reader = new MultipartReader(boundary.Value, HttpContext.Request.Body);

            //开始抽取一个地址
            MultipartSection section = await reader.ReadNextSectionAsync();

            //优先读取表单提交字段数据
            while (null != section)
            {
                //获取分片内容的描述部分
                bool hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                //若存在描述则执行下逻辑
                if (hasContentDispositionHeader)
                {
                    //判断是否包含
                    if (!MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        return Ok(new BizIOBatchUploadJsonResult("No valid file stream was identified..."));

                    //读取文件流
                    byte[] buffer = await FileHelpers.ProcessStreamedFile(section, contentDisposition, ModelState, _permittedExtensions, _fileSizeLimit);

                    //计算文件HASH值
                    string fileExt = Path.GetExtension(contentDisposition.FileName.Value).ToLowerInvariant();
                    string fileName = string.Format("{0}{1}", AtomicCore.MD5Handler.Generate(buffer, false), fileExt);

                    //计算存储路径 + 上传文件
                    string savePath = this.GetSaveIOPath(bizFolder, indexFolder, fileName);
                    await WriteFileAsync(buffer, savePath);

                    //将文件存储结果返回至集合
                    relativePathList.Add(this.GetRelativePath(bizFolder, indexFolder, fileName));
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            //返回成功数据
            return Ok(new BizIOBatchUploadJsonResult()
            {
                RelativePath = relativePathList
            });
        }

        /// <summary>
        /// 缓存式文件上传（单文件上传）
        /// 通过模型绑定先把整个文件保存到内存，然后我们通过IFormFile得到stream，优点是效率高，缺点对内存要求大。文件不宜过大
        /// </summary>
        /// <param name="fileName">文件名称（带后缀）</param>
        /// <param name="file">上传文件数据</param>
        /// <param name="bizFolder">业务文件夹</param>
        /// <param name="indexFolder">数据索引文件夹</param>
        /// <remarks>
        /// 整个文件读入 IFormFile，它是文件的 C# 表示形式，用于处理或保存文件。文件上传所用的资源（磁盘、内存）取决于并发文件上传的数量和大小。 
        /// 如果应用尝试缓冲过多上传，站点就会在内存或磁盘空间不足时崩溃。如果文件上传的大小或频率会消耗应用资源，请使用流式传输
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadingFormFile(string fileName, IFormFile file, string bizFolder, string indexFolder)
        {
            //基础判断
            if (string.IsNullOrEmpty(fileName))
                return Ok(new BizIOSingleUploadJsonResult("文件名称不允许为空"));
            if (null == file || file.Length <= 0)
                return Ok(new BizIOSingleUploadJsonResult("未检测到上传数据流"));
            if (string.IsNullOrEmpty(bizFolder))
                return Ok(new BizIOSingleUploadJsonResult("业务文件夹不允许为空"));

            //存储路径
            string relativePath = this.GetRelativePath(bizFolder, indexFolder, fileName);
            string savePath = this.GetSaveIOPath(bizFolder, indexFolder, fileName);

            //读取文件流并保存数据
            using (var stream = file.OpenReadStream())
                await WriteFileAsync(stream, savePath);

            //返回成功数据
            return Ok(new BizIOSingleUploadJsonResult()
            {
                RelativePath = relativePath
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 获取相对路径
        /// </summary>
        /// <param name="bizFolder"></param>
        /// <param name="indexFolder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetRelativePath(string bizFolder, string indexFolder, string fileName)
        {
            if (string.IsNullOrEmpty(bizFolder))
                throw new ArgumentNullException(nameof(bizFolder));

            StringBuilder strb = new StringBuilder("/");
            strb.Append(c_rootDir);
            strb.Append('/');
            strb.Append(bizFolder);
            strb.Append('/');
            if (string.IsNullOrEmpty(indexFolder))
                strb.Append(fileName);
            else
                strb.AppendFormat("/{0}/{1}", indexFolder, fileName);

            return strb.ToString();
        }

        /// <summary>
        /// 获取存储IO路径
        /// </summary>
        /// <param name="bizFolder"></param>
        /// <param name="indexFolder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetSaveIOPath(string bizFolder, string indexFolder, string fileName)
        {
            if (string.IsNullOrEmpty(bizFolder))
                throw new ArgumentNullException(nameof(bizFolder));

            if (!Directory.Exists(c_rootDir))
                Directory.CreateDirectory(c_rootDir);

            string io_bizFolder = this._pathProvider.MapPath(string.Format("{0}\\{1}", c_rootDir, bizFolder));
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
            const int FILE_WRITE_SIZE = 84975;
            int writeCount = 0;

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, FILE_WRITE_SIZE, true))
            {
                int readCount = 0;
                byte[] byteArr = new byte[FILE_WRITE_SIZE];

                while ((readCount = await stream.ReadAsync(byteArr, 0, byteArr.Length)) > 0)
                {
                    await fileStream.WriteAsync(byteArr, 0, readCount);
                    writeCount += readCount;
                }
            }

            return writeCount;
        }

        /// <summary>
        /// 写文件导入磁盘
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="path">文件保存路径</param>
        /// <returns></returns>
        private async Task WriteFileAsync(byte[] buffer, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write))
                await fs.WriteAsync(buffer.AsMemory(0, buffer.Length));
        }

        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
                return Encoding.UTF8;

            return mediaType.Encoding;
        }

        #endregion
    }

    public class FormData
    {
        public string Note { get; set; }
    }
}
