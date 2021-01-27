using System.ServiceModel;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// 返回查询的目录的结果集
    /// </summary>
    [MessageContract]
    public class BizIOOutputDirectory : BizIOOutputBase
    {
        /// <summary>
        /// 符合条件的文件Uri地址集合
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string[] IOFileUri { get; set; }
    }
}
