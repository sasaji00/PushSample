using NUnit.Framework;
using PushSample.Models.Implements;

namespace PushSample.Test.Models
{
    /// <summary>
    /// NotificationUserテスト
    /// </summary>
    [TestFixture]
    public class NotificationUserTest
    {
        /// <summary>
        /// NotificationUser格納用変数
        /// </summary>
        private NotificationUser notificationUser;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.notificationUser = new NotificationUser();
        }

        /// <summary>
        /// ユーザー名入出力テスト
        /// </summary>
        [Test]
        public void ユーザー名入出力テスト()
        {
            var testName = "hoge";
            this.notificationUser.Name = testName;
            Assert.AreEqual(testName, this.notificationUser.Name);
        }
    }
}
