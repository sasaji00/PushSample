namespace PushSample.Helpers.Interfaces
{
    /// <summary>
    /// トークンを読み書きするインターフェイス
    /// </summary>
    public interface IAuthHelper
    {
        /// <summary>
        /// 現在のTokenを取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>取得したトークン</returns>
        string GetToken(string key);

        /// <summary>
        /// トークンを保存する
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="token">保存するトークン</param>
        void SaveToken(string key, string token);
    }
}
