using Newtonsoft.Json.Linq;

namespace AtomicCore.BlockChain.TronNet
{
    /// <summary>
    /// TronNet MachineInfo Json
    /// </summary>
    public class TronNetMachineInfoJson
    {
        public int cpuCount { get; set; }

        public decimal cpuRate { get; set; }

        public int deadLockThreadCount { get; set; }

        public JObject deadLockThreadInfoList { get; set; }

        public ulong freeMemory { get; set; }

        public string javaVersion { get; set; }

        public ulong jvmFreeMemory { get; set; }

        public ulong jvmTotalMemory { get; set; }

        public TronNetMachineMemoryDescInfoJson[] memoryDescInfoList { get; set; }

        public string osName { get; set; }

        public decimal processCpuRate { get; set; }

        public int threadCount { get; set; }

        public ulong totalMemory { get; set; }
    }
}
