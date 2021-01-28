using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AtomicCore.IOStorage.StoragePort
{
    /// <summary>
    /// 路径操作实现类
    /// </summary>
    public class BizPathSrvProvider : IBizPathSrvProvider
    {
        /// <summary>
        /// 路径环境变量
        /// </summary>
        private IWebHostEnvironment _hostEnv;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostEnv"></param>
        public BizPathSrvProvider(IWebHostEnvironment hostEnv)
        {
            this._hostEnv = hostEnv;
        }

        /// <summary>
        /// 获取IO路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string MapPath(string path)
        {
            var filePath = Path.Combine(_hostEnv.WebRootPath, path);
            return filePath;
        }
    }
}
