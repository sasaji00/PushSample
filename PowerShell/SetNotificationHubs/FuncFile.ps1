######################################################################
# [�t�@�C����]
#  FuncFile.ps1
#
# [�T�v]
#  Notification Hubs �̊��\�z�p�̊֐��Q�B
#  SetNotificationHubs.ps1 �Ŏg�p����B
#
######################################################################

######################################################################
# [�֐���]
#  CheckEnvFile
#
# [�T�v]
#  ���ݒ�t�@�C���̕K�{�ݒ荀��(���[�U�ύX�����̂�)���`�F�b�N����B
#  �K�{���ڂ��ݒ肳��Ă��Ȃ��ꍇ�́A�v���Z�X�I��(exit = 1)
#
# [����]
#  �Ȃ�
#
######################################################################
function CheckEnvFile {
	# Azure �T�u�X�N���v�V���� ID �ݒ�`�F�b�N
	if (!$AzureSubscriptionID) {
		Write-Host "##### AzureSubscriptionID is not set in the environment setting file."
		exit 1;
	}

	# ���\�[�X�O���[�v���ݒ�`�F�b�N
	if (!$ResourceGroupName) {
		Write-Host "##### ResourceGroupName is not set in the environment setting file."
		exit 1;
	}

	# ���쐬�ꏊ�ݒ�`�F�b�N
	if (!$Location) {
		Write-Host "##### Location is not set in the environment setting file."
		exit 1;
	}
	
	# �l�[���X�y�[�X���ݒ�`�F�b�N
	if (!$HubNamespace) {
		Write-Host "##### HubNamespace is not set in the environment setting file."
		exit 1;
	}

	# �n�u���ݒ�`�F�b�N
	if (!$HubName) {
		Write-Host "##### HubName is not set in the environment setting file."
		exit 1;
	}

	# App Service �v�����ݒ�`�F�b�N
	if (!$AppServicePlanName) {
		Write-Host "##### AppServicePlanName is not set in the environment setting file."
		exit 1;
	}

	# Web App ���ݒ�`�F�b�N
	if (!$WebAppName) {
		Write-Host "##### WebAppName is not set in the environment setting file."
		exit 1;
	}

	Write-Host "##### The check of the environment setting file is completed.";
}

######################################################################
# [�֐���]
#  ConnectAzure
#
# [�T�v]
#  �ȉ��̏������s���B
#  1. Azure �̐ڑ��m�F������B
#  2. ���ڑ��̏ꍇ�́AAzure �ɐڑ����s���A�T�u�X�N���v�V����ID���L����
#     �`�F�b�N����B
#  3. ���삷��T�u�X�N���v�V������ݒ肷��B
#
#  �G���[�͔��������ꍇ�́A�v���Z�X�I��(exit = 2)
#
# [���ӎ���]
#  Azure �ɖ��ڑ��̏ꍇ�́A�T�C���C������|�̃��b�Z�[�W���\�������B
#  �\�������F�ؗpURL(https://microsoft.com/devicelogin)�ɃA�N�Z�X���āA
#  �\������Ă���R�[�h����͂���B
#  ��������{���Ȃ��Ə������p������Ȃ��B
#
# [����]
#  �Ȃ�
#
######################################################################
function ConnectAzure {
	try {
		# Azure �ɐڑ��ς݂����m�F�B���ڑ��̏ꍇ�́A��O����
		$FoundSubscription = Get-AzSubscription -SubscriptionId $AzureSubscriptionID -ErrorAction Stop 2>$null;

		Write-Host "##### It is already connected to Azure.";
	} catch {
		try {
			# Azure �֐ڑ�
			$ConnectAzureAccount = Connect-AzAccount -ErrorAction Stop 2>$null;

			Write-Host "##### It was connected to Azure.";

			# �ڑ����� Azure �ɐݒ�t�@�C���ɋL�ڂ����T�u�X�N���v�V����ID�����邩���`�F�b�N
			$FoundSubscription = Get-AzSubscription -SubscriptionId $AzureSubscriptionID -ErrorAction Stop 2>$null;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
	}

	# Azure �ڑ����\��
	$FoundSubscription;

	# ���[�U���T�u�X�N���v�V�����𕡐������Ă���ꍇ�����邽�߂����őΏ�
	try {
		# ���삷��T�u�X�N���v�V���� ID ��ݒ�
		$SetSubscripion = Select-AzSubscription -Subscription $AzureSubscriptionID -ErrorAction Stop 2>$null;

		Write-Host "##### It has set the [$AzureSubscriptionID] subscription.";
	} catch {
		Write-Error($_.Exception);
		exit 2;
	}
}

######################################################################
# [�֐���]
#  CreateResourceGroup
#
# [�T�v]
#  ���\�[�X�O���[�v���쐬����B
#  �w�肳�ꂽ���\�[�X�O���[�v�������݂��Ȃ��ꍇ�́A�V�K�ɍ쐬����B
#  �쐬�Ɏ��s�����ꍇ�́A�v���Z�X�I��(exit = 2)
#
# [����]
#  �Ȃ�
#
######################################################################
function CreateResourceGroup {
	# �������\�[�X�O���[�v�擾
	$FoundResourceGroup = Get-AzResourceGroup -Name $ResourceGroupName -Location $Location 2>$null;

	# ���݂��Ȃ��ꍇ�́A�V�K�쐬
	if (!$FoundResourceGroup) {
		try {
			# ���\�[�X�O���[�v��V�K�쐬
			$FoundResourceGroup = New-AzResourceGroup -Name $ResourceGroupName -Location $Location -ErrorAction Stop;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$ResourceGroupName] resource group was created.";

		# �O�̂��߃X���[�v
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$ResourceGroupName] resource group already exists.";
	}

	# ���\�[�X�O���[�v���\��
	$FoundResourceGroup;
}

######################################################################
# [�֐���]
#  CreateNotificationHubNamespace
#
# [�T�v]
#  Notificatin Hubs �̃l�[���X�y�[�X���쐬����B
#  �w�肳�ꂽ�l�[���X�y�[�X�����݂��Ȃ��ꍇ�́A�V�K�ɍ쐬����B
#  �쐬�Ɏ��s�����ꍇ�́A�v���Z�X�I��(exit = 2)
#
# [����]
#  �Ȃ�
#
######################################################################
function CreateNotificationHubNamespace {
	# ���� Notificatin Hubs �̃l�[���X�y�[�X�擾
	$FoundNamespaces = Get-AzNotificationHubsNamespace -ResourceGroup $ResourceGroupName -Namespace $HubNamespace 2>$null;

	# ���݂��Ȃ��ꍇ�́A�V�K�쐬
	if (!$FoundNamespaces) {
		try {
			# Notificatin Hubs �̃l�[���X�y�[�X��V�K�쐬
			$FoundNamespaces = New-AzNotificationHubsNamespace -ResourceGroup $ResourceGroupName -Namespace $HubNamespace -Location $Location -ErrorAction Stop;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$HubNamespace] namespace was created.";

		# �O�̂��߃X���[�v
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$HubNamespace] namespace already exists.";
	}

	# Notificatin Hubs �̃l�[���X�y�[�X�̏��\��
	$FoundNamespaces;
}

######################################################################
# [�֐���]
#  CreateNotificationHub
#
# [�T�v]
#  Notificatin Hubs���쐬����B
#  �w�肳�ꂽ�n�u�����݂��Ȃ��ꍇ�́A�V�K�ɍ쐬����B
#  �쐬�Ɏ��s�����ꍇ�́A�v���Z�X�I��(exit = 2)
#
# [����]
#  �Ȃ�
#
######################################################################
function CreateNotificationHub {
	# ����Notificaion Hubs�擾
	$FoundHub = Get-AzNotificationHub -ResourceGroup $ResourceGroupName -Namespace $HubNamespace -NotificationHub $HubName 2>$null;

	# ���݂��Ȃ��ꍇ�́A�V�K�쐬
	if (!$FoundHub) {
		try {
			# �R�}���h���͗p��json�t�@�C���쐬
			$HubJsonData | Out-File ($ScriptPath + $HubInputFile);

			# Notificaion Hubs��V�K�쐬
			$FoundHub = New-AzNotificationHub -ResourceGroup $ResourceGroupName -Namespace $HubNamespace -InputFile ($ScriptPath + $HubInputFile) -ErrorAction Stop;

			# �쐬����json�t�@�C�����폜
			Remove-Item ($ScriptPath + $HubInputFile);
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$HubName] notification hub was created.";

		# �O�̂��߃X���[�v
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$HubName] notification hub already exists.";
	}

	# Notificaion Hubs �̏��\��
	$FoundHub;
}

######################################################################
# [�֐���]
#  UpdateNotificationHub
#
# [�T�v]
#  Notificatin Hubs��GCM(FCM)�AAPNs�̏����X�V����B
#  �w�肳�ꂽ�n�u�����݂��Ȃ��ꍇ�A�X�V�Ɏ��s�����ꍇ�́A�v���Z�X�I��(exit = 2)
#
# [���ӎ���]
#  Notification Hubs �̍X�V�́A Microsoft Azure Notification Hubs NuGet
#  �p�b�P�[�W�ɗp�ӂ���Ă��� .NET �N���C�A���g���g�p�B
#
# [����]
#  �Ȃ�
#
######################################################################
function UpdateNotificationHub {
	try{
		# [Microsoft.Azure.NotificationHubs.dll] �A�Z���u�����X�N���v�g�ɒǉ�
		$Assembly = Get-ChildItem ($ScriptPath + $PackagesFolder) -Include $NotificationHubsDll -Recurse;
		Add-Type -Path $Assembly.FullName;
		Write-Host "##### The [Microsoft.Azure.NotificationHubs.dll] assembly has been successfully added to the script.";

		# ���� Notificatin Hubs ������擾
		$HubListKeys = Get-AzNotificationHubListKeys -ResourceGroup $ResourceGroupName -Namespace $HubNamespace -NotificationHub $HubName -AuthorizationRule DefaultFullSharedAccessSignature -ErrorAction Stop;

		if ($HubListKeys) {
			# Notificatin Hubs �X�V�p�� NamespaceManager �I�u�W�F�N�g����
			Write-Host "##### Creating a NamespaceManager object for the [$HubNamespace] namespace...";

			$NamespaceManager = [Microsoft.Azure.NotificationHubs.NamespaceManager]::CreateFromConnectionString($HubListKeys.PrimaryConnectionString);
			Write-Host "##### NamespaceManager object for the [$HubNamespace] namespace has been successfully created.";

			# Notificatin Hubs �̌��ݏ��擾
			$NHDescription = $NamespaceManager.GetNotificationHub($HubName);

			# ���O�m�F�̂���Notificatin Hubs �̏��\��
			Write-Host "##### Check the notification hub before updating.";
			$NHDescription;
			$NHDescription.GcmCredential;
			$NHDescription.ApnsCredential;

			if ($UpdateGCM) {
				# GCM(FCM) �ݒ�
				$NHDescription.GcmCredential = New-Object -TypeName Microsoft.Azure.NotificationHubs.GcmCredential -ArgumentList $GcmApiKey;
			}

			if ($UpdateAPNs) {
				# APNs �p�I�u�W�F�N�g����
				$ApnsCredential = New-Object -TypeName Microsoft.Azure.NotificationHubs.ApnsCredential;

				# �G���h�|�C���g�ݒ�
				$ApnsCredential.Endpoint = $ApnsEndpoint;

				# Apple �ؖ����v���t�@�C���� base64 �f�[�^�ɕϊ����A�ݒ�
				# Apple �ؖ����v���t�@�C�������݂��Ȃ��ꍇ�́A��O����
				$FileContentBytes = get-content ($ScriptPath + $ApnsCertificateFile) -Encoding Byte;
				$ApnsCredential.ApnsCertificate = [System.Convert]::ToBase64String($FileContentBytes);

				# �p�X���[�h�ݒ�
				$ApnsCredential.CertificateKey = $ApnsCertificateKey;

				# APNs �ݒ�
				$NHDescription.ApnsCredential = $ApnsCredential;
			}

			# Notificatin Hubs �̍X�V
 			$Result = $NamespaceManager.UpdateNotificationHub($NHDescription);

			Write-Host "##### The [$HubName] notification hub was updated.";

			# �O�̂��߃X���[�v
			Start-Sleep -s $SleepTime;

			# ����m�F�̂���Notificatin Hubs �̏��\��
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
# [�֐���]
#  CreateAppServicePlan
#
# [�T�v]
#  App Service �v�������t���[�v�����ō쐬����B
#  �쐬�Ɏ��s�����ꍇ�́A�v���Z�X�I��(exit = 2)
#
# [����]
#  �Ȃ�
#
######################################################################
function CreateAppServicePlan {
	# ���� App Service �v�����擾
	$FoundAppServicePlan = Get-AzAppServicePlan -ResourceGroupName $ResourceGroupName -Name $AppServicePlanName 2>$null;

	# ���݂��Ȃ��ꍇ�́A�V�K�쐬
	if (!$FoundAppServicePlan) {
		try {
            # App Service �v������V�K�쐬
			$FoundAppServicePlan = New-AzAppServicePlan -ResourceGroupName $ResourceGroupName -Name $AppServicePlanName -Location $Location -Tier Free -ErrorAction Stop;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$AppServicePlanName] App Service Plan was created.";

		# �O�̂��߃X���[�v
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$AppServicePlanName] App Service Plan already exists.";
	}

	# App Service �v�����̏��\��
	$FoundAppServicePlan;
}

######################################################################
# [�֐���]
#  CreateWebApp
#
# [�T�v]
#  Web App ���쐬����B
#  �쐬�Ɏ��s�����ꍇ�́A�v���Z�X�I��(exit = 2)
#
# [����]
#  �Ȃ�
#
######################################################################
function CreateWebApp {
	# ���� Web App�擾
	$FoundWebApp = Get-AzWebApp -Name $WebAppName -ResourceGroupName $ResourceGroupName 2>$null;

	# ���݂��Ȃ��ꍇ�́A�V�K�쐬
	if (!$FoundWebApp) {
		try {
            # Web App ��V�K�쐬
			$FoundWebApp = New-AzWebApp -ResourceGroupName $ResourceGroupName -Name $WebAppName -AppServicePlan $AppServicePlanName -Location $Location -ErrorAction Stop;
		} catch {
			Write-Error($_.Exception);
			exit 2;
		}
		Write-Host "##### The [$WebAppName] Web App was created.";

		# �O�̂��߃X���[�v
		Start-Sleep -s $SleepTime;
	} else {
		Write-Host "##### The [$WebAppName] Web App already exists.";
	}

	# Web App �̏��\��
	$FoundWebApp;
}
