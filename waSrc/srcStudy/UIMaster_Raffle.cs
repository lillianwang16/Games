using System;
using Tutorial;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class UIMaster_Raffle : UIMaster
{
	// Token: 0x06000570 RID: 1392 RVA: 0x00023426 File Offset: 0x00021826
	public override void UI_Awake()
	{
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00023428 File Offset: 0x00021828
	public override void UI_Start()
	{
		this.BackFunc();
		this.RaffleUI.GetComponent<RaffelPanel>().Init();
		if (SuperGameMaster.GetTmpRaffleResult() != -1)
		{
			this.freezeObject(true);
			this.blockUI(true, new Color(0f, 0f, 0f, 0f));
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("前回のふくびきの景品を\n受け取っていません");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.RaffleUI.GetComponent<RaffelPanel>().SetTmpResult();
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.RaffleUI.GetComponent<RaffelPanel>().CloseResultButton();
			});
		}
		if (!SuperGameMaster.GetFirstFlag(Flag.FIRST_RAFFLE_ROLL))
		{
			SuperGameMaster.SetFirstFlag(Flag.FIRST_RAFFLE_ROLL);
			this.freezeObject(true);
			this.blockUI(true, new Color(0f, 0f, 0f, 0f));
			HelpPanel help = this.HelpUI.GetComponent<HelpPanel>();
			help.OpenPanel("ふくびき券5枚で\n1回抽選に挑戦ができます\n景品はここでしか手に入らない\n旅に役立つものものばかり\nぜひ挑戦してみてください");
			help.ResetOnClick_Screen();
			help.SetOnClick_Screen(delegate
			{
				help.ClosePanel();
			});
			help.SetOnClick_Screen(delegate
			{
				this.freezeObject(false);
			});
			help.SetOnClick_Screen(delegate
			{
				this.blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			return;
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x000235B2 File Offset: 0x000219B2
	public override void UI_Update()
	{
		if (this.LotteryWheelPanel.activeSelf)
		{
			this.LotteryWheelPanel.GetComponent<LotteryWheelPanel>().Proc();
		}
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x000235D4 File Offset: 0x000219D4
	public override void UI_FixedUpdate()
	{
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x000235D6 File Offset: 0x000219D6
	public override void UI_OnDisable()
	{
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x000235D8 File Offset: 0x000219D8
	public override void UI_OnPouse()
	{
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x000235DA File Offset: 0x000219DA
	public override void UI_OnResume()
	{
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x000235DC File Offset: 0x000219DC
	public override void UI_ApplicationQuit()
	{
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x000235DE File Offset: 0x000219DE
	public void PushShopButton()
	{
		this.changeScene(Scenes.Shop);
		this.freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Move"]);
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x00023607 File Offset: 0x00021A07
	public void BackFunc()
	{
		this.BackFunc_Reset();
		this.BackFunc_Set(delegate
		{
			this.PushShopButton();
		});
	}

	// Token: 0x04000506 RID: 1286
	public GameObject LotteryWheelPanel;

	// Token: 0x04000507 RID: 1287
	public GameObject RaffleUI;

	// Token: 0x04000508 RID: 1288
	[Space(10f)]
	public GameObject ConfilmUI;

	// Token: 0x04000509 RID: 1289
	public GameObject HelpUI;
}
