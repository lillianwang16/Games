using System;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class GameMaster_Album : GameMaster
{
	// Token: 0x060004A4 RID: 1188 RVA: 0x000218C8 File Offset: 0x0001FCC8
	public override void Master_Awake()
	{
		this.reloadTimer = 0f;
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x000218D5 File Offset: 0x0001FCD5
	public override void Master_Start()
	{
		this.UIMaster.GetComponent<UIMaster>().setFadeIn(0.25f);
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x000218EC File Offset: 0x0001FCEC
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
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0002194E File Offset: 0x0001FD4E
	public override void Master_FixedUpdate()
	{
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00021950 File Offset: 0x0001FD50
	public override void Master_Disable()
	{
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00021952 File Offset: 0x0001FD52
	public override void Master_OnPouse()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0002195E File Offset: 0x0001FD5E
	public override void Master_OnResume()
	{
		if (this.reloadTimer > 0f)
		{
			Debug.Log("[GameMaster_Album] リロード猶予期間中 / reloadTimer = " + this.reloadTimer);
			return;
		}
		this.ChangeScene(Scenes._Reload);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00021992 File Offset: 0x0001FD92
	public override void Master_ApplicationQuit()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0002199E File Offset: 0x0001FD9E
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

	// Token: 0x060004AD RID: 1197 RVA: 0x000219DA File Offset: 0x0001FDDA
	public void PlayBGM_Dict(string bgm)
	{
		SuperGameMaster.audioMgr.PlayBGM(Define.BGMDict[bgm]);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x000219F1 File Offset: 0x0001FDF1
	public void PlaySE_Dict(string se)
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict[se]);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00021A08 File Offset: 0x0001FE08
	public void SetReloadTimer(float setTime)
	{
		this.reloadTimer = setTime;
		Debug.Log("[GameMaster_Album] リロード時間が再設定されました / setTime = " + setTime);
	}

	// Token: 0x040004B8 RID: 1208
	private bool sceneTransition;

	// Token: 0x040004B9 RID: 1209
	private Scenes setNextScene;

	// Token: 0x040004BA RID: 1210
	public float reloadTimer;
}
