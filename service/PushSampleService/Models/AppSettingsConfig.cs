namespace PushSampleService.Models
{
    /// <summary>
    /// 環境設定用クラス
    /// </summary>
    public class AppSettingsConfig
    {
        /// <summary>
        /// 通知ハブの接続文字列
        /// Azure→Notification Hubs→Access Policies→DefaultFullSharedAccessSignature
        /// </summary>
        public string NotificationHubConnectionString { get; set; }

        /// <summary>
        /// 通知ハブの名前
        /// </summary>
        public string NotificationHubName { get; set; }
    }
}
