using System;
using UnityEngine;

// Token: 0x020000BF RID: 191
public class GameMaster_MainIn : GameMaster
{
	// Token: 0x060004B1 RID: 1201 RVA: 0x00021A2E File Offset: 0x0001FE2E
	public override void Master_Awake()
	{
		this.reloadTimer = 0f;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00021A3B File Offset: 0x0001FE3B
	public override void Master_Start()
	{
		this.UIMaster.GetComponent<UIMaster>().setFadeIn(0.25f);
		SuperGameMaster.tutorial.StartTutorial(base.gameObject);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00021A64 File Offset: 0x0001FE64
	public override void Master_Update()
	{
		if (this.sceneTransition && this.UIMaster.GetComponent<UIMaster>().checkFadeComplete())
		{
			this.ChangeSceneUpdate(this.setNextScene);
			this.sceneTransition = false;
		}
		if (this.reloadTimer > 0f)
		{
			this.reloadTimer -= Time.deltaTime;
		}
		SuperGameMaster.tutorial.UpdateTutorial();
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00021AD0 File Offset: 0x0001FED0
	public override void Master_FixedUpdate()
	{
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00021AD2 File Offset: 0x0001FED2
	public override void Master_Disable()
	{
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00021AD4 File Offset: 0x0001FED4
	public override void Master_OnPouse()
	{
		this.TravelCheck();
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00021AE6 File Offset: 0x0001FEE6
	public override void Master_OnResume()
	{
		if (this.reloadTimer > 0f)
		{
			Debug.Log("[GameMaster_Album] リロード猶予期間中 / reloadTimer = " + this.reloadTimer);
			return;
		}
		if (!this.StopReload)
		{
			this.ChangeScene(Scenes._Reload);
		}
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00021B25 File Offset: 0x0001FF25
	public override void Master_ApplicationQuit()
	{
		this.TravelCheck();
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x00021B37 File Offset: 0x0001FF37
	public override void ChangeSceneCall(Scenes _nextScene)
	{
		if (this.nowSceneChanging || this.sceneTransition)
		{
			return;
		}
		this.sceneTransition = true;
		this.setNextScene = _nextScene;
		this.UIMaster.GetComponent<UIMaster>().setFadeOut(0.25f);
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00021B73 File Offset: 0x0001FF73
	public void PlayBGM_Dict(string bgm)
	{
		SuperGameMaster.audioMgr.PlayBGM(Define.BGMDict[bgm]);
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00021B8A File Offset: 0x0001FF8A
	public void PlaySE_Dict(string se)
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict[se]);
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00021BA1 File Offset: 0x0001FFA1
	public void SaveAndStopReload(bool ReLoadFlag)
	{
		SuperGameMaster.SaveData();
		this.StopReload = ReLoadFlag;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00021BAF File Offset: 0x0001FFAF
	public void SetStopReload(bool flag)
	{
		this.StopReload = flag;
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00021BB8 File Offset: 0x0001FFB8
	public void SetReloadTimer(float setTime)
	{
		this.reloadTimer = setTime;
		Debug.Log("[GameMaster_MainIn] リロード時間が再設定されました / setTime = " + setTime);
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00021BD8 File Offset: 0x0001FFD8
	public void TravelCheck()
	{
		GameObject bagDeskUI = this.UIMaster.GetComponent<UIMaster_MainIn>().BagDeskUI;
		if (bagDeskUI.activeSelf)
		{
			bagDeskUI.GetComponent<BagDeskPanels>().CloseCheck();
		}
	}

	// Token: 0x040004BB RID: 1211
	private bool sceneTransition;

	// Token: 0x040004BC RID: 1212
	private Scenes setNextScene;

	// Token: 0x040004BD RID: 1213
	private bool StopReload;

	// Token: 0x040004BE RID: 1214
	public float reloadTimer;
}
