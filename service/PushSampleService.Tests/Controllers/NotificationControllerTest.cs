using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PushSampleService.Controllers;
using PushSampleService.Models;
using PushSampleService.Tests.Stubs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushSampleService.Tests.Controllers
{
    /// <summary>
    /// プッシュ通知コントローラクラスのテスト
    /// </summary>
    [TestFixture]
    public class NotificationControllerTest
    {
        /// <summary>
        /// ログ出力用オブジェクトのモック
        /// </summary>
        private ILogger<NotificationController> mockLogger;

        /// <summary>
        /// テスト対象クラス
        /// </summary>
        private NotificationController notificationController;

        /// <summary>
        /// テスト対象クラス(例外発生用)
        /// </summary>
        private NotificationController notificationControllerEx;

        /// <summary>
        /// テスト準備
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.mockLogger = new Mock<ILogger<NotificationController>>().Object;
            this.notificationController = new NotificationController(new NotificationHubClientServiceStub(true), this.mockLogger);
            this.notificationControllerEx = new NotificationController(new NotificationHubClientServiceStub(false), this.mockLogger);
        }

        /// <summary>
        /// テスト終了処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.mockLogger = null;
            this.notificationController = null;
            this.notificationControllerEx = null;
        }

        /// <summary>
        /// タグを指定せずに実行すると全体にプッシュ通知が送信される
        /// </summary>
        /// <returns>なし</returns>
        [Test]
        public async Task タグを指定せずに実行すると全体にプッシュ通知が送信される()
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = "test_message";

            var result = await this.notificationController.Post(notificationContent);
            var resultObject = result as OkObjectResult;
            Assert.AreEqual(resultObject.StatusCode, 200);
            Assert.AreEqual(resultObject.Value, "OK");
        }
        
        /// <summary>
        /// タグを指定して実行するとタグを登録した端末にプッシュ通知が送信される
        /// </summary>
        /// <returns>なし</returns>
        [Test]
        public async Task タグを指定して実行するとタグを登録した端末にプッシュ通知が送信される()
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = "test_message";
            notificationContent.Tags = new List<string>();
            notificationContent.Tags.Add("test_tag");

            var result = await this.notificationController.Post(notificationContent);
            var resultObject = result as OkObjectResult;
            Assert.AreEqual(resultObject.StatusCode, 200);
            Assert.AreEqual(resultObject.Value, "OK");
        }

        /// <summary>
        /// プッシュ通知の呼び出し先で処理が失敗すると例外が発生する
        /// </summary>
        [Test]
        public void プッシュ通知の呼び出し先で処理が失敗すると例外が発生する()
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = "test_message";

            Assert.ThrowsAsync(typeof(Exception), () => this.notificationControllerEx.Post(notificationContent));
        }
    }
}
