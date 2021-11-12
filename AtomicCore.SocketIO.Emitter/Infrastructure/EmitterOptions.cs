namespace AtomicCore.SocketIO.Emitter
{
    /// <summary>
    /// Emitter Options
    /// </summary>
    public class EmitterOptions
    {
        /// <summary>
        /// 主机IP
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// KEY
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 请求版本
        /// </summary>
		public EVersion Version = EVersion.V0_9_9;

        /// <summary>
        /// EV版本
        /// </summary>
        public enum EVersion { V0_9_9, V1_4_4 };
    }
}