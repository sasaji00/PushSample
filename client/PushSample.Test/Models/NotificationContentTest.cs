using NUnit.Framework;
using PushSample.Models.Implements;
using System.Collections.Generic;

namespace PushSample.Test.Models
{
    /// <summary>
    /// プッシュ通知内容のテスト
    /// </summary>
    [TestFixture]
    public class NotificationContentTest
    {
        /// <summary>
        /// テスト対象クラス
        /// </summary>
        private NotificationContent notificationContent;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.notificationContent = new NotificationContent();
        }

        /// <summary> 
        /// 後処理 
        /// </summary> 
        [TearDown]
        public void TearDown()
        {
            this.notificationContent = null;
        }

        /// <summary>
        /// プッシュ通知内容を設定すると値を取得できる
        /// </summary>
        [Test]
        public void プッシュ通知内容を設定すると値を取得できる()
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
