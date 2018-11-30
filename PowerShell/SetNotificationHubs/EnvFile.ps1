######################################################################
# [�t�@�C����]
#  EnvFile.ps1
#
# [�T�v]
#  Notification Hubs �̊��\�z�p�̊��ݒ�t�@�C���B
#  SetNotificationHubs.ps1 �Ŏg�p����B
#
# [�֎~����]
#  ���[�U�ύX�s���ڂ̕ύX�B
#  
#
######################################################################

######################################################################
# ���[�U�ύX����
######################################################################
######################################################################
# �K�{�ݒ�
######################################################################
# Azure �T�u�X�N���v�V���� ID
$AzureSubscriptionID = "";

# ���\�[�X�O���[�v��
$ResourceGroupName = "";

# �e���Y�쐬�ꏊ
$Location = "";

# Notification Hubs �̃l�[���X�y�[�X��
$HubNamespace = "";

# Notification Hubs �̃n�u��
$HubName = "";

# App Service �v������
$AppServicePlanName = "";

# Web App ��
$WebAppName = "";

######################################################################
# �C�Ӑݒ�
######################################################################
######################################################################
# Google(GCM) �ݒ荀��
######################################################################
# Google(GCM) ��API �L�[
$GcmApiKey = "";

######################################################################
# APNs �ݒ荀��
######################################################################
# �G���h�|�C���g(gateway.push.apple.com �� gateway.sandbox.push.apple.com��ݒ�)
$ApnsEndpoint = "";

# Apple �ؖ����v���t�@�C����
$ApnsCertificateFile = "";

# Apple �ؖ����v���t�@�C���쐬���̃p�X���[�h
$ApnsCertificateKey = "";

######################################################################
# ���[�U�ύX�s����
######################################################################
# Notification Hubs �쐬���̓��� json �t�@�C��(�ύX�s��)
$HubInputFile = "\hubinputfile.json";

# Notification Hubs �쐬�pjson�^�f�[�^(�ύX�s��)
$HubJsonData = '{"name": "' + $HubName + '",  "Location": "' + $Location + '",  "Properties": {  }}'

# Microsoft.Azure.NotificationHubs �i�[�t�H���_(�ύX�s��)
$PackagesFolder = "\packages";

# Microsoft.Azure.NotificationHubs �̃��W���[����(�ύX�s��)
$NotificationHubsDll = "Microsoft.Azure.NotificationHubs.dll";

# �X���[�v����
$SleepTime = 10;
