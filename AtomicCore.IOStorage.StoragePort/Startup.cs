using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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

            //如果部署在linux系统上，需要加上下面的配置：
            //services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
            //如果部署在IIS上，需要加上下面的配置：
            //services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

            #endregion

            #region 加载读取配置项（AppSettings）

            IConfigurationSection appSettings = Configuration.GetSection("AppSettings");
            services.Configure<BizAppSettings>(appSettings);
            services.AddSingleton<IBizPathSrvProvider, BizPathSrvProvider>();

            #endregion

            #region 设置上传最大限制阀值（修改默认阀值）

            services.Configure<FormOptions>(options =>
            {
                options.ValueLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = long.MaxValue;
                options.MultipartHeadersLengthLimit = int.MaxValue;
            });

            #endregion

            #region Session、IHttpContextAccessor、MVC、Cookie

            /* 《调试模式 -> 若是调试模式,开始razor运行时编译模式》 */
            if (this.WebHostEnvironment.IsDevelopment())
                services.AddRazorPages().AddRazorRuntimeCompilation();

            /* 《启用通用中间件服务》 */
            services.AddMemoryCache();                                      // 使用MemoryCache中间件（代码中允许使用 IMemoryCache 接口）
            services.AddSession();                                          // 使用Session（实践证明需要在配置 Mvc 之前）
            services.AddHttpContextAccessor();                              // 注册当前线程全生命周期上下文接口调用
            services.AddOptions();                                          // 配置Options模式

            /* 《HtmlEncoder设置》 */
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));   // 设置HTML编码解码实现
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);  // 注册Encoding服务

            /* 《数据保护》 */
            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(
                this.WebHostEnvironment.ContentRootPath +
                Path.DirectorySeparatorChar +
                "DataProtection"
            ));

            /*
             * 《MVC相关中间件》
             * 1.注册MVC控制器和视图
             * 2.支持NewtonsoftJson
             * 3.设置MVC版本
             */
            services.AddControllersWithViews(options =>
            {
                /*
                *  1.设置全局拦截(管理后台的请求拦截)
                *  2.修改控制器模型绑定规则
                */
                options.Filters.Add<BizPermissionTokenAttribute>();
                //options.ModelMetadataDetailsProviders.Add(new BizModelBindingMetadataProvider());
            })
            .AddNewtonsoftJson(options =>
            {
                /*
                *  支持 NewtonsoftJson
                *  首字母大写 -> DefaultContractResolver
                *  首字母小写 -> CamelCasePropertyNamesContractResolver
                */
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            /*
             * 《Cookie相关中间件》
             * 1.设置cookie策略
             * 2.Add CookieTempDataProvider after AddMvc and include ViewFeatures.
             */
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                options.HttpOnly = HttpOnlyPolicy.Always;
                options.Secure = CookieSecurePolicy.SameAsRequest;
            });
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /* 调试DEBUG模式 */
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            /* 激活静态资源访问(调试模式不缓存Cache) */
            if (env.IsDevelopment())
                app.UseStaticFiles();
            else
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    OnPrepareResponse = SetCacheControl
                });
            }

            /* 按顺序启动激活Mvc相关中间件 */
            app.UseSession();                               //激活Session
            app.UseRouting();                               //激活Routing
            //app.UseAuthorization();                         //激活认证服务
            app.UseResponseCaching();                       //激活输出缓存
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 设置cache control
        /// </summary>
        /// <param name="context"></param>
        private static void SetCacheControl(StaticFileResponseContext context)
        {
            int second = 365 * 24 * 60 * 60;
            context.Context.Response.Headers.Add("Cache-Control", new[] { "public,max-age=" + second });
            context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddYears(1).ToString("R") }); // Format RFC1123
        }

        #endregion
    }
}
