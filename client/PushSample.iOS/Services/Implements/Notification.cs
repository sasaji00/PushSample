using Foundation;
using PushSample.Constants;
using PushSample.Services.Interfaces;
using System.Collections.Generic;
using WindowsAzure.Messaging;

namespace PushSample.iOS.Services.Implements
{
    /// <summary>
    /// NotificationHub接続クラス
    /// </summary>
    public class Notification : INotificationHub
    {
        /// <summary>
        /// SBNotificationHubインスタンス
        /// </summary>
        private SBNotificationHub sBNotificationHub;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PushSample.iOS.Services.Implements.Notification"/> class.
        /// </summary>
        /// <param name="listenConnectionString">Listen connection string.</param>
        /// <param name="notificationHubName">Notification hub name.</param>
        public Notification(string listenConnectionString, string notificationHubName)
        {
            this.sBNotificationHub = new SBNotificationHub(listenConnectionString, notificationHubName);
        }

        /// <summary>
        /// 通知用テンプレートを登録する
        /// </summary>
        /// <param name="pnsHandle">PNSハンドル</param>
        /// <param name="tags">タグ</param>
        public void RegisterTemplate(string pnsHandle, List<string> tags)
        {
            this.sBNotificationHub.RegisterTemplateAsyncAsync(
                new NSData(pnsHandle, NSDataBase64DecodingOptions.None),
                Constant.IOS.Aps.RegisteredForRemoteNotifications.Template.TokenName,
                Constant.IOS.Aps.RegisteredForRemoteNotifications.Template.Body,
                string.Empty,
                new NSSet(tags.ToArray()));
        }
    }
}