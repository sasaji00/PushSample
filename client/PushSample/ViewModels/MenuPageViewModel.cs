using Prism.Navigation;
using PushSample.Constants;
using PushSample.Managers.Interfaces;
using Reactive.Bindings;

namespace PushSample.ViewModels
{
    /// <summary>
    /// MenuPage ViewModel
    /// </summary>
    public class MenuPageViewModel : ViewModelBase
    {
        /// <summary>
        /// SubscribeManager
        /// </summary>
        private ISubscribeManager subscribeManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService">NavigationSerivce</param>
        /// <param name="subscribeManager">SubscribeManager</param>
        public MenuPageViewModel(INavigationService navigationService, ISubscribeManager subscribeManager) : base(navigationService)
        {
            this.Title = Message.PageTitle.MenuPage;
            this.PushRegisterCommand.Subscribe(async () => await this.NavigationService.NavigateAsync(Constant.PageName.PushRegisterPage));
            this.PushManagerCommand.Subscribe(async () => await this.NavigationService.NavigateAsync(Constant.PageName.PushManagerPage));

            // トークン登録処理
            this.subscribeManager = subscribeManager;
            this.subscribeManager.Register();
        }

        /// <summary>
        /// PushRegister画面遷移コマンド
        /// </summary>
        public ReactiveCommand PushRegisterCommand { get; } = new ReactiveCommand();

        /// <summary>
        /// PushManager画面遷移コマンド
        /// </summary>
        public ReactiveCommand PushManagerCommand { get; } = new ReactiveCommand();
    }
}