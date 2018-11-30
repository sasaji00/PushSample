using Foundation;
using Prism;
using Prism.Ioc;
using PushSample.Constants;
using PushSample.Helpers.Implements;
using PushSample.Managers.Implements;
using PushSample.Services.Implements;
using System;
using UIKit;
using UserNotifications;
using WindowsAzure.Messaging;

namespace PushSample.iOS
{
    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the 
    /// User Interface of the application, as well as listening (and optionally responding) to 
    /// application events from iOS.
    /// </summary>
    [Register(Constant.IOS.Register.AppDelegate)]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        /// This method is invoked when the application has loaded and is ready to run. In this 
        /// method you should instantiate the window, load the UI into it and then make the window
        /// visible.
        /// You have 17 seconds to return from this method, or iOS will terminate your application.
        /// </summary>
        /// <param name="app">UIApplication</param>
        /// <param name="options">NSDictionary</param>
        /// <returns>base.FinishedLaunching</returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // プッシュ通知初期化
            var notificationHubManager = NotificationHubManager.GetInstance();
            var notification = new Services.Implements.Notification(Constant.Push.ListenConnectionString, Constant.Push.NotificationHubName);
            notificationHubManager.SetNotificationHub(notification);

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                UNUserNotificationCenter.Current.RequestAuthorization(
                    UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound | UNAuthorizationOptions.Sound,
                    (granted, error) =>
                    {
                        if (granted)
                        {
                            InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);
                        }
                    });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                       UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                       new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }

            this.LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }

        /// <summary>
        /// 登録が失敗した時に呼ばれる
        /// </summary>
        /// <param name="application">Application.</param>
        /// <param name="error">Error.</param>
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            //Create Alert
            var okAlertController = UIAlertController.Create(string.Empty, "Error registering push notifications", UIAlertControllerStyle.Alert);

            //Add Action
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

            // Present Alert
            this.Window.RootViewController.PresentViewController(okAlertController, true, null);
        }

        /// <summary>
        /// プッシュ通知が登録される時に呼ばれる
        /// </summary>
        /// <param name="application">Application.</param>
        /// <param name="deviceToken">Device token.</param>
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var authHelper = new AuthHelper(new AccountStoreWrapper());
            authHelper.SaveToken(Constant.TokenKey, deviceToken.GetBase64EncodedString(NSDataBase64EncodingOptions.None));
        }

        /// <summary>
        /// プッシュ通知を受信する時に呼ばれる
        /// </summary>
        /// <param name="application">Application.</param>
        /// <param name="userInfo">User info.</param>
        /// <param name="completionHandler">Completion handler.</param>
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            NSDictionary aps = userInfo.ObjectForKey(new NSString(Constant.IOS.Aps.Key)) as NSDictionary;

            string alert = string.Empty;
            if (aps.ContainsKey(new NSString(Constant.IOS.Aps.Content.Key.Alert)))
            {
                alert = (aps[new NSString(Constant.IOS.Aps.Content.Key.Alert)] as NSString).ToString();
            }

            // Show alert
            if (!string.IsNullOrEmpty(alert))
            {
                var notificationAlert = UIAlertController.Create(Message.PushTitle, alert, UIAlertControllerStyle.Alert);
                notificationAlert.AddAction(UIAlertAction.Create(Message.Ok, UIAlertActionStyle.Cancel, null));
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(notificationAlert, true, null);
            }
        }
    }

    /// <summary>
    /// iOSInitializer
    /// </summary>
    public class iOSInitializer : IPlatformInitializer
    {
        /// <summary>
        /// RegisterTypes
        /// </summary>
        /// <param name="container">IContainerRegistry</param>
        public void RegisterTypes(IContainerRegistry container)
        {
        }
    }
}
