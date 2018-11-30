######################################################################
# [�t�@�C����]
#  SetNotificationHubs.ps1
#
# [�T�v]
#  Notification Hubs �̎��s���쐬 PowerShell�X�N���v�g
#  �ȉ������ԂɎ��s
#   1.���\�[�X�O���[�v�쐬
#   2.Notification Hubs �̃l�[���X�y�[�X�쐬
#   3.Notification Hubs �쐬
#   4.Notification Hubs ��Google(GCM)�AAPNs�̏���ݒ�
#   5.App Service �v�����쐬
#   6.Web App �쐬
#
# [����]
#  �Ȃ�
# [�I���l]
#   0 : ����I��
#   1 : ���ݒ�t�@�C���G���[
#   2 : �V�X�e���G���[
# 
# [���s��]
#  > .\SetNotificationHubs.ps1
#
######################################################################

######################################################################
# ���ݒ�
######################################################################
# �J�����g�t�H���_�p�X
$ScriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path;

# ���ݒ�t�@�C����
$EnvFile = "\EnvFile.ps1";

# �֐���`�t�@�C����
$FuncFile = "\FuncFile.ps1";

######################################################################
# Main����
######################################################################
Write-Host "######################################################################";
Write-Host "##### Start building environment of Notification Hubs.";

# ���ݒ�t�@�C���ǂݍ���
. ($ScriptPath + $EnvFile);

# �֐���`�t�@�C���ǂݍ���
. ($ScriptPath + $FuncFile);

# ���ݒ�t�@�C���`�F�b�N
Write-Host "######################################################################";
CheckEnvFile;

# GCM(FCM) �ݒ�`�F�b�N
$UpdateGCM = $false;
if ($GcmApiKey) {
	$UpdateGCM = $true;
}

# APNs �ݒ�`�F�b�N
$UpdateAPNs = $false;
if ($ApnsEndpoint -and $ApnsCertificateFile -and $ApnsCertificateKey ) {
	$UpdateAPNs = $true;
}

# Azure �ڑ�
Write-Host "######################################################################";
ConnectAzure;

######################################################################
# Notification Hubs �̊��\�z
######################################################################
# 1. ���\�[�X�O���[�v�쐬
Write-Host "######################################################################";
CreateResourceGroup;

# 2. Notification Hubs �̃l�[���X�y�[�X�쐬
Write-Host "######################################################################";
CreateNotificationHubNamespace;

# 3. Notification Hubs �쐬
Write-Host "######################################################################";
CreateNotificationHub;

# 4. Notification Hubs �� GCM(FCM)�AAPNs �̏���ݒ�
if ($UpdateGCM -or $UpdateAPNs) {
	Write-Host "######################################################################";
	UpdateNotificationHub;
}

# 5. App Service �v�����쐬
Write-Host "######################################################################";
CreateAppServicePlan;

# 6. Web App �쐬
Write-Host "######################################################################";
CreateWebApp;

Write-Host "##### Notification Hubs environment has been completed.";

exit 0;
