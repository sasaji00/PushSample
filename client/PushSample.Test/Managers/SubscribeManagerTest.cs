using Moq;
using NUnit.Framework;
using PushSample.Constants;
using PushSample.Helpers.Interfaces;
using PushSample.Managers.Implements;
using PushSample.Managers.Interfaces;
using PushSample.Models.Implements;
using PushSample.Services.Interfaces;
using System.Collections.Generic;

namespace PushSample.Test.Managers
{
    /// <summary>
    /// SubscribeManagerテスト
    /// </summary>
    [TestFixture]
    public class SubscribeManagerTest
    {
        /// <summary>
        /// authHelper
        /// </summary>
        private Mock<IAuthHelper> authHelper;

        /// <summary>
        /// notificationHubManager
        /// </summary>
        private INotificationHubManager notificationHubManager;

        /// <summary>
        /// notificationHub mock
        /// </summary>
        private Mock<INotificationHub> notificationHub;

        /// <summary>
        /// subscribeManager
        /// </summary>
        private ISubscribeManager subscribeManager;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.authHelper = new Mock<IAuthHelper>();
            this.notificationHubManager = NotificationHubManager.GetInstance();
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.authHelper = null;
            this.notificationHubManager.Init();
            this.notificationHub = new Mock<INotificationHub>();
            this.notificationHubManager.SetNotificationHub(this.notificationHub.Object);
            this.subscribeManager = null;
        }

        /// <summary>
        /// 購読登録処理_タグなし
        /// </summary>
        [Test]
        public void 購読登録処理_タグなし()
        {
            // 設定
            var pnsHandle = "pnsHandle";
            this.notificationHub = new Mock<INotificationHub>();
            this.notificationHub.Setup(m => m.RegisterTemplate(pnsHandle, It.IsAny<List<string>>()));
            this.notificationHubManager.SetNotificationHub(this.notificationHub.Object);
            this.authHelper.Setup(m => m.GetToken(It.Is<string>(p => p == Constant.TokenKey))).Returns(pnsHandle);
            this.subscribeManager = new SubscribeManager(this.authHelper.Object);
            
            // 実行
            this.subscribeManager.Register();

            // 検証
            this.authHelper.Verify(m => m.GetToken(Constant.TokenKey));
            this.notificationHub.Verify(m => m.RegisterTemplate(pnsHandle, It.Is<List<string>>(a => a.Count == 0)));
        }

        /// <summary>
        /// 購読登録処理_タグあり_ユーザ名
        /// </summary>
        [Test]
        public void 購読登録処理_タグあり_ユーザ名()
        {
            // 設定
            var pnsHandle = "pnsHandle";
            var subscribeUser = new SubscribeUser() { Name = "fujitsu" };
            var tag = Constant.Push.Tags.UserNamePrefix + subscribeUser.Name;
            this.notificationHub = new Mock<INotificationHub>();
            this.notificationHub.Setup(m => m.RegisterTemplate(pnsHandle, It.IsAny<List<string>>()));
            this.notificationHubManager.SetNotificationHub(this.notificationHub.Object);
            this.authHelper.Setup(m => m.GetToken(It.Is<string>(p => p == Constant.TokenKey))).Returns(pnsHandle);
            this.subscribeManager = new SubscribeManager(this.authHelper.Object);

            // 実行
            this.subscribeManager.Register(subscribeUser);

            // 検証
            this.authHelper.Verify(m => m.GetToken(Constant.TokenKey));
            this.notificationHub.Verify(m => m.RegisterTemplate(pnsHandle, It.Is<List<string>>(a => a[0] == tag)));
        }

        /// <summary>
        /// 購読登録処理_タグあり_ユーザ名
        /// </summary>
        [Test]
        public void 購読登録処理_タグあり_カテゴリ()
        {
            // 設定
            var pnsHandle = "pnsHandle";
            var subscribeCategories = new List<SubscribeCategory>() { };
            subscribeCategories.Add(new SubscribeCategory("JapaneseFood", "和食", false));
            subscribeCategories.Add(new SubscribeCategory("FrenchFood", "フレンチ", false));
            subscribeCategories.Add(new SubscribeCategory("ChineseFood", "中華", true));

            this.notificationHub = new Mock<INotificationHub>();
            this.notificationHub.Setup(m => m.RegisterTemplate(pnsHandle, It.IsAny<List<string>>()));
            this.notificationHubManager.SetNotificationHub(this.notificationHub.Object);
            this.authHelper.Setup(m => m.GetToken(It.Is<string>(p => p == Constant.TokenKey))).Returns(pnsHandle);
            this.subscribeManager = new SubscribeManager(this.authHelper.Object);

            // 実行
            this.subscribeManager.Register(subscribeCategories);

            // 検証
            this.authHelper.Verify(m => m.GetToken(Constant.TokenKey));
            this.notificationHub.Verify(m => m.RegisterTemplate(pnsHandle, It.Is<List<string>>(a => a[0] == subscribeCategories[2].Name)));
        }
    }
}
