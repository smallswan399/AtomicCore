using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AtomicCore.IOStorage.StoragePort
{
    /// <summary>
    /// NetCore Startup
    /// </summary>
    public class Startup
    {
        #region Constructor

        /// <summary>
        /// 注入构造函数
        /// </summary>
        /// <param name="configuration">配置接口注入</param>
        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Propertys

        /// <summary>
        /// 配置信息解耦
        /// </summary>
        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        #endregion

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region 启用IIS进程内核承载模型（https://docs.microsoft.com/zh-cn/aspnet/core/host-and-deploy/iis/?view=aspnetcore-3.1）

            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.AutomaticAuthentication = false;
            //});

            #endregion

            #region 启用IIS进程外承载模型

            //services.Configure<IISOptions>(options =>
            //{
            //    options.ForwardClientCertificate = false;
            //});

            #endregion

            #region 加载读取配置项（AppSettings）

            IConfigurationSection appSettings = Configuration.GetSection("AppSettings");
            services.Configure<BizAppSettings>(appSettings);
            services.AddSingleton<IBizPathSrvProvider, BizPathSrvProvider>();

            #endregion

            #region 添加MVC服务

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            /* 激活MVC路由 */
            app.UseRouting();

            /* 激活输出缓存 */
            app.UseResponseCaching();

            /* 设置MVC默认访问 */
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }

        #endregion
    }
}
