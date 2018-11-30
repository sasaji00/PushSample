using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;
using PushSample.Constants;

namespace PushSample.Droid
{
    /// <summary>
    /// 通知を受け取った時の処理を行うクラス
    /// </summary>
    [Service]
    [IntentFilter(new[] { Constant.Android.Intent.MessageEvent })]
    public class FirebaseNotificationService : FirebaseMessagingService
    {
        /// <summary>
        /// The tag.
        /// </summary>
        private const string ServiceName = "FirebaseNotificationService";

        /// <summary>
        /// 通知を受け取る
        /// </summary>
        /// <param name="message">message</param>
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(ServiceName, "From: " + message.From);

            // Pull message body out of the template
            var messageBody = message.Data[Constant.Android.FireBase.RemoteMessage.Key.Message];
            if (string.IsNullOrWhiteSpace(messageBody))
            {
                return;
            }

            Log.Debug(ServiceName, "Notification message body: " + messageBody);
            this.SendNotification(messageBody);
        }

        /// <summary>
        /// 通知を左上に表示する
        /// </summary>
        /// <param name="messageBody">messageBody</param>
        private void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, Constant.Android.FireBase.RemoteMessage.Key.ChannelId)
                .SetSmallIcon(Resource.Drawable.icon)
                .SetContentTitle(Message.PushTitle)
                .SetContentText(messageBody)
                .SetContentIntent(pendingIntent)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}