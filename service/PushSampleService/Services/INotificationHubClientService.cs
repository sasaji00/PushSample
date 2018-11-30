using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushSampleService.Services
{
    /// <summary>
    /// NotificationHubsと接続するクラスのインターフェース
    /// </summary>
    public interface INotificationHubClientService
    {
        /// <summary>
        /// プッシュ通知を送信
        /// </summary>
        /// <param name="messages">メッセージ</param>
        /// <returns>なし</returns>
        Task SendPushAsync(Dictionary<string, string> messages);

        /// <summary>
        /// タグを指定してプッシュ通知を送信
        /// </summary>
        /// <param name="messages">メッセージ</param>
        /// <param name="tags">タグ</param>
        /// <returns>なし</returns>
        Task SendPushAsync(Dictionary<string, string> messages, List<string> tags);
    }
}
