namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// Tron Transaction Type Enum
    /// </summary>
    public enum TronTransactionTypeEnum
    {
        /// <summary>
        /// Trx Transfer
        /// </summary>
        TransferContract = 1,

        /// <summary>
        /// Trc10 Contract Tigger
        /// </summary>
        TransferAssetContract = 2,

        /// <summary>
        /// Trc20 Contract Trigger
        /// </summary>
        TriggerSmartContract = 3
    }
}
