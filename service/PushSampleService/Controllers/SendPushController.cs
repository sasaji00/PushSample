using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PushSampleService.Constants;
using PushSampleService.Services;

namespace PushSampleService.Controllers
{
    /// <summary>
    /// プッシュ通知用コントローラ
    /// </summary>
    [Produces("application/json")]
    [Route("api/SendPush")]
    public class SendPushController : Controller
    {
        /// <summary>
        /// NotificationHubsと接続するクラス
        /// </summary>
        private INotificationHubClientService notificationHubClientService;

        /// <summary>
        /// ログ出力用オブジェクト
        /// </summary>
        private ILogger<SendPushController> logger;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="notificationHubClientService">NotificationHubsと接続するクライアント</param>
        /// <param name="logger">ログ出力用オブジェクト</param>
        public SendPushController(INotificationHubClientService notificationHubClientService, ILogger<SendPushController> logger)
        {
            this.logger = logger;
            this.notificationHubClientService = notificationHubClientService;
        }

        /// <summary>
        /// メッセージをプッシュ通知する
        /// GET api/SendPush/samplemessage?tags=tag1,tag2
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="tags">カンマ区切りのタグ</param>
        /// <returns>なし</returns>
        [HttpGet("{message}")]
        public async Task<IActionResult> Get(string message, [FromQuery] string tags)
        {
            try
            {
                if (!string.IsNullOrEmpty(tags))
                {
                    // タグを指定してプッシュ通知を送信
                    await this.notificationHubClientService.SendPushAsync(this.CreatePushMessage(message), tags.Split(Constant.Comma).ToList());
                }
                else
                {
                    // プッシュ通知を送信
                    await this.notificationHubClientService.SendPushAsync(this.CreatePushMessage(message));
                }
                
                return this.Ok(Message.ResponseOk);
            }
            catch (Exception ex)
            {
                // エラーログ出力
                this.logger.LogError(ex.StackTrace);
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