using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005E RID: 94
public class PicturePanel : MonoBehaviour
{
	// Token: 0x06000355 RID: 853 RVA: 0x00012360 File Offset: 0x00010760
	public void Init()
	{
		this.AlbumFlag = (SuperGameMaster.GetNowScenes() == Scenes.Album);
		Sprite sprite = this.SocialImg[0];
		foreach (Image image in this.refSocialImg)
		{
			image.sprite = sprite;
		}
		this.unsetCursor();
		if (this.AlbumFlag)
		{
			this.itemMax = 60;
		}
		else
		{
			this.itemMax = 30;
		}
		this.deleteMode = false;
		this.deleteIndex = new List<int>();
		this.GetSnapList();
		this.CreateItemButton();
		if (this.snapList_sprite.Count > this.pageItemMax && !this.NextBtn.activeSelf)
		{
			this.NextBtn.SetActive(true);
		}
		this.S_FlickChecker = base.GetComponent<FlickCheaker>();
		this.deleteBtn.GetComponent<Button>().interactable = false;
		this.chgEvtId = SuperGameMaster.picture.GetChangePictureData();
		if (this.chgEvtId != -1)
		{
			this.chgPicDateTime = SuperGameMaster.picture.GetChangePictureData_DateTime();
			this.deleteMode = true;
			this.deleteBtn.GetComponent<Button>().interactable = true;
		}
	}

	// Token: 0x06000356 RID: 854 RVA: 0x000124B0 File Offset: 0x000108B0
	public void BackMain()
	{
		SuperGameMaster.picture.ChangePictureData(-1, new DateTime(1970, 1, 1));
	}

	// Token: 0x06000357 RID: 855 RVA: 0x000124CC File Offset: 0x000108CC
	public void GetSnapList()
	{
		this.snapList_tex = SuperGameMaster.GetPictureList(this.AlbumFlag, TextureFormat.RGB24);
		this.snapList_sprite = new List<Sprite>();
		for (int i = 0; i < this.snapList_tex.Count; i++)
		{
			this.snapList_sprite.Add(Sprite.Create(this.snapList_tex[i], new Rect(0f, 0f, (float)this.snapList_tex[i].width, (float)this.snapList_tex[i].height), Vector2.zero));
		}
		this.snapList_dateTime = SuperGameMaster.GetPictureList_DateTime(true);
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00012574 File Offset: 0x00010974
	public void CreateItemButton()
	{
		this.unsetCursor();
		for (int i = -1; i <= 1; i++)
		{
			for (int j = 0; j < this.pageItemMax; j++)
			{
				int num = j + (this.nowPage + i) * this.pageItemMax;
				if (num < 0)
				{
					break;
				}
				if (num > this.itemMax - 1)
				{
					break;
				}
				if (num >= this.snapList_sprite.Count)
				{
					break;
				}
				Vector3 position = new Vector3(this.btnPref.transform.localPosition.x, this.btnPref.transform.localPosition.y, 0f);
				position.x += (float)(j % this.column) * this.btnPadding.x + (float)(this.pageWidth * i);
				position.y += (float)(j / this.column) * this.btnPadding.y;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.btnPref, position, Quaternion.identity);
				gameObject.transform.SetParent(base.transform, false);
				if (num < this.snapList_sprite.Count)
				{
					gameObject.GetComponent<PictureButton>().setIndex(num);
					gameObject.GetComponent<PictureButton>().setDateTime(this.snapList_dateTime[num]);
					gameObject.GetComponent<PictureButton>().CngImage(this.snapList_sprite[num]);
				}
				else
				{
					gameObject.GetComponent<PictureButton>().setIndex(-1);
					gameObject.GetComponent<Button>().enabled = false;
				}
				if (num == this.selectIndex && this.selectIndex != -1)
				{
					this.setCursor(gameObject.transform.localPosition);
				}
			}
		}
		this.AllSetDeleteButton(this.deleteMode);
		this.albumPageUI.SetPage(this.nowPage + 1, (this.snapList_sprite.Count - 1) / this.pageItemMax + 1);
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00012778 File Offset: 0x00010B78
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
					if (transform.name.Equals("PictureButton(Clone)"))
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

	// Token: 0x0600035A RID: 858 RVA: 0x000128F0 File Offset: 0x00010CF0
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
		if (!this.NextBtn.activeSelf)
		{
			this.NextBtn.SetActive(true);
		}
		if (this.deleteMode)
		{
			this.deleteIndex = new List<int>();
		}
		if (!this.deleteMode)
		{
			this.deleteBtn.GetComponent<Button>().interactable = false;
		}
		this.AllButtonDelete();
		this.CreateItemButton();
		this.flickMove -= (float)this.pageWidth;
		if (this.flickMove < (float)(-(float)this.pageWidth))
		{
			this.flickMove = (float)(-(float)this.pageWidth);
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_PageNext"]);
	}

	// Token: 0x0600035B RID: 859 RVA: 0x000129D8 File Offset: 0x00010DD8
	public void PushNext()
	{
		if ((this.nowPage + 1) * this.pageItemMax >= this.itemMax)
		{
			return;
		}
		if ((this.nowPage + 1) * this.pageItemMax >= this.snapList_sprite.Count)
		{
			return;
		}
		this.nowPage++;
		if ((this.nowPage + 1) * this.pageItemMax >= this.snapList_sprite.Count)
		{
			this.NextBtn.SetActive(false);
		}
		if (!this.PrevBtn.gameObject.activeSelf)
		{
			this.PrevBtn.SetActive(true);
		}
		if (this.deleteMode)
		{
			this.deleteIndex = new List<int>();
		}
		if (!this.deleteMode)
		{
			this.deleteBtn.GetComponent<Button>().interactable = false;
		}
		this.AllButtonDelete();
		this.CreateItemButton();
		this.flickMove += (float)this.pageWidth;
		if (this.flickMove > (float)this.pageWidth)
		{
			this.flickMove = (float)this.pageWidth;
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_PageNext"]);
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00012B04 File Offset: 0x00010F04
	public void AllButtonDelete()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.Equals("PictureButton(Clone)"))
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

	// Token: 0x0600035D RID: 861 RVA: 0x00012B84 File Offset: 0x00010F84
	public void SetPanelData(int selectCursorIndex, Vector3 pos)
	{
		if (!this.deleteMode)
		{
			if (selectCursorIndex == -1)
			{
				this.unsetCursor();
				return;
			}
			if (Mathf.Abs(this.flickMove) > this.S_FlickChecker.flickMin / 3f)
			{
				return;
			}
			if (this.selectIndex != selectCursorIndex)
			{
				this.selectIndex = selectCursorIndex;
				this.socialBtn.GetComponent<Button>().interactable = true;
				this.setCursor(pos);
				SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
				this.deleteBtn.GetComponent<Button>().interactable = true;
			}
			else
			{
				this.PictureViewUI.GetComponent<PictureViewPanel>().OpenPanel(this.snapList_sprite[selectCursorIndex]);
			}
		}
		else
		{
			if (Mathf.Abs(this.flickMove) > this.S_FlickChecker.flickMin / 3f)
			{
				return;
			}
			if (selectCursorIndex == -1)
			{
				return;
			}
			int num2 = this.deleteIndex.FindIndex((int num) => num.Equals(selectCursorIndex));
			if (num2 == -1)
			{
				this.deleteIndex.Add(selectCursorIndex);
				this.SetDeleteButton(true, selectCursorIndex);
				this.deleteBtn.GetComponent<Button>().interactable = true;
			}
			else
			{
				this.deleteIndex.RemoveAt(num2);
				this.SetDeleteButton(false, selectCursorIndex);
			}
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
		}
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00012D1C File Offset: 0x0001111C
	public void PushDeleteButton()
	{
		if (!this.deleteMode)
		{
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel_YesNo("選択中の写真を削除しますか？");
			confilm.ResetOnClick_Yes();
			confilm.SetOnClick_Yes(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponentInParent<UIMaster>().freezeObject(false);
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponent<FlickCheaker>().FlickInit();
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.SimpleDeleteItem(this.selectIndex);
			});
			confilm.ResetOnClick_No();
			confilm.SetOnClick_No(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_No(delegate
			{
				this.GetComponentInParent<UIMaster>().freezeObject(false);
			});
			confilm.SetOnClick_No(delegate
			{
				this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			confilm.SetOnClick_No(delegate
			{
				this.GetComponent<FlickCheaker>().FlickInit();
			});
		}
		else if (this.deleteIndex.Count == 0)
		{
			if (this.chgEvtId != -1)
			{
				ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
				confilm.OpenPanel_YesNo("写真の入れ替えをやめますか？");
				confilm.ResetOnClick_Yes();
				confilm.SetOnClick_Yes(delegate
				{
					confilm.ClosePanel();
				});
				confilm.SetOnClick_Yes(delegate
				{
					this.GetComponentInParent<UIMaster>().freezeObject(false);
				});
				confilm.SetOnClick_Yes(delegate
				{
					this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
				});
				confilm.SetOnClick_Yes(delegate
				{
					this.GetComponent<FlickCheaker>().FlickInit();
				});
				confilm.SetOnClick_Yes(delegate
				{
					this.GetComponentInParent<UIMaster_Album>().BackMain();
				});
				confilm.ResetOnClick_No();
				confilm.SetOnClick_No(delegate
				{
					confilm.ClosePanel();
				});
				confilm.SetOnClick_No(delegate
				{
					this.GetComponentInParent<UIMaster>().freezeObject(false);
				});
				confilm.SetOnClick_No(delegate
				{
					this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
				});
				confilm.SetOnClick_No(delegate
				{
					this.GetComponent<FlickCheaker>().FlickInit();
				});
				return;
			}
			this.deleteBtn.GetComponent<Button>().interactable = false;
			this.deleteMode = false;
			this.AllSetDeleteButton(this.deleteMode);
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		}
		else
		{
			base.GetComponentInParent<UIMaster>().freezeObject(true);
			base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel_YesNo("選択中の写真を削除しますか？");
			confilm.ResetOnClick_Yes();
			confilm.SetOnClick_Yes(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponentInParent<UIMaster>().freezeObject(false);
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponent<FlickCheaker>().FlickInit();
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.DeleteListItem();
			});
			confilm.ResetOnClick_No();
			confilm.SetOnClick_No(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_No(delegate
			{
				this.GetComponentInParent<UIMaster>().freezeObject(false);
			});
			confilm.SetOnClick_No(delegate
			{
				this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			confilm.SetOnClick_No(delegate
			{
				this.GetComponent<FlickCheaker>().FlickInit();
			});
		}
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00013104 File Offset: 0x00011504
	public void ChangePicture()
	{
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		Texture2D tmpPicture = SuperGameMaster.GetTmpPicture(this.chgEvtId, TextureFormat.RGB24);
		SuperGameMaster.SavePictureList(true, tmpPicture, this.chgPicDateTime);
		SuperGameMaster.evtMgr.delete_ActEvt_forId(this.chgEvtId);
		SuperGameMaster.DeleteTmpPicture(this.chgEvtId);
		SuperGameMaster.picture.ChangePictureData(-1, new DateTime(1970, 1, 1));
		base.GetComponentInParent<UIMaster>().OnSave();
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel("写真を入れ替えしました");
		confilm.ResetOnClick_Screen();
		confilm.SetOnClick_Screen(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.GetComponentInParent<UIMaster_Album>().BackMain();
		});
		this.snapList_tex.Add(tmpPicture);
		this.snapList_sprite.Add(Sprite.Create(tmpPicture, new Rect(0f, 0f, (float)tmpPicture.width, (float)tmpPicture.height), Vector2.zero));
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00013244 File Offset: 0x00011644
	public void AllSetDeleteButton(bool flag)
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.Equals("PictureButton(Clone)"))
				{
					int childIndex = transform.GetComponent<PictureButton>().index;
					if (childIndex != -1)
					{
						if (this.deleteMode && this.deleteIndex.FindIndex((int num) => num.Equals(childIndex)) != -1)
						{
							transform.GetComponent<PictureButton>().SetDeleteButton(true);
						}
						else
						{
							transform.GetComponent<PictureButton>().SetDeleteButton(false);
						}
					}
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

	// Token: 0x06000361 RID: 865 RVA: 0x00013320 File Offset: 0x00011720
	public void SetDeleteButton(bool flag, int idx)
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.Equals("PictureButton(Clone)") && transform.GetComponent<PictureButton>().index == idx)
				{
					transform.GetComponent<PictureButton>().SetDeleteButton(flag);
					break;
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

	// Token: 0x06000362 RID: 866 RVA: 0x000133B8 File Offset: 0x000117B8
	public void DeleteItemBtn(int index)
	{
	}

	// Token: 0x06000363 RID: 867 RVA: 0x000133BC File Offset: 0x000117BC
	public void DeleteListItem()
	{
		this.deleteIndex.Sort((int x, int y) => y - x);
		foreach (int index in this.deleteIndex)
		{
			this.DeleteItem(index);
		}
		this.deleteIndex = new List<int>();
		this.deleteBtn.GetComponent<Button>().interactable = false;
		this.deleteMode = false;
		this.socialBtn.GetComponent<Button>().interactable = false;
		if (this.chgEvtId != -1)
		{
			this.ChangePicture();
		}
		this.SaveAndReCreate();
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0001348C File Offset: 0x0001188C
	public void SimpleDeleteItem(int index)
	{
		this.DeleteItem(index);
		this.deleteBtn.GetComponent<Button>().interactable = false;
		this.socialBtn.GetComponent<Button>().interactable = false;
		this.SaveAndReCreate();
	}

	// Token: 0x06000365 RID: 869 RVA: 0x000134BD File Offset: 0x000118BD
	public void DeleteItem(int index)
	{
		SuperGameMaster.DeletePictureList(this.AlbumFlag, index);
		this.snapList_tex.RemoveAt(index);
		this.snapList_sprite.RemoveAt(index);
	}

	// Token: 0x06000366 RID: 870 RVA: 0x000134E4 File Offset: 0x000118E4
	public void SaveAndReCreate()
	{
		if (this.nowPage * this.pageItemMax >= this.snapList_sprite.Count && this.snapList_sprite.Count != 0)
		{
			this.PushPrev();
			this.NextBtn.SetActive(false);
			base.GetComponentInParent<UIMaster>().OnSave();
			return;
		}
		if (this.snapList_sprite.Count != 0)
		{
			this.NextBtn.SetActive(false);
		}
		this.AllButtonDelete();
		this.CreateItemButton();
		base.GetComponentInParent<UIMaster>().OnSave();
	}

	// Token: 0x06000367 RID: 871 RVA: 0x00013570 File Offset: 0x00011970
	public void setCursor(Vector3 pos)
	{
		this.Cursor.SetActive(true);
		Vector3 localPosition = pos;
		localPosition.x += this.CursorPadding.x;
		localPosition.y += this.CursorPadding.y;
		this.Cursor.transform.localPosition = localPosition;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x000135CE File Offset: 0x000119CE
	public void setCursor(Vector3 pos, float alpha)
	{
		this.setCursor(pos);
		this.Cursor.GetComponent<CanvasGroup>().alpha = alpha;
	}

	// Token: 0x06000369 RID: 873 RVA: 0x000135E8 File Offset: 0x000119E8
	public void unsetCursor()
	{
		this.selectIndex = -1;
		this.Cursor.SetActive(false);
		this.Cursor.GetComponent<CanvasGroup>().alpha = 1f;
		this.socialBtn.GetComponent<Button>().interactable = false;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x00013624 File Offset: 0x00011A24
	public void PushSocial()
	{
		if (this.selectIndex == -1)
		{
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("共有する画像を選択してください");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			return;
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		base.GetComponentInParent<UIMaster_Album>().GameMaster.GetComponent<GameMaster_Album>().SetReloadTimer(2f);
		base.StartCoroutine(base.GetComponent<SocialSender>().SendSocial("#旅かえる", string.Empty, this.snapList_tex[this.selectIndex], base.GetComponentInParent<UIMaster>()));
	}

	// Token: 0x0600036B RID: 875 RVA: 0x000136ED File Offset: 0x00011AED
	public void ChgDeleteMode()
	{
		this.deleteIndex = new List<int>();
		this.deleteMode = true;
		this.unsetCursor();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
	}

	// Token: 0x040001D7 RID: 471
	private bool AlbumFlag;

	// Token: 0x040001D8 RID: 472
	public GameObject PictureViewUI;

	// Token: 0x040001D9 RID: 473
	public GameObject ConfilmUI;

	// Token: 0x040001DA RID: 474
	public GameObject btnPref;

	// Token: 0x040001DB RID: 475
	public GameObject PrevBtn;

	// Token: 0x040001DC RID: 476
	public GameObject NextBtn;

	// Token: 0x040001DD RID: 477
	public GameObject Cursor;

	// Token: 0x040001DE RID: 478
	public GameObject deleteBtn;

	// Token: 0x040001DF RID: 479
	public GameObject socialBtn;

	// Token: 0x040001E0 RID: 480
	public List<Image> refSocialImg;

	// Token: 0x040001E1 RID: 481
	public List<Sprite> SocialImg;

	// Token: 0x040001E2 RID: 482
	private int nowPage;

	// Token: 0x040001E3 RID: 483
	public int selectIndex = -1;

	// Token: 0x040001E4 RID: 484
	private int itemMax;

	// Token: 0x040001E5 RID: 485
	public int pageItemMax = 6;

	// Token: 0x040001E6 RID: 486
	public int column = 2;

	// Token: 0x040001E7 RID: 487
	public int pageWidth = 768;

	// Token: 0x040001E8 RID: 488
	public Vector2 btnPadding = new Vector2(280f, -200f);

	// Token: 0x040001E9 RID: 489
	public Vector2 CursorPadding = new Vector2(-20f, 20f);

	// Token: 0x040001EA RID: 490
	private float flickMove;

	// Token: 0x040001EB RID: 491
	public FlickCheaker S_FlickChecker;

	// Token: 0x040001EC RID: 492
	public float flickMoveMax = 384f;

	// Token: 0x040001ED RID: 493
	public float flickMoveVector = 1.1f;

	// Token: 0x040001EE RID: 494
	public float flickMoveAttract = 2f;

	// Token: 0x040001EF RID: 495
	private List<Texture2D> snapList_tex;

	// Token: 0x040001F0 RID: 496
	private List<Sprite> snapList_sprite;

	// Token: 0x040001F1 RID: 497
	private List<DateTime> snapList_dateTime;

	// Token: 0x040001F2 RID: 498
	private bool deleteMode;

	// Token: 0x040001F3 RID: 499
	public List<int> deleteIndex;

	// Token: 0x040001F4 RID: 500
	public AlbumPagePanel albumPageUI;

	// Token: 0x040001F5 RID: 501
	public int chgEvtId = -1;

	// Token: 0x040001F6 RID: 502
	public DateTime chgPicDateTime = new DateTime(1970, 1, 1);

	// Token: 0x0200005F RID: 95
	public enum SocialSprite
	{
		// Token: 0x040001F9 RID: 505
		NONE = -1,
		// Token: 0x040001FA RID: 506
		Android,
		// Token: 0x040001FB RID: 507
		iOS
	}
}
