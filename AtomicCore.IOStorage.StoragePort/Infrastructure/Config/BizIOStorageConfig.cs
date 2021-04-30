namespace AtomicCore.IOStorage.StoragePort
{
    /// <summary>
    /// IOStorage Config配置信息
    /// </summary>
    public class BizIOStorageConfig
    {
        /// <summary>
        /// APP应用TOKEN
        /// </summary>
        public string AppToken { get; set; }

        /// <summary>
        /// 存储根路径
        /// </summary>
        public string SaveRootDir { get; set; }

        /// <summary>
        /// 允许存储文件格式要求
        /// </summary>
        public string AllowFileExts { get; set; }

        /// <summary>
        /// 允许文件存储的最大限制（单位:MB）
        /// </summary>
        public string AllowFileMBSizeLimit { get; set; }
    }
}
