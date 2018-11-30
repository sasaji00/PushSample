using Prism.Mvvm;
using Prism.Navigation;

namespace PushSample.ViewModels
{
    /// <summary>
    /// ViewModelBase
    /// </summary>
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        /// <summary>
        /// Title
        /// </summary>
        private string title;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService">NavigationSerivice</param>
        public ViewModelBase(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }

        /// <summary>
        /// Title
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// NavigationSerivice
        /// </summary>
        public INavigationService NavigationService { get; private set; }

        /// <summary>
        /// OnNavigatedFrom
        /// </summary>
        /// <param name="parameters">NavigationParameters</param>
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// OnNavigatedTo
        /// </summary>
        /// <param name="parameters">NavigationParameters</param>
        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// OnNavigatingTo
        /// </summary>
        /// <param name="parameters">NavigationParameters</param>
        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }

        /// <summary>
        /// Destroy
        /// </summary>
        public virtual void Destroy()
        {
        }
    }
}
