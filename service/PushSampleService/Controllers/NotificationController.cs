using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PushSampleService.Constants;
using PushSampleService.Models;
using PushSampleService.Services;

namespace PushSampleService.Controllers
{
    /// <summary>
    /// プッシュ通知コントローラ
    /// </summary>
    [Route("api/notification")]
    [Produces("application/json")]
    public class NotificationController : Controller
    {
        /// <summary>
        /// NotificationHubsと接続するクラス
        /// </summary>
        private INotificationHubClientService notificationHubClientService;

        /// <summary>
        /// ログ出力用オブジェクト
        /// </summary>
        private ILogger<NotificationController> logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="notificationHubClientService">NotificationHubsと接続するクライアント</param>
        /// <param name="logger">ログ出力用オブジェクト</param>
        public NotificationController(INotificationHubClientService notificationHubClientService, ILogger<NotificationController> logger)
        {
            this.logger = logger;
            this.notificationHubClientService = notificationHubClientService;
        }

        /// <summary>
        /// メッセージをプッシュ通知する
        /// </summary>
        /// <param name="notificationContent">プッシュ通知内容</param>
        /// <returns>結果</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NotificationContent notificationContent)
        {
            try
            {
                if (notificationContent.Tags?.Count() > 0)
                {
                    // タグを指定してプッシュ通知を送信
                    await this.notificationHubClientService.SendPushAsync(this.CreatePushMessage(notificationContent.Message), notificationContent.Tags);
                }
                else
                {
                    // プッシュ通知を送信
                    await this.notificationHubClientService.SendPushAsync(this.CreatePushMessage(notificationContent.Message));
                }

                return this.Ok(Message.ResponseOk);
            }
            catch (Exception ex)
            {
                // エラーログ出力
                this.logger.LogError(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// プッシュ通知用メッセージ生成
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns>プッシュ通知用に成型したメッセージ</returns>
        private Dictionary<string, string> CreatePushMessage(string message)
        {
            var pushMessage = new Dictionary<string, string>();
            pushMessage[Constant.PushMessageKey] = message;
            return pushMessage;
        }
    }
}
