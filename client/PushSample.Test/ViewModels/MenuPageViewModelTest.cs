using Moq;
using NUnit.Framework;
using Prism.Navigation;
using PushSample.Constants;
using PushSample.Managers.Interfaces;
using PushSample.ViewModels;

namespace PushSample.Test.ViewModels
{
    /// <summary>
    /// MenuPageViewModelのテスト
    /// </summary>
    [TestFixture]
    public class MenuPageViewModelTest
    {
        /// <summary>
        /// NavigationService モック
        /// </summary>
        private Mock<INavigationService> navigationService;

        /// <summary>
        /// SubscribeManager モック
        /// </summary>
        private Mock<ISubscribeManager> subscribeManager;

        /// <summary>
        /// MenuPageViewModel
        /// </summary>
        private MenuPageViewModel menuPageViewModel;

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.navigationService = new Mock<INavigationService>();
            this.subscribeManager = new Mock<ISubscribeManager>();
            this.menuPageViewModel = new MenuPageViewModel(this.navigationService.Object, this.subscribeManager.Object);
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.navigationService = null;
            this.subscribeManager = null;
            this.menuPageViewModel = null;
        }

        /// <summary>
        /// 画面名のテスト
        /// </summary>
        [Test]
        public void コンストラクタテスト()
        {
            Assert.AreEqual(Message.PageTitle.MenuPage, this.menuPageViewModel.Title);
            Assert.IsNotNull(this.menuPageViewModel.NavigationService);
        }

        /// <summary>
        /// PushRegister画面遷移テスト
        /// </summary>
        [Test]
        public void PushRegister画面遷移テスト()
        {
            var command = this.menuPageViewModel.PushRegisterCommand;
            command.Execute();
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());

            this.navigationService.Verify(m => m.NavigateAsync(Constant.PageName.PushRegisterPage), Times.AtMostOnce);
            this.subscribeManager.Verify(m => m.Register(), Times.AtMostOnce);
        }

        /// <summary>
        /// PushManager画面遷移テスト
        /// </summary>
        [Test]
        public void PushManager画面遷移テスト()
        {
            var command = this.menuPageViewModel.PushManagerCommand;
            command.Execute();
            Assert.IsNotNull(command);
            Assert.IsTrue(command.CanExecute());

            this.navigationService.Verify(m => m.NavigateAsync(Constant.PageName.PushManagerPage), Times.AtMostOnce);
            this.subscribeManager.Verify(m => m.Register(), Times.AtMostOnce);
        }
    }
}
