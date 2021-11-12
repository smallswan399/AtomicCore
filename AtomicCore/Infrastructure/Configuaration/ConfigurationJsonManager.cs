using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

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
            ResetRootDir(null);
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

        #region Public Methods

        /// <summary>
        /// Reset Root
        /// </summary>
        /// <param name="dirPath">root dir path</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void ResetRootDir(string dirPath = null)
        {
            string baseDir = System.IO.Directory.GetCurrentDirectory();
            if (!string.IsNullOrEmpty(dirPath))
                baseDir = Path.Combine(baseDir, dirPath);

            string appsettingJsonPath = Path.Combine(baseDir, c_appsettingsFileName);
            string connectionsJsonPath = Path.Combine(baseDir, c_connectionsFileName);

            bool appsetting_existed = true;
            bool connection_existed = true;
            Console.WriteLine($"[ConfigurationJsonManager] --> check appsetting.json file existed, the path is '{appsettingJsonPath}'");
            Console.WriteLine($"[ConfigurationJsonManager] --> check connection.json file existed, the path is '{connectionsJsonPath}'");

            if (!File.Exists(appsettingJsonPath))
            {
                Console.WriteLine($"[ConfigurationJsonManager] --> The '{c_appsettingsFileName}' file does not exist, please check if the file is added to the project and set to 'copy if newer'!");
                appsetting_existed = false;
            }
            if (!File.Exists(connectionsJsonPath))
            {
                Console.WriteLine($"[ConfigurationJsonManager]  --> The {c_connectionsFileName} file does not exist, please check if the file is added in the project and set to 'copy if newer'!");
                connection_existed = false;
            }

            if (appsetting_existed)
                AppSettings = new ConfigurationBuilder()
                        .SetBasePath(baseDir)
                        .AddJsonFile(c_appsettingsFileName, optional: true, reloadOnChange: true)
                        .Build();

            if (connection_existed)
            {
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
        }

        #endregion
    }
}
