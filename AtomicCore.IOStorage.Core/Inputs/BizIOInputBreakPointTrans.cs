using System;
using System.IO;
using System.ServiceModel;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// IO BreakPointTransmition参数类
    /// </summary>
    [MessageContract]
    public class BizIOInputBreakPointTrans : BizIOInputBase, IDisposable
    {
        #region Propertys

        /// <summary>
        /// 文件存储根目录(例如:Upload)
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String IORootFolder { get; set; }

        /// <summary>
        /// 业务存储文件夹(例如:在Upload根文件夹下的News业务文件夹)
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String IOBizFolder { get; set; }

        /// <summary>
        /// 保存的图片的业务索引文件夹名（例如:News业务文件夹下以为资讯ID标识创建的文件夹）
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String IOIndexFolder { get; set; }

        /// <summary>
        /// 保存的文件名称
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String IOFileName { get; set; }

        /// <summary>
        /// 文件流
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream IOFileStream { get; set; }

        /// <summary>
        /// 继续断点续传写入的流的开始下标索引
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long StartOffset { get; set; }

        #endregion

        #region IDisposable

        /// <summary>
        /// 析构释放非托管资源对象
        /// </summary>
        void IDisposable.Dispose()
        {
            if (this.IOFileStream != null)
            {
                this.IOFileStream.Close();
                this.IOFileStream = null;
            }
        }

        #endregion
    }
}
