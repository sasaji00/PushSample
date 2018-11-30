using Foundation;
using PushSample.Constants;
using PushSample.Helpers.Implements;
using PushSample.Models.Implements;
using PushSample.Services.Implements;
using PushSample.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using WindowsAzure.Messaging;

namespace PushSample.iOS.Managers.Implements
{
    /// <summary>
    /// NotificationHubを操作するクラス
    /// </summary>
    public sealed class NotificationHubManager
    {
        /// <summary>
        /// インスタンスを生成する
        /// </summary>
        private static readonly NotificationHubManager Instance = new NotificationHubManager();

        /// <summary>
        /// NotificationHub インスタンス
        /// </summary>
        private SBNotificationHub notificationHubInstance;

        /// <summary>
        /// AuthHelperインスタンス
        /// </summary>
        private AuthHelper authHelper;

        /// <summary>
        /// コンストラクタs
        /// </summary>
        private NotificationHubManager()
        {
            IAccountStoreWrapper accountStoreWrapper = new AccountStoreWrapper();
            this.authHelper = new AuthHelper(accountStoreWrapper);
        }

        /// <summary>
        /// インスタンスを取得する
        /// </summary>
        /// <returns>NotificationHubManagerインスタンス</returns>
        public static NotificationHubManager GetInstance()
        {
            return Instance;
        }

        /// <summary>
        /// トークンを保存する
        /// </summary>
        /// <param name="token">Firebaseで発行されたトークン</param>
        public void SaveToken(NSData token)
        {
            this.authHelper.SaveToken(Constant.TokenKey, token.GetBase64EncodedString(NSDataBase64EncodingOptions.None));
        }

        /// <summary>
        /// トークンを取得する
        /// </summary>
        /// <returns>取得したトークン</returns>
        public NSData GetToken()
        {
            var token = this.authHelper.GetToken(Constant.TokenKey);
            return new NSData(token, NSDataBase64DecodingOptions.None);
        }

        /// <summary>
        /// NotificationHubインスタンスを設定する
        /// </summary>
        /// <param name="notificationHubName">NotificationHubインスタンス</param>
        /// <param name="listenConnectionString">接続文字列</param>
        public void SetNotificationHub(string notificationHubName, string listenConnectionString)
        {
            this.notificationHubInstance = new SBNotificationHub(listenConnectionString, notificationHubName);
        }

        /// <summary>
        /// NotificationHubへの登録処理
        /// </summary>
        /// <returns>Registration Id</returns>
        public string Register()
        {
            return this.Register(new List<string>() { });
        }

        /// <summary>
        /// NotificationHubへの登録処理
        /// </summary>
        /// <param name="username">ユーザー名</param>
        /// <returns>Registration Id</returns>
        public string Register(string username)
        {
            var tags = new List<string>() { Constants.Constant.Push.Tags.UserNamePrefix + username };
            return this.Register(tags);
        }

        /// <summary>
        /// NotificationHubへの登録処理
        /// </summary>
        /// <param name="subscribeCategories">カテゴリリスト</param>
        /// <returns>Registration Id</returns>
        public string Register(List<SubscribeCategory> subscribeCategories)
        {
            List<string> subscribeCategoryNames = new List<string>();
            subscribeCategoryNames = subscribeCategories.FindAll(x => x.Flag == true).Select(x => x.Name).Cast<string>().ToList();
            var tags = subscribeCategoryNames;
            return this.Register(tags);
        }

        /// <summary>
        /// NotificationHubへの登録処理
        /// </summary>
        /// <param name="tags">tagリスト</param>
        /// <returns>Registration Id</returns>
        public string Register(List<string> tags)
        {
            this.notificationHubInstance.RegisterTemplateAsyncAsync(
                this.GetToken(),
                Constant.IOS.Aps.RegisteredForRemoteNotifications.Template.TokenName,
                Constant.IOS.Aps.RegisteredForRemoteNotifications.Template.Body,
                string.Empty,
                new NSSet(tags.ToArray()));
            return string.Empty;
        }
    }
}
