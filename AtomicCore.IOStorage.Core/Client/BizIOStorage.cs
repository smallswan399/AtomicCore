using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// IO存储客户端调用服务类
    /// </summary>
    public class BizIOStorageClient
    {
        private const string c_singleFile = "/ApiService/UploadingStream";

        /// <summary>
        /// 服务端基础URL
        /// </summary>
        private readonly string _baseUrl = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseUrl">服务端URL地址</param>
        public BizIOStorageClient(string baseUrl)
        {
            this._baseUrl = baseUrl;
        }

        /// <summary>
        /// 单文件上传
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <returns></returns>
        public BizIOSingleUploadJsonResult UploadFile(BizIOSingleUploadInput input)
        {
            //基础判断
            if (null == input)
                return new BizIOSingleUploadJsonResult("input参数为空");
            if (string.IsNullOrEmpty(input.BizFolder))
                return new BizIOSingleUploadJsonResult("请指定业务文件夹");
            if (null == input.FileStream || input.FileStream.Length <= 0)
                return new BizIOSingleUploadJsonResult("文件流不允许为空");

            //HTTP请求终端
            //string 

            return null;
        }
    }
}
