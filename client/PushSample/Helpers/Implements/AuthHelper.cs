using System.Linq;
using PushSample.Constants;
using PushSample.Helpers.Interfaces;
using PushSample.Services.Interfaces;
using Xamarin.Auth;

namespace PushSample.Helpers.Implements
{
    /// <summary>
    /// Xamarin.Authのトークンを読み書きするクラス
    /// </summary>
    public class AuthHelper : IAuthHelper
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="accountStoreWrapper">AccountStoreのラッパークラス</param>
        public AuthHelper(IAccountStoreWrapper accountStoreWrapper)
        {
            this.Store = accountStoreWrapper;
        }

        /// <summary>
        /// Xamarin.AuthのAccountStore
        /// </summary>
        private IAccountStoreWrapper Store { get; set; }

        /// <summary>
        /// 現在のTokenを取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>取得したトークン</returns>
        public string GetToken(string key)
        {
            // AccountStoreから取得
            var account = this.Store.FindAccountsForService(Constant.AppName).FirstOrDefault(x => x.Username.Equals(string.Empty));

            // 存在すれば返却
            if (account != null && account.Properties.ContainsKey(key))
            {
                return account.Properties[key];
            }

            // なければnull返却
            return null;
        }

        /// <summary>
        /// トークンを保存する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="token">保存するトークン</param>
        public void SaveToken(string key, string token)
        {
            // Account作成
            Account account = new Account();

            account.Properties.Add(key, token);

            // すでにあるAccountを削除
            this.ClearToken(key);

            // 保存
            this.Store.Save(account, Constant.AppName);
        }

        /// <summary>
        /// AccountStoreのクリア
        /// </summary>
        /// <param name="key">キー</param>
        private void ClearToken(string key)
        {
            var accounts = this.Store.FindAccountsForService(Constant.AppName);
            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    if (account.Properties.ContainsKey(key))
                    {
                        account.Properties.Remove(key);
                    }
                }
            }
        }
    }
}
