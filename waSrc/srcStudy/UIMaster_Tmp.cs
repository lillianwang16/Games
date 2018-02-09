using System;

// Token: 0x020000D5 RID: 213
public class UIMaster_Tmp : UIMaster
{
	// Token: 0x060005D1 RID: 1489 RVA: 0x000238CE File Offset: 0x00021CCE
	public override void UI_Awake()
	{
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x000238D0 File Offset: 0x00021CD0
	public override void UI_Start()
	{
		this.BackFunc();
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x000238D8 File Offset: 0x00021CD8
	public override void UI_Update()
	{
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x000238DA File Offset: 0x00021CDA
	public override void UI_FixedUpdate()
	{
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x000238DC File Offset: 0x00021CDC
	public override void UI_OnDisable()
	{
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x000238DE File Offset: 0x00021CDE
	public override void UI_OnPouse()
	{
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x000238E0 File Offset: 0x00021CE0
	public override void UI_OnResume()
	{
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x000238E2 File Offset: 0x00021CE2
	public override void UI_ApplicationQuit()
	{
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x000238E4 File Offset: 0x00021CE4
	public void BackFunc()
	{
		this.changeScene(Scenes._Prev);
		this.freezeAll(true);
	}
}
