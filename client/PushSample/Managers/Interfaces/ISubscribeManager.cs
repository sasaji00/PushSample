using PushSample.Models.Implements;
using System.Collections.Generic;

namespace PushSample.Managers.Interfaces
{
    /// <summary>
    /// プッシュ通知購読管理クラスインターフェイス
    /// </summary>
    public interface ISubscribeManager
    {
        /// <summary>
        /// 購読登録処理
        /// </summary>
        void Register();

        /// <summary>
        /// 購読登録処理
        /// </summary>
        /// <param name="subscribeUser">登録ユーザー情報</param>
        void Register(SubscribeUser subscribeUser);

        /// <summary>
        /// 購読登録処理
        /// </summary>
        /// <param name="subscribeCategories">登録カテゴリ情報</param>
        void Register(List<SubscribeCategory> subscribeCategories);
    }
}
