using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BA RID: 186
public class DebugButton : MonoBehaviour
{
	// Token: 0x0600045E RID: 1118 RVA: 0x0001F723 File Offset: 0x0001DB23
	public void UIActive()
	{
		base.gameObject.SetActive(!base.gameObject.activeSelf);
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0001F73E File Offset: 0x0001DB3E
	public void Debug_CloverGet(int num)
	{
		SuperGameMaster.getCloverPoint(num);
		SuperGameMaster.GetTicket(num / 100);
		Debug.Log("[DebugButton] クローバー取得 " + num);
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0001F764 File Offset: 0x0001DB64
	public void Debug_GetItem()
	{
		SuperGameMaster.getCloverPoint(20000);
		SuperGameMaster.GetTicket(50);
		foreach (ItemDataFormat itemDataFormat in SuperGameMaster.sDataBase.ItemDB.data)
		{
			SuperGameMaster.GetItem(itemDataFormat.id, 20);
		}
		Debug.Log("[DebugButton] アイテム取得");
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0001F7EC File Offset: 0x0001DBEC
	public void Debug_TrashItem()
	{
		foreach (ItemDataFormat itemDataFormat in SuperGameMaster.sDataBase.ItemDB.data)
		{
			SuperGameMaster.UseItem(itemDataFormat.id, 20);
		}
		Debug.Log("[DebugButton] アイテム削除");
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001F864 File Offset: 0x0001DC64
	public void Debug_AddTimer(float addTime)
	{
		SuperGameMaster.deviceTime = SuperGameMaster.deviceTime.AddHours((double)(-(double)addTime));
		Debug.Log(string.Concat(new object[]
		{
			"[DebugButton] ",
			addTime,
			" 時間加算：",
			SuperGameMaster.deviceTime.ToString(),
			" || ",
			SuperGameMaster.saveData.lastDateTime.ToString(),
			" リロードします"
		}));
		SuperGameMaster.travel.UpDateNotification();
		base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster>().ChangeScene(Scenes._Reload);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0001F908 File Offset: 0x0001DD08
	public void Debug_Data_Init()
	{
		File.Delete(Define.SaveName_Binary);
		Directory.Delete(Define.SaveName_Binary_PicDir, true);
		SuperGameMaster.audioMgr.StopBGM();
		SuperGameMaster.LoadData();
		Debug.Log("[DebugButton] セーブデータを削除 : path = " + SaveDataFormat.SavePath + "GameData.sav");
		SuperGameMaster.CancelLocalNotification(72091216);
		SuperGameMaster.setNextScene(Scenes._Reload);
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0001F962 File Offset: 0x0001DD62
	public void OpenSwitch(GameObject obj)
	{
		obj.SetActive(!obj.activeSelf);
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0001F973 File Offset: 0x0001DD73
	public void Debug_Test_ShowInterStatial()
	{
		SuperGameMaster.admobMgr.ShowInterstitial();
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0001F980 File Offset: 0x0001DD80
	public void DebugTest_Notification_Change(GameObject obj)
	{
		SuperGameMaster.travel.DebugTest_Notification = !SuperGameMaster.travel.DebugTest_Notification;
		obj.GetComponentInChildren<Text>().text = "test：" + SuperGameMaster.travel.DebugTest_Notification.ToString();
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0001F9CE File Offset: 0x0001DDCE
	public void Debug_Notification_ON(int sec)
	{
		SuperGameMaster.SendLocalNotification("通知テスト：通常 ", sec, 72091216);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0001F9E0 File Offset: 0x0001DDE0
	public void Debug_Notification_OFF()
	{
		SuperGameMaster.CancelLocalNotification(72091216);
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0001F9EC File Offset: 0x0001DDEC
	public void Debug_Test_AB_Notification_OFF()
	{
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001F9EE File Offset: 0x0001DDEE
	public void Debug_Test_A_Notification(int delayTime)
	{
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0001F9F0 File Offset: 0x0001DDF0
	public void Debug_Test_B_Notification(int delayTime)
	{
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0001F9F2 File Offset: 0x0001DDF2
	public void Debug_Test_PlugIn_Notification(int delayTime)
	{
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0001F9F4 File Offset: 0x0001DDF4
	public void Debug_Test_PlugIn_Notification_Off()
	{
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0001F9F6 File Offset: 0x0001DDF6
	public void RegisterNotificationSettings()
	{
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001F9F8 File Offset: 0x0001DDF8
	public void CheckNotificationRegistered()
	{
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0001F9FA File Offset: 0x0001DDFA
	public void OpenApplicationSettings()
	{
	}
}
