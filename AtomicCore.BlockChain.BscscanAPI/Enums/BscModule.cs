namespace AtomicCore.BlockChain.BscscanAPI
{
    /// <summary>
    /// bsc模块枚举
    /// </summary>
    public enum BscModule
    {
        /// <summary>
        /// 账户模块
        /// </summary>
        Accounts = 1,

        /// <summary>
        /// 合约模块
        /// </summary>
        Contracts = 2,

        /// <summary>
        /// 交易模块
        /// </summary>
        Transactions = 3,

        /// <summary>
        /// 区块模块
        /// </summary>
        Blocks = 4,

        /// <summary>
        /// 日志模块
        /// </summary>
        Logs = 5,

        /// <summary>
        /// GETH代理模块
        /// </summary>
        GethProxy = 6,

        /// <summary>
        /// 代币模块
        /// </summary>
        Tokens = 7,

        /// <summary>
        /// 手续费模块
        /// </summary>
        GasTracker = 8,

        /// <summary>
        /// 状态模块
        /// </summary>
        Stats = 9
    }
}
