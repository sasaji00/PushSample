using Prism;
using Prism.Ioc;
using Prism.Logging;
using Prism.Unity;
using PushSample.Helpers.Implements;
using PushSample.Helpers.Interfaces;
using PushSample.Managers.Implements;
using PushSample.Managers.Interfaces;
using PushSample.Services.Implements;
using PushSample.Services.Interfaces;
using PushSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace PushSample
{
    /// <summary>
    /// Prism
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <summary>
        /// The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
        /// This imposes a limitation in which the App class must have a default constructor.
        /// App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
        /// </summary>
        public App() : this(null)
        {
        }

        /// <summary>
        /// Initializer
        /// </summary>
        /// <param name="initializer">The parameter is not used</param>
        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        /// <summary>
        /// OnInitialized
        /// </summary>
        protected override async void OnInitialized()
        {
            this.InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MenuPage");
        }

        /// <summary>
        /// RegisterRequiredTypes
        /// </summary>
        /// <param name="containerRegistry">ContainerRegistry</param>
        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);

            // Prismのログをデバッグログに出力する
            containerRegistry.RegisterSingleton<ILoggerFacade, DebugLogger>();
        }

        /// <summary>
        /// RegisterTypes
        /// </summary>
        /// <param name="containerRegistry">ContainerRegistry</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Prismのログをデバッグログに出力する
            containerRegistry.RegisterSingleton<ILoggerFacade, DebugLogger>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MenuPage>();
            containerRegistry.RegisterForNavigation<PushManagerPage>();
            containerRegistry.RegisterForNavigation<PushRegisterPage>();

            containerRegistry.Register<ISubscribeManager, SubscribeManager>();
            containerRegistry.Register<IAuthHelper, AuthHelper>();
            containerRegistry.RegisterSingleton<IPushManager, PushManager>();
            containerRegistry.RegisterSingleton<IHttpHandler, HttpHandler>();
            containerRegistry.RegisterSingleton<IAccountStoreWrapper, AccountStoreWrapper>();
        }
    }
}
