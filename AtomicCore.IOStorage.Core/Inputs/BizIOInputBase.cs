using System;
using System.ServiceModel;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// 接口参数输入抽象类
    /// </summary>
    [MessageContract]
    public abstract class BizIOInputBase
    {
        /// <summary>
        /// 服务器允许信任的标识约定
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public String ServiceToken { get; set; }
    }
}
