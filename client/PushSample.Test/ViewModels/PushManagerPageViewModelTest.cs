using Moq;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using PushSample.Constants;
using PushSample.Managers.Interfaces;
using PushSample.Models.Implements;
using PushSample.ViewModels;
using System;
using System.Collections.Generic;

namespace PushSample.Test.ViewModels
{
    /// <summary>
    /// PushManagerPageViewModelのテスト
    /// </summary>
    [TestFixture]
    public class PushManagerPageViewModelTest
    {
        /// <summary>
        /// テスト用ユーザ
        /// </summary>
        private readonly string testUser = "testUser";

        /// <summary>
        /// テスト用メッセージ
        /// </summary>
        private readonly string testMessage = "testMessage";

        /// <summary>
        /// 予測される結果メッセージ
        /// </summary>
        private readonly string resultMessage = "OK";

        /// <summary>
        /// テスト用エラーメッセージ
        /// </summary>
        private readonly string errorMessage = "error";

        /// <summary>
        /// NavigationService モック
        /// </summary>
        private Mock<INavigationService> navigationService;

        /// <summary>
        /// PushManager モック
        /// </summary>
        private Mock<IPushManager> pushManager;

        /// <summary>
        /// MenuPageViewModel
        /// </summary>
        private PushManagerPageViewModel pushManagerPageViewModel;

        /// <summary>
        /// PageDialogSerivce モック
        /// </summary>
        private Mock<IPageDialogService> pageDialogService;

        /// <summary>
        /// ボタンタイプ
        /// </summary>
        private string[] buttonTypes = new string[] { "All", "User", "Category" };

        /// <summary>
        /// カテゴリ通知テスト用 List
        /// </summary>
        private List<NotificationCategory> notificationCategories = new List<NotificationCategory>();

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.navigationService = new Mock<INavigationService>();
            this.pageDialogService = new Mock<IPageDialogService>();
            this.pushManager = new Mock<IPushManager>();
            this.pushManagerPageViewModel = new PushManagerPageViewModel(this.navigationService.Object, this.pushManager.Object, this.pageDialogService.Object);

            // カテゴリ通知テスト用
            this.notificationCategories.Add(new NotificationCategory("JapaneseFood", "和食", true));
            this.notificationCategories.Add(new NotificationCategory("FrenchFood", "フレンチ", false));
            this.notificationCategories.Add(new NotificationCategory("ChineseFood", "中華", false));
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.navigationService = null;
            this.pageDialogService = null;
            this.pushManager = null;
            this.pushManagerPageViewModel = null;
            this.notificationCategories.Clear();
        }

        /// <summary>
        /// コンストラクタテスト
        /// </summary>
        [Test]
        public void PushManagerPageViewModel_コンストラクタテスト()
        {
            Assert.AreEqual(Message.PageTitle.PushManagerPage, this.pushManagerPageViewModel.Title);
            Assert.IsNotNull(this.pushManagerPageViewModel.NavigationService);
        }

        /// <summary>
        /// PushCommandテスト 全体通知
        /// </summary>
        [Test]
        public void PushManagerPageViewModel_PushCommand全体通知テスト()
        {
            this.pushManager.Setup(m => m.SendAsync(this.testMessage)).ReturnsAsync(this.resultMessage);
            this.pushManager.Verify();
            this.pushManagerPageViewModel.MessageEntry.Value = this.testMessage;
            var command = this.pushManagerPageViewModel.PushCommand;
            command.Execute(this.buttonTypes[0]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());
            Assert.AreEqual(this.resultMessage, this.pushManagerPageViewModel.Result.Value);

            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Notification, this.pushManagerPageViewModel.Result.Value, Message.Ok), Times.AtMostOnce());
        }

        /// <summary>
        /// PushCommandテスト ユーザ通知
        /// </summary>
        [Test]
        public void PushManagerPageViewModel_PushCommandユーザ通知テスト()
        {
            this.pushManager.Setup(m => m.SendAsync(this.testMessage, It.IsAny<NotificationUser>())).ReturnsAsync(this.resultMessage);
            this.pushManager.Verify();
            this.pushManagerPageViewModel.MessageEntry.Value = this.testMessage;
            this.pushManagerPageViewModel.UserName.Value = this.testUser;
            var command = this.pushManagerPageViewModel.PushCommand;
            command.Execute(this.buttonTypes[1]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());
            Assert.AreEqual(this.resultMessage, this.pushManagerPageViewModel.Result.Value);

            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Notification, this.pushManagerPageViewModel.Result.Value, Message.Ok), Times.AtMostOnce());
        }

        /// <summary>
        /// PushCommandテスト カテゴリ通知
        /// </summary>
        [Test]
        public void PushManagerPageViewModel_PushCommandカテゴリ通知_テスト()
        {
            this.pushManager.Setup(m => m.SendAsync(this.testMessage, this.notificationCategories)).ReturnsAsync(this.resultMessage);
            this.pushManager.Verify();
            this.pushManagerPageViewModel.MessageEntry.Value = this.testMessage;
            this.pushManagerPageViewModel.Categories.Value = this.notificationCategories;
            var command = this.pushManagerPageViewModel.PushCommand;
            command.Execute(this.buttonTypes[2]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());
            Assert.AreEqual(this.resultMessage, this.pushManagerPageViewModel.Result.Value);

            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Notification, this.pushManagerPageViewModel.Result.Value, Message.Ok), Times.AtMostOnce());
        }

        /// <summary>
        /// PushCommandテスト エラー発生時 全体通知
        /// </summary>
        [Test]
        public void PushManagerPageViewModel_PushCommand全体エラー発生時テスト()
        {
            Exception ex = new Exception(this.errorMessage);
            this.pushManager.Setup(m => m.SendAsync(this.testMessage)).Throws(ex);
            this.pushManager.Verify();
            this.pushManagerPageViewModel.MessageEntry.Value = this.testMessage;
            var command = this.pushManagerPageViewModel.PushCommand;
            command.Execute(this.buttonTypes[0]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());
            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Error, ex.Message, Message.Ok), Times.AtMostOnce());

            ex = null;
        }

        /// <summary>
        /// PushCommandテスト エラー発生時　ユーザ通知
        /// </summary>
        [Test]
        public void PushManagerPageViewModel_PushCommandユーザエラー発生時テスト()
        {
            Exception ex = new Exception(this.errorMessage);
            NotificationUser notificationUser = new NotificationUser();
            notificationUser.Name = this.testUser;
            this.pushManager.Setup(m => m.SendAsync(this.testMessage, notificationUser)).Throws(ex);
            this.pushManager.Verify();
            this.pushManagerPageViewModel.MessageEntry.Value = this.testMessage;
            this.pushManagerPageViewModel.UserName.Value = this.testUser;
            var command = this.pushManagerPageViewModel.PushCommand;
            command.Execute(this.buttonTypes[1]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());
            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Error, ex.Message, Message.Ok), Times.AtMostOnce());

            ex = null;
            notificationUser = new NotificationUser();
        }

        /// <summary>
        /// PushCommandテスト エラー発生時 カテゴリ通知
        /// </summary>
        [Test]
        public void PushManagerPageViewModel_PushCommandカテゴリ通知エラー発生時_テスト()
        {
            Exception ex = new Exception(this.errorMessage);
            this.pushManager.Setup(m => m.SendAsync(this.testMessage, this.notificationCategories)).Throws(ex);
            this.pushManager.Verify();
            this.pushManagerPageViewModel.MessageEntry.Value = this.testMessage;
            this.pushManagerPageViewModel.Categories.Value = this.notificationCategories;
            var command = this.pushManagerPageViewModel.PushCommand;
            command.Execute(this.buttonTypes[2]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());
            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Error, ex.Message, Message.Ok), Times.AtMostOnce());

            ex = null;
        }
    }
}