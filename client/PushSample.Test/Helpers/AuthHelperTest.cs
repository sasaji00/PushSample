using Moq;
using NUnit.Framework;
using PushSample.Constants;
using PushSample.Helpers.Implements;
using PushSample.Helpers.Interfaces;
using PushSample.Services.Interfaces;
using System.Collections.Generic;
using Xamarin.Auth;

namespace PushSample.Test.Helpers
{
    /// <summary>
    /// AuthHeler Test
    /// </summary>
    [TestFixture]
    public class AuthHelperTest
    {
        /// <summary>
        /// トークン名
        /// </summary>
        private readonly string tokenKey = "DeviceToken";

        /// <summary>
        /// 予測される結果メッセージ
        /// </summary>
        private readonly string resultToken = "SampleToken";

        /// <summary>
        /// HttpHandler Modk
        /// </summary>
        private Mock<IAccountStoreWrapper> accountStoreWrapper;

        /// <summary>
        /// PushManager
        /// </summary>
        private IAuthHelper authHelper;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.accountStoreWrapper = new Mock<IAccountStoreWrapper>();
            this.authHelper = new AuthHelper(this.accountStoreWrapper.Object);
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.accountStoreWrapper = null;
            this.authHelper = null;
        }

        /// <summary>
        /// トークンの取得をするテスト_トークンが見つかる
        /// </summary>
        [Test]
        public void トークンの取得をするテスト_トークンが見つかる()
        {
            var accountList = new List<Account> { };

            // Account作成
            Account account = new Account();
            account.Properties.Add(this.tokenKey, this.resultToken);
            accountList.Add(account);

            this.accountStoreWrapper.Setup(m => m.FindAccountsForService(Constant.AppName)).Returns(accountList);

            Assert.AreEqual(this.resultToken, this.authHelper.GetToken(this.tokenKey));
        }

        /// <summary>
        /// トークンの取得をするテスト_トークンが見つからない
        /// </summary>
        [Test]
        public void トークンの取得をするテスト_トークンが見つからない()
        {
            var accountList = new List<Account> { };

            // Account作成
            Account account = new Account();
            account.Properties.Add(string.Empty, this.resultToken);
            accountList.Add(account);

            this.accountStoreWrapper.Setup(m => m.FindAccountsForService(Constant.AppName)).Returns(accountList);

            Assert.AreEqual(null, this.authHelper.GetToken(this.tokenKey));
        }

        /// <summary>
        /// トークンを保存するテスト
        /// </summary>
        [Test]
        public void トークンの保存をするテスト()
        {
            var accountList = new List<Account> { };

            // Account作成
            Account account = new Account();
            account.Properties.Add(this.tokenKey, this.resultToken);
            accountList.Add(account);

            this.accountStoreWrapper.Setup(m => m.FindAccountsForService(Constant.AppName)).Returns(accountList);
            this.accountStoreWrapper.Setup(m => m.Save(accountList[0], Constant.AppName));

            this.authHelper.SaveToken(this.tokenKey, this.resultToken);

            this.accountStoreWrapper.Verify(m => m.FindAccountsForService(Constant.AppName), Times.Once());
            this.accountStoreWrapper.Verify(m => m.Save(It.Is<Account>(p => p.Properties[this.tokenKey] == this.resultToken), Constant.AppName), Times.Once());
        }
    }
}
