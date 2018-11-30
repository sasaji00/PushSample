using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PushSampleService.Controllers;
using PushSampleService.Tests.Stubs;
using System;
using System.Threading.Tasks;

namespace PushSampleService.Tests.Controllers
{
    /// <summary>
    /// プッシュ通知コントローラクラスのテスト
    /// </summary>
    [TestFixture]
    public class SendPushControllerTest
    {
        /// <summary>
        /// ログ出力用オブジェクトのモック
        /// </summary>
        private ILogger<SendPushController> mockLogger;

        /// <summary>
        /// テスト対象クラス
        /// </summary>
        private SendPushController sendPushController;

        /// <summary>
        /// テスト対象クラス(例外発生用)
        /// </summary>
        private SendPushController sendPushControllerEx;

        /// <summary>
        /// テスト準備
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.mockLogger = new Mock<ILogger<SendPushController>>().Object;
            this.sendPushController = new SendPushController(new NotificationHubClientServiceStub(true), this.mockLogger);
            this.sendPushControllerEx = new SendPushController(new NotificationHubClientServiceStub(false), this.mockLogger);
        }

        /// <summary>
        /// テスト終了処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.mockLogger = null;
            this.sendPushController = null;
            this.sendPushControllerEx = null;
        }

        /// <summary>
        /// プッシュ通知が送信されるとステータスコード200が返却される
        /// </summary>
        /// <returns>なし</returns>
        [Test]
        public async Task プッシュ通知が送信されるとステータスコード200が返却される()
        {
            var result = await this.sendPushController.Get("test_message", null);
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
            Assert.ThrowsAsync(typeof(Exception), () => this.sendPushControllerEx.Get("test_message", null));
        }

        /// <summary>
        /// タグ別プッシュ通知が送信されるとステータスコード200が返却される
        /// </summary>
        /// <returns>なし</returns>
        [Test]
        public async Task タグ別プッシュ通知が送信されるとステータスコード200が返却される()
        {
            var result = await this.sendPushController.Get("test_message", "tag1,tag2");
            var resultObject = result as OkObjectResult;
            Assert.AreEqual(resultObject.StatusCode, 200);
            Assert.AreEqual(resultObject.Value, "OK");
        }

        /// <summary>
        /// タグ別プッシュ通知の呼び出し先で処理が失敗すると例外が発生する
        /// </summary>
        [Test]
        public void タグ別プッシュ通知の呼び出し先で処理が失敗すると例外が発生する()
        {
            Assert.ThrowsAsync(typeof(Exception), () => this.sendPushControllerEx.Get("test_message", "tag1,tag2"));
        }
    }
}
