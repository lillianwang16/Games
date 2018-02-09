using System;
using System.Collections.Generic;
using Flag;
using Mail;
using TimerEvent;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000063 RID: 99
public class ResultPanel : MonoBehaviour
{
	// Token: 0x06000381 RID: 897 RVA: 0x00014550 File Offset: 0x00012950
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.PushInfo();
		});
	}

	// Token: 0x06000382 RID: 898 RVA: 0x0001457C File Offset: 0x0001297C
	public void BackFunc_Travel()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CheckExit_TravelResult();
		});
	}

	// Token: 0x06000383 RID: 899 RVA: 0x000145A8 File Offset: 0x000129A8
	public void BackFunc_Picture()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CheckExit_PictureResult();
		});
	}

	// Token: 0x06000384 RID: 900 RVA: 0x000145D4 File Offset: 0x000129D4
	public void CheckTravelEvent()
	{
		if (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.BackHome) != -1 || SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Return) != -1)
		{
			int num = SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Return);
			if (num != -1)
			{
				EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_ActEvt(num);
				SuperGameMaster.evtMgr.delete_ActEvt_forId(eventTimerFormat.id);
			}
			if (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.BackHome) == -1)
			{
				this.OpenView(ResultPanel.ResultMode.Return);
				this.BackFunc();
			}
			else
			{
				this.OpenView(ResultPanel.ResultMode.BackTravel);
				this.BackFunc();
			}
			SuperGameMaster.SetFrogMotion(-1);
		}
		else if (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Picture) != -1)
		{
			this.OpenView(ResultPanel.ResultMode.Picture);
			this.BackFunc();
		}
		else if (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.GoTravel) != -1)
		{
			this.OpenView(ResultPanel.ResultMode.GoTravel);
			this.BackFunc();
		}
		else if (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Drift) != -1)
		{
			this.OpenView(ResultPanel.ResultMode.Drift);
			this.BackFunc();
		}
		else if (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Friend) != -1 && this.FriendComeCheck())
		{
			this.OpenView(ResultPanel.ResultMode.Friend);
			this.BackFunc();
		}
		else
		{
			if (this.MODE != ResultPanel.ResultMode.NONE)
			{
				SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
			}
			if (!SuperGameMaster.GetFirstFlag(Flag.FIRST_COME_FRIEND) && this.MODE == ResultPanel.ResultMode.Friend)
			{
				SuperGameMaster.audioMgr.StopSE();
				SuperGameMaster.SetFirstFlag(Flag.FIRST_COME_FRIEND);
				base.GetComponentInParent<UIMaster>().freezeObject(true);
				base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0f));
				HelpPanel help = base.GetComponentInParent<UIMaster_MainOut>().HelpUI.GetComponent<HelpPanel>();
				help.OpenPanel(SuperGameMaster.GetFrogName() + "の<color=#61a8c7><b>友だち</b></color>が遊びにきました\n" + SuperGameMaster.GetFrogName() + "からもらった<color=#61a8c7><b>めいぶつ</b></color>で\n<color=#61a8c7><b>おもてなし</b></color>をしてあげましょう");
				help.ResetOnClick_Screen();
				help.SetOnClick_Screen(delegate
				{
					help.ClosePanel();
				});
				help.SetOnClick_Screen(delegate
				{
					this.GetComponentInParent<UIMaster>().freezeObject(false);
				});
				help.SetOnClick_Screen(delegate
				{
					this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
				});
			}
			if (this.MODE == ResultPanel.ResultMode.Friend)
			{
				this.MailUI.CheckNewMail();
			}
			base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
		}
	}

	// Token: 0x06000385 RID: 901 RVA: 0x0001483C File Offset: 0x00012C3C
	public void OpenView(ResultPanel.ResultMode mode)
	{
		this.MODE = mode;
		base.gameObject.SetActive(true);
		this.InfoButton.SetActive(true);
		this.TravelPanel.SetActive(false);
		this.PicturePanel.SetActive(false);
		this.PrevBtn.SetActive(false);
		this.NextBtn.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		switch (this.MODE)
		{
		case ResultPanel.ResultMode.GoTravel:
			this.LoadMessage();
			break;
		case ResultPanel.ResultMode.Drift:
			this.LoadMessage();
			break;
		case ResultPanel.ResultMode.BackTravel:
			this.LoadResult();
			break;
		case ResultPanel.ResultMode.Picture:
			this.LoadPicture();
			break;
		case ResultPanel.ResultMode.Friend:
			this.LoadMessage();
			break;
		case ResultPanel.ResultMode.Return:
			this.LoadMessage();
			break;
		}
		base.GetComponentInParent<UIMaster>().OnSave();
		SuperGameMaster.audioMgr.StopSE();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00014964 File Offset: 0x00012D64
	public void CloseView()
	{
		base.gameObject.SetActive(false);
		this.InfoButton.SetActive(false);
		this.TravelPanel.SetActive(false);
		this.PicturePanel.SetActive(false);
		this.PrevBtn.SetActive(false);
		this.NextBtn.SetActive(false);
		this.BackBlockUI.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		this.DeleteImage();
		this.CheckTravelEvent();
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00014A04 File Offset: 0x00012E04
	public void PushInfo()
	{
		switch (this.MODE)
		{
		case ResultPanel.ResultMode.GoTravel:
			this.InfoButton.SetActive(false);
			this.CloseView();
			break;
		case ResultPanel.ResultMode.Drift:
			this.InfoButton.SetActive(false);
			this.CloseView();
			break;
		case ResultPanel.ResultMode.BackTravel:
			if (this.InfoButtonPage == 0)
			{
				this.InfoButton.SetActive(false);
				this.InfoButton.SetActive(true);
				this.InfoButtonImage.sprite = this.LabelSprites[2];
				this.InfoButtonText.GetComponent<Text>().text = "おみやげをもらいました";
				this.InfoButtonPage++;
			}
			else if (this.InfoButtonPage == 1)
			{
				this.InfoButton.SetActive(false);
				this.BackBlockUI.SetActive(true);
				this.TravelPanel.SetActive(true);
				SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
				this.InfoButtonPage = 0;
				this.BackFunc_Travel();
			}
			break;
		case ResultPanel.ResultMode.Picture:
			this.InfoButton.SetActive(false);
			this.PicturePanel.SetActive(true);
			if (this.pageMax > 1)
			{
				this.NextBtn.SetActive(true);
			}
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
			this.BackFunc_Picture();
			break;
		case ResultPanel.ResultMode.Friend:
			this.InfoButton.SetActive(false);
			this.CloseView();
			break;
		case ResultPanel.ResultMode.Return:
			this.InfoButton.SetActive(false);
			this.CloseView();
			break;
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00014BA8 File Offset: 0x00012FA8
	public void LoadMessage()
	{
		switch (this.MODE)
		{
		case ResultPanel.ResultMode.GoTravel:
		{
			this.InfoButton.SetActive(true);
			this.InfoButtonImage.sprite = this.LabelSprites[0];
			this.InfoButtonText.GetComponent<Text>().text = SuperGameMaster.GetFrogName() + "は\n旅立っています";
			int index = SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.GoTravel);
			EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_ActEvt(index);
			SuperGameMaster.evtMgr.delete_ActEvt_forId(eventTimerFormat.id);
			SuperGameMaster.set_FlagAdd(Flag.Type.GOTRAVEL, 1);
			break;
		}
		case ResultPanel.ResultMode.Drift:
		{
			this.InfoButton.SetActive(true);
			this.InfoButtonImage.sprite = this.LabelSprites[0];
			this.InfoButtonText.GetComponent<Text>().text = SuperGameMaster.GetFrogName() + "は\nどこかへ出かけています";
			int index2 = SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Drift);
			EventTimerFormat eventTimerFormat2 = SuperGameMaster.evtMgr.get_ActEvt(index2);
			SuperGameMaster.evtMgr.delete_ActEvt_forId(eventTimerFormat2.id);
			SuperGameMaster.set_FlagAdd(Flag.Type.GOTRAVEL, 1);
			break;
		}
		case ResultPanel.ResultMode.Friend:
		{
			string text = string.Empty;
			int index3 = SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Friend);
			EventTimerFormat eventTimerFormat3 = SuperGameMaster.evtMgr.get_ActEvt(index3);
			CharacterDataFormat characterDataFormat = SuperGameMaster.sDataBase.get_CharaDB_forId(eventTimerFormat3.evtId);
			this.InfoButtonImage.sprite = this.LabelSprites[1];
			text = text + characterDataFormat.name + "が\n遊びにきています";
			List<int> list = new List<int>();
			list.Add(eventTimerFormat3.evtValue[0]);
			list.Add(eventTimerFormat3.evtValue[1]);
			list.Add(eventTimerFormat3.evtValue[2]);
			list.Add(eventTimerFormat3.evtValue[3]);
			list.Add(eventTimerFormat3.evtValue[4]);
			list.Add(1);
			SuperGameMaster.evtMgr.set_ActEvt_forId(eventTimerFormat3.id, list);
			this.InfoButton.SetActive(true);
			this.InfoButtonText.GetComponent<Text>().text = text;
			if (UnityEngine.Random.Range(0, 100) < 100)
			{
				int num = SuperGameMaster.saveData.MailList.FindIndex((MailEventFormat mail) => mail.mailEvt.Equals(EvtId.Leaflet));
				if (num == -1)
				{
					MailEventFormat mailEventFormat = new MailEventFormat();
					mailEventFormat.NewMail();
					mailEventFormat.title = characterDataFormat.name + "のチラシ";
					mailEventFormat.senderCharaId = characterDataFormat.id;
					mailEventFormat.mailEvt = EvtId.Leaflet;
					mailEventFormat.mailId = SuperGameMaster.saveData.MailList_nextId;
					mailEventFormat.date = SuperGameMaster.saveData.lastDateTime;
					this.MailUI.CreateMailEvt(mailEventFormat);
				}
			}
			break;
		}
		case ResultPanel.ResultMode.Return:
			this.InfoButton.SetActive(true);
			this.InfoButtonImage.sprite = this.LabelSprites[0];
			this.InfoButtonText.GetComponent<Text>().text = SuperGameMaster.GetFrogName() + "が\n帰ってきました";
			break;
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00014ED8 File Offset: 0x000132D8
	public bool FriendComeCheck()
	{
		if (this.MODE != ResultPanel.ResultMode.Friend)
		{
			int num = SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Friend);
			if (num != -1)
			{
				EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_ActEvt(num);
				if (eventTimerFormat.evtValue[5] == 0)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00014F24 File Offset: 0x00013324
	public void LoadResult()
	{
		this.InfoButton.SetActive(true);
		this.InfoButtonPage = 0;
		this.InfoButtonImage.sprite = this.LabelSprites[0];
		if (SuperGameMaster.GetHome())
		{
			this.InfoButtonText.GetComponent<Text>().text = SuperGameMaster.GetFrogName() + "が\n帰っています";
		}
		else
		{
			this.InfoButtonText.GetComponent<Text>().text = SuperGameMaster.GetFrogName() + "が\n帰っていたようです";
		}
		this.nowPage = 0;
		this.pageMax = 0;
		this.TravelPanel.transform.localPosition = Vector3.zero;
		while (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.BackHome) != -1)
		{
			int index = SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.BackHome);
			EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_ActEvt(index);
			int num = eventTimerFormat.evtValue[1];
			int num2 = SuperGameMaster.get_Flag(Flag.Type.TRAVEL_TIMEMIN);
			int num3 = SuperGameMaster.get_Flag(Flag.Type.TRAVEL_TIMEMAX);
			if (num2 == 0)
			{
				SuperGameMaster.set_Flag(Flag.Type.TRAVEL_TIMEMIN, num);
			}
			if (num2 > num)
			{
				SuperGameMaster.set_Flag(Flag.Type.TRAVEL_TIMEMIN, num);
			}
			if (num3 < num)
			{
				SuperGameMaster.set_Flag(Flag.Type.TRAVEL_TIMEMAX, num);
			}
			bool success = false;
			if (eventTimerFormat.evtValue[0] == 0)
			{
				success = true;
			}
			List<int> list = new List<int>(eventTimerFormat.evtValue);
			list.RemoveAt(0);
			list.RemoveAt(0);
			Vector3 position = new Vector3(this.TravelResultPref.transform.localPosition.x, this.TravelResultPref.transform.localPosition.y, 0f);
			position.x += (float)(this.pageWidth * this.pageMax);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TravelResultPref, position, Quaternion.identity);
			gameObject.transform.SetParent(this.TravelPanel.GetComponent<RectTransform>(), false);
			gameObject.GetComponent<TravelResultPanel>().Controller = base.gameObject;
			gameObject.GetComponent<TravelResultPanel>().CreateResult(success, eventTimerFormat.id, new List<int>(list), eventTimerFormat.evtId);
			SuperGameMaster.getCloverPoint(list[0]);
			SuperGameMaster.GetTicket(list[1]);
			list.RemoveAt(0);
			list.RemoveAt(0);
			for (int i = 0; i < list.Count; i += 2)
			{
				SuperGameMaster.GetItem(list[i], list[i + 1]);
			}
			SuperGameMaster.evtMgr.delete_ActEvt_forId(eventTimerFormat.id);
			this.pageMax++;
		}
		this.S_FlickChecker.stopFlick(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000151D0 File Offset: 0x000135D0
	public void LoadPicture()
	{
		this.InfoButton.SetActive(true);
		this.InfoButtonImage.sprite = this.LabelSprites[3];
		this.InfoButtonText.GetComponent<Text>().text = "写真がとどいています";
		this.nowPage = 0;
		this.pageMax = 0;
		this.PicturePanel.transform.localPosition = Vector3.zero;
		foreach (EventTimerFormat eventTimerFormat in SuperGameMaster.evtMgr.get_ActEvtList_forType(TimerEvent.Type.Picture))
		{
			Texture2D tmpPicture = SuperGameMaster.GetTmpPicture(eventTimerFormat.id, TextureFormat.RGB24);
			Vector3 position = new Vector3(this.PictureResultPref.transform.localPosition.x, this.PictureResultPref.transform.localPosition.y, 0f);
			position.x += (float)(this.pageWidth * this.pageMax);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PictureResultPref, position, Quaternion.identity);
			gameObject.transform.SetParent(this.PicturePanel.GetComponent<RectTransform>(), false);
			gameObject.GetComponent<PictureResultPanel>().Controller = base.gameObject;
			Sprite socialImg = this.SocialImg[0];
			gameObject.GetComponent<PictureResultPanel>().CreateResult(tmpPicture, eventTimerFormat.id, eventTimerFormat.addTime, socialImg);
			this.pageMax++;
		}
		if (this.pageMax == 1)
		{
			this.S_FlickChecker.stopFlick(true);
		}
		else
		{
			this.S_FlickChecker.stopFlick(false);
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
	}

	// Token: 0x0600038C RID: 908 RVA: 0x000153A4 File Offset: 0x000137A4
	public void DeleteImage()
	{
		if (this.MODE == ResultPanel.ResultMode.BackTravel)
		{
			RectTransform component = this.TravelPanel.GetComponent<RectTransform>();
			for (int i = 0; i < component.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
			}
		}
		if (this.MODE == ResultPanel.ResultMode.Picture)
		{
			if (this.pageMax <= 0)
			{
				return;
			}
			RectTransform component2 = this.PicturePanel.GetComponent<RectTransform>();
			for (int j = 0; j < component2.transform.childCount; j++)
			{
				int evtId = component2.GetChild(j).GetComponent<PictureResultPanel>().evtId;
				UnityEngine.Object.Destroy(component2.GetChild(j).gameObject);
				this.DeletePictureEvt(evtId);
			}
		}
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00015464 File Offset: 0x00013864
	public void DeletePictureResultPanel(int _evtId)
	{
		int num = -1;
		RectTransform component = this.PicturePanel.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			int evtId = component.GetChild(i).GetComponent<PictureResultPanel>().evtId;
			if (evtId == _evtId)
			{
				num = i;
				UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
				this.DeletePictureEvt(evtId);
				this.pageMax--;
				break;
			}
		}
		for (int j = num; j < component.transform.childCount; j++)
		{
			Vector3 localPosition = component.GetChild(j).transform.localPosition;
			localPosition.x -= (float)this.pageWidth;
			component.GetChild(j).transform.localPosition = localPosition;
		}
		if (this.nowPage == this.pageMax)
		{
			this.PushPrev();
		}
		if (this.nowPage == this.pageMax - 1)
		{
			this.NextBtn.SetActive(false);
		}
		if (this.pageMax <= 0)
		{
			this.CloseView();
		}
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00015586 File Offset: 0x00013986
	public void DeletePictureEvt(int _evtId)
	{
		SuperGameMaster.evtMgr.delete_ActEvt_forId(_evtId);
		SuperGameMaster.DeleteTmpPicture(_evtId);
	}

	// Token: 0x0600038F RID: 911 RVA: 0x0001559C File Offset: 0x0001399C
	public void CheckExit_PictureResult()
	{
		base.GetComponent<FlickCheaker>().stopFlick(true);
		ConfilmPanel confilm = base.GetComponent<ResultPanel>().ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel_YesNo("写真の受け取りを終了しますか？");
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
			this.CloseView();
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

	// Token: 0x06000390 RID: 912 RVA: 0x00015674 File Offset: 0x00013A74
	public void DeleteTravelResultPanel(int _evtId)
	{
		int num = -1;
		RectTransform component = this.TravelPanel.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			int evtId = component.GetChild(i).GetComponent<TravelResultPanel>().evtId;
			if (evtId == _evtId)
			{
				num = i;
				UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
				this.pageMax--;
				break;
			}
		}
		for (int j = num; j < component.transform.childCount; j++)
		{
			Vector3 localPosition = component.GetChild(j).transform.localPosition;
			localPosition.x -= (float)this.pageWidth;
			component.GetChild(j).transform.localPosition = localPosition;
		}
		if (this.pageMax <= 0)
		{
			this.CloseView();
		}
	}

	// Token: 0x06000391 RID: 913 RVA: 0x0001575C File Offset: 0x00013B5C
	public void CheckExit_TravelResult()
	{
		ConfilmPanel confilm = base.GetComponent<ResultPanel>().ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel_YesNo("アイテムを全て受け取りますか？");
		confilm.ResetOnClick_Yes();
		confilm.SetOnClick_Yes(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.CloseView();
		});
		confilm.ResetOnClick_No();
		confilm.SetOnClick_No(delegate
		{
			confilm.ClosePanel();
		});
	}

	// Token: 0x06000392 RID: 914 RVA: 0x000157F8 File Offset: 0x00013BF8
	public void PanelUpDate()
	{
		if (this.InfoButton.activeSelf)
		{
			return;
		}
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
			if (this.MODE == ResultPanel.ResultMode.BackTravel)
			{
				Vector3 localPosition = this.TravelPanel.transform.localPosition;
				localPosition.x = this.flickMove - (float)(this.nowPage * this.pageWidth);
				this.TravelPanel.transform.localPosition = localPosition;
			}
			if (this.MODE == ResultPanel.ResultMode.Picture)
			{
				Vector3 localPosition2 = this.PicturePanel.transform.localPosition;
				localPosition2.x = this.flickMove - (float)(this.nowPage * this.pageWidth);
				this.PicturePanel.transform.localPosition = localPosition2;
			}
			if ((double)Mathf.Abs(this.flickMove) < 0.1)
			{
				this.flickMove = 0f;
			}
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00015988 File Offset: 0x00013D88
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
		this.flickMove -= (float)this.pageWidth;
		if (this.flickMove < (float)(-(float)this.pageWidth))
		{
			this.flickMove = (float)(-(float)this.pageWidth);
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_PageNext"]);
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00015A38 File Offset: 0x00013E38
	public void PushNext()
	{
		if (this.nowPage + 1 >= this.pageMax)
		{
			return;
		}
		this.nowPage++;
		if (this.nowPage + 1 >= this.pageMax)
		{
			this.NextBtn.SetActive(false);
		}
		if (!this.PrevBtn.gameObject.activeSelf)
		{
			this.PrevBtn.SetActive(true);
		}
		this.flickMove += (float)this.pageWidth;
		if (this.flickMove > (float)this.pageWidth)
		{
			this.flickMove = (float)this.pageWidth;
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_PageNext"]);
	}

	// Token: 0x0400020F RID: 527
	private ResultPanel.ResultMode MODE = ResultPanel.ResultMode.NONE;

	// Token: 0x04000210 RID: 528
	[Header("【メッセージ表示】")]
	public GameObject InfoButton;

	// Token: 0x04000211 RID: 529
	public GameObject InfoButtonText;

	// Token: 0x04000212 RID: 530
	[Space(5f)]
	public Image InfoButtonImage;

	// Token: 0x04000213 RID: 531
	public List<Sprite> LabelSprites;

	// Token: 0x04000214 RID: 532
	public int InfoButtonPage;

	// Token: 0x04000215 RID: 533
	[Header("【旅の結果表示】")]
	public GameObject TravelPanel;

	// Token: 0x04000216 RID: 534
	public GameObject TravelResultPref;

	// Token: 0x04000217 RID: 535
	[Header("【写真の結果表示】")]
	public GameObject PicturePanel;

	// Token: 0x04000218 RID: 536
	public GameObject PictureResultPref;

	// Token: 0x04000219 RID: 537
	public List<Sprite> SocialImg;

	// Token: 0x0400021A RID: 538
	[Header("【ページめくり】")]
	public GameObject PrevBtn;

	// Token: 0x0400021B RID: 539
	public GameObject NextBtn;

	// Token: 0x0400021C RID: 540
	public int pageWidth = 768;

	// Token: 0x0400021D RID: 541
	private int nowPage;

	// Token: 0x0400021E RID: 542
	private int pageMax;

	// Token: 0x0400021F RID: 543
	[Header("【フリック設定】")]
	private float flickMove;

	// Token: 0x04000220 RID: 544
	public FlickCheaker S_FlickChecker;

	// Token: 0x04000221 RID: 545
	public float flickMoveMax = 384f;

	// Token: 0x04000222 RID: 546
	public float flickMoveVector = 1.1f;

	// Token: 0x04000223 RID: 547
	public float flickMoveAttract = 2f;

	// Token: 0x04000224 RID: 548
	[Space(10f)]
	public GameObject ConfilmUI;

	// Token: 0x04000225 RID: 549
	public MailScrollView MailUI;

	// Token: 0x04000226 RID: 550
	public GameObject BackBlockUI;

	// Token: 0x02000064 RID: 100
	public enum ResultMode
	{
		// Token: 0x04000229 RID: 553
		NONE = -1,
		// Token: 0x0400022A RID: 554
		GoTravel,
		// Token: 0x0400022B RID: 555
		Drift,
		// Token: 0x0400022C RID: 556
		BackTravel,
		// Token: 0x0400022D RID: 557
		Picture,
		// Token: 0x0400022E RID: 558
		Friend,
		// Token: 0x0400022F RID: 559
		Return
	}

	// Token: 0x02000065 RID: 101
	public enum LabelColor
	{
		// Token: 0x04000231 RID: 561
		NONE = -1,
		// Token: 0x04000232 RID: 562
		Red_Travel,
		// Token: 0x04000233 RID: 563
		Yellow_Friend,
		// Token: 0x04000234 RID: 564
		Blue_Result,
		// Token: 0x04000235 RID: 565
		Green_Picture
	}

	// Token: 0x02000066 RID: 102
	public enum SocialSprite
	{
		// Token: 0x04000237 RID: 567
		NONE = -1,
		// Token: 0x04000238 RID: 568
		Android,
		// Token: 0x04000239 RID: 569
		iOS
	}
}
