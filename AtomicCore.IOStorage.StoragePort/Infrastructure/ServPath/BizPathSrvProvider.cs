using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace AtomicCore.IOStorage.StoragePort
{
    /// <summary>
    /// 路径操作实现类
    /// </summary>
    public class BizPathSrvProvider : IBizPathSrvProvider
    {
        #region Variable

        /// <summary>
        /// 路径环境变量
        /// </summary>
        private readonly IWebHostEnvironment _hostEnv;

        /// <summary>
        /// 服务提供接口
        /// </summary>
        private readonly IOptionsMonitor<BizAppSettings> _appSettings;

        #endregion

        #region Constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostEnv">WEB变量</param>
        /// <param name="srvProvider">服务提供接口</param>
        public BizPathSrvProvider(IWebHostEnvironment hostEnv, IOptionsMonitor<BizAppSettings> appSettings)
        {
            this._hostEnv = hostEnv;
            this._appSettings = appSettings;

            this.SaveRootDir = appSettings.CurrentValue.SaveRootDir;
            this.PermittedExtensions = appSettings.CurrentValue.AllowFileExts.ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            this.FileSizeLimit = int.TryParse(appSettings.CurrentValue.AllowFileMBSizeLimit, out int size) ? size * 1024 * 1024 : 0;
        }

        #endregion

        #region Propertys

        /// <summary>
        /// 文件存储根目录
        /// </summary>
        public string SaveRootDir { get; }

        /// <summary>
        /// 允许存储的格式（eg -> .jpg .png ....）
        /// </summary>
        public string[] PermittedExtensions { get; }

        /// <summary>
        /// 允许存储的单文件最大限制（单位:B）
        /// </summary>
        public long FileSizeLimit { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 获取IO路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string MapPath(string path)
        {
            var filePath = Path.Combine(_hostEnv.WebRootPath ?? _hostEnv.ContentRootPath, path);
            return filePath;
        }

        #endregion
    }
}
