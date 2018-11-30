using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace PushSample.Services.Interfaces
{
    /// <summary>
    /// AccountStoreWrapper Interface
    /// </summary>
    public interface IAccountStoreWrapper
    {
        /// <summary>
        /// Appstore Deleteメソッド
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="serviceId">serviceId</param>
        void Delete(Account account, string serviceId);

        /// <summary>
        /// Appstore DeleteAsyncメソッド
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="serviceId">serviceId</param>
        /// <returns>Task</returns>
        Task DeleteAsync(Account account, string serviceId);

        /// <summary>
        /// Appstore FindAccountsForService
        /// </summary>
        /// <param name="serviceId">serviceId</param>
        /// <returns>IEnumerable Account</returns>
        IEnumerable<Account> FindAccountsForService(string serviceId);

        /// <summary>
        /// Appstore FindAccountsForServiceAsync
        /// </summary>
        /// <param name="serviceId">seriveId</param>
        /// <returns>Task</returns>
        Task<List<Account>> FindAccountsForServiceAsync(string serviceId);

        /// <summary>
        /// Accountstore Save
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="serviceId">serviceId</param>
        void Save(Account account, string serviceId);

        /// <summary>
        /// Accountstore SaveAsync
        /// </summary>
        /// <param name="account">account</param>
        /// <param name="serviceId">serviceId</param>
        /// <returns>Task</returns>
        Task SaveAsync(Account account, string serviceId);
    }
}
