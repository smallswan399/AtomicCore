using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AtomicCore
{
    /// <summary>
    /// 配置管理类
    /// </summary>
    public static class ConfigurationJsonManager
    {
        #region Variable

        ///// <summary>
        ///// .NET CORE appsetting.json
        ///// </summary>
        //private static readonly IConfiguration s_appsettingJson = null;

        #endregion

        #region Constructor

        /// <summary>
        /// 静态构造
        /// </summary>
        static ConfigurationJsonManager()
        {
            string baseDir = System.IO.Directory.GetCurrentDirectory();

            AppSettings = new ConfigurationBuilder()
                    .SetBasePath(baseDir)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

            ConnectionStrings = new Dictionary<string, ConnectionStringJsonSettings>();
            IConfiguration connectionJsonConf = new ConfigurationBuilder()
                    .SetBasePath(baseDir)
                    .AddJsonFile("connections.json", optional: true, reloadOnChange: true)
                    .Build();
            List<IConfigurationSection> childSections = connectionJsonConf.GetChildren().ToList();
            if (null != childSections && childSections.Any())
            {
                ConnectionStringJsonSettings jsonSetting = null;
                foreach (IConfigurationSection child in childSections)
                {
                    string each_connectionString = child["connectionString"];
                    string each_providerName = child["providerName"];

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
