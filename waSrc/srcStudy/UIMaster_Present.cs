using System;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class UIMaster_Present : UIMaster
{
	// Token: 0x06000564 RID: 1380 RVA: 0x000233AD File Offset: 0x000217AD
	public override void UI_Awake()
	{
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x000233AF File Offset: 0x000217AF
	public override void UI_Start()
	{
		this.CollectionUI.GetComponent<CollectionScrollView>().init();
		this.BackFunc();
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x000233C7 File Offset: 0x000217C7
	public override void UI_Update()
	{
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x000233C9 File Offset: 0x000217C9
	public override void UI_FixedUpdate()
	{
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x000233CB File Offset: 0x000217CB
	public override void UI_OnDisable()
	{
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x000233CD File Offset: 0x000217CD
	public override void UI_OnPouse()
	{
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x000233CF File Offset: 0x000217CF
	public override void UI_OnResume()
	{
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x000233D1 File Offset: 0x000217D1
	public override void UI_ApplicationQuit()
	{
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x000233D3 File Offset: 0x000217D3
	public void BackFunc()
	{
		this.BackFunc_Reset();
		this.BackFunc_Set(delegate
		{
			this.BackMain();
		});
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x000233ED File Offset: 0x000217ED
	public void BackMain()
	{
		this.changeScene(Scenes._Prev);
		this.freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
	}

	// Token: 0x04000505 RID: 1285
	public GameObject CollectionUI;
}
