using System.Collections.Generic;

namespace PushSample.Services.Interfaces
{
    /// <summary>
    /// NotificationHub接続クラス
    /// </summary>
    public interface INotificationHub
    {
        /// <summary>
        /// 通知用テンプレートを登録する
        /// </summary>
        /// <param name="pnsHandle">PNSハンドル</param>
        /// <param name="tags">タグ</param>
        void RegisterTemplate(string pnsHandle, List<string> tags);
    }
}
