using PushSample.Constants;
using PushSample.Helpers.Interfaces;
using PushSample.Managers.Interfaces;
using PushSample.Models.Implements;
using System.Collections.Generic;
using System.Linq;

namespace PushSample.Managers.Implements
{
    /// <summary>
    /// プッシュ通知購読管理クラス
    /// </summary>
    public class SubscribeManager : ISubscribeManager
    {
        /// <summary>
        /// authHelper
        /// </summary>
        private readonly IAuthHelper authHelper;

        /// <summary>
        /// NotificationHubManager
        /// </summary>
        private readonly INotificationHubManager notificationHubManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="authHelper">authHelper</param>
        public SubscribeManager(IAuthHelper authHelper)
        {
            this.authHelper = authHelper;
            this.notificationHubManager = NotificationHubManager.GetInstance();
        }

        /// <summary>
        /// 購読登録処理
        /// </summary>
        public void Register()
        {
            var pnsHandle = this.authHelper.GetToken(Constant.TokenKey);
            this.notificationHubManager.Register(pnsHandle);
        }

        /// <summary>
        /// 購読登録処理
        /// </summary>
        /// <param name="subscribeUser">登録ユーザー情報</param>
        public void Register(SubscribeUser subscribeUser)
        {
            var tags = new List<string>() { Constant.Push.Tags.UserNamePrefix + subscribeUser.Name };
            var pnsHandle = this.authHelper.GetToken(Constant.TokenKey);
            this.notificationHubManager.Register(pnsHandle, tags);
        }

        /// <summary>
        /// 購読登録処理
        /// </summary>
        /// <param name="subscribeCategories">登録カテゴリ情報</param>
        public void Register(List<SubscribeCategory> subscribeCategories)
        {
            var subscribeCategoryNames = subscribeCategories.FindAll(x => x.Flag == true).Select(x => x.Name).Cast<string>().ToList();
            var pnsHandle = this.authHelper.GetToken(Constant.TokenKey);
            this.notificationHubManager.Register(pnsHandle, subscribeCategoryNames);
        }
    }
}