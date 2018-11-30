using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using PushSampleService.Models;
using System.IO;

namespace PushSampleService.Tests.Models
{
    /// <summary>
    /// 環境設定クラスのテスト
    /// </summary>
    [TestFixture]
    public class AppSettingsConfigTest
    {
        /// <summary>
        /// 環境設定
        /// </summary>
        private IOptions<AppSettingsConfig> appSettingsConfig;

        /// <summary>
        /// テスト準備
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            // プロパティファイルをオブジェクト化
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.test.json")
                .Build();

            // サービスコレクションに設定を登録
            var service = new ServiceCollection();
            service.Configure<AppSettingsConfig>(config.GetSection("AppSettings"));

            // 設定を取り出す
            this.appSettingsConfig = service.BuildServiceProvider().GetService<IOptions<AppSettingsConfig>>();
        }

        /// <summary>
        /// テスト終了処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.appSettingsConfig = null;
        }

        /// <summary>
        /// 環境設定のオブジェクトにアクセスすると設定値が取得できる
        /// </summary>
        [Test]
        public void 環境設定のオブジェクトにアクセスすると設定値が取得できる()
        {
            Assert.AreEqual(this.appSettingsConfig.Value.NotificationHubName, "test_name");
            Assert.AreEqual(this.appSettingsConfig.Value.NotificationHubConnectionString, "test_string");
        }
    }
}
