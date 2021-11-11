using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AtomicCore
{
    /// <summary>
    /// 配置管理类
    /// </summary>
    public static class ConfigurationJsonManager
    {
        #region Variable

        /// <summary>
        /// appsettings.json
        /// </summary>
        private const string c_appsettingsFileName = "appsettings.json";

        /// <summary>
        /// connections.json
        /// </summary>
        private const string c_connectionsFileName = "connections.json";

        /// <summary>
        /// connections.json -> connectionString
        /// </summary>
        private const string c_connectionString = "connectionString";

        /// <summary>
        /// connections.json -> providerName
        /// </summary>
        private const string c_providerName = "providerName";

        #endregion

        #region Constructor

        /// <summary>
        /// 静态构造
        /// </summary>
        static ConfigurationJsonManager()
        {
            string baseDir = System.IO.Directory.GetCurrentDirectory();
            string appsettingJsonPath = string.Format("{0}\\{1}", baseDir, c_appsettingsFileName);
            string connectionsJsonPath = string.Format("{0}\\{1}", baseDir, c_connectionsFileName);
            if (!File.Exists(appsettingJsonPath))
                throw new FileNotFoundException(string.Format("{0}文件不存在,请检查是否项目中是否添加了该文件并设置为'如果较新则复制'!", c_appsettingsFileName));
            if (!File.Exists(connectionsJsonPath))
                throw new FileNotFoundException(string.Format("{0}文件不存在,请检查是否项目中是否添加了该文件并设置为'如果较新则复制'!", c_connectionsFileName));

            AppSettings = new ConfigurationBuilder()
                    .SetBasePath(baseDir)
                    .AddJsonFile(c_appsettingsFileName, optional: true, reloadOnChange: true)
                    .Build();

            ConnectionStrings = new Dictionary<string, ConnectionStringJsonSettings>();
            IConfiguration connectionJsonConf = new ConfigurationBuilder()
                    .SetBasePath(baseDir)
                    .AddJsonFile(c_connectionsFileName, optional: true, reloadOnChange: true)
                    .Build();
            List<IConfigurationSection> childSections = connectionJsonConf.GetChildren().ToList();
            if (null != childSections && childSections.Any())
            {
                ConnectionStringJsonSettings jsonSetting;
                foreach (IConfigurationSection child in childSections)
                {
                    string each_connectionString = child[c_connectionString];
                    string each_providerName = child[c_providerName];

                    if (string.IsNullOrEmpty(each_connectionString) || string.IsNullOrEmpty(each_providerName))
                        continue;

                    jsonSetting = new ConnectionStringJsonSettings()
                    {
                        Name = child.Key,
                        ConnectionString = each_connectionString,
                        ProviderName = each_providerName
                    };

                    ConnectionStrings.Add(child.Key, jsonSetting);
                }
            }
        }

        #endregion

        #region Propertys

        /// <summary>
        /// .NET CORE项目中的appsettings.json
        /// </summary>
        public static IConfiguration AppSettings { get; private set; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static IDictionary<string, ConnectionStringJsonSettings> ConnectionStrings { get; private set; }

        #endregion
    }
}
