using PushSample.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace PushSample.Services.Implements
{
    /// <summary>
    /// API通信
    /// </summary>
    public class HttpHandler : IHttpHandler
    {
        /// <summary>
        /// HttpClient
        /// </summary>
        private static HttpClient httpClient = new HttpClient();

        /// <summary>
        /// POST通信
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="content">ボディ</param>
        /// <returns>結果</returns>
        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await httpClient.PostAsync(url, content);
        }
    }
}
