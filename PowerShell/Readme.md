# Azure PowerShell を使用してプッシュ通知の実行環境を実現する

## 【概要】
Azure Portal で GUI で作成していたプッシュ通知の環境構築を Azure PowerShell を利用して自動化します。
以下の 6 つの操作を自動化します。
1. リソースグループの作成  
2. Azure Notification Hubs のネームスペースの作成
3. Azure Notification Hubs の作成
4. Azure Notification Hubs に Google(GCM) と APNs の情報を設定
5. App Service プランの作成
6. Web APP の作成

## 【採用技術】
環境構築スクリプトは、以下の技術を採用しています。

- **PowerShell**  
  PowerShell は、.NET 上に構築されたタスクベースのコマンドラインシェルおよびスクリプト言語です。  
  シェルウィンドウ、コマンド解析などのサービスを提供し、OS とプロセスを管理するタスクを迅速に自動できます。  
  Windows 上の PowerShell と、macOS および Linux 上の PowerShell Core という 2 つのバリエーションがあります。

- **Azure PowerShell Az モジュール**  
  Windows PowerShell または PowerShell Core に追加し、Azure サブスクリプションに接続してリソースを管理することができるモジュールです。  
  PowerShell 5.x と PowerShell 6.x で実行できます。

- **Microsoft Azure Notification Hubs NuGet パッケージの .NETクライアント**  
  Azure Notification Hubs の管理はまだ Azure PowerShell の PowerShell コマンドレットに含まれていません。  
  Azure Notification Hubs を構築するには、 Microsoft Azure Notification Hubs NuGet パッケージに用意されている .NET クライアントを使用します。

## 【前提条件】
1. [Azure サブスクリプション](https://azure.microsoft.com/ja-jp/free/)が取得済みであること

2. [PowerShell](https://docs.microsoft.com/ja-jp/powershell/scripting/powershell-scripting?view=powershell-5.1) がインストールされていること  

3. [Azure PowerShell Az モジュール](https://docs.microsoft.com/ja-jp/powershell/azure/overview?view=azurermps-6.13.0) がインストールされていること  
   **※Azure PowerShell AzureRM モジュールでは動作しないので注意**

4. [Firebase Cloud Messaging](https://console.firebase.google.com/) が設定済みであること  
    ※Android でプッシュ通知をしない場合は不要

5. Apple 証明書要求ファイルが作成済みであること  
    ※iOS でプッシュ通知をしない場合は不要

6. プッシュ通知のサンプルアプリをリポジトリのクローン済みであること

## 【スクリプト実行時の注意事項】
1. Microsoft アカウントへのログイン  
   Microsoft アカウントへのログインが求められる場合があります。  
   スクリプトで表示されるメッセージに従ってログイン処理を実行してください。

2. 既存資産の流用  
   以下の対象資産が既存の場合、その資産を流用するため新規作成しません。  
    - リソースグループ
    - Azure Notification Hubs のネームスペース
    - Azure Notification Hubs
    - App Service プラン
    - Web App  

   **Notification Hubs の Google(GCM) とAPNs の接続情報はスクリプトの実行の度に環境設定ファイルに記述された情報で更新されます。ただし、Google(GCM) の設定には制限があります。後述の「4. Google(GCM) の制限」を参照してください。**

3. 対象資産の作成プラン  
   対象資産を新規作成する場合は、フリープランで作成します。  

4. Google(GCM) の制限  
   環境設定ファイルに記述した Google(GCM) のAPI キーは以下の場合のみ更新されます。
    - Azure Notification Hubs が構築済み、かつ手動で Azure Notification Hubs の Google(GCM) の設定を行った状態からスクリプトを実行した場合

## 【フォルダ構成】
各スクリプトを格納する SetNotificationHubs フォルダは以下のように構成されます。

![PowerShellスクリプトフォルダ構成](img/Folder-01.png)

1. packages  
   Microsoft Azure Notification Hubs NuGet パッケージに用意されている .NET クライアントのモジュールを格納したフォルダです。  
   **※削除しないでください。**

2. SetNotificationHubs.ps1  
   Notification Hubs 構築用の実行ファイルです。

3. FuncFile.ps1  
   Notification Hubs 構築用の実行ファイルで使用する関数群を記述したファイルです。

4. EnvFile.ps1  
   上記2ファイルで使用する変数を記述した環境設定用のファイルです。

## 【事前準備】
### 1. 環境設定ファイルの編集する
実行前に、環境設定ファイル (EnvFile.ps1) に必要な情報を記述します。

#### 【必須設定項目】
- **$AzureSubscriptionID**  
  作成済みの Azure のサブスクリプション ID を記述します。  
  ```
  記述例)
  # AzureサブスクリプションID
  $AzureSubscriptionID = "ea4670c5-7969-4207-a3b9-d1ad922a9e54";
  ```

- **$ResourceGroupName**  
  作成するリソースグループ名を記述します。  
  ```
  記述例)
  # リソースグループ名
  $ResourceGroupName = "powershell-pushsample";
  ```

- **$Location**  
  メタデータが保存される場所(地域)を記述します。  
  ```
  記述例）
  # メタデータ保存場所
  $Location = "Japan East";
  ```

- **$HubNamespace**    
  作成する Notification Hubs のネームスペース名を記述します。  
  ```
  記述例）
  # Notification Hubs のネームスペース名
  $HubNamespace = "powershell-pushsample-hub-ns";
  ```

- **$HubName**  
  作成する Notification Hubs 名を記述します。
  ```
  記述例）
  # Notification Hubs のハブ名
  $HubName = "powershell-pushsample-hub";
  ```

- **$AppServicePlanName**  
  作成する App Service プラン名を記述します。
  ```
  記述例）
  # App Service プラン名
  $AppServicePlanName = "powershell-pushsample-appserviceplan";
  ```

- **$WebAppName**  
  作成する Web App 名を記述します。
  ```
  記述例）
  # Web App 名
  $WebAppName = "powershell-pushsample-webapp";
  ```

#### 【任意設定項目】
- Android でプッシュ通知をする場合  
  Android でプッシュ通知をする場合は、以下の項目を記述します。  
  **※プッシュ通知をしない場合は設定不要です。デフォルトの空文字から変更しないでください。**  

  - **$GcmApiKey**  
    Firebase コンソールから取得した[サーバーキー]、または [以前のサーバーキー] を記述します。  
    ただし、$GcmApiKey は以下の場合のみ有効です。
    - Notification Hubs が構築済み、かつ手動で Notification Hubs の Google(GCM) の設定を行った状態でスクリプトを実行した場合  
    ```
    記述例）
    # Google(GCM) の API キー
    $GcmApiKey = "AIzaSyBoPF37AHxdKhUu8n3iIPr9gAdk0mnkHXMa";
    ```

- iOS でプッシュ通知をする場合  
  iOS でプッシュ通知をする場合は、以下の項目を記述します。  
  **※プッシュ通知をしない場合は設定不要です。デフォルトの空文字から変更しないでください。**

  - **$ApnsEndpoint**  
    サンプルアプリの実行環境では、[gateway.sandbox.push.apple.com] を記述します。  
    ```
    記述例）
    # エンドポイント(gateway.push.apple.com か gateway.sandbox.push.apple.comを設定)
    $ApnsEndpoint = "gateway.sandbox.push.apple.com";
    ```

  - **$ApnsCertificateFile**  
    Apple 証明書要求ファイル名を記述します。  
    **※ファイル名の先頭にファイルセパレータの"\\"を付けてください。**
    ```
    記述例）
    # Apple 証明書要求ファイル名
    $ApnsCertificateFile = "\Certificates_aps_development.p12";
    ```

  - **$ApnsCertificateKey**  
    Apple 証明書要求ファイル作成時に設定したパスワードを記述します。  
    ```
    記述例）
    # Apple 証明書要求ファイル作成時のパスワード
    $ApnsCertificateKey = "12345";
    ```

環境設定ファイル (EnvFile.ps1) の記述例です。

![環境設定ファイルの記述例](img/EnvFile-sample-01.png)

### 2. Apple 証明書要求ファイルを格納する
iOS でプッシュ通知をする場合は、[1. 環境設定ファイルの編集] の [$ApnsCertificateFile] で記述したファイルを [SetNotificationHubs] フォルダ直下に格納します。

![Apple証明書の配置例](img/AppleCertificate.png)

### 3. スクリプトを実行できるようにポリシーを変更する
管理者権限で Windows PowerShell を起動し、以下のコマンドを実行します。

![ポリシー変更](img/Change-policy.png)

## 【スクリプトを実行する】
1. Windows PowerShell を起動し、「SetNotificationHubs」フォルダに移動します。  

   ![PowerShellスクリプト実行01](img/PowerShell-exec-01.png)

2. 以下のコマンドを実行します。  
   ※今回は、事前準備の記述例で設定した値を元に環境を構築します。

    ![PowerShellスクリプト実行02](img/PowerShell-exec-02.png)

    実行中に以下のような警告メッセージが表示された場合、Microsoft アカウントへのサインインを行います。  
    ※サインインが完了するまで、処理が中断されます。

   ![PowerShellスクリプト実行03](img/PowerShell-exec-03.png)

   表示されているURL(https://microsoft.com/devicelogin)にアクセスします。

   ![PowerShellスクリプト実行04](img/PowerShell-exec-04.png)

   コード（今回の例では、GAWCQBE7D）を入力し、続行をクリックします。

   ![PowerShellスクリプト実行05](img/PowerShell-exec-05.png)

   Microsoft アカウントの認証画面が表示されますので、ユーザ名、パスワードを入力してサインインします。  
   サインインが成功すると以下のように表示されます。

   ![PowerShellスクリプト実行06](img/PowerShell-exec-06.png)

   サインインが完了すると、Windows PowerShell 画面に接続情報が表示され、処理が続行されます。  

   ![PowerShellスクリプト実行07](img/PowerShell-exec-07.png)

   全ての処理が完了すると、以下の画面が表示されます。  

   ![PowerShellスクリプト実行08](img/PowerShell-exec-08.png)

   スクリプトの実行は以上です。

## 【スクリプト実行後作業】
1. Azure Portal で環境確認  
   Azure Portal にログインし、環境設定ファイルで記述した各資産が作成できているかを確認します。

   - リソースグループ  
     [powershell-pushsample] が作成されていることを確認します。

     ![Azure Portal-01](img/Azure-Portal-01.png)

   - Notification Hub Namespaces  
     [powershell-pushsample-hub-ns] が作成されていることを確認します。

     ![Azure Portal-02](img/Azure-Portal-02.png)

   - Notification Hubs  
     [powershell-pushsample-hub] が作成されていることを確認します。

     ![Azure Portal-03-01](img/Azure-Portal-03-01.png)

     - APNsの設定確認  
       [Apple (APNs)] に以下の項目が設定されていることを確認します。
       - Authentication Mode：Certifatie  
       - Password：●●●● (※パスワードは表示されません)
       - Application Mode：Sandbox

       ![Azure Portal-03-02](img/Azure-Portal-03-02.png)

     - Google(GCM) の設定確認   
       [Google (GCM)] に以下の項目が**設定されていないこと**を確認します。
       - API Key：未設定

       ![Azure Portal-03-03](img/Azure-Portal-03-03.png)

       ただし、Notification Hubs が構築済み、かつ手動で Notification Hubs の Google(GCM) の設定を行った状態でスクリプトを実行した場合は、 API Keyが設定されていることを確認します。

   - App Service プラン
     [powershell-pushsample-appserviceplan] が作成されていることを確認します。

     ![Azure Portal-04](img/Azure-Portal-04.png)

   - App Service
     [powershell-pushsample-webapp] が作成されていることを確認します。

     ![Azure Portal-05](img/Azure-Portal-05.png)

2. Google（GCM）の API Keyを設定  
   [Google (GCM)] をクリックして、 Firebase コンソールから取得した [以前のサーバーキー] を入力し、[Save] をクリックします。  

   ![Azure Portal-06-01](img/Azure-Portal-06-01.png)

   ![Azure Portal-06-02](img/Azure-Portal-06-02.png)

   [以前のサーバーキー] が間違っていたり、不正な値だった場合(例では不当な値[12345]と入力)は、エラーとなります。  
   この場合は、Firebase コンソールで [以前のサーバーキー] を確認して再度実行してください。

   ![Azure Portal-06-03](img/Azure-Portal-06-03.png)


3. フロントエンド、バックエンド資産を修正  
[Xamarin.Forms / .Net Core / Azure Notification Hubs を使用してプッシュ通知を実現する] を参照して、フロントエンド、バックエンド資産を修正します。

    - 自分の環境用にフロントエンド資産の設定を書き換える (iOS プロジェクト)  

    - 自分の環境用にフロントエンド資産の設定を書き換える (Android プロジェクト)

    - 自分の環境用にフロントエンド資産の設定を書き換える (共有プロジェクト)

    - 自分の環境用にバックエンド資産を書き換える

    - バックエンド資産を App Service に配置する

4. プッシュ通知受信の確認  
実機を使用して、プッシュ通知の受信を確認します。

## 【参考サイト】
Microsoft Azure Notification Hubs NuGet パッケージに用意されている .NET クライアントを利用する場合に参考にしたサイトです。

-  [Stack OverFlow：How to configure the APNS.Certificate in the arm template9](https://stackoverflow.com/questions/40842783/how-to-configure-the-apns-certificate-in-the-arm-template)  

-  [PowerShell を使用した Notification Hubs のデプロイと管理](
https://docs.microsoft.com/ja-jp/azure/notification-hubs/notification-hubs-deploy-and-manage-powershell)

Azure PowerShell をインストールし、それを使用して Azure リソースを管理するチュートリアルです。  
ただし、Azure PowerShell AzureRM モジュールを使用しています。

- [PowerShell でスクリプトを使用して Azure タスクを自動化する](https://docs.microsoft.com/ja-jp/learn/modules/automate-azure-tasks-with-powershell/)

## 【付録】
PowerShell 使用時に便利なエディタ、Azure PowerShell コマンドを紹介します。

- PowerShell 用のエディタ (ISE)  
  PowerShell には、開発環境として ISE （あいす）というエディタが標準で用意されています。  
  入力補完やデバッガ機能が存在するので、 PowerShell のスクリプトを書くときは、基本 ISE を使用すると便利です。  
  Windows10なら「ISE」、Windows8.1なら「powershell_ise」でファイル検索して実行すればエディタが起動します。

  [ISE起動例]

  ![ISE-01](img/ISE-01.png)

- リソースグループ名削除コマンド
  環境構築していて作成したものを不要のため削除したい場合があるかと思います。
  Azure Portal の GUI からも削除は可能ですが、対象資産を個別に削除する必要があり若干面倒です。  
  そこで、以下の PowerShell コマンドを実行することで一括削除できます。  
  ただし、指定したリソースグループ名に紐づく全ての資産を削除するので、利用には注意が必要です。
  ```
  Remove-AzResourceGroup -ResourceGroupName "リソースグループ名"
  ```
