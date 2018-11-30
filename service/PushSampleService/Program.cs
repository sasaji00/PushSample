using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PushSampleService
{
    /// <summary>
    /// 起動時の振る舞いを定義するクラス
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 起動時に呼び出されるメソッド
        /// </summary>
        /// <param name="args">オプション</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// アプリの起動
        /// </summary>
        /// <param name="args">オプション</param>
        /// <returns>Webサーバーのオブジェクト</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
