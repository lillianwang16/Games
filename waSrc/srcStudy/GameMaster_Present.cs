using System;

// Token: 0x020000C1 RID: 193
public class GameMaster_Present : GameMaster
{
	// Token: 0x060004D0 RID: 1232 RVA: 0x00021E18 File Offset: 0x00020218
	public override void Master_Awake()
	{
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x00021E1A File Offset: 0x0002021A
	public override void Master_Start()
	{
		this.UIMaster.GetComponent<UIMaster>().setFadeIn(0.25f);
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x00021E31 File Offset: 0x00020231
	public override void Master_Update()
	{
		if (this.sceneTransition && this.UIMaster.GetComponent<UIMaster>().checkFadeComplete())
		{
			this.ChangeSceneUpdate(this.setNextScene);
			this.sceneTransition = false;
		}
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x00021E66 File Offset: 0x00020266
	public override void Master_FixedUpdate()
	{
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x00021E68 File Offset: 0x00020268
	public override void Master_Disable()
	{
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x00021E6A File Offset: 0x0002026A
	public override void Master_OnPouse()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x00021E76 File Offset: 0x00020276
	public override void Master_OnResume()
	{
		this.ChangeScene(Scenes._Reload);
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x00021E7F File Offset: 0x0002027F
	public override void Master_ApplicationQuit()
	{
		SuperGameMaster.travel.UpDateNotification();
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x00021E8B File Offset: 0x0002028B
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

	// Token: 0x060004D9 RID: 1241 RVA: 0x00021EC7 File Offset: 0x000202C7
	public void PlayBGM_Dict(string bgm)
	{
		SuperGameMaster.audioMgr.PlayBGM(Define.BGMDict[bgm]);
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x00021EDE File Offset: 0x000202DE
	public void PlaySE_Dict(string se)
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict[se]);
	}

	// Token: 0x040004C5 RID: 1221
	public bool sceneTransition;

	// Token: 0x040004C6 RID: 1222
	private Scenes setNextScene;
}
