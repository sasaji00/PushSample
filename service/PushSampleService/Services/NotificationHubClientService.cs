using Microsoft.Azure.NotificationHubs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PushSampleService.Constants;
using PushSampleService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushSampleService.Services
{
    /// <summary>
    /// NotificationHubsと接続するクラス
    /// </summary>
    public class NotificationHubClientService : INotificationHubClientService
    {
        /// <summary>
        /// NotificationHubsと接続するクライアント
        /// </summary>
        private NotificationHubClient notificationHubClient;

        /// <summary>
        /// ログ出力用オブジェクト
        /// </summary>
        private ILogger<NotificationHubClientService> logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="appSettings">アプリ設定</param>
        /// <param name="logger">ログ出力用オブジェクト</param>
        public NotificationHubClientService(IOptions<AppSettingsConfig> appSettings, ILogger<NotificationHubClientService> logger)
        {
            // ログ出力設定
            this.logger = logger;

            // 接続文字列のログ出力
            this.logger.LogInformation(string.Format(
                Message.CurrentConnection,
                appSettings.Value.NotificationHubName,
                appSettings.Value.NotificationHubConnectionString));

            // クライアントを生成
            this.notificationHubClient = NotificationHubClient.CreateClientFromConnectionString(
                appSettings.Value.NotificationHubConnectionString, 
                appSettings.Value.NotificationHubName);
        }

        /// <summary>
        /// プッシュ通知を送信
        /// </summary>
        /// <param name="messages">メッセージ</param>
        /// <returns>なし</returns>
        public async Task SendPushAsync(Dictionary<string, string> messages)
        {
            // プッシュ通知を送信
            var result = await this.notificationHubClient.SendTemplateNotificationAsync(messages);

            // ログ出力
            this.logger.LogInformation(result.State.ToString());
        }

        /// <summary>
        /// タグを指定してプッシュ通知を送信
        /// </summary>
        /// <param name="messages">メッセージ</param>
        /// <param name="tags">タグ</param>
        /// <returns>なし</returns>
        public async Task SendPushAsync(Dictionary<string, string> messages, List<string> tags)
        {
            // タグを指定してプッシュ通知を送信(指定できるタグの数は最大20個)
            var result = await this.notificationHubClient.SendTemplateNotificationAsync(messages, tags);

            // ログ出力
            this.logger.LogInformation(result.State.ToString());
        }
    }
}
