using AtomicCore.IOStorage.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AtomicCore.IOStorage.StoragePort
{
    /// <summary>
    /// IO服务接口
    /// https://lebang2020.cn/details/210110njneqn2f.html
    /// </summary>
    public class BizIOStorageProvider : IBizIOStorageProvider
    {
        public BizIOOnputBreakPointTrans BreakPointTransmition(BizIOInputBreakPointTrans inputParam)
        {
            throw new NotImplementedException();
        }

        public BizIOOutputDelete Delete(BizIOInputDelete inputParam)
        {
            throw new NotImplementedException();
        }

        public BizIOOutputDirectory GetDirectory(BizIOInputDirectory inputParam)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 回路测试
        /// </summary>
        /// <returns></returns>
        public string LoopTester()
        {
            return string.Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public BizIOOutputSave Save(BizIOInputSave inputParam)
        {
            throw new NotImplementedException();
        }
    }
}
