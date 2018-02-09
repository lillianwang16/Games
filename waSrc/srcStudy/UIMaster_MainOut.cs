using System;
using Tutorial;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class UIMaster_MainOut : UIMaster
{
	// Token: 0x06000552 RID: 1362 RVA: 0x00022E64 File Offset: 0x00021264
	public override void UI_Awake()
	{
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00022E68 File Offset: 0x00021268
	public override void UI_Start()
	{
		this.BackFunc();
		if (Application.internetReachability != NetworkReachability.NotReachable && SuperGameMaster.GetIAPCallBackCntEnable())
		{
			this.ListenerUI.GetComponent<IAPListenerPanel>().Active_IAPListener();
			if (SuperGameMaster.PrevScene == Scenes.InitScene)
			{
				SuperGameMaster.IAPCallBackCntUse();
			}
		}
		if (SuperGameMaster.timeError && SuperGameMaster.PrevScene == Scenes.InitScene && SuperGameMaster.tutorial.TutorialComplete())
		{
			this.openWarningWindow(SuperGameMaster.timeErrorString);
			this.MailUI.GetComponent<MailScrollView>().CheckNewMail();
		}
		else
		{
			this.MailUI.GetComponent<MailScrollView>().CheckNewMail();
			this.ResultUI.GetComponent<ResultPanel>().CheckTravelEvent();
		}
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00022F13 File Offset: 0x00021313
	public override void UI_Update()
	{
		this.SubMenuUI.GetComponent<SubMenuPanel>().SubMenuMain(this.Check_blockUI());
		if (this.ResultUI.activeSelf)
		{
			this.ResultUI.GetComponent<ResultPanel>().PanelUpDate();
		}
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x00022F4B File Offset: 0x0002134B
	public override void UI_FixedUpdate()
	{
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00022F4D File Offset: 0x0002134D
	public override void UI_OnDisable()
	{
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00022F4F File Offset: 0x0002134F
	public override void UI_OnPouse()
	{
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00022F51 File Offset: 0x00021351
	public override void UI_OnResume()
	{
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00022F53 File Offset: 0x00021353
	public override void UI_ApplicationQuit()
	{
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00022F58 File Offset: 0x00021358
	public void openWarningWindow(string errorMessage)
	{
		this.freezeObject(true);
		this.blockUI(false, new Color(0.3f, 0.3f, 0.3f, 0f));
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel(errorMessage);
		confilm.ResetOnClick_Screen();
		confilm.SetOnClick_Screen(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.freezeObject(false);
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.blockUI(false, new Color(0f, 0f, 0f, 0f));
		});
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00023006 File Offset: 0x00021406
	public void BackFunc()
	{
		this.BackFunc_Reset();
		this.BackFunc_Set(delegate
		{
			this.ExitConfilm();
		});
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00023020 File Offset: 0x00021420
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

	// Token: 0x0600055D RID: 1373 RVA: 0x00023144 File Offset: 0x00021544
	public void AgreeUserPolicy()
	{
		SuperGameMaster.tutorial.StepTutorial(true);
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x00023151 File Offset: 0x00021551
	public void NotAgreeUserPolicy()
	{
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00023154 File Offset: 0x00021554
	public void TutorialUBlock()
	{
		Step tutorialStep = SuperGameMaster.tutorial.tutorialStep;
		switch (tutorialStep + 1)
		{
		case Step.a0_MO_FrogTap:
			this.SubMenuUI.SetActive(false);
			this.MoveUI.SetActive(false);
			this.CloverUI.SetActive(false);
			break;
		case Step.a1_MO_FrogName:
			this.SubMenuUI.SetActive(false);
			this.MoveUI.SetActive(false);
			this.CloverUI.SetActive(false);
			break;
		case Step.b0_MI_GoOut:
			this.SubMenuUI.SetActive(false);
			this.MoveUI.SetActive(true);
			this.MoveUI.GetComponent<MovePanel>().InBtn.SetActive(true);
			this.MoveUI.GetComponent<MovePanel>().ShopBtn.SetActive(false);
			this.CloverUI.SetActive(false);
			break;
		case Step.c1_MO_GetClover:
			this.SubMenuUI.GetComponent<SubMenuPanel>().BtnDisabled(true);
			this.MoveUI.GetComponent<MovePanel>().BtnDisabled(true, true, true);
			this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
			break;
		case Step.c2_MO_GoShop:
			this.SubMenuUI.GetComponent<SubMenuPanel>().BtnDisabled(true);
			this.MoveUI.GetComponent<MovePanel>().BtnDisabled(true, true, true);
			this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
			break;
		case Step.d0_SH_BuyStandby:
			this.SubMenuUI.GetComponent<SubMenuPanel>().BtnDisabled(true);
			this.MoveUI.GetComponent<MovePanel>().BtnDisabled(true, true, false);
			this.CloverUI.GetComponent<CloverPanel>().BtnDisabled(true);
			break;
		}
	}

	// Token: 0x040004F1 RID: 1265
	[Space(10f)]
	public GameObject SubMenuUI;

	// Token: 0x040004F2 RID: 1266
	public GameObject MoveUI;

	// Token: 0x040004F3 RID: 1267
	public GameObject ConfilmUI;

	// Token: 0x040004F4 RID: 1268
	public GameObject HelpUI;

	// Token: 0x040004F5 RID: 1269
	public GameObject WebViewUI;

	// Token: 0x040004F6 RID: 1270
	[Space(10f)]
	public GameObject CloverUI;

	// Token: 0x040004F7 RID: 1271
	public GameObject MailUI;

	// Token: 0x040004F8 RID: 1272
	public GameObject ResultUI;

	// Token: 0x040004F9 RID: 1273
	public GameObject ListenerUI;

	// Token: 0x040004FA RID: 1274
	[Space(10f)]
	public GameObject TitleUI;

	// Token: 0x040004FB RID: 1275
	public GameObject Title_StartUI;

	// Token: 0x040004FC RID: 1276
	public GameObject Title_PolicyUI;

	// Token: 0x040004FD RID: 1277
	public GameObject FrogNameUI;

	// Token: 0x040004FE RID: 1278
	public GameObject FrogCursorUI;

	// Token: 0x040004FF RID: 1279
	public GameObject MoveCursorUI;

	// Token: 0x04000500 RID: 1280
	public GameObject CloverMarkUI;

	// Token: 0x04000501 RID: 1281
	public GameObject CloverCursorUI;

	// Token: 0x04000502 RID: 1282
	public GameObject AddConfirm_pref;
}
