using Prism.Navigation;
using Prism.Services;
using PushSample.Constants;
using PushSample.Managers.Interfaces;
using PushSample.Models.Implements;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushSample.ViewModels
{
    /// <summary>
    /// PushManagerPage ViewModel
    /// </summary>
    public class PushManagerPageViewModel : ViewModelBase
    {
        /// <summary>
        /// PushManager
        /// </summary>
        private IPushManager pushManager;

        /// <summary>
        /// PageDialogService
        /// </summary>
        private IPageDialogService pageDialogService;

        /// <summary>
        /// CategoryList
        /// </summary>
        private List<NotificationCategory> notificationCategories = new List<NotificationCategory>();

        /// <summary>
        /// ユーザー
        /// </summary>
        private NotificationUser notificationUser;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="navigationService">NavigationService</param>
        /// <param name="pushManager">PushManager</param>
        /// /// <param name="pageDialogService">PageDialogService</param>
        public PushManagerPageViewModel(INavigationService navigationService, IPushManager pushManager, IPageDialogService pageDialogService) : base(navigationService)
        {
            this.Title = Message.PageTitle.PushManagerPage;
            this.pageDialogService = pageDialogService;
            this.ButtonTypes.Value = new string[] { "All", "User", "Category" };

            this.notificationCategories.Add(new NotificationCategory("JapaneseFood", "和食", false));
            this.notificationCategories.Add(new NotificationCategory("FrenchFood", "フレンチ", false));
            this.notificationCategories.Add(new NotificationCategory("ChineseFood", "中華", false));
            this.Categories.Value = this.notificationCategories;

            // Pushボタンイベント付与
            this.PushCommand.Subscribe(async (x) => await this.SendPush(x));
            this.pushManager = pushManager;
        }

        /// <summary>
        /// プッシュボタン バインディング
        /// </summary>
        public ReactiveCommand PushCommand { get; } = new ReactiveCommand();

        /// <summary>
        /// プッシュ通知結果
        /// </summary>
        public ReactiveProperty<string> Result { get; } = new ReactiveProperty<string>();

        /// <summary>
        /// UserName バインディング
        /// </summary>
        public ReactiveProperty<string> UserName { get; } = new ReactiveProperty<string>();

        /// <summary>
        /// ボタンタイプ配列
        /// </summary>
        public ReactiveProperty<string[]> ButtonTypes { get; } = new ReactiveProperty<string[]>();

        /// <summary>
        /// MessageEntry バインディング
        /// </summary>
        public ReactiveProperty<string> MessageEntry { get; } = new ReactiveProperty<string>();

        /// <summary>
        /// カテゴリー
        /// </summary>
        public ReactiveProperty<List<NotificationCategory>> Categories { get; } = new ReactiveProperty<List<NotificationCategory>>();

        /// <summary>
        /// Pushボタンイベント
        /// </summary>
        /// <param name="commandParam">ボタンタイプ</param>
        /// <returns>Task</returns>
        public async Task SendPush(object commandParam)
        {
            string buttonType = (string)commandParam;

            try
            {
                // 全てのユーザの場合
                if (buttonType.Equals(this.ButtonTypes.Value[0]))
                {
                    this.Result.Value = await this.pushManager.SendAsync(this.MessageEntry.Value);
                    await this.pageDialogService.DisplayAlertAsync(Message.Notification, this.Result.Value, Message.Ok);
                }

                // ユーザ選択の場合
                if (buttonType.Equals(this.ButtonTypes.Value[1]))
                {
                    this.notificationUser.Name = Constant.Push.Tags.UserNamePrefix + this.UserName.Value;
                    this.Result.Value = await this.pushManager.SendAsync(this.MessageEntry.Value, this.notificationUser);
                    await this.pageDialogService.DisplayAlertAsync(Message.Notification, this.Result.Value, Message.Ok);
                }

                // カテゴリ選択の場合
                if (buttonType.Equals(this.ButtonTypes.Value[2]))
                {
                    this.Result.Value = await this.pushManager.SendAsync(this.MessageEntry.Value, this.Categories.Value);
                    await this.pageDialogService.DisplayAlertAsync(Message.Notification, this.Result.Value, Message.Ok);
                }
            }
            catch (Exception ex)
            {
                await this.pageDialogService.DisplayAlertAsync(Message.Error, ex.Message, Message.Ok);
            }
        }
    }
}
