using System;

// Token: 0x020000C2 RID: 194
public class GameMaster_Raffle : GameMaster
{
	// Token: 0x060004DC RID: 1244 RVA: 0x00021EFD File Offset: 0x000202FD
	public override void Master_Awake()
	{
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x00021EFF File Offset: 0x000202FF
	public override void Master_Start()
	{
		this.UIMaster.GetComponent<UIMaster>().setFadeIn(0.25f);
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00021F16 File Offset: 0x00020316
	public override void Master_Update()
	{
		if (this.sceneTransition && this.UIMaster.GetComponent<UIMaster>().checkFadeComplete())
		{
			this.ChangeSceneUpdate(this.setNextScene);
			this.sceneTransition = false;
		}
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00021F4B File Offset: 0x0002034B
	public override void Master_FixedUpdate()
	{
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00021F4D File Offset: 0x0002034D
	public override void Master_Disable()
	{
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00021F4F File Offset: 0x0002034F
	public override void Master_OnPouse()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x00021F5B File Offset: 0x0002035B
	public override void Master_OnResume()
	{
		this.ChangeScene(Scenes._Reload);
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x00021F64 File Offset: 0x00020364
	public override void Master_ApplicationQuit()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00021F70 File Offset: 0x00020370
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

	// Token: 0x060004E5 RID: 1253 RVA: 0x00021FAC File Offset: 0x000203AC
	public void PlayBGM_Dict(string bgm)
	{
		SuperGameMaster.audioMgr.PlayBGM(Define.BGMDict[bgm]);
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00021FC3 File Offset: 0x000203C3
	public void PlaySE_Dict(string se)
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict[se]);
	}

	// Token: 0x040004C7 RID: 1223
	private bool sceneTransition;

	// Token: 0x040004C8 RID: 1224
	private Scenes setNextScene;
}
