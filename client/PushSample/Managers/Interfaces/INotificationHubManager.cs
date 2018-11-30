using PushSample.Services.Interfaces;
using System.Collections.Generic;

namespace PushSample.Managers.Interfaces
{
    /// <summary>
    /// NotificationHubManager インターフェイス
    /// </summary>
    public interface INotificationHubManager
    {
        /// <summary>
        /// NotificationHubインスタンスを設定する
        /// </summary>
        /// <param name="notificationHub">notificationHubインスタンス</param>
        void SetNotificationHub(INotificationHub notificationHub);

        /// <summary>
        /// NotificationHubへの登録処理
        /// </summary>
        /// <param name="pnsHandle">pnsに接続するためのToken</param>
        void Register(string pnsHandle);

        /// <summary>
        /// NotificationHubへの登録処理
        /// </summary>
        /// <param name="pnsHandle">pnsに接続するためのToken</param>
        /// <param name="tags">端末に紐付けるタグ</param>
        void Register(string pnsHandle, List<string> tags);

        /// <summary>
        /// インスタンス初期化
        /// </summary>
        void Init();
    }
}
