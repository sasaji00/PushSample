using Newtonsoft.Json;
using System.Collections.Generic;

namespace PushSample.Models.Implements
{
    /// <summary>
    /// プッシュ通知内容
    /// </summary>
    public class NotificationContent
    {
        /// <summary>
        /// メッセージ
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// タグ
        /// </summary>
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}
