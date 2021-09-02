namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// To manipulate account resources. Bandwidth or energy.
    /// </summary>
    public interface ITronNetAccountResourcesRest
    {
        /// <summary>
        /// Query the resource information of an account(bandwidth,energy,etc)
        /// </summary>
        /// <param name="address"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        TronNetAccountResourceJson GetAccountResource(string address, bool? visible = null);

        /// <summary>
        /// Query bandwidth information.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        TronNetAccountNetResourceJson GetAccountNet(string address, bool? visible = null);

    }
}
