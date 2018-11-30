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
    /// PushRegisterPageViewModel Test
    /// </summary>
    [TestFixture]
    public class PushRegisterPageViewModelTest
    {
        /// <summary>
        /// テスト用 エラーメッセージ
        /// </summary>
        private readonly string errorMessage = "ERROR";

        /// <summary>
        /// NavigationService モック
        /// </summary>
        private Mock<INavigationService> navigationService;

        /// <summary>
        /// PageDialogService モック
        /// </summary>
        private Mock<IPageDialogService> pageDialogService;

        /// <summary>
        /// subscribeManager モック
        /// </summary>
        private Mock<ISubscribeManager> subscribeManager;

        /// <summary>
        /// PushRegisterPageViewModel
        /// </summary>
        private PushRegisterPageViewModel pushRegisterPageViewModel;

        /// <summary>
        /// ボタンタイプ
        /// </summary>
        private string[] buttonTypes = new string[] { "User", "Category" };

        /// <summary>
        /// カテゴリ登録テスト用 List
        /// </summary>
        private List<SubscribeCategory> subscribeCategories = new List<SubscribeCategory>();

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.navigationService = new Mock<INavigationService>();
            this.pageDialogService = new Mock<IPageDialogService>();
            this.subscribeManager = new Mock<ISubscribeManager>();
            this.pushRegisterPageViewModel = new PushRegisterPageViewModel(this.navigationService.Object, this.pageDialogService.Object, this.subscribeManager.Object);

            // カテゴリ登録テスト用
            this.subscribeCategories.Add(new SubscribeCategory("JapaneseFood", "和食", true));
            this.subscribeCategories.Add(new SubscribeCategory("FrenchFood", "フレンチ", false));
            this.subscribeCategories.Add(new SubscribeCategory("ChineseFood", "中華", false));
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.navigationService = null;
            this.pageDialogService = null;
            this.subscribeManager = null;
            this.pushRegisterPageViewModel = null;
        }

        /// <summary>
        /// コンストラクタテスト
        /// </summary>
        [Test]
        public void PushRegisterPageViewModel_コンストラクタテスト()
        {
            Assert.AreEqual(Message.PageTitle.PushRegisterPage, this.pushRegisterPageViewModel.Title);
            Assert.IsNotNull(this.pushRegisterPageViewModel.NavigationService);
            Assert.IsNotNull(this.pushRegisterPageViewModel.RegisterCommand);
        }

        /// <summary>
        /// UserRegisterCommand実行時のテスト
        /// </summary>
        [Test]
        public void PushRegisterPageViewModel_RegisterCommandユーザ登録テスト()
        {
            this.subscribeManager.Setup(m => m.Register(It.IsAny<SubscribeUser>()));
            this.subscribeManager.Verify();
            this.pushRegisterPageViewModel = new PushRegisterPageViewModel(this.navigationService.Object, this.pageDialogService.Object, this.subscribeManager.Object);
            var command = this.pushRegisterPageViewModel.RegisterCommand;
            command.Execute(this.buttonTypes[0]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());

            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Register, Message.Ok, Message.Ok), Times.AtMostOnce());
        }

        /// <summary>
        /// UserRegisterCommand実行中エラー発生時のテスト
        /// </summary>
        [Test]
        public void PushRegisterPageViewModel_RegisterCommandエラー時テスト()
        {
            Exception ex = new Exception(this.errorMessage);
            this.subscribeManager.Setup(m => m.Register(It.IsAny<SubscribeUser>())).Throws(ex);
            this.subscribeManager.Verify();
            this.pushRegisterPageViewModel = new PushRegisterPageViewModel(this.navigationService.Object, this.pageDialogService.Object, this.subscribeManager.Object);
            var command = this.pushRegisterPageViewModel.RegisterCommand;
            command.Execute(this.buttonTypes[0]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());

            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Error, ex.Message, Message.Ok), Times.AtMostOnce());
        }

        /// <summary>
        /// カテゴリ登録実行時のテスト
        /// </summary>
        [Test]
        public void PushRegisterPageViewModel_RegisterCommandカテゴリ登録テスト()
        {
            this.subscribeManager.Setup(m => m.Register(this.subscribeCategories));
            this.subscribeManager.Verify();
            var command = this.pushRegisterPageViewModel.RegisterCommand;
            command.Execute(this.buttonTypes[1]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());

            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Register, Message.Ok, Message.Ok), Times.AtMostOnce());
        }

        /// <summary>
        /// カテゴリ登録実行中エラー発生時のテスト
        /// </summary>
        [Test]
        public void PushRegisterPageViewModel_RegisterCommandカテゴリ登録エラー発生時テスト()
        {
            Exception ex = new Exception(this.errorMessage);
            this.subscribeManager.Setup(m => m.Register(this.subscribeCategories)).Throws(ex);
            this.subscribeManager.Verify();
            var command = this.pushRegisterPageViewModel.RegisterCommand;
            command.Execute(this.buttonTypes[1]);
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());

            this.pageDialogService.Verify(m => m.DisplayAlertAsync(Message.Error, ex.Message, Message.Ok), Times.AtMostOnce());
        }
    }
}
