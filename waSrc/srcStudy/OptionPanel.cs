using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005C RID: 92
public class OptionPanel : MonoBehaviour
{
	// Token: 0x0600033B RID: 827 RVA: 0x00011AD8 File Offset: 0x0000FED8
	public void BackFunc()
	{
		UIMaster componentInParent = base.transform.parent.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.ClosePanel();
		});
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00011B10 File Offset: 0x0000FF10
	public void OpenPanel()
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<OtherPanel>().MainScrollView.SetActive(false);
		this.SetValue();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc();
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00011B60 File Offset: 0x0000FF60
	public void SetNoReload()
	{
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes != Scenes.MainOut)
		{
			if (nowScenes == Scenes.MainIn)
			{
				base.transform.parent.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainIn>().SetReloadTimer(1f);
			}
		}
		else
		{
			base.transform.parent.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SetReloadTimer(1f);
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00011BD8 File Offset: 0x0000FFD8
	public void ClosePanel()
	{
		base.gameObject.SetActive(false);
		base.GetComponentInParent<OtherPanel>().MainScrollView.SetActive(true);
		SuperGameMaster.audioMgr.SaveVolume();
		base.GetComponentInParent<OtherPanel>().SaveData();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		base.GetComponentInParent<OtherPanel>().BackFunc();
	}

	// Token: 0x0600033F RID: 831 RVA: 0x00011C3C File Offset: 0x0001003C
	public void SetValue()
	{
		this.BgmSlider.GetComponent<Slider>().value = (float)(SuperGameMaster.audioMgr.GetBgmVolume() / 20);
		this.SeSlider.GetComponent<Slider>().value = (float)(SuperGameMaster.audioMgr.GetSeVolume() / 20);
		this.CngNoticeFlag(SuperGameMaster.GetNoticeFlag());
		this.SetSupportID(false);
		SuperGameMaster.audioMgr.StopSE();
	}

	// Token: 0x06000340 RID: 832 RVA: 0x00011CA4 File Offset: 0x000100A4
	public void CngNoticeFlag(bool cngFlag)
	{
		if (cngFlag)
		{
			this.NoticeOnBtn.GetComponent<Button>().interactable = false;
			this.NoticeOffBtn.GetComponent<Button>().interactable = true;
		}
		else
		{
			this.NoticeOnBtn.GetComponent<Button>().interactable = true;
			this.NoticeOffBtn.GetComponent<Button>().interactable = false;
		}
		SuperGameMaster.SetNoticeFlag(cngFlag);
	}

	// Token: 0x06000341 RID: 833 RVA: 0x00011D08 File Offset: 0x00010108
	public void PushNoticeBtn(bool cngFlag)
	{
		this.CngNoticeFlag(cngFlag);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
		if (cngFlag)
		{
			Scenes nowScenes = SuperGameMaster.GetNowScenes();
			if (nowScenes != Scenes.MainOut)
			{
				if (nowScenes == Scenes.MainIn)
				{
					base.transform.parent.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainIn>().SetReloadTimer(1f);
				}
			}
			else
			{
				base.transform.parent.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SetReloadTimer(1f);
			}
			SuperGameMaster.iOS_AgreeNotifications();
			if (!SuperGameMaster.Android_CheckDoze())
			{
				ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
				confilm.OpenPanel("端末で省電力モードを設定していると\n通知が遅れて届く場合があります\n通知をすぐに受け取るには\n電池の最適化を無効にする\n必要があります");
				confilm.ResetOnClick_Screen();
				confilm.SetOnClick_Screen(delegate
				{
					confilm.ClosePanel();
				});
			}
		}
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00011DFE File Offset: 0x000101FE
	public void CngBgmValue(float cngVal)
	{
		SuperGameMaster.audioMgr.SetBgmVolume((int)cngVal * 20);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00011E28 File Offset: 0x00010228
	public void CngSeValue(float cngVal)
	{
		SuperGameMaster.audioMgr.SetSeVolume((int)cngVal * 20);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}

	// Token: 0x06000344 RID: 836 RVA: 0x00011E54 File Offset: 0x00010254
	public void PushSupportBtn()
	{
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel_YesNo("サポートIDを再生成しますか？");
		confilm.ResetOnClick_Yes();
		confilm.SetOnClick_Yes(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.SetSupportID(true);
		});
		confilm.ResetOnClick_No();
		confilm.SetOnClick_No(delegate
		{
			confilm.ClosePanel();
		});
	}

	// Token: 0x06000345 RID: 837 RVA: 0x00011EEC File Offset: 0x000102EC
	public void SetSupportID(bool reset)
	{
		string str = string.Empty;
		if (reset)
		{
			str = SuperGameMaster.setSupportID(-1);
		}
		else
		{
			str = SuperGameMaster.setSupportID(SuperGameMaster.getSupportID());
		}
		this.SupportUI.GetComponentInChildren<Text>().text = "サポートＩＤ： " + str;
	}

	// Token: 0x040001CC RID: 460
	public GameObject BgmSlider;

	// Token: 0x040001CD RID: 461
	public GameObject SeSlider;

	// Token: 0x040001CE RID: 462
	public GameObject NoticeOnBtn;

	// Token: 0x040001CF RID: 463
	public GameObject NoticeOffBtn;

	// Token: 0x040001D0 RID: 464
	public GameObject SupportUI;

	// Token: 0x040001D1 RID: 465
	private const int maxValue = 5;

	// Token: 0x040001D2 RID: 466
	[Space(10f)]
	public GameObject ConfilmUI;
}
