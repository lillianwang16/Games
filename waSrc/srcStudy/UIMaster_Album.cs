using System;
using UnityEngine;

// Token: 0x020000CB RID: 203
public class UIMaster_Album : UIMaster
{
	// Token: 0x06000536 RID: 1334 RVA: 0x00022A3D File Offset: 0x00020E3D
	public override void UI_Awake()
	{
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00022A3F File Offset: 0x00020E3F
	public override void UI_Start()
	{
		this.S_PicturePanel = this.PictureUI.GetComponent<PicturePanel>();
		this.S_PicturePanel.Init();
		this.BackFunc();
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00022A63 File Offset: 0x00020E63
	public override void UI_Update()
	{
		if (!this.Check_blockUI())
		{
			this.S_PicturePanel.PanelUpDate();
		}
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00022A7B File Offset: 0x00020E7B
	public override void UI_FixedUpdate()
	{
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x00022A7D File Offset: 0x00020E7D
	public override void UI_OnDisable()
	{
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00022A7F File Offset: 0x00020E7F
	public override void UI_OnPouse()
	{
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x00022A81 File Offset: 0x00020E81
	public override void UI_OnResume()
	{
		this.PictureUI.GetComponent<SocialSender>().DeleteTmpImage();
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00022A93 File Offset: 0x00020E93
	public override void UI_ApplicationQuit()
	{
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00022A95 File Offset: 0x00020E95
	public void BackFunc()
	{
		this.BackFunc_Reset();
		this.BackFunc_Set(delegate
		{
			this.BackMain();
		});
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00022AAF File Offset: 0x00020EAF
	public void BackMain()
	{
		this.PictureUI.GetComponent<PicturePanel>().BackMain();
		this.changeScene(Scenes._Prev);
		this.freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
	}

	// Token: 0x040004E1 RID: 1249
	public GameObject PictureUI;

	// Token: 0x040004E2 RID: 1250
	public GameObject ConfilmUI;

	// Token: 0x040004E3 RID: 1251
	private PicturePanel S_PicturePanel;
}
