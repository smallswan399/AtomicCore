using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;

namespace AtomicCore.IOStorage.StoragePort.Controllers
{
    /// <summary>
    /// MVC控制器抽象类
    /// </summary>
    public abstract class BizControllerBase : Controller, IBizPremissionIntercept
    {
        /// <summary>
        /// 头部信息Token
        /// </summary>
        private const string c_head_token = "token";

        private bool _hasReadConfig = false;
        private BizIOStorageConfig _ioStorageConfig = null;

        /// <summary>
        /// 存储配置数据
        /// </summary>
        public BizIOStorageConfig IOStorageConfig
        {
            get
            {
                if (null == this._ioStorageConfig || !_hasReadConfig)
                {
                    Type srvType = typeof(IOptionsMonitor<>).MakeGenericType(typeof(BizIOStorageConfig));
                    IOptionsMonitor<BizIOStorageConfig> opm = (IOptionsMonitor<BizIOStorageConfig>)this.HttpContext.RequestServices.GetService(srvType);
                    this._ioStorageConfig = opm.CurrentValue;
                }

                return this._ioStorageConfig;
            }
        }

        /// <summary>
        /// 是否有权限
        /// </summary>
        public bool HasPremission { get; private set; } = true;

        /// <summary>
        /// 请求拦截处理
        /// </summary>
        /// <param name="requestContext"></param>
        public void OnIntercept(ActionContext requestContext)
        {
            //判断系统是否配置了token权限
            string cfgToken = null == this.IOStorageConfig ? string.Empty : this.IOStorageConfig.AppToken;
            if (string.IsNullOrEmpty(cfgToken))
                return;

            //判断头部是否包含token
            bool hasHeadToken = requestContext.HttpContext.Request.Headers.TryGetValue(c_head_token, out StringValues headTK);
            if (!hasHeadToken)
                this.HasPremission = false;
            else
                this.HasPremission = cfgToken.Equals(headTK.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
