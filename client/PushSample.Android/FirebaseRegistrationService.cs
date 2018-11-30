using Android.App;
using Firebase.Iid;
using PushSample.Constants;
using PushSample.Helpers.Implements;
using PushSample.Helpers.Interfaces;
using PushSample.Services.Implements;

namespace PushSample.Droid
{
    /// <summary>
    /// Firebase registration service.
    /// </summary>
    [Service]
    [IntentFilter(new[] { Constant.Android.Intent.InstanceIdEvent })]
    public class FirebaseRegistrationService : FirebaseInstanceIdService
    {
        /// <summary>
        /// authHelper
        /// </summary>
        private IAuthHelper authHelper;

        /// <summary>
        /// 起動時にトークンを発行
        /// </summary>
        public override void OnTokenRefresh()
        {
            this.authHelper = new AuthHelper(new AccountStoreWrapper());
            this.authHelper.SaveToken(Constant.TokenKey, FirebaseInstanceId.Instance.Token);
        }
    }
}
