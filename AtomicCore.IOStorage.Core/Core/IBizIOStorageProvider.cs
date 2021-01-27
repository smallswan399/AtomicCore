using System.ServiceModel;

namespace AtomicCore.IOStorage.Core
{
    /// <summary>
    /// IO存储服务契约接口
    /// </summary>
    [ServiceContract]
    public interface IBizIOStorageProvider
    {
        /// <summary>
        /// 测试回路
        /// </summary>
        /// <returns></returns>
        [OperationContract()]
        string LoopTester();

        /// <summary>
        /// 列出目录
        /// </summary>
        /// <param name="inputParam"></param>
        /// <returns></returns>
        [OperationContract()]
        BizIOOutputDirectory GetDirectory(BizIOInputDirectory inputParam);

        /// <summary>
        /// 断点续传保存
        /// </summary>
        /// <param name="inputParam"></param>
        /// <returns></returns>
        [OperationContract()]
        BizIOOnputBreakPointTrans BreakPointTransmition(BizIOInputBreakPointTrans inputParam);

        /// <summary>
        /// IO保存
        /// </summary>
        /// <param name="inputParam"></param>
        /// <returns></returns>
        [OperationContract()]
        BizIOOutputSave Save(BizIOInputSave inputParam);

        /// <summary>
        /// IO删除
        /// </summary>
        /// <param name="inputParam"></param>
        /// <returns></returns>
        [OperationContract()]
        BizIOOutputDelete Delete(BizIOInputDelete inputParam);
    }
}
