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

        private const string ENV_KEY_APPTOKEN = "IOSTORAGE_APPTOKEN";
        private const string ENV_KEY_SAVEROOTDIR = "IOSTORAGE_SAVEROOTDIR";
        private const string ENV_KEY_ALLOWFILEEXTS = "IOSTORAGE_ALLOWFILEEXTS";
        private const string ENV_KEY_ALLOWFILEMBSIZELIMIT = "IOSTORAGE_ALLOWFILEMBSIZELIMIT";

        /// <summary>
        /// 路径环境变量
        /// </summary>
        private readonly IWebHostEnvironment _hostEnv;

        /// <summary>
        /// 服务提供接口
        /// </summary>
        private readonly IOptionsMonitor<BizIOStorageConfig> _ioStorageOption;

        #endregion

        #region Constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostEnv">WEB变量</param>
        /// <param name="srvProvider">服务提供接口</param>
        public BizPathSrvProvider(IWebHostEnvironment hostEnv, IOptionsMonitor<BizIOStorageConfig> ioStorageOption)
        {
            this._hostEnv = hostEnv;
            this._ioStorageOption = ioStorageOption;

            /* 优先判断从环境变量中获取值 */
            string env_appToken = Environment.GetEnvironmentVariable(ENV_KEY_APPTOKEN, EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(env_appToken))
                this.AppToken = ioStorageOption.CurrentValue.AppToken;
            else
            {
                Console.WriteLine($"--> BizPathSrvProvider's property 'AppToken' get from evn valiue is '{env_appToken}'");
                this.AppToken = env_appToken;
            }

            string env_saveRootDir = Environment.GetEnvironmentVariable(ENV_KEY_SAVEROOTDIR, EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(env_saveRootDir))
                this.SaveRootDir = ioStorageOption.CurrentValue.SaveRootDir.ToLower();
            else
            {
                Console.WriteLine($"--> BizPathSrvProvider's property 'SaveRootDir' get from evn valiue is '{env_saveRootDir.ToLower()}'");
                this.SaveRootDir = env_saveRootDir.ToLower();
            }

            string env_permittedExts = Environment.GetEnvironmentVariable(ENV_KEY_ALLOWFILEEXTS, EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(env_permittedExts))
                this.PermittedExtensions = ioStorageOption.CurrentValue.AllowFileExts.ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            else
            {
                Console.WriteLine($"--> BizPathSrvProvider's property 'PermittedExtensions' get from evn valiue is '{env_permittedExts}'");
                this.PermittedExtensions = env_permittedExts.ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            }

            string env_fileSizeLimit = Environment.GetEnvironmentVariable(ENV_KEY_ALLOWFILEMBSIZELIMIT, EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(env_fileSizeLimit))
                this.FileSizeLimit = int.TryParse(ioStorageOption.CurrentValue.AllowFileMBSizeLimit, out int size) ? size * 1024 * 1024 : 0;
            else
            {
                Console.WriteLine($"--> BizPathSrvProvider's property 'FileSizeLimit' get from evn valiue is '{env_fileSizeLimit}'M");
                this.FileSizeLimit = int.TryParse(env_fileSizeLimit, out int size) ? size * 1024 * 1024 : 0;
            }
        }

        #endregion

        #region Propertys

        /// <summary>
        /// appToken密钥
        /// </summary>
        public string AppToken { get; }

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
