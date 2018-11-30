using NUnit.Framework;
using PushSampleService.Models;
using System.Collections.Generic;

namespace PushSampleService.Tests.Models
{
    /// <summary>
    /// プッシュ通知内容クラスのテスト
    /// </summary>
    [TestFixture]
    public class NotificationContentTest
    {
        /// <summary>
        /// テスト対象クラス
        /// </summary>
        private NotificationContent notificationContent;

        /// <summary>
        /// テスト準備
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.notificationContent = new NotificationContent();
        }

        /// <summary>
        /// テスト終了処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.notificationContent = null;
        }

        /// <summary>
        /// プッシュ通知内容を設定すると値が取得できる
        /// </summary>
        [Test]
        public void プッシュ通知内容を設定すると値が取得できる()
        {
            var message = "test_message";
            var tags = new List<string>();
            tags.Add("test_tag");

            this.notificationContent.Message = message;
            this.notificationContent.Tags = tags;
            Assert.AreEqual(this.notificationContent.Message, message);
            Assert.AreEqual(this.notificationContent.Tags, tags);
        }
    }
}
