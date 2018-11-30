using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PushSampleService.Constants;
using PushSampleService.Models;
using PushSampleService.Services;

namespace PushSampleService
{
    /// <summary>
    /// アプリの初期化クラス
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// アプリの設定
        /// </summary>
        /// <param name="configuration">設定</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// 開始時にappsettings.jsonを読み込む
        /// </summary>
        /// <param name="env">env</param>
        public Startup(IHostingEnvironment env)
        {
            // appsettings.jsonファイルの読み込み
            this.Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile(Constant.AppSettingsFileName)
                .Build();
        }

        /// <summary>
        /// 設定
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // appsettings.jsonで設定したKeyValueをバインド            
            services.Configure<AppSettingsConfig>(this.Configuration.GetSection(Constant.AppSettingsSectionName));

            // DIコンテナへの登録
            services.AddTransient<INotificationHubClientService, NotificationHubClientService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        /// <param name="loggerFactory">loggerFactory</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
