using NUnit.Framework;
using PushSample.Models.Implements;

namespace PushSample.Test.Models
{
    /// <summary>
    /// NotificationCategoryテスト
    /// </summary>
    [TestFixture]
    public class NotificationCategoryTest
    {
        /// <summary>
        /// NotificationCategory格納用変数
        /// </summary>
        private NotificationCategory notificationCategory;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.notificationCategory = new NotificationCategory("categoryName", "showName", false);
        }

        /// <summary> 
        /// 後処理 
        /// </summary> 
        [TearDown]
        public void TearDown()
        {
            this.notificationCategory = null;
        }

        /// <summary>
        /// カテゴリ名入出力テスト
        /// </summary>
        [Test]
        public void カテゴリ名入出力テスト()
        {
            var showName = "showName";
            var testName = "hoge";
            this.notificationCategory.Name = testName;
            this.notificationCategory.ShowName = showName;
            this.notificationCategory.Flag = false;
            Assert.AreEqual(testName, this.notificationCategory.Name);
            Assert.AreEqual(showName, this.notificationCategory.ShowName);
            Assert.AreEqual(false, this.notificationCategory.Flag);
        }
    }
}
