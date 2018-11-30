using NUnit.Framework;
using PushSample.Models.Implements;

namespace PushSample.Test.Models
{
    /// <summary>
    /// SubscribeUserテスト
    /// </summary>
    [TestFixture]
    public class SubscribeUserTest
    {
        /// <summary>
        /// SubscribeUser格納用変数
        /// </summary>
        private SubscribeUser subscribeUser;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.subscribeUser = new SubscribeUser();
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.subscribeUser = new SubscribeUser();
        }

        /// <summary>
        /// ユーザー名入出力テスト
        /// </summary>
        [Test]
        public void ユーザー名入出力テスト()
        {
            var testName = "hoge";
            this.subscribeUser.Name = testName;
            Assert.AreEqual(testName, this.subscribeUser.Name);
        }
    }
}
