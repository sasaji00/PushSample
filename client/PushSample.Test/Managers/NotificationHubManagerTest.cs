using Moq;
using NUnit.Framework;
using PushSample.Managers.Implements;
using PushSample.Managers.Interfaces;
using PushSample.Services.Interfaces;
using System.Collections.Generic;

namespace PushSample.Test.Managers
{
    /// <summary>
    /// NotificationHubManagerテスト
    /// </summary>
    [TestFixture]
    public class NotificationHubManagerTest
    {
        /// <summary>
        /// notificationHubManagerインスタンス
        /// </summary>
        private INotificationHubManager notificationHubManager;

        /// <summary>
        /// notificationHubモック
        /// </summary>
        private Mock<INotificationHub> notificationHub;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.notificationHubManager = NotificationHubManager.GetInstance();
            this.notificationHub = new Mock<INotificationHub>();
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.notificationHubManager.Init();
        }

        /// <summary>
        /// インスタンス初期化処理する
        /// </summary>
        [Test]
        public void インスタンス初期化処理する()
        {
            // テスト出来ないので通過テストのみ
            this.notificationHubManager.Init();
        }

        /// <summary>
        /// NotificationHubインスタンスを設定する
        /// </summary>
        [Test]
        public void NotificationHubインスタンスを設定する()
        {
            // テスト出来ないので通過テストのみ
            this.notificationHubManager.SetNotificationHub(this.notificationHub.Object);
        }

        /// <summary>
        /// NotificationHubへの登録処理する_タグ無し
        /// </summary>
        [Test]
        public void NotificationHubへの登録処理_タグ無し()
        {
            // 設定
            var result = "pnsHandle";
            var tags = new List<string>();
            this.notificationHub.Setup(m => m.RegisterTemplate(result, tags));
            this.notificationHubManager.SetNotificationHub(this.notificationHub.Object);
            // 実行
            this.notificationHubManager.Register(result);
            // 検査
            this.notificationHub.Verify();
        }

        /// <summary>
        /// NotificationHubへの登録処理する_タグ有り
        /// </summary>
        [Test]
        public void NotificationHubへの登録処理_タグ有り()
        {
            // 設定
            var result = "pnsHandle";
            var tags = new List<string>() { "hoge" };
            this.notificationHub.Setup(m => m.RegisterTemplate(result, tags));
            this.notificationHubManager.SetNotificationHub(this.notificationHub.Object);
            // 実行
            this.notificationHubManager.Register(result);
            // 検査
            this.notificationHub.Verify();
        }
    }
}
