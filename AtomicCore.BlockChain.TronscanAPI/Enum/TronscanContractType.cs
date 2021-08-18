namespace AtomicCore.BlockChain.TronscanAPI
{
    /// <summary>
    /// Tronscan Contract Type
    /// </summary>
    public enum TronscanContractType
    {
        /// <summary>
        /// Trx Transfer
        /// </summary>
        TrxTransfer = 1,

        /// <summary>
        /// Trc10 Transfer(Other Trc10 Token)
        /// </summary>
        Trc10Transfer = 2,

        /// <summary>
        /// vote
        /// </summary>
        Vote = 4,

        /// <summary>
        /// lock trx assets for get bandwidth and energy
        /// </summary>
        LockTrxAssets = 11,

        /// <summary>
        /// Unlock Trx Assets
        /// </summary>
        UnlockTrxAssets = 12,

        /// <summary>
        /// Trc10 withdrawal income
        /// </summary>
        WithdrawalIncome = 13,

        /// <summary>
        /// Create Smart Contract
        /// </summary>
        CreateSmartContract = 30,

        /// <summary>
        /// Trigger Smart Contract(eg:Trc20 && 721)
        /// </summary>
        TriggerSmartContract = 31,
    }
}
