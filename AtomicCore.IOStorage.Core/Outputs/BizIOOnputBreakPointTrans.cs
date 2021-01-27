using System.ServiceModel;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// 断点续传IO返回的结果集
    /// </summary>
    [MessageContract]
    public class BizIOOnputBreakPointTrans : BizIOOutputBase
    {
        /// <summary>
        /// 当前流写入的下标偏移量
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long CurrentOffSet { get; set; }

        /// <summary>
        /// 保存文件的Url地址
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Url { get; set; }
    }
}
