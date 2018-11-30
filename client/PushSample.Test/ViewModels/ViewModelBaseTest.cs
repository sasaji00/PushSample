using Moq;
using NUnit.Framework;
using Prism.Navigation;
using PushSample.ViewModels;

namespace PushSample.Test.ViewModels
{
    /// <summary>
    /// ViewModelBase テスト
    /// </summary>
    [TestFixture]
    public class ViewModelBaseTest
    {
        /// <summary>
        /// NavigationSerivce モック
        /// </summary>
        private Mock<INavigationService> navigationService;

        /// <summary>
        /// ViewModelBase
        /// </summary>
        private ViewModelBase viewModelBase;

        /// <summary>
        /// テスト用 タイトル変数
        /// </summary>
        private string testTitle = "testTitle";

        /// <summary>
        /// テスト用 NavigationParameters変数
        /// </summary>
        private NavigationParameters navigationParameter = new NavigationParameters
        {
            { "testKey", "testValue" },
        };

        /// <summary>
        /// 前処理
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.navigationService = new Mock<INavigationService>();
            this.viewModelBase = new ViewModelBase(this.navigationService.Object);
        }

        /// <summary>
        /// 後処理
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            this.navigationService = null;
            this.viewModelBase = null;
        }

        /// <summary>
        /// コンストラクタテスト
        /// </summary>
        [Test]
        public void ViewModelBase_コンストラクタテスト()
        {
            Assert.IsNotNull(this.viewModelBase.NavigationService);
        }

        /// <summary>
        /// ViewModelBase タイトルテスト
        /// </summary>
        [Test]
        public void ViewModelBase_Titleテスト()
        {
            this.viewModelBase.Title = this.testTitle;
            Assert.AreEqual(this.testTitle, this.viewModelBase.Title);
        }

        /// <summary>
        /// ViewModelBase OnNavigationtedFromテスト
        /// </summary>
        [Test]
        public void ViewModelBase_OnNavigatedFromテスト()
        {
            this.viewModelBase.OnNavigatedFrom(this.navigationParameter);
        }

        /// <summary>
        /// ViewModelBase OnNavigatedToテスト
        /// </summary>
        [Test]
        public void ViewModelBase_OnNavigatedToテスト()
        {
            this.viewModelBase.OnNavigatedTo(this.navigationParameter);
        }

        /// <summary>
        /// ViewModelBase OnNavigatingテスト
        /// </summary>
        [Test]
        public void ViewModelBase_OnNavigatingToテスト()
        {
            this.viewModelBase.OnNavigatingTo(this.navigationParameter);
        }

        /// <summary>
        /// ViewModelBase Destroyテスト
        /// </summary>
        [Test]
        public void ViewModelBase_Destroy()
        {
            this.viewModelBase.Destroy();
        }
    }
}
