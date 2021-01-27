using System.ServiceModel;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// 保存IO返回的结果集
    /// </summary>
    [MessageContract]
    public class BizIOOutputSave : BizIOOutputBase
    {
        /// <summary>
        /// 保存文件的Url地址
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Url { get; set; }
    }
}
