using Android.Content;
using PushSample.Constants;
using PushSample.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsAzure.Messaging;

namespace PushSample.Droid.Services.Implements
{
    /// <summary>
    /// NotificationHub接続クラス
    /// </summary>
    public class Notification : INotificationHub
    {
        /// <summary>
        /// NotificationHubインスタンス
        /// </summary>
        private NotificationHub notificationHub;

        /// <summary>
        /// NotificationHubの設定
        /// </summary>
        /// <param name="notificationHubName">notificationHub名</param>
        /// <param name="listenConnectionString">接続文字列</param>
        /// <param name="context">Androidコンテキスト</param>
        public Notification(string notificationHubName, string listenConnectionString, Android.Content.Context context)
        {
            this.notificationHub = new NotificationHub(Constant.Push.NotificationHubName, Constant.Push.ListenConnectionString, context);
        }

        /// <summary>
        /// 通知用テンプレートを登録する
        /// </summary>
        /// <param name="pnsHandle">PNSハンドル</param>
        /// <param name="tags">タグ</param>
        public void RegisterTemplate(string pnsHandle, List<string> tags)
        {
            var task = new Task(() =>
            {
                this.notificationHub.RegisterTemplate(
                    pnsHandle,
                    Constant.Android.FireBase.SendRegistrationToServer.Key,
                    Constant.Android.FireBase.SendRegistrationToServer.Template.Value.Body,
                    tags.ToArray());
            });
            task.Start();
            task.Wait();
        }
    }
}