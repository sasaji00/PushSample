using PushSampleService.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushSampleService.Tests.Stubs
{
    /// <summary>
    /// NotificationHubsと接続するクラスのスタブ
    /// </summary>
    public class NotificationHubClientServiceStub : INotificationHubClientService
    {
        /// <summary>
        /// 処理の成功可否
        /// </summary>
        private bool isSuccess;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isSuccess">処理の成功可否</param>
        public NotificationHubClientServiceStub(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        /// <summary>
        /// プッシュ通知を送信
        /// </summary>
        /// <param name="messages">メッセージ</param>
        /// <returns>なし</returns>
        public async Task SendPushAsync(Dictionary<string, string> messages)
        {
            await Task.Run(() => 
            {
                if (!isSuccess)
                {
                    throw new Exception("test_error");
                }
            });
        }

        /// <summary>
        /// タグを指定してプッシュ通知を送信
        /// </summary>
        /// <param name="messages">メッセージ</param>
        /// <param name="tags">タグ</param>
        /// <returns>なし</returns>
        public async Task SendPushAsync(Dictionary<string, string> messages, List<string> tags)
        {
            await Task.Run(() =>
            {
                if (!isSuccess)
                {
                    throw new Exception("test_error");
                }
            });
        }
    }
}
