using System;
using System.ServiceModel;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// 服务返回抽象基础类
    /// </summary>
    [MessageContract]
    public abstract class BizIOOutputBase
    {
        #region Propertys

        /// <summary>
        /// 服务异常信息
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Exception ServiceException { get; set; }

        /// <summary>
        /// 服务执行错误信息
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string ServiceError { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 结果集实例是否无异常无错误
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAvailable()
        {
            return string.IsNullOrEmpty(this.ServiceError) && (null == this.ServiceException);
        }

        #endregion
    }
}
