using System.Collections.Generic;
using System.Threading.Tasks;
using PushSample.Models.Implements;

namespace PushSample.Managers.Interfaces
{
    /// <summary>
    /// メッセージ通知管理クラス
    /// </summary>
    public interface IPushManager
    {
        /// <summary>
        /// 全体通知
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns>結果</returns>
        Task<string> SendAsync(string message);

        /// <summary>
        /// 特定ユーザに通知
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="notificationUser">通知対象ユーザー情報</param>
        /// <returns>結果</returns>
        Task<string> SendAsync(string message, NotificationUser notificationUser);

        /// <summary>
        /// 特定カテゴリ選択ユーザに通知
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="notificationCategories">通知対象カテゴリ情報</param>
        /// <returns>結果</returns>
        Task<string> SendAsync(string message, List<NotificationCategory> notificationCategories);
    }
}
