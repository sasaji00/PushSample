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
    /// PushRegisterPage ViewModel
    /// </summary>
    public class PushRegisterPageViewModel : ViewModelBase
    {
        /// <summary>
        /// pageDialogService
        /// </summary>
        private IPageDialogService pageDialogService;

        /// <summary>
        /// subscribeManager
        /// </summary>
        private ISubscribeManager subscribeManager;

        /// <summary>
        /// subscribeUser
        /// </summary>
        private SubscribeUser subscribeUser;

        /// <summary>
        /// カテゴリリスト
        /// </summary>
        private List<SubscribeCategory> subscribeCategories = new List<SubscribeCategory>();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="navigationService">navigationService</param>
        /// <param name="pageDialogService">pageDialogService</param>
        /// <param name="subscribeManager">subscribeManager</param>
        public PushRegisterPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, ISubscribeManager subscribeManager) : base(navigationService)
        {
            this.pageDialogService = pageDialogService;
            this.subscribeManager = subscribeManager;
            this.Title = Message.PageTitle.PushRegisterPage;
            this.ButtonTypes.Value = new string[] { "User", "Category" };

            this.subscribeCategories.Add(new SubscribeCategory("JapaneseFood", "和食", false));
            this.subscribeCategories.Add(new SubscribeCategory("FrenchFood", "フレンチ", false));
            this.subscribeCategories.Add(new SubscribeCategory("ChineseFood", "中華", false));
            this.Categories.Value = this.subscribeCategories;

            this.RegisterCommand.Subscribe(async (x) => await this.RegisterPushAsync(x));
        }

        /// <summary>
        /// プッシュボタン バインディング
        /// </summary>
        public ReactiveCommand RegisterCommand { get; } = new ReactiveCommand();

        /// <summary>
        /// UserName バインディング
        /// </summary>
        public ReactiveProperty<string> UserName { get; } = new ReactiveProperty<string>();

        /// <summary>
        /// カテゴリ
        /// </summary>
        public ReactiveProperty<List<SubscribeCategory>> Categories { get; } = new ReactiveProperty<List<SubscribeCategory>>();

        /// <summary>
        /// ボタンタイプ配列
        /// </summary>
        public ReactiveProperty<string[]> ButtonTypes { get; } = new ReactiveProperty<string[]>();

        /// <summary>
        /// RegisterPushボタンイベント
        /// </summary>
        /// <param name="commandParam">ボタン種類</param>
        /// <returns>Task</returns>
        public async Task RegisterPushAsync(object commandParam)
        {
            // ボタンタイプ
            string buttonType = (string)commandParam;

            try
            {
                if (buttonType.Equals(this.ButtonTypes.Value[0]))
                {
                    this.subscribeUser.Name = this.UserName.Value;
                    this.subscribeManager.Register(this.subscribeUser);
                    await this.pageDialogService.DisplayAlertAsync(Message.Register, Message.Ok, Message.Ok);
                }

                if (buttonType.Equals(this.ButtonTypes.Value[1]))
                {
                    this.subscribeManager.Register(this.Categories.Value);
                    await this.pageDialogService.DisplayAlertAsync(Message.Register, Message.Ok, Message.Ok);
                }
            }
            catch (Exception ex)
            {
                await this.pageDialogService.DisplayAlertAsync(Message.Error, ex.Message, Message.Ok);
            }
        }
    }
}
