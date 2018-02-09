using System;
using System.Collections;
using Item;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000051 RID: 81
public class DisplayPanel : MonoBehaviour
{
	// Token: 0x060002D9 RID: 729 RVA: 0x0000C720 File Offset: 0x0000AB20
	public void Init()
	{
		this.selectShopIndex = -1;
		this.shopItemMax = SuperGameMaster.sDataBase.count_ShopDB();
		this.CreateItemButton();
		this.SetInfoPanelData(-1, Vector3.zero);
		this.S_FlickChecker = base.GetComponent<FlickCheaker>();
		Vector2 v = this.InfoPanel.transform.parent.transform.localPosition;
		float num = Camera.main.orthographicSize / 4.8f;
		v.y -= (num * 960f - 960f) / 4.45f;
		this.InfoPanel.transform.parent.transform.localPosition = v;
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0000C7D4 File Offset: 0x0000ABD4
	public void CreateItemButton()
	{
		this.unsetCursor();
		for (int i = -1; i <= 1; i++)
		{
			for (int j = 0; j < this.pageDisplayMax; j++)
			{
				int num = j + (this.nowPage + i) * this.pageDisplayMax;
				if (num < 0)
				{
					break;
				}
				if (num > this.shopItemMax - 1)
				{
					break;
				}
				Vector3 position = new Vector3(this.btnPref.transform.localPosition.x, this.btnPref.transform.localPosition.y, 0f);
				position.x += (float)(j % this.column) * this.btnPadding.x + (float)(this.pageWidth * i);
				position.y += (float)(j / this.column) * this.btnPadding.y;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.btnPref, position, Quaternion.identity);
				gameObject.transform.SetParent(base.transform, false);
				ShopDataFormat shopData = SuperGameMaster.sDataBase.get_ShopDB(num);
				ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(shopData.itemId);
				gameObject.GetComponent<DisplayItemButton>().setShopIndex(num);
				gameObject.GetComponent<DisplayItemButton>().CngDisplayName(itemDataFormat.name);
				gameObject.GetComponent<DisplayItemButton>().CngDisplayImage(itemDataFormat.img, shopData.fixY);
				gameObject.GetComponent<DisplayItemButton>().CngBackImage(this.ItemBackSprite[j % 4]);
				if (num == this.selectShopIndex)
				{
					this.setCursor(gameObject.transform.localPosition);
				}
				if (itemDataFormat.type != Item.Type.LunchBox)
				{
					if (SuperGameMaster.saveData.ItemList.FindIndex((ItemListFormat item) => item.id.Equals(shopData.itemId)) != -1)
					{
						gameObject.GetComponent<DisplayItemButton>().SetSoldOut();
					}
					else
					{
						gameObject.GetComponent<DisplayItemButton>().CngPriceNum(itemDataFormat.price);
					}
				}
				else
				{
					gameObject.GetComponent<DisplayItemButton>().CngPriceNum(itemDataFormat.price);
				}
			}
		}
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0000CA00 File Offset: 0x0000AE00
	public void PanelUpDate()
	{
		base.GetComponent<FlickCheaker>().FlickUpdate();
		if (Input.GetMouseButton(0))
		{
			if (this.S_FlickChecker.nowFlickVector() != Vector2.zero)
			{
				this.flickMove = this.S_FlickChecker.nowFlickVector().x / this.flickMoveAttract;
				if (Mathf.Abs(this.flickMove) > this.flickMoveMax)
				{
					this.flickMove = Mathf.Sign(this.flickMove) * this.flickMoveMax;
				}
			}
		}
		else if (this.flickMove != 0f)
		{
			this.flickMove /= this.flickMoveVector;
		}
		if (this.flickMove != 0f)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.name.Equals("DisplayItemButton(Clone)"))
					{
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			Vector3 localPosition = base.transform.localPosition;
			localPosition.x = this.flickMove;
			base.transform.localPosition = localPosition;
			if ((double)Mathf.Abs(this.flickMove) < 0.1)
			{
				this.flickMove = 0f;
			}
		}
	}

	// Token: 0x060002DC RID: 732 RVA: 0x0000CB78 File Offset: 0x0000AF78
	public void PushPrev()
	{
		if (this.nowPage == 0)
		{
			return;
		}
		this.nowPage--;
		if (this.nowPage == 0)
		{
			this.PrevBtn.SetActive(false);
		}
		if (!this.NextBtn.gameObject.activeSelf)
		{
			this.NextBtn.SetActive(true);
		}
		this.allDisplayButtonDelete();
		this.CreateItemButton();
		this.flickMove -= (float)this.pageWidth;
		if (this.flickMove < (float)(-(float)this.pageWidth))
		{
			this.flickMove = (float)(-(float)this.pageWidth);
		}
		this.PatternImage[0].GetComponent<Image>().sprite = this.PatternSprite[this.nowPage % 2];
		this.PatternImage[1].GetComponent<Image>().sprite = this.PatternSprite[(this.nowPage + 1) % 2];
		this.PatternImage[2].GetComponent<Image>().sprite = this.PatternSprite[(this.nowPage + 1) % 2];
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0000CC98 File Offset: 0x0000B098
	public void PushNext()
	{
		if ((this.nowPage + 1) * this.pageDisplayMax >= this.shopItemMax)
		{
			return;
		}
		this.nowPage++;
		if ((this.nowPage + 1) * this.pageDisplayMax >= this.shopItemMax)
		{
			this.NextBtn.SetActive(false);
		}
		if (!this.PrevBtn.gameObject.activeSelf)
		{
			this.PrevBtn.SetActive(true);
		}
		this.allDisplayButtonDelete();
		this.CreateItemButton();
		this.flickMove += (float)this.pageWidth;
		if (this.flickMove > (float)this.pageWidth)
		{
			this.flickMove = (float)this.pageWidth;
		}
		this.PatternImage[0].GetComponent<Image>().sprite = this.PatternSprite[this.nowPage % 2];
		this.PatternImage[1].GetComponent<Image>().sprite = this.PatternSprite[(this.nowPage + 1) % 2];
		this.PatternImage[2].GetComponent<Image>().sprite = this.PatternSprite[(this.nowPage + 1) % 2];
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0000CDD4 File Offset: 0x0000B1D4
	public void allDisplayButtonDelete()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.Equals("DisplayItemButton(Clone)"))
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0000CE54 File Offset: 0x0000B254
	public void SetInfoPanelData(int shopIndex, Vector3 pos)
	{
		if (shopIndex == -1)
		{
			this.unsetCursor();
			this.InfoPanel.GetComponent<InfoPanel>().SetInfoPanel(-1);
			return;
		}
		if (Mathf.Abs(this.flickMove) > this.S_FlickChecker.flickMin / 3f)
		{
			return;
		}
		if (this.selectShopIndex != shopIndex)
		{
			this.InfoPanel.GetComponent<InfoPanel>().SetInfoPanel(shopIndex);
			this.selectShopIndex = shopIndex;
			this.setCursor(pos);
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
		}
		else
		{
			ShopDataFormat shopDataFormat = SuperGameMaster.sDataBase.get_ShopDB(shopIndex);
			ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(shopDataFormat.itemId);
			if (itemDataFormat == null)
			{
				return;
			}
			if (!itemDataFormat.spend && SuperGameMaster.FindItemStock(itemDataFormat.id) != 0)
			{
				SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
				return;
			}
			if (SuperGameMaster.CloverPointStock() >= itemDataFormat.price)
			{
				if (SuperGameMaster.FindItemStock(shopDataFormat.itemId) < 99)
				{
					base.GetComponent<FlickCheaker>().stopFlick(true);
					ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
					if (itemDataFormat.type == Item.Type.LunchBox)
					{
						confilm.OpenPanel_YesNo(string.Concat(new object[]
						{
							itemDataFormat.name,
							"\nを買いますか？\n（所持数\u3000",
							SuperGameMaster.FindItemStock(shopDataFormat.itemId),
							"）"
						}));
					}
					else
					{
						confilm.OpenPanel_YesNo(itemDataFormat.name + "\nを買いますか？");
					}
					confilm.ResetOnClick_Yes();
					confilm.SetOnClick_Yes(delegate
					{
						confilm.ClosePanel();
					});
					confilm.SetOnClick_Yes(delegate
					{
						this.GetComponent<FlickCheaker>().stopFlick(false);
					});
					confilm.SetOnClick_Yes(delegate
					{
						this.BuyItem();
					});
					confilm.ResetOnClick_No();
					confilm.SetOnClick_No(delegate
					{
						confilm.ClosePanel();
					});
					confilm.SetOnClick_No(delegate
					{
						this.GetComponent<FlickCheaker>().stopFlick(false);
					});
				}
				else
				{
					base.GetComponent<FlickCheaker>().stopFlick(true);
					ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
					confilm.OpenPanel("もちものがいっぱいです");
					confilm.ResetOnClick_Screen();
					confilm.SetOnClick_Screen(delegate
					{
						confilm.ClosePanel();
					});
					confilm.SetOnClick_Screen(delegate
					{
						this.GetComponent<FlickCheaker>().stopFlick(false);
					});
				}
			}
			else
			{
				base.GetComponent<FlickCheaker>().stopFlick(true);
				ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
				confilm.OpenPanel("みつ葉が足りません");
				confilm.ResetOnClick_Screen();
				confilm.SetOnClick_Screen(delegate
				{
					confilm.ClosePanel();
				});
				confilm.SetOnClick_Screen(delegate
				{
					this.GetComponent<FlickCheaker>().stopFlick(false);
				});
			}
		}
	}

	// Token: 0x060002E0 RID: 736 RVA: 0x0000D180 File Offset: 0x0000B580
	public void BuyItem()
	{
		ShopDataFormat shopDataFormat = SuperGameMaster.sDataBase.get_ShopDB(this.selectShopIndex);
		ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(shopDataFormat.itemId);
		SuperGameMaster.getCloverPoint(-itemDataFormat.price);
		SuperGameMaster.GetItem(shopDataFormat.itemId, 1);
		base.GetComponentInParent<UIMaster>().OnSave();
		if (itemDataFormat.type != Item.Type.LunchBox)
		{
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.name.Equals("DisplayItemButton(Clone)") && transform.GetComponent<DisplayItemButton>().shopIndex == this.selectShopIndex)
					{
						transform.GetComponent<DisplayItemButton>().SetSoldOut();
						transform.GetComponent<DisplayItemButton>().NumDelete();
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			this.selectShopIndex = -1;
			this.SetInfoPanelData(-1, Vector3.zero);
			base.GetComponent<FlickCheaker>().stopFlick(true);
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel(itemDataFormat.name + "\nを買いました");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.GetComponent<FlickCheaker>().stopFlick(false);
			});
		}
		if (SuperGameMaster.tutorial.TutorialComplete() && (itemDataFormat.type != Item.Type.Tool || SuperGameMaster.GetFirstFlag(Flag.FIRST_BUY_TOOL)) && UnityEngine.Random.Range(0, 100) < 15)
		{
			SuperGameMaster.GetTicket(1);
			base.GetComponentInParent<UIMaster>().OnSave();
			if (itemDataFormat.type != Item.Type.LunchBox)
			{
				ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
				confilm.SetOnClick_Screen(delegate
				{
					confilm.OpenPanel(string.Empty);
				});
				confilm.SetOnClick_Screen(delegate
				{
					confilm.AddContents(UnityEngine.Object.Instantiate<GameObject>(this.AddConfirm_pref));
				});
				confilm.SetOnClick_Screen(delegate
				{
					confilm.ResetOnClick_Screen();
				});
				confilm.SetOnClick_Screen(delegate
				{
					this.GetComponent<FlickCheaker>().stopFlick(true);
				});
				confilm.SetOnClick_Screen(delegate
				{
					confilm.SetOnClick_Screen(delegate
					{
						confilm.ClosePanel();
					});
				});
				confilm.SetOnClick_Screen(delegate
				{
					confilm.SetOnClick_Screen(delegate
					{
						this.GetComponent<FlickCheaker>().stopFlick(false);
					});
				});
			}
			else
			{
				base.GetComponent<FlickCheaker>().stopFlick(true);
				ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
				confilm.OpenPanel(string.Empty);
				confilm.AddContents(UnityEngine.Object.Instantiate<GameObject>(this.AddConfirm_pref));
				confilm.ResetOnClick_Screen();
				confilm.SetOnClick_Screen(delegate
				{
					confilm.ClosePanel();
				});
				confilm.SetOnClick_Screen(delegate
				{
					this.GetComponent<FlickCheaker>().stopFlick(false);
				});
			}
		}
		if (itemDataFormat.type == Item.Type.Tool && !SuperGameMaster.GetFirstFlag(Flag.FIRST_BUY_TOOL))
		{
			SuperGameMaster.SetFirstFlag(Flag.FIRST_BUY_TOOL);
			base.GetComponent<FlickCheaker>().stopFlick(true);
			HelpPanel help = base.GetComponentInParent<UIMaster_Shop>().HelpUI.GetComponent<HelpPanel>();
			help.OpenPanel(string.Concat(new string[]
			{
				"<color=#61a8c7><b>どうぐ</b></color>をしたくしてあげると\n",
				SuperGameMaster.GetFrogName(),
				"の行き先に影響をあたえます\n色んな<color=#61a8c7><b>どうぐ</b></color>を用意して",
				SuperGameMaster.GetFrogName(),
				"に\n旅の提案をしてあげましょう"
			}));
			help.ResetOnClick_Screen();
			help.SetOnClick_Screen(delegate
			{
				help.ClosePanel();
			});
			help.SetOnClick_Screen(delegate
			{
				this.GetComponent<FlickCheaker>().stopFlick(false);
			});
			help.gameObject.SetActive(false);
			ConfilmPanel component = this.ConfilmUI.GetComponent<ConfilmPanel>();
			component.SetOnClick_Screen(delegate
			{
				help.gameObject.SetActive(true);
			});
			component.SetOnClick_Screen(delegate
			{
				this.GetComponent<FlickCheaker>().stopFlick(true);
			});
		}
		SuperGameMaster.audioMgr.StopSE();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Buy"]);
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x0000D5DC File Offset: 0x0000B9DC
	public void setCursor(Vector3 pos)
	{
		this.DisplayCursorImage.SetActive(true);
		this.DisplayCursorImage.transform.localPosition = pos;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0000D5FB File Offset: 0x0000B9FB
	public void setCursor(Vector3 pos, float alpha)
	{
		this.setCursor(pos);
		this.DisplayCursorImage.GetComponent<CanvasGroup>().alpha = alpha;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0000D615 File Offset: 0x0000BA15
	public void unsetCursor()
	{
		this.DisplayCursorImage.SetActive(false);
		this.DisplayCursorImage.GetComponent<CanvasGroup>().alpha = 1f;
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0000D638 File Offset: 0x0000BA38
	public int GetSelectShopIndex()
	{
		return this.selectShopIndex;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0000D640 File Offset: 0x0000BA40
	public void ResetSelectShopIndex()
	{
		this.selectShopIndex = -1;
	}

	// Token: 0x04000180 RID: 384
	public GameObject ConfilmUI;

	// Token: 0x04000181 RID: 385
	public GameObject AddConfirm_pref;

	// Token: 0x04000182 RID: 386
	[Space(10f)]
	public GameObject btnPref;

	// Token: 0x04000183 RID: 387
	public GameObject PrevBtn;

	// Token: 0x04000184 RID: 388
	public GameObject NextBtn;

	// Token: 0x04000185 RID: 389
	public GameObject InfoPanel;

	// Token: 0x04000186 RID: 390
	public GameObject DisplayCursorImage;

	// Token: 0x04000187 RID: 391
	public GameObject[] PatternImage;

	// Token: 0x04000188 RID: 392
	public Sprite[] PatternSprite;

	// Token: 0x04000189 RID: 393
	public Sprite[] ItemBackSprite;

	// Token: 0x0400018A RID: 394
	private int nowPage;

	// Token: 0x0400018B RID: 395
	private int selectShopIndex;

	// Token: 0x0400018C RID: 396
	private int shopItemMax;

	// Token: 0x0400018D RID: 397
	public int pageDisplayMax = 4;

	// Token: 0x0400018E RID: 398
	public int column = 2;

	// Token: 0x0400018F RID: 399
	public int pageWidth = 768;

	// Token: 0x04000190 RID: 400
	public Vector2 btnPadding = new Vector2(300f, -300f);

	// Token: 0x04000191 RID: 401
	private float flickMove;

	// Token: 0x04000192 RID: 402
	private FlickCheaker S_FlickChecker;

	// Token: 0x04000193 RID: 403
	public float flickMoveMax = 384f;

	// Token: 0x04000194 RID: 404
	public float flickMoveVector = 1.1f;

	// Token: 0x04000195 RID: 405
	public float flickMoveAttract = 2f;
}
