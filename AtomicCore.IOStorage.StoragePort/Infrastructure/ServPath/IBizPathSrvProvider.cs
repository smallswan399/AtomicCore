namespace AtomicCore.IOStorage.StoragePort
{
    /// <summary>
    /// 路径接口
    /// </summary>
    public interface IBizPathSrvProvider
    {
        /// <summary>
        /// 获取IO路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string MapPath(string path);
    }
}
