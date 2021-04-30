using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        /// 请求拦截处理
        /// </summary>
        /// <param name="requestContext"></param>
        public void OnIntercept(ActionContext requestContext)
        {
            string token = this.IOStorageConfig.AppToken;

            if (requestContext.HttpContext.Request.Headers.TryGetValue(c_head_token, out StringValues headTK))
            {

            }
        }
    }
}
