using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using PushSample.Constants;
using PushSample.Managers.Implements;
using WindowsAzure.Messaging;

namespace PushSample.Droid
{
    /// <summary>
    /// MainActivity
    /// </summary>
    [Activity(Label = Constant.Android.Activity.Label, Icon = Constant.Android.Activity.Icon, Theme = Constant.Android.Activity.Theme, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// OnCreate
        /// </summary>
        /// <param name="bundle">Bundle</param>
        protected override void OnCreate(Bundle bundle)
        {
            MainActivity.TabLayoutResource = Resource.Layout.Tabbar;
            MainActivity.ToolbarResource = Resource.Layout.Toolbar;

            // プッシュ通知初期化
            var notificationHubManager = NotificationHubManager.GetInstance();
            var notification = new Services.Implements.Notification(Constant.Push.NotificationHubName, Constant.Push.ListenConnectionString, this);
            notificationHubManager.SetNotificationHub(notification);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.LoadApplication(new App(new AndroidInitializer()));
        }
    }

    /// <summary>
    /// AndroidInitializer
    /// </summary>
    public class AndroidInitializer : IPlatformInitializer
    {
        /// <summary>
        /// RegisterTypes
        /// </summary>
        /// <param name="container">ContainerRegistry</param>
        public void RegisterTypes(IContainerRegistry container)
        {
        }
    }
}
