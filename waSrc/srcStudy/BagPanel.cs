using System;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000048 RID: 72
public class BagPanel : MonoBehaviour
{
	// Token: 0x06000293 RID: 659 RVA: 0x00009E94 File Offset: 0x00008294
	public void Enable()
	{
		if (!this.DeskPanel)
		{
			this.tmpBagList = SuperGameMaster.GetBagList_virtual();
			if (this.tmpBagList.Count == 0)
			{
				this.tmpBagList = SuperGameMaster.GetBagList();
			}
		}
		else
		{
			this.tmpBagList = SuperGameMaster.GetDeskList_virtual();
			if (this.tmpBagList.Count == 0)
			{
				this.tmpBagList = SuperGameMaster.GetDeskList();
			}
		}
		this.SetPanelButton();
		if (!this.DeskPanel)
		{
			this.ChangeBtnEnable(SuperGameMaster.GetStandby());
		}
		else
		{
			this.ResetBtnEnableCheck();
		}
		this.ChangeButton.SetActive(SuperGameMaster.GetHome());
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00009F34 File Offset: 0x00008334
	public void Disable()
	{
	}

	// Token: 0x06000295 RID: 661 RVA: 0x00009F36 File Offset: 0x00008336
	public void OpenBagScrollView(Item.Type itemType, int btnId)
	{
		this.ChangeButton.GetComponent<Button>().enabled = false;
		base.GetComponentInParent<FlickCheaker>().stopFlick(true);
		this.ItemView.GetComponent<ItemScrollView>().OpenScrollView(base.gameObject, ItemScrollView.Mode.Equip, btnId, itemType);
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00009F70 File Offset: 0x00008370
	public void CloseBagScrollView1Result(Item.Type itemType, int btnId, int itemId)
	{
		this.ChangeButton.GetComponent<Button>().enabled = true;
		base.GetComponentInParent<FlickCheaker>().stopFlick(false);
		if (itemType == Item.Type.NONE)
		{
			return;
		}
		if (itemType != Item.Type._ElmMax)
		{
			ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(itemId);
			if (itemDataFormat == null)
			{
				return;
			}
			this.BagButton[btnId].GetComponent<BagPanelButton>().CngItemImage(itemDataFormat.img);
			this.tmpBagList[btnId] = itemId;
		}
		else
		{
			this.BagButton[btnId].GetComponent<BagPanelButton>().CngItemImage(this.DefaultSplite);
			this.tmpBagList[btnId] = -1;
		}
		if (this.DeskPanel)
		{
			this.ResetBtnEnableCheck();
		}
	}

	// Token: 0x06000297 RID: 663 RVA: 0x0000A01C File Offset: 0x0000841C
	public void SetPanelButton()
	{
		for (int i = 0; i < this.tmpBagList.Count; i++)
		{
			int num = this.tmpBagList[i];
			ItemDataFormat itemDataFormat = new ItemDataFormat();
			if (num != -1)
			{
				itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(num);
				this.BagButton[i].GetComponent<BagPanelButton>().CngItemImage(itemDataFormat.img);
			}
			else
			{
				this.BagButton[i].GetComponent<BagPanelButton>().CngItemImage(this.DefaultSplite);
			}
		}
	}

	// Token: 0x06000298 RID: 664 RVA: 0x0000A0A0 File Offset: 0x000084A0
	public void SaveBagList()
	{
		if (!this.DeskPanel)
		{
			List<int> list = new List<int>();
			foreach (int item in this.tmpBagList)
			{
				list.Add(item);
			}
			SuperGameMaster.SaveBagList(list);
			SuperGameMaster.SaveBagList_virtual(list);
		}
		else
		{
			List<int> list2 = new List<int>();
			foreach (int item2 in this.tmpBagList)
			{
				list2.Add(item2);
			}
			SuperGameMaster.SaveDeskList(list2);
			SuperGameMaster.SaveDeskList_virtual(list2);
		}
	}

	// Token: 0x06000299 RID: 665 RVA: 0x0000A180 File Offset: 0x00008580
	public List<int> Get_tmpItemListAll()
	{
		return base.GetComponentInParent<BagDeskPanels>().Get_tmpListAll();
	}

	// Token: 0x0600029A RID: 666 RVA: 0x0000A190 File Offset: 0x00008590
	public void PushReset()
	{
		base.GetComponentInParent<FlickCheaker>().stopFlick(true);
		ConfilmPanel confilm = base.GetComponentInParent<BagDeskPanels>().ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel_YesNo("つくえにだしているものを\n全てかたづけますか？");
		confilm.ResetOnClick_Yes();
		confilm.SetOnClick_Yes(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.GetComponentInParent<FlickCheaker>().stopFlick(false);
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.DeskReset();
		});
		confilm.ResetOnClick_No();
		confilm.SetOnClick_No(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_No(delegate
		{
			this.GetComponentInParent<FlickCheaker>().stopFlick(false);
		});
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000A268 File Offset: 0x00008668
	public void DeskReset()
	{
		for (int i = 0; i < SuperGameMaster.saveData.deskList.Count; i++)
		{
			this.tmpBagList[i] = -1;
		}
		this.SetPanelButton();
		this.ActionBtn.GetComponent<Button>().interactable = false;
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000A2BC File Offset: 0x000086BC
	public void ResetBtnEnableCheck()
	{
		bool flag = true;
		foreach (int num in this.tmpBagList)
		{
			if (num != -1)
			{
				flag = false;
				break;
			}
		}
		this.ActionBtn.GetComponent<Button>().interactable = !flag;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000A338 File Offset: 0x00008738
	public void PushComplete()
	{
		if (!SuperGameMaster.GetStandby())
		{
			if (this.tmpBagList[0] != -1)
			{
				if (!SuperGameMaster.tutorial.ClockOk())
				{
					if (!base.GetComponentInParent<BagDeskPanels>().GetComponentInParent<UIMaster_MainIn>().BagCompleteCursor.GetComponentInChildren<Image>().enabled)
					{
						HelpPanel help = base.GetComponentInParent<BagDeskPanels>().GetComponentInParent<UIMaster_MainIn>().HelpUI.GetComponent<HelpPanel>();
						help.OpenPanel("おべんとうに「えびづるのスコーン」\nおまもりに「よつ葉」\nを選んで<color=#61a8c7><b>かんりょう</b></color>ボタンを\n押してください");
						help.ResetOnClick_Screen();
						help.SetOnClick_Screen(delegate
						{
							help.ClosePanel();
						});
						return;
					}
					base.GetComponentInParent<BagDeskPanels>().CloseCheck();
				}
				SuperGameMaster.SetStandby(true);
				SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
			}
			else
			{
				if (!SuperGameMaster.tutorial.ClockOk())
				{
					HelpPanel help = base.GetComponentInParent<BagDeskPanels>().GetComponentInParent<UIMaster_MainIn>().HelpUI.GetComponent<HelpPanel>();
					help.OpenPanel("おべんとうに「えびづるのスコーン」\nおまもりに「よつ葉」\nを選んで<color=#61a8c7><b>かんりょう</b></color>ボタンを\n押してください");
					help.ResetOnClick_Screen();
					help.SetOnClick_Screen(delegate
					{
						help.ClosePanel();
					});
					return;
				}
				base.GetComponentInParent<FlickCheaker>().stopFlick(true);
				ConfilmPanel confilm = base.GetComponentInParent<BagDeskPanels>().ConfilmUI.GetComponent<ConfilmPanel>();
				confilm.OpenPanel("おべんとうを支度してください");
				confilm.ResetOnClick_Screen();
				confilm.SetOnClick_Screen(delegate
				{
					confilm.ClosePanel();
				});
				confilm.SetOnClick_Screen(delegate
				{
					this.GetComponentInParent<FlickCheaker>().stopFlick(false);
				});
			}
		}
		else
		{
			SuperGameMaster.SetStandby(false);
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		}
		this.ChangeBtnEnable(SuperGameMaster.GetStandby());
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000A524 File Offset: 0x00008924
	public void ChangeBtnEnable(bool isStandby)
	{
		foreach (GameObject gameObject in this.BagButton)
		{
			if (isStandby)
			{
				gameObject.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
			}
			else
			{
				gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f);
			}
			gameObject.GetComponent<Button>().enabled = !isStandby;
		}
		if (isStandby)
		{
			this.ActionBtn.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
		}
		else
		{
			this.ActionBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f);
		}
	}

	// Token: 0x04000141 RID: 321
	public GameObject[] BagButton;

	// Token: 0x04000142 RID: 322
	public GameObject ItemView;

	// Token: 0x04000143 RID: 323
	public Sprite DefaultSplite;

	// Token: 0x04000144 RID: 324
	public GameObject ChangeButton;

	// Token: 0x04000145 RID: 325
	public GameObject ActionBtn;

	// Token: 0x04000146 RID: 326
	public GameObject CloseBtn;

	// Token: 0x04000147 RID: 327
	public bool DeskPanel;

	// Token: 0x04000148 RID: 328
	public List<int> tmpBagList;
}
