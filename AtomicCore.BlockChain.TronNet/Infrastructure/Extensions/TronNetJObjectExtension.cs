using Newtonsoft.Json.Linq;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet JObject Extension
    /// </summary>
    public static class TronNetJObjectExtension
    {
        /// <summary>
        /// JObject => Contract Type Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jobject"></param>
        /// <returns></returns>
        public static T ToContractValue<T>(this JObject jobject)
            where T : TronNetContractBaseValueJson, new()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jobject.ToString());
        }
    }
}
