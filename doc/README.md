## PushSample

### これは何
プッシュ通知の送信と受信をおこなうアプリケーションです。

### WebAPIの説明
- AppService の URL
  - https://pushsampleservice.azurewebsites.net/
  - 確認方法: [Azure](http://portal.azure.com) → App Service → PushSampleService → 概要 → URL
1. 全体へのプッシュ通知: /api/SendPush/{通知するMessage}
1. カテゴリ別のプッシュ通知: /api/SendPush/{通知するMessage}?category={カテゴリ}
1. ユーザー別のプッシュ通知:  /api/SendPush/{通知するMessage}?user={ユーザー}

- カテゴリ、ユーザーは複数で、半角カンマで区切る　例）category=和食,フレンチ
- カテゴリとユーザーは同時に送信することはできない
