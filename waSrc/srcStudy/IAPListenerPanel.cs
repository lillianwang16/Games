using System;
using UnityEngine;
using UnityEngine.Purchasing;

// Token: 0x02000055 RID: 85
public class IAPListenerPanel : MonoBehaviour
{
	// Token: 0x06000301 RID: 769 RVA: 0x0000E327 File Offset: 0x0000C727
	public void Active_IAPListener()
	{
		base.gameObject.SetActive(true);
		Debug.Log("[IAPListenerPanel] IAPListenerPanel が有効です");
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0000E340 File Offset: 0x0000C740
	public void IAP_Complete(Product product)
	{
		SuperGameMaster.IAPCallBackCntReset();
		base.GetComponentInParent<UIMaster>().BackFunc_Stop(true);
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
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes == Scenes.MainOut)
		{
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SaveAndStopReload(true);
		}
		this.confilm.OpenPanel("みつ葉" + num + "\nを購入しました");
		this.confilm.ResetOnClick_Screen();
		this.confilm.SetOnClick_Screen(delegate
		{
			this.confilm.ClosePanel();
		});
		this.confilm.SetOnClick_Screen(delegate
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Stop(false);
		});
		Scenes nowScenes2 = SuperGameMaster.GetNowScenes();
		if (nowScenes2 != Scenes.MainOut)
		{
			SuperGameMaster.SaveData();
		}
		else
		{
			this.confilm.SetOnClick_Screen(delegate
			{
				base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SetStopReload(false);
			});
		}
	}

	// Token: 0x06000303 RID: 771 RVA: 0x0000E4A8 File Offset: 0x0000C8A8
	public void IAP_Failed(Product product, PurchaseFailureReason reason)
	{
		SuperGameMaster.IAPCallBackCntReset();
		base.GetComponentInParent<UIMaster>().BackFunc_Stop(true);
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes == Scenes.MainOut)
		{
			base.GetComponentInParent<UIMaster>().GameMaster.GetComponent<GameMaster_MainOut>().SaveAndStopReload(false);
		}
		this.confilm.OpenPanel("購入できませんでした");
		this.confilm.ResetOnClick_Screen();
		this.confilm.SetOnClick_Screen(delegate
		{
			this.confilm.ClosePanel();
		});
		this.confilm.SetOnClick_Screen(delegate
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Stop(false);
		});
	}

	// Token: 0x040001A6 RID: 422
	[SerializeField]
	private ConfilmPanel confilm;
}
