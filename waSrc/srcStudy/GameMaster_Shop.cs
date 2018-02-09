using System;

// Token: 0x020000C3 RID: 195
public class GameMaster_Shop : GameMaster
{
	// Token: 0x060004E8 RID: 1256 RVA: 0x00021FE2 File Offset: 0x000203E2
	public override void Master_Awake()
	{
	}

	// Token: 0x060004E9 RID: 1257 RVA: 0x00021FE4 File Offset: 0x000203E4
	public override void Master_Start()
	{
		this.UIMaster.GetComponent<UIMaster>().setFadeIn(0.25f);
		SuperGameMaster.tutorial.StartTutorial(base.gameObject);
	}

	// Token: 0x060004EA RID: 1258 RVA: 0x0002200B File Offset: 0x0002040B
	public override void Master_Update()
	{
		if (this.sceneTransition && this.UIMaster.GetComponent<UIMaster>().checkFadeComplete())
		{
			this.ChangeSceneUpdate(this.setNextScene);
			this.sceneTransition = false;
		}
		SuperGameMaster.tutorial.UpdateTutorial();
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0002204A File Offset: 0x0002044A
	public override void Master_FixedUpdate()
	{
	}

	// Token: 0x060004EC RID: 1260 RVA: 0x0002204C File Offset: 0x0002044C
	public override void Master_Disable()
	{
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x0002204E File Offset: 0x0002044E
	public override void Master_OnPouse()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0002205A File Offset: 0x0002045A
	public override void Master_OnResume()
	{
		if (!this.StopReload)
		{
			this.ChangeScene(Scenes._Reload);
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0002206E File Offset: 0x0002046E
	public override void Master_ApplicationQuit()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0002207A File Offset: 0x0002047A
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

	// Token: 0x060004F1 RID: 1265 RVA: 0x000220B6 File Offset: 0x000204B6
	public void PlayBGM_Dict(string bgm)
	{
		SuperGameMaster.audioMgr.PlayBGM(Define.BGMDict[bgm]);
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x000220CD File Offset: 0x000204CD
	public void PlaySE_Dict(string se)
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict[se]);
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x000220E4 File Offset: 0x000204E4
	public void SaveAndStopReload(bool ReLoadFlag)
	{
		SuperGameMaster.SaveData();
		this.StopReload = ReLoadFlag;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x000220F2 File Offset: 0x000204F2
	public void SetStopReload(bool flag)
	{
		this.StopReload = flag;
	}

	// Token: 0x040004C9 RID: 1225
	private bool sceneTransition;

	// Token: 0x040004CA RID: 1226
	private Scenes setNextScene;

	// Token: 0x040004CB RID: 1227
	private bool StopReload;
}
