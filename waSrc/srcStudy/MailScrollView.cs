using System;
using System.Collections;
using Mail;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000073 RID: 115
public class MailScrollView : MonoBehaviour
{
	// Token: 0x060003E1 RID: 993 RVA: 0x00018E10 File Offset: 0x00017210
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.Disable();
		});
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00018E3C File Offset: 0x0001723C
	public void BackFunc_Clear()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00018E58 File Offset: 0x00017258
	public void Enable()
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.CreateMailButton();
		this.CheckNewMail();
		SuperGameMaster.admobMgr.ShowBanner(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc();
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x00018EDC File Offset: 0x000172DC
	public void Disable()
	{
		base.gameObject.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		this.DeleteMailButtonAll();
		this.CheckNewMail();
		SuperGameMaster.admobMgr.ShowBanner(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x00018F64 File Offset: 0x00017364
	public bool CheckNewMail()
	{
		foreach (MailEventFormat mailEventFormat in SuperGameMaster.saveData.MailList)
		{
			if (!mailEventFormat.opened)
			{
				this.PostObj.GetComponent<Post>().DispNew(true);
				this.AllAcceptBtn.GetComponent<Image>().color = new Color(1f, 1f, 1f);
				this.AllAcceptBtn.GetComponent<Button>().interactable = true;
				return true;
			}
		}
		this.PostObj.GetComponent<Post>().DispNew(false);
		this.AllAcceptBtn.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
		this.AllAcceptBtn.GetComponent<Button>().interactable = false;
		return false;
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00019060 File Offset: 0x00017460
	public void CreateMailButton()
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		float spacing = component.GetComponent<VerticalLayoutGroup>().spacing;
		float preferredHeight = this.btnPref.GetComponent<LayoutElement>().preferredHeight;
		foreach (MailEventFormat mailEventFormat in SuperGameMaster.saveData.MailList)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.btnPref);
			gameObject.transform.SetParent(component, false);
			gameObject.GetComponent<MailButton>().setId(mailEventFormat.mailId);
			gameObject.GetComponent<MailButton>().CngTitleName(mailEventFormat.title);
			if (mailEventFormat.senderCharaId != -1)
			{
				CharacterDataFormat characterDataFormat = SuperGameMaster.sDataBase.get_CharaDB_forId(mailEventFormat.senderCharaId);
				gameObject.GetComponent<MailButton>().CngSenderImage(characterDataFormat.img);
			}
			else
			{
				gameObject.GetComponent<MailButton>().CngSenderImage(null);
			}
			if (mailEventFormat.mailEvt == EvtId.Gift || mailEventFormat.mailEvt == EvtId.Management)
			{
				if (mailEventFormat.CloverPoint > 0)
				{
					gameObject.GetComponent<MailButton>().SetImageAndNum(mailEventFormat.CloverPoint, this.cloverImg);
				}
				else if (mailEventFormat.ticket > 0)
				{
					gameObject.GetComponent<MailButton>().SetImageAndNum(mailEventFormat.ticket, this.ticketImg);
				}
				else if (mailEventFormat.itemId == 1000)
				{
					gameObject.GetComponent<MailButton>().SetImageAndNum(mailEventFormat.itemStock, this.fourCloverImg);
				}
				else
				{
					gameObject.GetComponent<MailButton>().SetImageAndNum(0, this.cloverImg);
				}
			}
			if (mailEventFormat.mailEvt == EvtId.Leaflet)
			{
				gameObject.GetComponent<MailButton>().SetImage(this.leafletImg);
			}
			gameObject.GetComponent<MailButton>().SetMailEvtId(mailEventFormat.mailEvt, mailEventFormat.opened);
			if (mailEventFormat.itemId != -1 && mailEventFormat.itemId != 1000)
			{
				if (mailEventFormat.itemId < 10000)
				{
					ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(mailEventFormat.itemId);
					gameObject.GetComponent<MailButton>().CngItemImage(itemDataFormat.img);
				}
				else
				{
					CollectionDataFormat collectionDataFormat = SuperGameMaster.sDataBase.get_CollectDB_forId(mailEventFormat.itemId - 10000);
					gameObject.GetComponent<MailButton>().CngItemImage(collectionDataFormat.img);
				}
			}
			else
			{
				gameObject.GetComponent<MailButton>().CngItemImage(null);
			}
			gameObject.GetComponent<MailButton>().mailScrollView = base.gameObject;
			if (mailEventFormat.opened)
			{
				gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
			}
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x00019324 File Offset: 0x00017724
	public void DeleteMailButtonAll()
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
		}
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0001936C File Offset: 0x0001776C
	public void DeleteMailButton(int delId)
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			if (delId == component.GetChild(i).GetComponent<MailButton>().mailId)
			{
				component.GetChild(i).GetComponent<MailButton>().DeleteButton();
				break;
			}
		}
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x000193D0 File Offset: 0x000177D0
	public void CreateMailEvt(MailEventFormat mailEvt)
	{
		int count = SuperGameMaster.saveData.MailList.Count;
		if (count >= 100)
		{
			SuperGameMaster.saveData.MailList.RemoveAt(0);
			Debug.Log("[MailScrollView] 昔のメールを削除しました");
		}
		mailEvt.mailId = SuperGameMaster.saveData.MailList_nextId;
		mailEvt.date = SuperGameMaster.saveData.lastDateTime;
		SuperGameMaster.saveData.MailList.Add(mailEvt);
		SuperGameMaster.saveData.MailList_nextId++;
		Debug.Log(string.Concat(new object[]
		{
			"[MailScrollView] メール追加（",
			SuperGameMaster.saveData.MailList.Count,
			"） ID:",
			mailEvt.mailId,
			" next:",
			SuperGameMaster.saveData.MailList_nextId
		}));
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x000194B0 File Offset: 0x000178B0
	public void OpenMailEvt(int _mailId)
	{
		this.mailWindow.GetComponent<MailWindow>().OpenWindow(_mailId);
		this.blockPanel.SetActive(true);
		base.GetComponent<ScrollRect>().enabled = false;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000194DC File Offset: 0x000178DC
	public void PushMailEvt(int _mailId, EvtId mailEvtId)
	{
		int num = SuperGameMaster.saveData.MailList.FindIndex((MailEventFormat mail) => mail.mailId.Equals(_mailId));
		if (num == -1)
		{
			return;
		}
		switch (mailEvtId + 1)
		{
		case EvtId.Message:
			this.OpenMailEvt(_mailId);
			break;
		case EvtId.Picture:
			this.OpenMailEvt(_mailId);
			break;
		case EvtId.Gift:
			this.OpenMailEvt(_mailId);
			break;
		case EvtId.Management:
		case EvtId.Leaflet:
			if (!SuperGameMaster.saveData.MailList[num].opened)
			{
				this.GetItem(num);
			}
			SuperGameMaster.saveData.MailList.RemoveAt(num);
			this.DeleteMailButton(_mailId);
			break;
		case (EvtId)5:
			base.StartCoroutine(this.ShowInterStitial(_mailId, mailEvtId, num));
			break;
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x000195D0 File Offset: 0x000179D0
	public IEnumerator ShowInterStitial(int _mailId, EvtId mailEvtId, int mailIndex)
	{
		if (!SuperGameMaster.admobMgr.ShowInterstitial_OK() && !SuperGameMaster.admobMgr.nowLoadInterstitial)
		{
			SuperGameMaster.admobMgr.RequestInterstitial();
			Debug.Log("[admobManager] インタースティシャルを再読み込みします");
		}
		base.GetComponentInParent<UIMaster>().freezeUI(true, new Color(0f, 0f, 0f, 0.3f));
		float timer;
		for (timer = 0f; timer < 3f; timer += Time.deltaTime)
		{
			if (!SuperGameMaster.admobMgr.nowLoadInterstitial)
			{
				break;
			}
			yield return null;
		}
		if (!SuperGameMaster.admobMgr.ShowInterstitial_OK())
		{
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("インターネットに接続できませんでした\n電波状況や端末オプションを\nご確認のうえ、再度お試しください");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
		}
		else
		{
			CharacterDataFormat characterDataFormat = SuperGameMaster.sDataBase.get_CharaDB_forId(SuperGameMaster.saveData.MailList[mailIndex].senderCharaId);
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel_YesNo(characterDataFormat.name + "が \n持ってきたチラシ（広告）を\n見ますか？");
			confilm.ResetOnClick_Yes();
			confilm.SetOnClick_Yes(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Yes(delegate
			{
				SuperGameMaster.saveData.MailList.RemoveAt(mailIndex);
			});
			confilm.SetOnClick_Yes(delegate
			{
				SuperGameMaster.GetTicket(1);
			});
			confilm.SetOnClick_Yes(delegate
			{
				confilm.OpenPanel(string.Empty);
			});
			confilm.SetOnClick_Yes(delegate
			{
				confilm.AddContents(UnityEngine.Object.Instantiate<GameObject>(this.$this.AddConfirm_pref));
			});
			confilm.SetOnClick_Yes(delegate
			{
				confilm.ResetOnClick_Screen();
			});
			confilm.SetOnClick_Yes(delegate
			{
				confilm.SetOnClick_Screen(delegate
				{
					confilm.ClosePanel();
				});
			});
			confilm.SetOnClick_Yes(delegate
			{
				confilm.SetOnClick_Screen(delegate
				{
					this.$this.DeleteMailButton(_mailId);
				});
			});
			confilm.SetOnClick_Yes(delegate
			{
				confilm.SetOnClick_Screen(delegate
				{
					SuperGameMaster.admobMgr.ShowInterstitial();
				});
			});
			confilm.ResetOnClick_No();
			confilm.SetOnClick_No(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_No(delegate
			{
				SuperGameMaster.saveData.MailList.RemoveAt(mailIndex);
			});
			confilm.SetOnClick_No(delegate
			{
				this.$this.DeleteMailButton(_mailId);
			});
		}
		base.GetComponentInParent<UIMaster>().freezeUI(false, new Color(0f, 0f, 0f, 0f));
		Debug.Log(string.Concat(new object[]
		{
			"[MailScrollView] 広告処理終了：",
			SuperGameMaster.admobMgr.nowLoadInterstitial,
			" / timer = ",
			timer
		}));
		yield break;
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x000195F9 File Offset: 0x000179F9
	public void CloseMailEvt()
	{
		this.blockPanel.SetActive(false);
		base.GetComponent<ScrollRect>().enabled = true;
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00019614 File Offset: 0x00017A14
	public void PushAcceptAll()
	{
		this.DeleteMailButtonAll();
		for (int i = SuperGameMaster.saveData.MailList.Count - 1; i >= 0; i--)
		{
			if (!SuperGameMaster.saveData.MailList[i].opened)
			{
				this.GetItem(i);
			}
			SuperGameMaster.saveData.MailList.RemoveAt(i);
		}
		this.CreateMailButton();
		base.GetComponentInParent<UIMaster>().OnSave();
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel("すべてのゆうびんを受け取りました");
		confilm.ResetOnClick_Screen();
		confilm.SetOnClick_Screen(delegate
		{
			confilm.ClosePanel();
		});
		this.CheckNewMail();
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x000196DC File Offset: 0x00017ADC
	public void GetItem(int MailListIndex)
	{
		SuperGameMaster.saveData.MailList[MailListIndex].opened = true;
		SuperGameMaster.getCloverPoint(SuperGameMaster.saveData.MailList[MailListIndex].CloverPoint);
		SuperGameMaster.GetTicket(SuperGameMaster.saveData.MailList[MailListIndex].ticket);
		if (SuperGameMaster.saveData.MailList[MailListIndex].itemId != -1)
		{
			SuperGameMaster.GetItem(SuperGameMaster.saveData.MailList[MailListIndex].itemId, SuperGameMaster.saveData.MailList[MailListIndex].itemStock);
		}
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00019780 File Offset: 0x00017B80
	public void NewMailEvent()
	{
		int max = SuperGameMaster.sDataBase.count_MailEvtDB();
		this.CreateMailEvt(SuperGameMaster.sDataBase.get_MailEvtDB(UnityEngine.Random.Range(0, max)));
		this.PostObj.GetComponent<Post>().DispNew(true);
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x000197C0 File Offset: 0x00017BC0
	public void OpenConsole()
	{
		this.ConsoleUI.SetActive(true);
		this.ConsoleText.text = string.Empty;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x000197DE File Offset: 0x00017BDE
	public void CloseConsole()
	{
		this.ConsoleUI.SetActive(false);
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x000197EC File Offset: 0x00017BEC
	public void EnterConsole()
	{
		this.CloseConsole();
		int num = -1;
		if (this.ConsoleText.text.Length == 8)
		{
			num = SuperGameMaster.checkHoten(this.ConsoleText.text);
		}
		if (num <= -1)
		{
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("入力に失敗しました\n（エラー：" + -num + ")");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
		}
		else if (num != 2147483647)
		{
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("三つ葉を" + num + "追加しました");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
		}
	}

	// Token: 0x0400027D RID: 637
	public GameObject btnPref;

	// Token: 0x0400027E RID: 638
	public GameObject contentsList;

	// Token: 0x0400027F RID: 639
	public GameObject ConfilmUI;

	// Token: 0x04000280 RID: 640
	public GameObject AddConfirm_pref;

	// Token: 0x04000281 RID: 641
	public GameObject blockPanel;

	// Token: 0x04000282 RID: 642
	public GameObject mailWindow;

	// Token: 0x04000283 RID: 643
	public GameObject AllAcceptBtn;

	// Token: 0x04000284 RID: 644
	public GameObject PostObj;

	// Token: 0x04000285 RID: 645
	public GameObject ConsoleUI;

	// Token: 0x04000286 RID: 646
	public InputField ConsoleText;

	// Token: 0x04000287 RID: 647
	public Sprite cloverImg;

	// Token: 0x04000288 RID: 648
	public Sprite ticketImg;

	// Token: 0x04000289 RID: 649
	public Sprite fourCloverImg;

	// Token: 0x0400028A RID: 650
	public Sprite leafletImg;
}
