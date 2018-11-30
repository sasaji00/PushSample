namespace PushSample.Models.Implements
{
    /// <summary>
    /// NotificationHubに設定するユーザが選択したカテゴリ情報
    /// </summary>
    public class SubscribeCategory
    {
        /// <summary>
        /// カテゴリと選択状況
        /// </summary>
        /// /// <param name="name">カテゴリ名</param>
        /// <param name="showName">表示用カテゴリ名</param>
        /// <param name="flag">選択状況</param>
        public SubscribeCategory(string name, string showName, bool flag)
        {
            this.Name = name;
            this.ShowName = showName;
            this.Flag = flag;
        }

        /// <summary>
        /// カテゴリ名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表示用カテゴリ名
        /// </summary>
        public string ShowName { get; set; }

        /// <summary>
        /// 選択状況
        /// </summary>
        public bool Flag { get; set; }
    }
}
