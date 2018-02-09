using System;
using UnityEngine;

// Token: 0x020000C0 RID: 192
public class GameMaster_MainOut : GameMaster
{
	// Token: 0x060004C1 RID: 1217 RVA: 0x00021C14 File Offset: 0x00020014
	public override void Master_Awake()
	{
		this.reloadTimer = 0f;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00021C24 File Offset: 0x00020024
	public override void Master_Start()
	{
		this.UIMaster.GetComponent<UIMaster>().setFadeIn(0.25f);
		if (SuperGameMaster.GetHome())
		{
			this.BackRenderer.sprite = this.InFrogSprite;
		}
		SuperGameMaster.tutorial.StartTutorial(base.gameObject);
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x00021C74 File Offset: 0x00020074
	public override void Master_Update()
	{
		if (this.sceneTransition && this.UIMaster.GetComponent<UIMaster>().checkFadeComplete())
		{
			this.ChangeSceneUpdate(this.setNextScene);
			this.sceneTransition = false;
		}
		SuperGameMaster.tutorial.UpdateTutorial();
		if (this.reloadTimer > 0f)
		{
			this.reloadTimer -= Time.deltaTime;
		}
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x00021CE0 File Offset: 0x000200E0
	public override void Master_FixedUpdate()
	{
	}

	// Token: 0x060004C5 RID: 1221 RVA: 0x00021CE2 File Offset: 0x000200E2
	public override void Master_Disable()
	{
	}

	// Token: 0x060004C6 RID: 1222 RVA: 0x00021CE4 File Offset: 0x000200E4
	public override void Master_OnPouse()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x00021CF0 File Offset: 0x000200F0
	public override void Master_OnResume()
	{
		if (this.reloadTimer > 0f)
		{
			Debug.Log("[GameMaster_Album] リロード猶予期間中 / reloadTimer = " + this.reloadTimer);
			return;
		}
		if (SuperGameMaster.admobMgr.ReloadCheck())
		{
			return;
		}
		if (SuperGameMaster.admobMgr.is_Banner_enable)
		{
			SuperGameMaster.admobMgr.ShowBanner(false);
		}
		if (!this.StopReload)
		{
			this.ChangeScene(Scenes._Reload);
		}
	}

	// Token: 0x060004C8 RID: 1224 RVA: 0x00021D65 File Offset: 0x00020165
	public override void Master_ApplicationQuit()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x00021D71 File Offset: 0x00020171
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

	// Token: 0x060004CA RID: 1226 RVA: 0x00021DAD File Offset: 0x000201AD
	public void PlayBGM_Dict(string bgm)
	{
		SuperGameMaster.audioMgr.PlayBGM(Define.BGMDict[bgm]);
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x00021DC4 File Offset: 0x000201C4
	public void PlaySE_Dict(string se)
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict[se]);
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x00021DDB File Offset: 0x000201DB
	public void SaveAndStopReload(bool ReLoadFlag)
	{
		SuperGameMaster.SaveData();
		this.StopReload = ReLoadFlag;
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x00021DE9 File Offset: 0x000201E9
	public void SetStopReload(bool flag)
	{
		this.StopReload = flag;
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x00021DF2 File Offset: 0x000201F2
	public void SetReloadTimer(float setTime)
	{
		this.reloadTimer = setTime;
		Debug.Log("[GameMaster_MainOut] リロード時間が再設定されました / setTime = " + setTime);
	}

	// Token: 0x040004BF RID: 1215
	public bool sceneTransition;

	// Token: 0x040004C0 RID: 1216
	private Scenes setNextScene;

	// Token: 0x040004C1 RID: 1217
	private bool StopReload;

	// Token: 0x040004C2 RID: 1218
	public float reloadTimer;

	// Token: 0x040004C3 RID: 1219
	[SerializeField]
	private SpriteRenderer BackRenderer;

	// Token: 0x040004C4 RID: 1220
	[SerializeField]
	private Sprite InFrogSprite;
}
