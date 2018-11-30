namespace PushSample.Constants
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class Constant
    {
        /// <summary>
        /// アプリ名
        /// </summary>
        public const string AppName = "PushSample";

        /// <summary>
        /// ユーザー名
        /// </summary>
        public const string UserName = "SampleUser";

        /// <summary>
        /// token取得キー
        /// </summary>
        public const string TokenKey = "DeviceToken";

        /// <summary>
        /// ページ名
        /// </summary>
        public static class PageName
        {
            /// <summary>
            /// 通知管理画面名 タイトル
            /// </summary>
            public const string PushManagerPage = "PushManagerPage";

            /// <summary>
            /// 通知登録画面名 タイトル
            /// </summary>
            public const string PushRegisterPage = "PushRegisterPage";
        }

        /// <summary>
        /// プッシュ通知
        /// </summary>
        public static class Push
        {
            /// <summary>
            /// ConnectionString
            /// </summary>
            public const string ListenConnectionString = "Endpoint=sb://pushsampleservice-hub-dev-ns.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=TGjunfr2dZc+owu61V/r4GlktKnKdv0OQ6i0BHiFqUM=";

            /// <summary>
            /// HubName
            /// </summary>
            public const string NotificationHubName = "pushsampleservice-hub-dev";

            /// <summary>
            /// tags
            /// </summary>
            public static class Tags
            {
                /// <summary>
                /// ユーザー名登録時の接頭辞
                /// </summary>
                public const string UserNamePrefix = "username:";
            }
        }

        /// <summary>
        /// API Path
        /// </summary>
        public static class Api
        {
            /// <summary>
            /// プッシュ通知送信APIのURL
            /// </summary>
            public const string NotificationUrl = "https://pushsampleservice.azurewebsites.net/api/notification";
        }

        /// <summary>
        /// HTTP通信
        /// </summary>
        public static class Http
        {
            /// <summary>
            /// コンテントタイプ
            /// </summary>
            public const string ContentType = "application/json";
        }

        /// <summary>
        /// Android用定数クラス
        /// </summary>
        public static class Android
        {
            /// <summary>
            /// Activity設定クラス
            /// </summary>
            public static class Activity
            {
                /// <summary>
                /// Label
                /// </summary>
                public const string Label = "PushSample";

                /// <summary>
                /// Icon
                /// </summary>
                public const string Icon = "@drawable/icon";

                /// <summary>
                /// StyleTheame
                /// </summary>
                public const string Theme = "@style/MainTheme";
            }

            /// <summary>
            /// インテント名定数クラス
            /// </summary>
            public static class Intent
            {
                /// <summary>
                /// プッシュ通知取得インテント
                /// </summary>
                public const string MessageEvent = "com.google.firebase.MESSAGING_EVENT";

                /// <summary>
                /// インスタンスID発行インテント
                /// </summary>
                public const string InstanceIdEvent = "com.google.firebase.INSTANCE_ID_EVENT";
            }

            /// <summary>
            /// プッシュ通知取得定数クラス
            /// </summary>
            public static class FireBase
            {
                /// <summary>
                /// 通知を取得する際の型
                /// </summary>
                public static class RemoteMessage
                {
                    /// <summary>
                    /// 取得する際の文字列
                    /// </summary>
                    public static class Key
                    {
                        /// <summary>
                        /// プッシュ通知メッセージ取得用文字列
                        /// </summary>
                        public const string Message = "message";

                        /// <summary>
                        /// チャンネルID
                        /// </summary>
                        public const string ChannelId = "PushChannelId";
                    }
                }

                /// <summary>
                /// 通知用定数クラス
                /// </summary>
                public static class SendRegistrationToServer
                {
                    /// <summary>
                    /// プッシュ通知作成キー
                    /// </summary>
                    public const string Key = "genericMessage";

                    /// <summary>
                    /// プッシュ通知テンプレート
                    /// </summary>
                    public static class Template
                    {
                        /// <summary>
                        /// プッシュ通知テンプレート作成キー
                        /// </summary>
                        public static class Key
                        {
                            /// <summary>
                            /// プッシュ通知メッセージテンプレートキー
                            /// </summary>
                            public const string Body = "body";
                        }

                        /// <summary>
                        /// プッシュ通知テンプレート作成値
                        /// </summary>
                        public static class Value
                        {
                            /// <summary>
                            /// プッシュ通知メッセージテンプレート
                            /// </summary>
                            public const string Body = "{\"data\":{\"message\":\"$(messageParam)\"}}";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// iOS用定数クラス
        /// </summary>
        public static class IOS
        {
            /// <summary>
            /// Register設定用定数クラス
            /// </summary>
            public static class Register
            {
                /// <summary>
                /// AppDelegate
                /// </summary>
                public const string AppDelegate = "AppDelegate";
            }

            /// <summary>
            /// プッシュ通知用定数クラス
            /// </summary>
            public static class Aps
            {
                /// <summary>
                /// APS辞書キー
                /// </summary>
                public const string Key = "aps";

                /// <summary>
                /// プッシュ通知登録用定数クラス
                /// </summary>
                public static class RegisteredForRemoteNotifications
                {
                    /// <summary>
                    /// ハブ登録用テンプレート
                    /// </summary>
                    public static class Template
                    {
                        /// <summary>
                        /// トークン名
                        /// </summary>
                        public const string TokenName = "simpleAPNSTemplate";

                        /// <summary>
                        /// 登録テンプレート
                        /// </summary>
                        public const string Body = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";
                    }
                }

                /// <summary>
                /// APS格納コンテンツ
                /// </summary>
                public static class Content
                {
                    /// <summary>
                    /// APS格納キー
                    /// </summary>
                    public static class Key
                    {
                        /// <summary>
                        /// アラート
                        /// </summary>
                        public const string Alert = "alert";
                    }
                }
            }
        }
    }
}