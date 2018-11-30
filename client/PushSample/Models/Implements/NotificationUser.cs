using PushSample.Models.Interfaces;

namespace PushSample.Models.Implements
{
    /// <summary>
    /// 通知するユーザー情報
    /// </summary>
    public struct NotificationUser : INotificationDataModel
    {
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }
    }
}
