######################################################################
# [ファイル名]
#  FuncFile.ps1
#
# [概要]
#  Notification Hubs の環境構築用の関数群。
#  SetNotificationHubs.ps1 で使用する。
#
######################################################################

######################################################################
# [関数名]
#  CheckEnvFile
#
# [概要]
#  環境設定ファイルの必須設定項目(ユーザ変更部分のみ)をチェックする。
#  必須項目が設定されていない場合は、プロセス終了(exit = 1)
#
# [引数]
#  なし
#
######################################################################
function CheckEnvFile {
	# Azure サブスクリプション ID 設定チェック
	if (!$AzureSubscriptionID) {
		Write-Host "##### AzureSubscriptionID is not set in the environment setting file."
		exit 1;
	}

	# リソースグループ名設定チェック
	if (!$ResourceGroupName) {
		Write-Host "##### ResourceGroupName is not set in the environment setting file."
		exit 1;
	}

	# 環境作成場所設定チェック
	if (!$Location) {
		Write-Host "##### Location is not set in the environment setting file."
		exit 1;
	}
	
	# ネームスペース名設定チェック
	if (!$HubNamespace) {
		Write-Host "##### HubNamespace is not set in the environment setting file."
		exit 1;
	}

	# ハブ名設定チェック
	if (!$HubName) {
		Write-Host "##### HubName is not set in the environment setting file."
		exit 1;
	}

	# App Service プラン設定チェック
	if (!$AppServicePlanName) {
		Write-Host "##### AppServicePlanName is not set in the environment setting file."
		exit 1;
	}

	# Web App 名設定チェック
	if (!$WebAppName) {
		Write-Host "##### WebAppName is not set in the environment setting file."
		exit 1;
	}

	Write-Host "##### The check of the environment setting file is completed.";
}

######################################################################
# [関数名]
#  ConnectAzure
#
# [概要]
#  以下の処理を行う。
#  1. Azure の接続確認をする。
#  2. 未接続の場合は、Azure に接続を行い、サブスクリプションIDが有効か
#     チェックする。
#  3. 操作するサブスクリプションを設定する。
#
#  エラーは発生した場合は、プロセス終了(exit = 2)
#
# [注意事項]
#  Azure に未接続の場合は、サインインする旨のメッセージが表示される。
#  表示される認証用URL(https://microsoft.com/devicelogin)にアクセスして、
#  表示されているコードを入力する。
#  これを実施しないと処理が継続されない。
#
# [引数]
#  なし
#
######################################################################
function ConnectAzure {
	try {
		# Azure に接続済みかを確認。未接続の場合は、例外発生
		$FoundSubscription = Get-AzSubscription -SubscriptionId $AzureSubscriptionID -ErrorAction Stop 2>$null;

		Write-Host "##### It is already connected to Azure.";
	} catch {
		try {
			# Azure へ接続
			$ConnectAzureAccount = Connect-AzAccount -ErrorAction Stop 2>$null;

			Write-Host "##### It was connected to Azure.";

			# 接続した Azure に設定ファイルに記載したサブスクリプションIDがあるかをチェック
			$FoundSubscription = Get-AzSubscription -SubscriptionId $AzureSubscriptionID -ErrorAction Stop 2>$null;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
	}

	# Azure 接続情報表示
	$FoundSubscription;

	# ユーザがサブスクリプションを複数持っている場合があるためここで対処
	try {
		# 操作するサブスクリプション ID を設定
		$SetSubscripion = Select-AzSubscription -Subscription $AzureSubscriptionID -ErrorAction Stop 2>$null;

		Write-Host "##### It has set the [$AzureSubscriptionID] subscription.";
	} catch {
		Write-Error($_.Exception);
		exit 2;
	}
}

######################################################################
# [関数名]
#  CreateResourceGroup
#
# [概要]
#  リソースグループを作成する。
#  指定されたリソースグループ名が存在しない場合は、新規に作成する。
#  作成に失敗した場合は、プロセス終了(exit = 2)
#
# [引数]
#  なし
#
######################################################################
function CreateResourceGroup {
	# 既存リソースグループ取得
	$FoundResourceGroup = Get-AzResourceGroup -Name $ResourceGroupName -Location $Location 2>$null;

	# 存在しない場合は、新規作成
	if (!$FoundResourceGroup) {
		try {
			# リソースグループを新規作成
			$FoundResourceGroup = New-AzResourceGroup -Name $ResourceGroupName -Location $Location -ErrorAction Stop;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$ResourceGroupName] resource group was created.";

		# 念のためスリープ
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$ResourceGroupName] resource group already exists.";
	}

	# リソースグループ情報表示
	$FoundResourceGroup;
}

######################################################################
# [関数名]
#  CreateNotificationHubNamespace
#
# [概要]
#  Notificatin Hubs のネームスペースを作成する。
#  指定されたネームスペースが存在しない場合は、新規に作成する。
#  作成に失敗した場合は、プロセス終了(exit = 2)
#
# [引数]
#  なし
#
######################################################################
function CreateNotificationHubNamespace {
	# 既存 Notificatin Hubs のネームスペース取得
	$FoundNamespaces = Get-AzNotificationHubsNamespace -ResourceGroup $ResourceGroupName -Namespace $HubNamespace 2>$null;

	# 存在しない場合は、新規作成
	if (!$FoundNamespaces) {
		try {
			# Notificatin Hubs のネームスペースを新規作成
			$FoundNamespaces = New-AzNotificationHubsNamespace -ResourceGroup $ResourceGroupName -Namespace $HubNamespace -Location $Location -ErrorAction Stop;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$HubNamespace] namespace was created.";

		# 念のためスリープ
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$HubNamespace] namespace already exists.";
	}

	# Notificatin Hubs のネームスペースの情報表示
	$FoundNamespaces;
}

######################################################################
# [関数名]
#  CreateNotificationHub
#
# [概要]
#  Notificatin Hubsを作成する。
#  指定されたハブが存在しない場合は、新規に作成する。
#  作成に失敗した場合は、プロセス終了(exit = 2)
#
# [引数]
#  なし
#
######################################################################
function CreateNotificationHub {
	# 既存Notificaion Hubs取得
	$FoundHub = Get-AzNotificationHub -ResourceGroup $ResourceGroupName -Namespace $HubNamespace -NotificationHub $HubName 2>$null;

	# 存在しない場合は、新規作成
	if (!$FoundHub) {
		try {
			# コマンド入力用のjsonファイル作成
			$HubJsonData | Out-File ($ScriptPath + $HubInputFile);

			# Notificaion Hubsを新規作成
			$FoundHub = New-AzNotificationHub -ResourceGroup $ResourceGroupName -Namespace $HubNamespace -InputFile ($ScriptPath + $HubInputFile) -ErrorAction Stop;

			# 作成したjsonファイルを削除
			Remove-Item ($ScriptPath + $HubInputFile);
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$HubName] notification hub was created.";

		# 念のためスリープ
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$HubName] notification hub already exists.";
	}

	# Notificaion Hubs の情報表示
	$FoundHub;
}

######################################################################
# [関数名]
#  UpdateNotificationHub
#
# [概要]
#  Notificatin HubsにGCM(FCM)、APNsの情報を更新する。
#  指定されたハブが存在しない場合、更新に失敗した場合は、プロセス終了(exit = 2)
#
# [注意事項]
#  Notification Hubs の更新は、 Microsoft Azure Notification Hubs NuGet
#  パッケージに用意されている .NET クライアントを使用。
#
# [引数]
#  なし
#
######################################################################
function UpdateNotificationHub {
	try{
		# [Microsoft.Azure.NotificationHubs.dll] アセンブリをスクリプトに追加
		$Assembly = Get-ChildItem ($ScriptPath + $PackagesFolder) -Include $NotificationHubsDll -Recurse;
		Add-Type -Path $Assembly.FullName;
		Write-Host "##### The [Microsoft.Azure.NotificationHubs.dll] assembly has been successfully added to the script.";

		# 既存 Notificatin Hubs から情報取得
		$HubListKeys = Get-AzNotificationHubListKeys -ResourceGroup $ResourceGroupName -Namespace $HubNamespace -NotificationHub $HubName -AuthorizationRule DefaultFullSharedAccessSignature -ErrorAction Stop;

		if ($HubListKeys) {
			# Notificatin Hubs 更新用の NamespaceManager オブジェクト生成
			Write-Host "##### Creating a NamespaceManager object for the [$HubNamespace] namespace...";

			$NamespaceManager = [Microsoft.Azure.NotificationHubs.NamespaceManager]::CreateFromConnectionString($HubListKeys.PrimaryConnectionString);
			Write-Host "##### NamespaceManager object for the [$HubNamespace] namespace has been successfully created.";

			# Notificatin Hubs の現在情報取得
			$NHDescription = $NamespaceManager.GetNotificationHub($HubName);

			# 事前確認のためNotificatin Hubs の情報表示
			Write-Host "##### Check the notification hub before updating.";
			$NHDescription;
			$NHDescription.GcmCredential;
			$NHDescription.ApnsCredential;

			if ($UpdateGCM) {
				# GCM(FCM) 設定
				$NHDescription.GcmCredential = New-Object -TypeName Microsoft.Azure.NotificationHubs.GcmCredential -ArgumentList $GcmApiKey;
			}

			if ($UpdateAPNs) {
				# APNs 用オブジェクト生成
				$ApnsCredential = New-Object -TypeName Microsoft.Azure.NotificationHubs.ApnsCredential;

				# エンドポイント設定
				$ApnsCredential.Endpoint = $ApnsEndpoint;

				# Apple 証明書要求ファイルを base64 データに変換し、設定
				# Apple 証明書要求ファイルが存在しない場合は、例外発生
				$FileContentBytes = get-content ($ScriptPath + $ApnsCertificateFile) -Encoding Byte;
				$ApnsCredential.ApnsCertificate = [System.Convert]::ToBase64String($FileContentBytes);

				# パスワード設定
				$ApnsCredential.CertificateKey = $ApnsCertificateKey;

				# APNs 設定
				$NHDescription.ApnsCredential = $ApnsCredential;
			}

			# Notificatin Hubs の更新
 			$Result = $NamespaceManager.UpdateNotificationHub($NHDescription);

			Write-Host "##### The [$HubName] notification hub was updated.";

			# 念のためスリープ
			Start-Sleep -s $SleepTime;

			# 事後確認のためNotificatin Hubs の情報表示
			Write-Host "##### Check the updated notification hub.";
			$Result;
			$Result.GcmCredential;
			$Result.ApnsCredential;
		} else {
			Write-Host "##### The [$HubName] notification hub does not exist."
			exit 2;
		}
	} catch {
		Write-Error($_.Exception);
		exit 2;
	}
}

######################################################################
# [関数名]
#  CreateAppServicePlan
#
# [概要]
#  App Service プランをフリープランで作成する。
#  作成に失敗した場合は、プロセス終了(exit = 2)
#
# [引数]
#  なし
#
######################################################################
function CreateAppServicePlan {
	# 既存 App Service プラン取得
	$FoundAppServicePlan = Get-AzAppServicePlan -ResourceGroupName $ResourceGroupName -Name $AppServicePlanName 2>$null;

	# 存在しない場合は、新規作成
	if (!$FoundAppServicePlan) {
		try {
            # App Service プランを新規作成
			$FoundAppServicePlan = New-AzAppServicePlan -ResourceGroupName $ResourceGroupName -Name $AppServicePlanName -Location $Location -Tier Free -ErrorAction Stop;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$AppServicePlanName] App Service Plan was created.";

		# 念のためスリープ
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$AppServicePlanName] App Service Plan already exists.";
	}

	# App Service プランの情報表示
	$FoundAppServicePlan;
}

######################################################################
# [関数名]
#  CreateWebApp
#
# [概要]
#  Web App を作成する。
#  作成に失敗した場合は、プロセス終了(exit = 2)
#
# [引数]
#  なし
#
######################################################################
function CreateWebApp {
	# 既存 Web App取得
	$FoundWebApp = Get-AzWebApp -Name $WebAppName -ResourceGroupName $ResourceGroupName 2>$null;

	# 存在しない場合は、新規作成
	if (!$FoundWebApp) {
		try {
            # Web App を新規作成
			$FoundWebApp = New-AzWebApp -ResourceGroupName $ResourceGroupName -Name $WebAppName -AppServicePlan $AppServicePlanName -Location $Location -ErrorAction Stop;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$WebAppName] Web App was created.";

		# 念のためスリープ
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$WebAppName] Web App already exists.";
	}

	# Web App の情報表示
	$FoundWebApp;
}
