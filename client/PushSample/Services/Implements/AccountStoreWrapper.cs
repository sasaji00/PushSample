using PushSample.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace PushSample.Services.Implements
{
    /// <summary>
    /// AccountStore ラッパークラス
    /// </summary>
    public class AccountStoreWrapper : AccountStore, IAccountStoreWrapper
    {
        /// <summary>
        /// AccountStore
        /// </summary>
        private readonly AccountStore accountStore;

        /// <summary>
        /// constructor
        /// </summary>
        public AccountStoreWrapper()
        {
            this.accountStore = AccountStore.Create();
        }

        /// <summary>
        /// Appstore Deleteメソッド
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="serviceId">serviceId</param>
        public override void Delete(Account account, string serviceId)
        {
            this.accountStore.Delete(account, serviceId);
        }

        /// <summary>
        /// Appstore DeleteAsyncメソッド
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="serviceId">serviceId</param>
        /// <returns>Task</returns>
        public override Task DeleteAsync(Account account, string serviceId)
        {
            return this.accountStore.DeleteAsync(account, serviceId);
        }

        /// <summary>
        /// Appstore FindAccountsForService
        /// </summary>
        /// <param name="serviceId">serviceId</param>
        /// <returns>IEnumerable Account</returns>
        public override IEnumerable<Account> FindAccountsForService(string serviceId)
        {
            return this.accountStore.FindAccountsForService(serviceId);
        }

        /// <summary>
        /// Appstore FindAccountsForServiceAsync
        /// </summary>
        /// <param name="serviceId">seriveId</param>
        /// <returns>Task</returns>
        public override Task<List<Account>> FindAccountsForServiceAsync(string serviceId)
        {
            return this.accountStore.FindAccountsForServiceAsync(serviceId);
        }

        /// <summary>
        /// Accountstore Save
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="serviceId">serviceId</param>
        public override void Save(Account account, string serviceId)
        {
            this.accountStore.Save(account, serviceId);
        }

        /// <summary>
        /// Accountstore SaveAsync
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="serviceId">serviceId</param>
        /// <returns>Task</returns>
        public override Task SaveAsync(Account account, string serviceId)
        {
            return this.accountStore.SaveAsync(account, serviceId);
        }
    }
}
