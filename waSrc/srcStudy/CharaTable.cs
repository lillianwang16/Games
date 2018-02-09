using System;
using System.Collections.Generic;
using Item;
using TimerEvent;
using UnityEngine;

// Token: 0x02000040 RID: 64
public class CharaTable : MonoBehaviour
{
	// Token: 0x06000260 RID: 608 RVA: 0x00007C87 File Offset: 0x00006087
	public void Init()
	{
		this.ResetFocus();
		this.CheckFriendCreate();
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00007C98 File Offset: 0x00006098
	public void getHitChara(int charaId)
	{
		this.focusCharaId = charaId;
		int index = this.RefChara.FindIndex((GameObject rec) => rec.GetComponent<CharaObject>().charaId.Equals(this.focusCharaId));
		EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_ActEvt_forId(this.RefActEvtId[index]);
		if (eventTimerFormat.evtValue[1] == 1)
		{
			this.ConfilmUI.GetComponentInParent<UIMaster>().freezeObject(true);
			this.ConfilmUI.GetComponentInParent<UIMaster>().blockUI(false, new Color(0.3f, 0.3f, 0.3f, 0f));
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			int num = SuperGameMaster.sDataBase.get_CharaDB_rowItemId_index(eventTimerFormat.evtValue[2]);
			CharacterDataFormat characterDataFormat = SuperGameMaster.sDataBase.get_CharaDB_forId(charaId);
			float num2 = 100f + (float)characterDataFormat.taste[num];
			if (num2 >= 180f)
			{
				confilm.OpenPanel(characterDataFormat.name + "は\n喜んでいます");
			}
			else if (num2 >= 160f)
			{
				confilm.OpenPanel(characterDataFormat.name + "は\n嬉しそうです");
			}
			else if (num2 >= 120f)
			{
				confilm.OpenPanel(characterDataFormat.name + "は\nお腹がいっぱいです");
			}
			else
			{
				confilm.OpenPanel(characterDataFormat.name + "は\nもう食べられません");
			}
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.ConfilmUI.GetComponentInParent<UIMaster>().freezeObject(false);
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.ConfilmUI.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			this.ResetFocus();
			return;
		}
		this.ItemView.GetComponent<ItemScrollView>().OpenScrollView(base.gameObject, ItemScrollView.Mode.Present, this.focusCharaId);
	}

	// Token: 0x06000262 RID: 610 RVA: 0x00007E94 File Offset: 0x00006294
	public void selectedItem(int charaId, int itemId)
	{
		if (itemId == -1)
		{
			this.ResetFocus();
			return;
		}
		int index = this.RefChara.FindIndex((GameObject rec) => rec.GetComponent<CharaObject>().charaId.Equals(charaId));
		EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_ActEvt_forId(this.RefActEvtId[index]);
		ItemDataFormat itemDataFormat = new ItemDataFormat();
		itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(itemId);
		bool flag = false;
		for (int i = 0; i < itemDataFormat.effectType.Length; i++)
		{
			if (itemDataFormat.effectElm[i] == EffectElm.FL_RARE)
			{
				flag = true;
			}
		}
		int num = UnityEngine.Random.Range(0, 100);
		int num2 = 0;
		Define.Gift gift = Define.Gift.NONE;
		for (int j = 0; j < 3; j++)
		{
			if (flag)
			{
				num2 += Define.FRIEND_GIFTPER_RARE[(Define.Gift)j];
			}
			else
			{
				num2 += Define.FRIEND_GIFTPER_NORMAL[(Define.Gift)j];
			}
			if (num < num2)
			{
				gift = (Define.Gift)j;
				break;
			}
		}
		CharacterDataFormat characterDataFormat = SuperGameMaster.sDataBase.get_CharaDB_forId(charaId);
		int num3 = Define.FRIEND_GIFTFIX[gift];
		if (gift != Define.Gift.Clover)
		{
			if (gift != Define.Gift.FourClover)
			{
				if (gift == Define.Gift.Ticket)
				{
					if (flag)
					{
						num3 += UnityEngine.Random.Range(1, 4);
					}
				}
			}
		}
		else
		{
			int num4 = SuperGameMaster.sDataBase.get_CharaDB_rowItemId_index(itemId);
			float num5 = (100f + (float)characterDataFormat.taste[num4]) / 100f;
			float num6 = (float)(eventTimerFormat.activeTime / 1800);
			int num7 = new List<int>
			{
				eventTimerFormat.evtValue[2],
				eventTimerFormat.evtValue[3],
				eventTimerFormat.evtValue[4]
			}.FindIndex((int rec) => rec.Equals(itemId));
			float num8 = 1f;
			if (num7 != -1)
			{
				num8 = Define.FRIEND_ITEM_DEBUFF[num7];
			}
			num3 += (int)((float)characterDataFormat.cloverPow * num5 * num6 * num8 / 15f);
			Debug.Log(string.Concat(new object[]
			{
				"みつば数算出：[",
				num3,
				"] pow = ",
				characterDataFormat.cloverPow,
				" / taste = ",
				num5,
				" / count = ",
				num6,
				" / buf = ",
				num8
			}));
			if (flag)
			{
				num3 += 20;
			}
		}
		Debug.Log(string.Concat(new object[]
		{
			"[CharaTable] お返しの抽選を行ないました： charaId = ",
			charaId,
			" / itemId = ",
			itemId,
			"(",
			flag,
			") >> ",
			gift.ToString(),
			"(",
			num3,
			")"
		}));
		List<int> list = new List<int>();
		list.Add(eventTimerFormat.evtValue[0]);
		list.Add(1);
		list.Add(itemId);
		list.Add(eventTimerFormat.evtValue[2]);
		list.Add(eventTimerFormat.evtValue[3]);
		list.Add(eventTimerFormat.evtValue[5]);
		SuperGameMaster.evtMgr.set_ActEvt_forId(eventTimerFormat.id, list);
		EventTimerFormat eventTimerFormat2 = new EventTimerFormat();
		eventTimerFormat2.id = -1;
		eventTimerFormat2.timeSpanSec = eventTimerFormat.timeSpanSec;
		eventTimerFormat2.activeTime = -1;
		eventTimerFormat2.addTime = new DateTime(1970, 1, 1);
		eventTimerFormat2.evtType = TimerEvent.Type.Gift;
		eventTimerFormat2.evtId = charaId;
		eventTimerFormat2.evtValue = new List<int>();
		eventTimerFormat2.evtValue.Add((int)gift);
		eventTimerFormat2.evtValue.Add(num3);
		eventTimerFormat2.trigger = true;
		SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat2);
		this.ResetFocus();
		SuperGameMaster.UseItem(itemId, 1);
		this.ConfilmUI.GetComponentInParent<UIMaster>().freezeObject(true);
		this.ConfilmUI.GetComponentInParent<UIMaster>().blockUI(true, new Color(0.3f, 0.3f, 0.3f, 0f));
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel(characterDataFormat.name + "に" + itemDataFormat.name + "\nをふるまいました");
		confilm.ResetOnClick_Screen();
		confilm.SetOnClick_Screen(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.ConfilmUI.GetComponentInParent<UIMaster>().freezeObject(false);
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.ConfilmUI.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		});
	}

	// Token: 0x06000263 RID: 611 RVA: 0x000083AF File Offset: 0x000067AF
	public void ResetFocus()
	{
		this.focusCharaId = -1;
	}

	// Token: 0x06000264 RID: 612 RVA: 0x000083B8 File Offset: 0x000067B8
	public void CheckFriendCreate()
	{
		List<int> list = new List<int>();
		foreach (EventTimerFormat eventTimerFormat in SuperGameMaster.evtMgr.get_ActEvtList_forType(TimerEvent.Type.Friend))
		{
			if (list.Count == 0)
			{
				this.CreteCharacter(eventTimerFormat.evtId, eventTimerFormat.evtValue[0]);
				this.RefActEvtId.Add(eventTimerFormat.id);
			}
			list.Add(eventTimerFormat.evtId);
		}
		foreach (EventTimerFormat eventTimerFormat2 in SuperGameMaster.evtMgr.get_TimerEvtList_forType(TimerEvent.Type.Friend))
		{
			list.Add(eventTimerFormat2.evtId);
		}
		int makeId = -1;
		int charaDB_lastIndexId = SuperGameMaster.sDataBase.get_CharaDB_lastIndexId();
		while (list.Count < SuperGameMaster.sDataBase.count_CharaDB())
		{
			while (makeId < charaDB_lastIndexId)
			{
				makeId++;
				Debug.Log(string.Concat(new object[]
				{
					"makeId = ",
					makeId,
					" / maxId",
					charaDB_lastIndexId,
					" // listCnt",
					list.Count,
					" / DBCount = ",
					SuperGameMaster.sDataBase.count_CharaDB()
				}));
				if (list.FindIndex((int rec) => rec.Equals(makeId)) == -1)
				{
					if (SuperGameMaster.sDataBase.search_CharaDBIndex_forId(makeId) != -1)
					{
						break;
					}
				}
			}
			EventTimerFormat eventTimerFormat3 = new EventTimerFormat();
			eventTimerFormat3.id = -1;
			eventTimerFormat3.timeSpanSec = 1;
			eventTimerFormat3.activeTime = 0;
			eventTimerFormat3.addTime = new DateTime(1970, 1, 1);
			eventTimerFormat3.evtType = TimerEvent.Type.Friend;
			eventTimerFormat3.evtId = makeId;
			eventTimerFormat3.evtValue = new List<int>();
			eventTimerFormat3.evtValue.Add(0);
			eventTimerFormat3.evtValue.Add(0);
			eventTimerFormat3.evtValue.Add(-1);
			eventTimerFormat3.evtValue.Add(-1);
			eventTimerFormat3.evtValue.Add(-1);
			eventTimerFormat3.evtValue.Add(0);
			eventTimerFormat3.trigger = false;
			SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat3);
			list.Add(makeId);
			Debug.Log("[CharaTable] キャラID（" + makeId + "）に対する Timer / Active のイベントが存在しなかったため、Timer イベントを生成しました。");
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x00008688 File Offset: 0x00006A88
	public void CreteCharacter(int charaId, int posId)
	{
		CharacterDataFormat characterDataFormat = SuperGameMaster.sDataBase.get_CharaDB_forId(charaId);
		this.CreteCharacter(charaId, characterDataFormat.aniName, characterDataFormat.rndPos[posId], 0, characterDataFormat.size, characterDataFormat.offset);
	}

	// Token: 0x06000266 RID: 614 RVA: 0x000086D1 File Offset: 0x00006AD1
	public void CreteCharacter(int charaId, string aniName, Vector3 aniPos, int act)
	{
		this.CreteCharacter(charaId, aniName, aniPos, act, Vector2.zero, Vector2.zero);
	}

	// Token: 0x06000267 RID: 615 RVA: 0x000086E8 File Offset: 0x00006AE8
	public void CreteCharacter(int charaId, string aniName, Vector3 aniPos, int act, Vector2 hit_size, Vector2 hit_offset)
	{
		if (this.RefChara == null)
		{
			this.RefChara = new List<GameObject>();
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CharaPref);
		gameObject.transform.SetParent(base.transform, false);
		gameObject.GetComponent<CharaObject>().SetChara(charaId);
		GameObject gameObject2 = AnmAnimation.CreateGameObject("aniObj", aniName, aniName);
		gameObject2.GetComponent<AnmAnimation>().SetAction(act);
		gameObject2.transform.SetParent(gameObject.transform, false);
		this.RefChara.Add(gameObject);
		gameObject.transform.localPosition = aniPos;
		if (hit_size != Vector2.zero)
		{
			gameObject.GetComponent<BoxCollider2D>().size = hit_size;
		}
		if (hit_offset != Vector2.zero)
		{
			gameObject.GetComponent<BoxCollider2D>().offset = hit_offset;
		}
		Debug.Log(string.Concat(new object[]
		{
			"[charaTable] キャラクタ生成[",
			this.RefChara.Count,
			"]： Id = ",
			charaId,
			" / name = ",
			aniName,
			" / aniPos = ",
			aniPos,
			" / fixPos(size)(offset) = ",
			hit_size,
			hit_offset
		}));
	}

	// Token: 0x04000112 RID: 274
	public GameObject CharaPref;

	// Token: 0x04000113 RID: 275
	public List<GameObject> RefChara;

	// Token: 0x04000114 RID: 276
	public List<int> RefActEvtId;

	// Token: 0x04000115 RID: 277
	public GameObject ItemView;

	// Token: 0x04000116 RID: 278
	public GameObject ConfilmUI;

	// Token: 0x04000117 RID: 279
	public GameObject HelpUI;

	// Token: 0x04000118 RID: 280
	public int focusCharaId;
}
