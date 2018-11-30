using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using PushSample.Constants;
using PushSample.Managers.Implements;
using PushSample.Models.Implements;
using PushSample.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace PushSample.Test.Managers
{
    /// <summary>
    /// PushManager テスト
    /// </summary>
    [TestFixture]
    public class PushManagerTest
    {
        /// <summary>
        /// テスト対象クラス
        /// </summary>
        private PushManager pushManager;

        /// <summary>
        /// HttpHandlerのモック
        /// </summary>
        private Mock<IHttpHandler> httpHandler;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.httpHandler = new Mock<IHttpHandler>();
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.pushManager = null;
            this.httpHandler = null;
        }

        /// <summary>
        /// 全体通知テスト
        /// </summary>
        [Test]
        public void 全体通知API呼び出しテスト()
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = "test_message";
            var json = JsonConvert.SerializeObject(notificationContent);
            var stringContent = new StringContent(json, Encoding.UTF8, Constant.Http.ContentType);
            var expected = stringContent.ReadAsStringAsync().ToString();
            HttpResponseMessage httpResponseMessage = this.CreateResponseMessage(Message.Ok, System.Net.HttpStatusCode.OK);
            this.httpHandler.Setup(m => m.PostAsync(Constant.Api.NotificationUrl, It.IsAny<StringContent>())).ReturnsAsync(httpResponseMessage);
            this.pushManager = new PushManager(this.httpHandler.Object);
            var result = this.pushManager.SendAsync("test_message");
            this.httpHandler.Verify(m => m.PostAsync(Constant.Api.NotificationUrl, It.Is<StringContent>(x => x.ReadAsStringAsync().ToString().Equals(expected))));
            Assert.AreEqual(result.Result, Message.Ok);
        }

        /// <summary>
        /// 特定ユーザー通知テスト
        /// </summary>
        [Test]
        public void 特定ユーザー通知API呼び出しテスト()
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = "test_message";
            notificationContent.Tags = new List<string>();
            notificationContent.Tags.Add("username:test");
            var json = JsonConvert.SerializeObject(notificationContent);
            var stringContent = new StringContent(json, Encoding.UTF8, Constant.Http.ContentType);
            var expected = stringContent.ReadAsStringAsync().ToString();
            HttpResponseMessage httpResponseMessage = this.CreateResponseMessage(Message.Ok, System.Net.HttpStatusCode.OK);
            this.httpHandler.Setup(m => m.PostAsync(Constant.Api.NotificationUrl, It.IsAny<StringContent>())).ReturnsAsync(httpResponseMessage);
            this.pushManager = new PushManager(this.httpHandler.Object);
            var user = new NotificationUser();
            user.Name = "username:test";
            var result = this.pushManager.SendAsync("test_message", user);
            this.httpHandler.Verify(m => m.PostAsync(Constant.Api.NotificationUrl, It.Is<StringContent>(x => x.ReadAsStringAsync().ToString().Equals(expected))));
            Assert.AreEqual(result.Result, Message.Ok);
        }

        /// <summary>
        /// 特定カテゴリ通知テスト
        /// </summary>
        [Test]
        public void 特定カテゴリ通知API呼び出しテスト()
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = "test_message";
            notificationContent.Tags = new List<string>();
            notificationContent.Tags.Add("tag1");
            notificationContent.Tags.Add("tag3");
            var json = JsonConvert.SerializeObject(notificationContent);
            var stringContent = new StringContent(json, Encoding.UTF8, Constant.Http.ContentType);
            var expected = stringContent.ReadAsStringAsync().ToString();
            HttpResponseMessage httpResponseMessage = this.CreateResponseMessage(Message.Ok, System.Net.HttpStatusCode.OK);
            this.httpHandler.Setup(m => m.PostAsync(Constant.Api.NotificationUrl, It.IsAny<StringContent>())).ReturnsAsync(httpResponseMessage);
            this.pushManager = new PushManager(this.httpHandler.Object);
            var categories = new List<NotificationCategory>();
            categories.Add(new NotificationCategory("tag1", "tag1", true));
            categories.Add(new NotificationCategory("tag2", "tag2", false));
            categories.Add(new NotificationCategory("tag3", "tag3", true));
            var result = this.pushManager.SendAsync("test_message", categories);
            this.httpHandler.Verify(m => m.PostAsync(Constant.Api.NotificationUrl, It.Is<StringContent>(x => x.ReadAsStringAsync().ToString().Equals(expected))));
            Assert.AreEqual(result.Result, Message.Ok);
        }

        /// <summary>
        /// HttpResponseMessageを作成する
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="statusCode">ステータスコード</param>
        /// <returns>HttpResponseMessage</returns>
        private HttpResponseMessage CreateResponseMessage(string message, System.Net.HttpStatusCode statusCode)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(message)
            };
        }
    }
}
