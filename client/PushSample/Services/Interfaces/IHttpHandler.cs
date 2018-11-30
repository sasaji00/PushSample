using System.Net.Http;
using System.Threading.Tasks;

namespace PushSample.Services.Interfaces
{
    /// <summary>
    /// API通信 Interface
    /// </summary>
    public interface IHttpHandler
    {
        /// <summary>
        /// POST通信
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="content">ボディ</param>
        /// <returns>結果</returns>
        Task<HttpResponseMessage> PostAsync(string url, HttpContent content);
    }
}
