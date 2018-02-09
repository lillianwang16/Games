using System;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000D0 RID: 208
public class UIMaster_Shop : UIMaster
{
	// Token: 0x0600057C RID: 1404 RVA: 0x000236BE File Offset: 0x00021ABE
	public override void UI_Awake()
	{
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x000236C0 File Offset: 0x00021AC0
	public override void UI_Start()
	{
		this.S_DisplayPanel = this.DisplayPanel.GetComponent<DisplayPanel>();
		this.S_DisplayPanel.Init();
		this.BackFunc();
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x000236E4 File Offset: 0x00021AE4
	public override void UI_Update()
	{
		if (!this.Check_blockUI())
		{
			this.S_DisplayPanel.PanelUpDate();
		}
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x000236FC File Offset: 0x00021AFC
	public override void UI_FixedUpdate()
	{
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x000236FE File Offset: 0x00021AFE
	public override void UI_OnDisable()
	{
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00023700 File Offset: 0x00021B00
	public override void UI_OnPouse()
	{
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00023702 File Offset: 0x00021B02
	public override void UI_OnResume()
	{
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00023704 File Offset: 0x00021B04
	public override void UI_ApplicationQuit()
	{
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00023706 File Offset: 0x00021B06
	public void PushRaffleButton()
	{
		this.changeScene(Scenes.Raffle);
		this.freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Move"]);
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x0002372F File Offset: 0x00021B2F
	public void BackFunc()
	{
		this.BackFunc_Reset();
		this.BackFunc_Set(delegate
		{
			this.BackMain();
		});
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x0002374C File Offset: 0x00021B4C
	public void BackMain()
	{
		if (SuperGameMaster.PrevScene == Scenes.MainOut || SuperGameMaster.PrevScene == Scenes.MainIn)
		{
			this.changeScene(Scenes._Prev);
		}
		else
		{
			this.changeScene(Scenes.MainOut);
		}
		this.freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Move"]);
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x000237A4 File Offset: 0x00021BA4
	public void TutorialUBlock()
	{
		Step tutorialStep = SuperGameMaster.tutorial.tutorialStep;
		if (tutorialStep != Step.d0_SH_BuyStandby)
		{
			if (tutorialStep != Step.d1_SH_BuyItem)
			{
				if (tutorialStep == Step.d2_SH_GoHome)
				{
					this.MoveUI.GetComponent<MovePanel>().BtnDisabled(false, true, true);
					this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
					this.RaffleUI.GetComponent<Button>().interactable = false;
				}
			}
			else
			{
				this.MoveUI.GetComponent<MovePanel>().BtnDisabled(true, true, true);
				this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
				this.RaffleUI.GetComponent<Button>().interactable = false;
			}
		}
		else
		{
			this.MoveUI.GetComponent<MovePanel>().BtnDisabled(true, true, true);
			this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
			this.RaffleUI.GetComponent<Button>().interactable = false;
		}
	}

	// Token: 0x0400050A RID: 1290
	[Space(10f)]
	public GameObject MoveUI;

	// Token: 0x0400050B RID: 1291
	public GameObject CloverUI;

	// Token: 0x0400050C RID: 1292
	public GameObject RaffleUI;

	// Token: 0x0400050D RID: 1293
	public GameObject ConfilmUI;

	// Token: 0x0400050E RID: 1294
	public GameObject HelpUI;

	// Token: 0x0400050F RID: 1295
	[Space(10f)]
	public GameObject DisplayPanel;

	// Token: 0x04000510 RID: 1296
	public GameObject MoveCursorUI;

	// Token: 0x04000511 RID: 1297
	private DisplayPanel S_DisplayPanel;
}
