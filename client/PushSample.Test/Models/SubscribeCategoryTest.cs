using NUnit.Framework;
using PushSample.Models.Implements;

namespace PushSample.Test.Models
{
    /// <summary>
    /// SubscribeCategoryテスト
    /// </summary>
    [TestFixture]
    public class SubscribeCategoryTest
    {
        /// <summary>
        /// SubscribeCategory格納変数
        /// </summary>
        private SubscribeCategory subscribeCategory;

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.subscribeCategory = null;
        }

        /// <summary>
        /// カテゴリ入出力テスト
        /// </summary>
        [Test]
        public void カテゴリ入出力テスト()
        {
            string testName = "testName";
            string testShowName = "testShowName";
            bool testFlag = true;
            this.subscribeCategory = new SubscribeCategory(testName, testShowName, testFlag);
            Assert.AreEqual(testName, this.subscribeCategory.Name);
            Assert.AreEqual(testShowName, this.subscribeCategory.ShowName);
            Assert.AreEqual(testFlag, this.subscribeCategory.Flag);
        }
    }
}
