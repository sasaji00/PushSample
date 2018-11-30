######################################################################
# [ファイル名]
#  SetNotificationHubs.ps1
#
# [概要]
#  Notification Hubs の実行環境作成 PowerShellスクリプト
#  以下を順番に実行
#   1.リソースグループ作成
#   2.Notification Hubs のネームスペース作成
#   3.Notification Hubs 作成
#   4.Notification Hubs にGoogle(GCM)、APNsの情報を設定
#   5.App Service プラン作成
#   6.Web App 作成
#
# [引数]
#  なし
# [終了値]
#   0 : 正常終了
#   1 : 環境設定ファイルエラー
#   2 : システムエラー
# 
# [実行例]
#  > .\SetNotificationHubs.ps1
#
######################################################################

######################################################################
# 環境設定
######################################################################
# カレントフォルダパス
$ScriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path;

# 環境設定ファイル名
$EnvFile = "\EnvFile.ps1";

# 関数定義ファイル名
$FuncFile = "\FuncFile.ps1";

######################################################################
# Main処理
######################################################################
Write-Host "######################################################################";
Write-Host "##### Start building environment of Notification Hubs.";

# 環境設定ファイル読み込み
. ($ScriptPath + $EnvFile);

# 関数定義ファイル読み込み
. ($ScriptPath + $FuncFile);

# 環境設定ファイルチェック
Write-Host "######################################################################";
CheckEnvFile;

# GCM(FCM) 設定チェック
$UpdateGCM = $false;
if ($GcmApiKey) {
	$UpdateGCM = $true;
}

# APNs 設定チェック
$UpdateAPNs = $false;
if ($ApnsEndpoint -and $ApnsCertificateFile -and $ApnsCertificateKey ) {
	$UpdateAPNs = $true;
}

# Azure 接続
Write-Host "######################################################################";
ConnectAzure;

######################################################################
# Notification Hubs の環境構築
######################################################################
# 1. リソースグループ作成
Write-Host "######################################################################";
CreateResourceGroup;

# 2. Notification Hubs のネームスペース作成
Write-Host "######################################################################";
CreateNotificationHubNamespace;

# 3. Notification Hubs 作成
Write-Host "######################################################################";
CreateNotificationHub;

# 4. Notification Hubs に GCM(FCM)、APNs の情報を設定
if ($UpdateGCM -or $UpdateAPNs) {
	Write-Host "######################################################################";
	UpdateNotificationHub;
}

# 5. App Service プラン作成
Write-Host "######################################################################";
CreateAppServicePlan;

# 6. Web App 作成
Write-Host "######################################################################";
CreateWebApp;

Write-Host "##### Notification Hubs environment has been completed.";

exit 0;
