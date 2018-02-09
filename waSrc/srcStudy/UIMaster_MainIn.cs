using System;
using Tutorial;
using UnityEngine;

// Token: 0x020000CC RID: 204
public class UIMaster_MainIn : UIMaster
{
	// Token: 0x06000542 RID: 1346 RVA: 0x00022AF8 File Offset: 0x00020EF8
	public override void UI_Awake()
	{
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00022AFC File Offset: 0x00020EFC
	public override void UI_Start()
	{
		this.BagDeskUI.GetComponent<BagDeskPanels>().CheckEmpty();
		if (SuperGameMaster.GetHome() && SuperGameMaster.tutorial.TutorialComplete())
		{
			this.FrogStateUI.GetComponent<FrogStatePanel>().CheckGetAchive();
			this.FrogStateUI.GetComponent<FrogStatePanel>().GetAchiveDisp();
		}
		this.BackFunc();
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00022B58 File Offset: 0x00020F58
	public override void UI_Update()
	{
		this.SubMenuUI.GetComponent<SubMenuPanel>().SubMenuMain(this.Check_blockUI());
		if (this.BagDeskUI.activeSelf)
		{
			this.BagDeskUI.GetComponent<BagDeskPanels>().PanelUpDate();
		}
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00022B90 File Offset: 0x00020F90
	public override void UI_FixedUpdate()
	{
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00022B92 File Offset: 0x00020F92
	public override void UI_OnDisable()
	{
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00022B94 File Offset: 0x00020F94
	public override void UI_OnPouse()
	{
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00022B96 File Offset: 0x00020F96
	public override void UI_OnResume()
	{
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00022B98 File Offset: 0x00020F98
	public override void UI_ApplicationQuit()
	{
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00022B9A File Offset: 0x00020F9A
	public void BackFunc()
	{
		this.BackFunc_Reset();
		this.BackFunc_Set(delegate
		{
			this.ExitConfilm();
		});
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00022BB4 File Offset: 0x00020FB4
	public void ExitConfilm()
	{
		this.freezeObject(true);
		this.blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel_YesNo("アプリケーションを終了しますか？");
		confilm.ResetOnClick_Yes();
		confilm.SetOnClick_Yes(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Yes(delegate
		{
			SuperGameMaster.SaveData();
		});
		confilm.SetOnClick_Yes(delegate
		{
			Application.Quit();
		});
		confilm.ResetOnClick_No();
		confilm.SetOnClick_No(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_No(delegate
		{
			this.freezeObject(false);
		});
		confilm.SetOnClick_No(delegate
		{
			this.blockUI(false, new Color(0f, 0f, 0f, 0f));
		});
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00022CD8 File Offset: 0x000210D8
	public void TutorialUBlock()
	{
		Step tutorialStep = SuperGameMaster.tutorial.tutorialStep;
		if (tutorialStep != Step.b0_MI_GoOut)
		{
			if (tutorialStep == Step.e0_MI_OpenBag)
			{
				this.SubMenuUI.GetComponent<SubMenuPanel>().BtnDisabled(true);
				this.MoveUI.GetComponent<MovePanel>().BtnDisabled(true, true, true);
				this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
				this.BagDeskUI.GetComponent<BagDeskPanels>().BtnDisabled(false);
			}
		}
		else
		{
			this.SubMenuUI.GetComponent<SubMenuPanel>().BtnDisabled(true);
			this.MoveUI.GetComponent<MovePanel>().BtnDisabled(true, false, true);
			this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
			this.BagDeskUI.GetComponent<BagDeskPanels>().BtnDisabled(true);
		}
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00022D9C File Offset: 0x0002119C
	public void TutorialUBlockAll()
	{
		this.SubMenuUI.GetComponent<SubMenuPanel>().BtnDisabled(true);
		this.MoveUI.GetComponent<MovePanel>().BtnDisabled(true, true, true);
		this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
		this.BagDeskUI.GetComponent<BagDeskPanels>().BtnDisabled(true);
	}

	// Token: 0x040004E4 RID: 1252
	[Space(10f)]
	public GameObject SubMenuUI;

	// Token: 0x040004E5 RID: 1253
	public GameObject MoveUI;

	// Token: 0x040004E6 RID: 1254
	public GameObject ConfilmUI;

	// Token: 0x040004E7 RID: 1255
	public GameObject HelpUI;

	// Token: 0x040004E8 RID: 1256
	[Space(10f)]
	public GameObject CloverUI;

	// Token: 0x040004E9 RID: 1257
	public GameObject BagDeskUI;

	// Token: 0x040004EA RID: 1258
	public GameObject FrogStateUI;

	// Token: 0x040004EB RID: 1259
	public GameObject UrabeyaUI;

	// Token: 0x040004EC RID: 1260
	[Space(10f)]
	public GameObject MoveCursorUI;

	// Token: 0x040004ED RID: 1261
	public GameObject BagMarkUI;

	// Token: 0x040004EE RID: 1262
	public GameObject BagCompleteCursor;
}
