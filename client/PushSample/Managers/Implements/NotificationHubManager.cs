using PushSample.Managers.Interfaces;
using PushSample.Services.Interfaces;
using System.Collections.Generic;

namespace PushSample.Managers.Implements
{
    /// <summary>
    /// NotificationHubを操作するクラス
    /// </summary>
    public class NotificationHubManager : INotificationHubManager
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        private static NotificationHubManager instance = new NotificationHubManager();

        /// <summary>
        /// NotificationHub インスタンス
        /// </summary>
        private INotificationHub notificationHub;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private NotificationHubManager()
        {
        }

        /// <summary>
        /// インスタンスを取得する
        /// </summary>
        /// <returns>NotificationHubManagerインスタンス</returns>
        public static NotificationHubManager GetInstance()
        {
            return instance;
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init()
        {
            instance = new NotificationHubManager();
        }

        /// <summary>
        /// NotificationHubインスタンスを設定する
        /// </summary>
        /// <param name="instance">NotificationHubインスタンス</param>
        public void SetNotificationHub(INotificationHub instance)
        {
            this.notificationHub = instance;
        }

        /// <summary>
        /// NotificationHubへの登録処理
        /// </summary>
        /// <param name="pnsHandle">pnsに接続するためのToken</param>
        public void Register(string pnsHandle)
        {
            this.Register(pnsHandle, new List<string>() { });
        }

        /// <summary>
        /// NotificationHubへの登録処理
        /// </summary>
        /// <param name="pnsHandle">pnsに接続するためのToken</param>
        /// <param name="tags">端末に紐付けるタグ</param>
        public void Register(string pnsHandle, List<string> tags)
        {
            this.notificationHub.RegisterTemplate(pnsHandle, tags);
        }
    }
}