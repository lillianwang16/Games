using System;
using Prize;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000075 RID: 117
public class PrizeScrollView : MonoBehaviour
{
	// Token: 0x060003FD RID: 1021 RVA: 0x0001A0EC File Offset: 0x000184EC
	public void OpenScrollView(Rank setRank)
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.CreateButton(setRank);
		this.CloseButton.SetActive(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x0001A164 File Offset: 0x00018564
	public void CloseScrollView()
	{
		this.DeleteButtonAll();
		base.gameObject.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		SuperGameMaster.SetTmpRaffleResult(-1);
		base.GetComponentInParent<UIMaster>().OnSave();
		SuperGameMaster.audioMgr.StopSE();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Buy"]);
		base.GetComponentInParent<UIMaster_Raffle>().BackFunc();
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0001A1F4 File Offset: 0x000185F4
	public void SelectPrize(int prizeId)
	{
		this.selectPrizeId = prizeId;
		PrizeDataFormat prizeDataFormat = SuperGameMaster.sDataBase.get_PrizeDB_forId(this.selectPrizeId);
		ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(prizeDataFormat.itemId);
		if (prizeDataFormat.itemId != -1 && itemDataFormat == null)
		{
			return;
		}
		if (prizeDataFormat.itemId == -1)
		{
			itemDataFormat = new ItemDataFormat();
			itemDataFormat.name = "ふくびき券";
		}
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel_YesNo(itemDataFormat.name + "\nを受け取りますか");
		confilm.ResetOnClick_Yes();
		confilm.SetOnClick_Yes(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.GetPrize();
		});
		confilm.ResetOnClick_No();
		confilm.SetOnClick_No(delegate
		{
			confilm.ClosePanel();
		});
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0001A2F0 File Offset: 0x000186F0
	public void GetPrize()
	{
		PrizeDataFormat prizeDataFormat = SuperGameMaster.sDataBase.get_PrizeDB_forId(this.selectPrizeId);
		if (prizeDataFormat.itemId != -1)
		{
			SuperGameMaster.GetItem(prizeDataFormat.itemId, prizeDataFormat.stock);
		}
		else
		{
			SuperGameMaster.GetTicket(prizeDataFormat.stock);
		}
		this.CloseScrollView();
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0001A344 File Offset: 0x00018744
	public void CreateButton(Rank createRank)
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		float spacing = component.GetComponent<VerticalLayoutGroup>().spacing;
		float preferredHeight = this.btnPref.GetComponent<LayoutElement>().preferredHeight;
		bool flag = true;
		for (int i = 0; i < SuperGameMaster.sDataBase.count_PrizeDB(); i++)
		{
			PrizeDataFormat prizeDataFormat = SuperGameMaster.sDataBase.get_PrizeDB(i);
			if (createRank == Rank.NONE || prizeDataFormat.rank == createRank)
			{
				ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(prizeDataFormat.itemId);
				if (prizeDataFormat.itemId == -1 || itemDataFormat != null)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.btnPref);
					gameObject.transform.SetParent(component, false);
					gameObject.GetComponent<PrizeButton>().setPrizeId(prizeDataFormat.id, prizeDataFormat.rank);
					gameObject.GetComponent<PrizeButton>().CngStockNum(prizeDataFormat.stock);
					if (prizeDataFormat.itemId != -1)
					{
						gameObject.GetComponent<PrizeButton>().CngPrizeName(itemDataFormat.name);
						gameObject.GetComponent<PrizeButton>().CngImage(itemDataFormat.img);
						int num = SuperGameMaster.FindItemStock(prizeDataFormat.itemId);
						gameObject.GetComponent<PrizeButton>().CngHaveItemStock(num);
						if (num >= 99)
						{
							gameObject.GetComponent<Button>().interactable = false;
						}
					}
					else
					{
						gameObject.GetComponent<PrizeButton>().CngPrizeName("ふくびき券");
						int num2 = SuperGameMaster.TicketStock();
						gameObject.GetComponent<PrizeButton>().CngHaveItemStock(num2);
						if (num2 >= 999)
						{
							gameObject.GetComponent<Button>().interactable = false;
						}
					}
					if (gameObject.GetComponent<Button>().interactable)
					{
						flag = false;
					}
					gameObject.GetComponent<PrizeButton>().PrizeScrollViewUI = base.gameObject;
				}
			}
		}
		if (flag)
		{
			int num3 = Define.PrizeClover[createRank];
			SuperGameMaster.getCloverPoint(num3);
			base.GetComponentInParent<UIMaster>().OnSave();
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("みつ葉を" + num3 + "枚\n受け取りました");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				this.CloseScrollView();
			});
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0001A5AC File Offset: 0x000189AC
	public void DeleteButtonAll()
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
		}
	}

	// Token: 0x0400028D RID: 653
	public GameObject btnPref;

	// Token: 0x0400028E RID: 654
	public GameObject contentsList;

	// Token: 0x0400028F RID: 655
	public GameObject ConfilmUI;

	// Token: 0x04000290 RID: 656
	public GameObject CloseButton;

	// Token: 0x04000291 RID: 657
	public Rank setPrizeRank;

	// Token: 0x04000292 RID: 658
	public int selectPrizeId;
}
