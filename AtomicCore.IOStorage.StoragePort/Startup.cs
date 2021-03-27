using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
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
        /// 构造函数
        /// </summary>
        /// <param name="configuration">系统配置</param>
        /// <param name="env">WebHost变量</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
        }

        #endregion

        #region Propertys

        /// <summary>
        /// 系统配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// WebHost变量
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; }

        #endregion

        #region Public Methods

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region AtomicCore引擎初始化

            AtomicCore.AtomicKernel.Initialize();

            #endregion

            #region 运行环境部署（Linux or IIS）

            ///* 如果部署在linux系统上，需要加上下面的配置： */
            //services.Configure<KestrelServerOptions>(options => 
            //{
            //    options.AllowSynchronousIO = true;
            //});

            ///* 如果部署在IIS上，需要加上下面的配置： */
            //services.Configure<IISServerOptions>(options => 
            //{
            //    options.AllowSynchronousIO = true;
            //    options.AutomaticAuthentication = false;
            //});
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

            #region 设置上传最大限制阀值（修改默认阀值）

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
            });

            #endregion

            #region 添加MVC服务

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /* 调试DEBUG模式 */
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
