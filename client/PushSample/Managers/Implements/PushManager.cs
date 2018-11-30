using Newtonsoft.Json;
using PushSample.Constants;
using PushSample.Managers.Interfaces;
using PushSample.Models.Implements;
using PushSample.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PushSample.Managers.Implements
{
    /// <summary>
    /// PushManager
    /// </summary>
    public class PushManager : IPushManager
    {
        /// <summary>
        /// HttpHandler
        /// </summary>
        private readonly IHttpHandler httpHandler;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpHandler">HttpHandler</param>
        public PushManager(IHttpHandler httpHandler)
        {
            this.httpHandler = httpHandler;
        }

        /// <summary>
        /// 全体通知
        /// </summary>
        /// <param name="message">通知メッセージ</param>
        /// <returns>結果</returns>
        public async Task<string> SendAsync(string message)
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = message;
            var json = JsonConvert.SerializeObject(notificationContent);
            var stringContent = new StringContent(json, Encoding.UTF8, Constant.Http.ContentType);
            HttpResponseMessage response = await this.httpHandler.PostAsync(Constant.Api.NotificationUrl, stringContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 特定ユーザに通知
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="user">通知対象ユーザー情報</param>
        /// <returns>結果</returns>
        public async Task<string> SendAsync(string message, NotificationUser user)
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = message;
            notificationContent.Tags = new List<string>();
            notificationContent.Tags.Add(user.Name);
            var json = JsonConvert.SerializeObject(notificationContent);
            var stringContent = new StringContent(json, Encoding.UTF8, Constant.Http.ContentType);
            HttpResponseMessage response = await this.httpHandler.PostAsync(Constant.Api.NotificationUrl, stringContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 特定カテゴリを選択したユーザーに通知
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="categories">通知対象カテゴリ情報</param>
        /// <returns>結果</returns>
        public async Task<string> SendAsync(string message, List<NotificationCategory> categories)
        {
            var notificationContent = new NotificationContent();
            notificationContent.Message = message;
            notificationContent.Tags = categories.Where(x => x.Flag == true).Select(x => x.Name).ToList();
            var json = JsonConvert.SerializeObject(notificationContent);
            var stringContent = new StringContent(json, Encoding.UTF8, Constant.Http.ContentType);
            HttpResponseMessage response = await this.httpHandler.PostAsync(Constant.Api.NotificationUrl, stringContent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
