using System;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000071 RID: 113
public class ItemScrollView : MonoBehaviour
{
	// Token: 0x060003D4 RID: 980 RVA: 0x000180B8 File Offset: 0x000164B8
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CloseScrollView(-1);
		});
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x000180E4 File Offset: 0x000164E4
	public void OpenScrollView(GameObject _callObj, ItemScrollView.Mode _setListMode)
	{
		this.CallObj = _callObj;
		this.listMode = _setListMode;
		this.OpenScrollView();
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x000180FA File Offset: 0x000164FA
	public void OpenScrollView(GameObject _callObj, ItemScrollView.Mode _setListMode, int _setId)
	{
		this.CallObj = _callObj;
		this.listMode = _setListMode;
		this.setId = _setId;
		this.OpenScrollView();
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00018117 File Offset: 0x00016517
	public void OpenScrollView(GameObject _callObj, ItemScrollView.Mode _setListMode, int _setId, Item.Type _itemType)
	{
		this.CallObj = _callObj;
		this.listMode = _setListMode;
		this.setId = _setId;
		this.setItemType = _itemType;
		this.OpenScrollView();
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0001813C File Offset: 0x0001653C
	public void OpenScrollView()
	{
		base.gameObject.SetActive(true);
		ItemScrollView.Mode mode = this.listMode;
		if (mode != ItemScrollView.Mode.View)
		{
			if (mode != ItemScrollView.Mode.Present)
			{
				if (mode == ItemScrollView.Mode.Equip)
				{
					this.CreateButton(this.setItemType);
					this.MaskItemType((int)this.setItemType, true);
					SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
				}
			}
			else
			{
				base.GetComponentInParent<UIMaster>().freezeObject(true);
				base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
				this.setItemType = Item.Type.Specialty;
				this.CreateButton(this.setItemType);
				this.MaskItemType((int)this.setItemType, true);
				SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
			}
		}
		else
		{
			base.GetComponentInParent<UIMaster>().freezeObject(true);
			base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
			this.setItemType = Item.Type.LunchBox;
			this.CreateButton();
			this.MaskItemType((int)this.setItemType, false);
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		}
		this.BackFunc();
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0001828C File Offset: 0x0001668C
	public void CloseScrollView(int selectItemId)
	{
		this.DeleteButtonAll();
		base.gameObject.SetActive(false);
		this.DeleteButtonAll();
		ItemScrollView.Mode mode = this.listMode;
		if (mode != ItemScrollView.Mode.View)
		{
			if (mode != ItemScrollView.Mode.Present)
			{
				if (mode == ItemScrollView.Mode.Equip)
				{
					base.gameObject.SetActive(false);
					if (selectItemId != -1)
					{
						if (selectItemId != -2)
						{
							this.CallObj.GetComponent<BagPanel>().CloseBagScrollView1Result(this.setItemType, this.setId, selectItemId);
							SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
						}
						else
						{
							this.CallObj.GetComponent<BagPanel>().CloseBagScrollView1Result(Item.Type._ElmMax, this.setId, selectItemId);
							SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
						}
					}
					else
					{
						this.CallObj.GetComponent<BagPanel>().CloseBagScrollView1Result(Item.Type.NONE, this.setId, selectItemId);
						SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
					}
					this.CallObj.GetComponentInParent<BagDeskPanels>().BackFunc();
				}
			}
			else
			{
				base.gameObject.SetActive(false);
				base.GetComponentInParent<UIMaster>().freezeObject(false);
				base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
				this.CallObj.GetComponent<CharaTable>().selectedItem(this.setId, selectItemId);
				base.gameObject.SetActive(false);
				this.DeleteButtonAll();
				if (selectItemId == -1)
				{
					SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
				}
				base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
			}
		}
		else
		{
			base.gameObject.SetActive(false);
			base.GetComponentInParent<UIMaster>().freezeObject(false);
			base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			base.gameObject.SetActive(false);
			this.DeleteButtonAll();
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
			Scenes nowScenes = SuperGameMaster.GetNowScenes();
			if (nowScenes != Scenes.MainOut)
			{
				if (nowScenes == Scenes.MainIn)
				{
					base.GetComponentInParent<UIMaster_MainIn>().BackFunc();
				}
			}
			else
			{
				base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
			}
		}
	}

	// Token: 0x060003DA RID: 986 RVA: 0x000184E9 File Offset: 0x000168E9
	public void CreateButton()
	{
		this.CreateButton(Item.Type.NONE);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000184F4 File Offset: 0x000168F4
	public void CreateButton(Item.Type itemType)
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		this.DeleteButtonAll();
		List<int> list = new List<int>();
		if (this.listMode == ItemScrollView.Mode.Equip)
		{
			list = this.CallObj.GetComponent<BagPanel>().Get_tmpItemListAll();
		}
		else
		{
			list.AddRange(SuperGameMaster.GetBagList());
			list.AddRange(SuperGameMaster.GetDeskList());
		}
		using (List<ItemListFormat>.Enumerator enumerator = SuperGameMaster.saveData.ItemList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ItemListFormat item = enumerator.Current;
				ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(item.id);
				if (itemDataFormat != null)
				{
					if (itemType == Item.Type.NONE || itemDataFormat.type == itemType)
					{
						GameObject gameObject;
						if (itemDataFormat.type != Item.Type.Specialty)
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.btnPref);
							gameObject.transform.SetParent(component, false);
							gameObject.GetComponent<ItemButton>().CngItemName(itemDataFormat.name);
							gameObject.GetComponent<ItemButton>().CngItemInfo(itemDataFormat.info);
						}
						else
						{
							gameObject = UnityEngine.Object.Instantiate<GameObject>(this.spe_btnPref);
							gameObject.transform.SetParent(this.spe_contentsList.GetComponent<RectTransform>(), false);
						}
						gameObject.GetComponent<ItemButton>().setItemId(itemDataFormat.id, itemDataFormat.type);
						gameObject.GetComponent<ItemButton>().CngImage(itemDataFormat.img);
						gameObject.GetComponent<ItemButton>().ItemScrollView = base.gameObject;
						ItemScrollView.Mode mode = this.listMode;
						if (mode != ItemScrollView.Mode.View)
						{
							if (mode != ItemScrollView.Mode.Present)
							{
								if (mode == ItemScrollView.Mode.Equip)
								{
									if (itemDataFormat.spend)
									{
										int num = item.stock;
										if (list.FindIndex((int itemId) => itemId.Equals(item.id)) != -1)
										{
											foreach (int num2 in list)
											{
												if (num2 == item.id)
												{
													num--;
												}
											}
										}
										gameObject.GetComponent<ItemButton>().CngStockNum(num);
										if (this.CallObj.GetComponent<BagPanel>().tmpBagList[this.setId] == itemDataFormat.id)
										{
											gameObject.GetComponent<ItemButton>().Fade(new Color(0.7f, 0.7f, 0.7f));
											gameObject.GetComponent<ItemButton>().SetCheckImage(true);
											gameObject.GetComponent<ItemButton>().setItemId(-2, Item.Type._ElmMax);
										}
										else if (num <= 0)
										{
											UnityEngine.Object.Destroy(gameObject);
										}
									}
									else
									{
										if (list.FindIndex((int itemId) => itemId.Equals(item.id)) != -1)
										{
											gameObject.GetComponent<Button>().interactable = false;
											if (this.CallObj.GetComponent<BagPanel>().tmpBagList[this.setId] == itemDataFormat.id)
											{
												gameObject.GetComponent<ItemButton>().SetCheckImage(true);
												gameObject.GetComponent<ItemButton>().Fade(new Color(0.7f, 0.7f, 0.7f));
												gameObject.GetComponent<ItemButton>().setItemId(-2, Item.Type._ElmMax);
												gameObject.GetComponent<Button>().interactable = true;
											}
											else
											{
												gameObject.GetComponent<ItemButton>().Fade(new Color(0.7f, 0.7f, 0.7f));
											}
										}
										gameObject.GetComponent<ItemButton>().CngStockNum(-2);
									}
								}
							}
							else if (itemDataFormat.spend)
							{
								int num3 = item.stock;
								if (list.FindIndex((int itemId) => itemId.Equals(item.id)) != -1)
								{
									foreach (int num4 in list)
									{
										if (num4 == item.id)
										{
											num3--;
										}
									}
								}
								if (num3 > 0)
								{
									gameObject.GetComponent<ItemButton>().CngStockNum(num3);
								}
								else
								{
									UnityEngine.Object.Destroy(gameObject);
								}
							}
							else
							{
								gameObject.GetComponent<ItemButton>().CngStockNum(-2);
							}
						}
						else
						{
							gameObject.GetComponent<Button>().enabled = false;
							if (itemDataFormat.spend)
							{
								int num5 = item.stock;
								if (list.FindIndex((int itemId) => itemId.Equals(item.id)) != -1)
								{
									foreach (int num6 in list)
									{
										if (num6 == item.id)
										{
											num5--;
										}
									}
								}
								if (num5 > 0)
								{
									gameObject.GetComponent<ItemButton>().CngStockNum(num5);
								}
								else
								{
									UnityEngine.Object.Destroy(gameObject);
								}
							}
							else
							{
								gameObject.GetComponent<ItemButton>().CngStockNum(-2);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00018A68 File Offset: 0x00016E68
	public void DeleteButtonAll()
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
		}
		component = this.spe_contentsList.GetComponent<RectTransform>();
		for (int j = 0; j < component.transform.childCount; j++)
		{
			UnityEngine.Object.Destroy(component.GetChild(j).gameObject);
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00018AE7 File Offset: 0x00016EE7
	public void MaskItemType(int btnTypeId)
	{
		this.MaskItemType(btnTypeId, false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00018B0C File Offset: 0x00016F0C
	public void MaskItemType(int btnTypeId, bool fixFlag)
	{
		this.setItemType = (Item.Type)btnTypeId;
		for (int i = 0; i < this.TabButtons.Length; i++)
		{
			if (fixFlag)
			{
				if (btnTypeId == i)
				{
					this.TabButtons[i].GetComponent<Button>().enabled = true;
					this.TabButtons[i].GetComponent<Button>().interactable = false;
					this.TabButtons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f);
				}
				else
				{
					this.TabButtons[i].GetComponent<Button>().enabled = false;
					this.TabButtons[i].GetComponent<Button>().interactable = false;
					this.TabButtons[i].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
				}
			}
			else
			{
				this.TabButtons[i].GetComponent<Image>().color = new Color(1f, 1f, 1f);
				if (!this.TabButtons[i].activeSelf)
				{
					this.TabButtons[i].SetActive(true);
				}
				if (!this.TabButtons[i].GetComponent<Button>().enabled)
				{
					this.TabButtons[i].GetComponent<Button>().enabled = true;
				}
				if (btnTypeId == i)
				{
					this.TabButtons[i].GetComponent<Button>().interactable = false;
				}
				else
				{
					this.TabButtons[i].GetComponent<Button>().interactable = true;
				}
			}
		}
		if (!fixFlag)
		{
			RectTransform component = this.contentsList.GetComponent<RectTransform>();
			for (int j = 0; j < component.transform.childCount; j++)
			{
				if (this.setItemType == component.GetChild(j).GetComponent<ItemButton>().itemType)
				{
					component.GetChild(j).gameObject.SetActive(true);
				}
				else
				{
					component.GetChild(j).gameObject.SetActive(false);
				}
			}
		}
		if (this.setItemType != Item.Type.Specialty)
		{
			this.contentsList.SetActive(true);
			this.scrollbar.GetComponent<Image>().enabled = true;
			this.scrollbar.GetComponent<Scrollbar>().targetGraphic.GetComponent<Image>().enabled = true;
			this.spe_ScrollView.SetActive(false);
		}
		else
		{
			this.contentsList.SetActive(false);
			this.scrollbar.GetComponent<Image>().enabled = false;
			this.scrollbar.GetComponent<Scrollbar>().targetGraphic.GetComponent<Image>().enabled = false;
			this.scrollbar.GetComponent<Image>().enabled = false;
			this.spe_ScrollView.SetActive(true);
		}
	}

	// Token: 0x0400026D RID: 621
	public GameObject btnPref;

	// Token: 0x0400026E RID: 622
	public GameObject contentsList;

	// Token: 0x0400026F RID: 623
	public GameObject scrollbar;

	// Token: 0x04000270 RID: 624
	[Header("specialty")]
	public GameObject spe_btnPref;

	// Token: 0x04000271 RID: 625
	public GameObject spe_ScrollView;

	// Token: 0x04000272 RID: 626
	public GameObject spe_contentsList;

	// Token: 0x04000273 RID: 627
	public GameObject[] TabButtons;

	// Token: 0x04000274 RID: 628
	public GameObject CallObj;

	// Token: 0x04000275 RID: 629
	public ItemScrollView.Mode listMode;

	// Token: 0x04000276 RID: 630
	public int setId;

	// Token: 0x04000277 RID: 631
	public Item.Type setItemType;

	// Token: 0x02000072 RID: 114
	public enum Mode
	{
		// Token: 0x04000279 RID: 633
		NONE = -1,
		// Token: 0x0400027A RID: 634
		View,
		// Token: 0x0400027B RID: 635
		Present,
		// Token: 0x0400027C RID: 636
		Equip
	}
}
