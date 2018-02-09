using System;
using UnityEngine;
using UnityEngine.Purchasing;

// Token: 0x02000056 RID: 86
public class IAPPanel : MonoBehaviour
{
	// Token: 0x0600030A RID: 778 RVA: 0x0000E594 File Offset: 0x0000C994
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.PushCloseButton();
		});
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0000E5C0 File Offset: 0x0000C9C0
	public void OpenIAPPanel()
	{
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("インターネットに接続できませんでした\n電波状況や端末オプションを\nご確認のうえ、再度お試しください");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.GetComponentInParent<UIMaster>().freezeObject(false);
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			return;
		}
		base.gameObject.SetActive(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc();
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0000E6B4 File Offset: 0x0000CAB4
	public void PushCloseButton()
	{
		base.gameObject.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		base.GetComponentInParent<UIMaster>().stopUpDate_UI(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes != Scenes.MainOut)
		{
			if (nowScenes != Scenes.MainIn)
			{
				if (nowScenes == Scenes.Shop)
				{
					base.GetComponentInParent<UIMaster_Shop>().BackFunc();
				}
			}
			else
			{
				base.GetComponentInParent<UIMaster_MainIn>().BackFunc();
			}
		}
		else
		{
			base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0000E774 File Offset: 0x0000CB74
	public void IAP_ButtonPush()
	{
		this.BtnBlocker.SetActive(true);
		SuperGameMaster.IAPCallBackCntReset();
		switch (SuperGameMaster.GetNowScenes())
		{
		case Scenes.MainOut:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SaveAndStopReload(true);
			break;
		case Scenes.MainIn:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainIn>().SaveAndStopReload(true);
			break;
		case Scenes.Shop:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_Shop>().SaveAndStopReload(true);
			break;
		default:
			SuperGameMaster.SaveData();
			break;
		}
		base.GetComponentInParent<UIMaster>().BackFunc_Stop(true);
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0000E818 File Offset: 0x0000CC18
	public void IAP_Complete(Product product)
	{
		SuperGameMaster.IAPCallBackCntReset();
		int num = 0;
		string id = product.definition.id;
		if (id != null)
		{
			if (!(id == "CLOVER_ADD_1"))
			{
				if (!(id == "CLOVER_ADD_2"))
				{
					if (!(id == "CLOVER_ADD_3"))
					{
						if (id == "CLOVER_ADD_4")
						{
							num = 2800;
						}
					}
					else
					{
						num = 1800;
					}
				}
				else
				{
					num = 1000;
				}
			}
			else
			{
				num = 400;
			}
		}
		SuperGameMaster.getCloverPoint(num);
		switch (SuperGameMaster.GetNowScenes())
		{
		case Scenes.MainOut:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SaveAndStopReload(true);
			break;
		case Scenes.MainIn:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainIn>().SaveAndStopReload(true);
			break;
		case Scenes.Shop:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_Shop>().SaveAndStopReload(true);
			break;
		default:
			SuperGameMaster.SaveData();
			break;
		}
		this.BtnBlocker.SetActive(false);
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel("みつ葉" + num + "\nを購入しました");
		confilm.ResetOnClick_Screen();
		confilm.SetOnClick_Screen(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.GetComponentInParent<UIMaster>().BackFunc_Stop(false);
		});
		switch (SuperGameMaster.GetNowScenes())
		{
		case Scenes.MainOut:
			confilm.SetOnClick_Screen(delegate
			{
				this.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SetStopReload(false);
			});
			break;
		case Scenes.MainIn:
			confilm.SetOnClick_Screen(delegate
			{
				this.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainIn>().SetStopReload(false);
			});
			break;
		case Scenes.Shop:
			confilm.SetOnClick_Screen(delegate
			{
				this.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_Shop>().SetStopReload(false);
			});
			break;
		default:
			SuperGameMaster.SaveData();
			break;
		}
	}

	// Token: 0x0600030F RID: 783 RVA: 0x0000EA34 File Offset: 0x0000CE34
	public void IAP_Failed(Product product, PurchaseFailureReason reason)
	{
		SuperGameMaster.IAPCallBackCntReset();
		this.BtnBlocker.SetActive(false);
		switch (SuperGameMaster.GetNowScenes())
		{
		case Scenes.MainOut:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SaveAndStopReload(false);
			break;
		case Scenes.MainIn:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainIn>().SaveAndStopReload(false);
			break;
		case Scenes.Shop:
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_Shop>().SaveAndStopReload(false);
			break;
		default:
			SuperGameMaster.SaveData();
			break;
		}
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel("購入できませんでした");
		confilm.ResetOnClick_Screen();
		confilm.SetOnClick_Screen(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.GetComponentInParent<UIMaster>().BackFunc_Stop(false);
		});
	}

	// Token: 0x06000310 RID: 784 RVA: 0x0000EB34 File Offset: 0x0000CF34
	public void PushUserPolicy()
	{
		string url = string.Empty;
		url = Define.URL_UserPolicy_Android;
		if (this.UserPolicyUI.GetComponent<WebViewPanel>().OpenPanel(url, WebViewPanel.Back.IAPPanel))
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x040001A7 RID: 423
	public GameObject BtnBlocker;

	// Token: 0x040001A8 RID: 424
	public GameObject ConfilmUI;

	// Token: 0x040001A9 RID: 425
	public GameObject UserPolicyUI;
}
