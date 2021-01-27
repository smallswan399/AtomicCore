using System;
using System.ServiceModel;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// IO删除输入参数
    /// </summary>
    [MessageContract]
    public class BizIOInputDelete : BizIOInputBase
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

        #endregion
    }
}
