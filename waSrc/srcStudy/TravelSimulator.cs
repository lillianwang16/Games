using System;
using System.Collections.Generic;
using Item;
using Node;
using TimerEvent;
using UnityEngine;

// Token: 0x020000EB RID: 235
public class TravelSimulator : MonoBehaviour
{
	// Token: 0x0600072C RID: 1836 RVA: 0x0002BEA8 File Offset: 0x0002A2A8
	public void UpDateNotification()
	{
		if (!SuperGameMaster.tutorial.ClockOk())
		{
			return;
		}
		if (this.DebugTest_Notification)
		{
			return;
		}
		if (SuperGameMaster.timeError)
		{
			SuperGameMaster.CancelLocalNotification(72091216);
			return;
		}
		if (!SuperGameMaster.GetNoticeFlag())
		{
			SuperGameMaster.CancelLocalNotification(72091216);
			return;
		}
		int num = SuperGameMaster.evtMgr.search_TimerEvtIndex_forType(TimerEvent.Type.BackHome);
		if (num != -1)
		{
			EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_TimerEvt(num);
			if (SuperGameMaster.GetStandby())
			{
				SuperGameMaster.SendLocalNotification(SuperGameMaster.GetFrogName() + "が帰ってきました", eventTimerFormat.timeSpanSec - SuperGameMaster.saveData.restTime + SuperGameMaster.GetStandbyWait(), 72091216);
			}
			else
			{
				SuperGameMaster.SendLocalNotification(SuperGameMaster.GetFrogName() + "が帰ってきました", eventTimerFormat.timeSpanSec - (int)SuperGameMaster.getGameTimer(), 72091216);
			}
			return;
		}
		int num2 = SuperGameMaster.evtMgr.search_TimerEvtIndex_forType(TimerEvent.Type.Return);
		if (num2 != -1)
		{
			EventTimerFormat eventTimerFormat2 = SuperGameMaster.evtMgr.get_TimerEvt(num2);
			SuperGameMaster.SendLocalNotification(SuperGameMaster.GetFrogName() + "が帰ってきました", eventTimerFormat2.timeSpanSec - (int)SuperGameMaster.getGameTimer(), 72091216);
			return;
		}
		SuperGameMaster.CancelLocalNotification(72091216);
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x0002BFD0 File Offset: 0x0002A3D0
	public void Proc(int timeSpan)
	{
		int num = SuperGameMaster.saveData.restTime - SuperGameMaster.saveData.standbyWait;
		if (SuperGameMaster.saveData.home)
		{
			SuperGameMaster.saveData.standbyWait -= timeSpan;
			SuperGameMaster.saveData.restTime -= timeSpan;
			if (SuperGameMaster.saveData.standby && SuperGameMaster.saveData.standbyWait <= 0)
			{
				SuperGameMaster.evtMgr.Proc(num, TimerEvent.Type.GoTravel);
				SuperGameMaster.evtMgr.Proc(num, TimerEvent.Type.BackHome);
				SuperGameMaster.evtMgr.Proc(num, TimerEvent.Type.Picture);
				SuperGameMaster.saveData.restTime = SuperGameMaster.saveData.standbyWait;
				SuperGameMaster.saveData.standbyWait = 0;
				SuperGameMaster.saveData.standby = false;
				SuperGameMaster.saveData.lastTravelTime -= num;
			}
			if (SuperGameMaster.saveData.restTime <= 0)
			{
				SuperGameMaster.saveData.home = false;
			}
		}
		SuperGameMaster.saveData.lastTravelTime -= timeSpan;
		if (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.BackHome) != -1 || SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Return) != -1)
		{
			int num2 = SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Return);
			if (num2 != -1)
			{
				EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_ActEvt(num2);
				this.SetReturnState();
			}
			do
			{
				if (num2 == -1)
				{
					SuperGameMaster.UseBagItem();
				}
				int num3 = UnityEngine.Random.Range(21600, 43200);
				SuperGameMaster.saveData.restTime = SuperGameMaster.saveData.lastTravelTime + num3;
				if (SuperGameMaster.saveData.restTime > 0)
				{
					SuperGameMaster.saveData.home = true;
				}
				else
				{
					SuperGameMaster.saveData.home = false;
				}
				SuperGameMaster.saveData.standbyWait = SuperGameMaster.saveData.lastTravelTime + num3 / 2;
				SuperGameMaster.saveData.lastTravelTime = SuperGameMaster.saveData.lastTravelTime + num3;
				if (!this.ResetBag())
				{
					break;
				}
				this.GoTravel(SuperGameMaster.saveData.restTime);
				num2 = -1;
			}
			while (SuperGameMaster.saveData.lastTravelTime < 0);
		}
		if (SuperGameMaster.saveData.lastTravelTime < 0)
		{
			if (!SuperGameMaster.saveData.drift)
			{
				EventTimerFormat eventTimerFormat2 = new EventTimerFormat();
				eventTimerFormat2.id = -1;
				eventTimerFormat2.timeSpanSec = -1;
				eventTimerFormat2.activeTime = -1;
				eventTimerFormat2.addTime = new DateTime(1970, 1, 1);
				eventTimerFormat2.evtType = TimerEvent.Type.Drift;
				eventTimerFormat2.evtId = 0;
				eventTimerFormat2.evtValue = new List<int>();
				eventTimerFormat2.evtValue.Add(eventTimerFormat2.timeSpanSec);
				eventTimerFormat2.trigger = true;
				SuperGameMaster.evtMgr.ActiveAdd(eventTimerFormat2);
			}
			this.SetDriftState();
		}
		SuperGameMaster.evtMgr.Proc(0, TimerEvent.Type.GoTravel);
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0002C27C File Offset: 0x0002A67C
	public void StandByTravel()
	{
		if (SuperGameMaster.saveData.home)
		{
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.GoTravel);
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.BackHome);
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Picture);
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Drift);
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Return);
			if (this.ResetBag())
			{
				this.GoTravel(SuperGameMaster.saveData.restTime);
				Debug.Log(string.Concat(new object[]
				{
					"[TravelSimulator] 在宅中であるため、旅行イベントの再生成を行ないました。\u3000home = ",
					SuperGameMaster.saveData.home,
					" / drift = ",
					SuperGameMaster.saveData.drift
				}));
			}
			else
			{
				SuperGameMaster.saveData.lastTravelTime = SuperGameMaster.saveData.restTime;
				Debug.Log(string.Concat(new object[]
				{
					"[TravelSimulator] 「べんとう」がないため、旅行イベントを作成できません。\u3000home = ",
					SuperGameMaster.saveData.home,
					" / drift = ",
					SuperGameMaster.saveData.drift
				}));
			}
		}
		else
		{
			if (!SuperGameMaster.saveData.drift)
			{
				Debug.Log(string.Concat(new object[]
				{
					"[TravelSimulator] 在宅中・放浪中ではないため、イベントの再生成は行ないません。\u3000home = ",
					SuperGameMaster.saveData.home,
					" / drift = ",
					SuperGameMaster.saveData.drift
				}));
				return;
			}
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.GoTravel);
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.BackHome);
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Picture);
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Drift);
			SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Return);
			List<int> bagList = SuperGameMaster.GetBagList();
			List<int> deskList = SuperGameMaster.GetDeskList();
			if (bagList[0] == -1 && deskList[0] == -1 && deskList[1] == -1)
			{
				SuperGameMaster.saveData.lastTravelTime = -1;
				Debug.Log(string.Concat(new object[]
				{
					"[TravelSimulator] 「べんとう」がないため、放浪イベントを作成しました。\u3000home = ",
					SuperGameMaster.saveData.home,
					" / drift = ",
					SuperGameMaster.saveData.drift
				}));
				return;
			}
			EventTimerFormat eventTimerFormat = new EventTimerFormat();
			eventTimerFormat.id = -1;
			eventTimerFormat.timeSpanSec = UnityEngine.Random.Range(300, 1800);
			eventTimerFormat.activeTime = -1;
			eventTimerFormat.addTime = new DateTime(1970, 1, 1);
			eventTimerFormat.evtType = TimerEvent.Type.Return;
			eventTimerFormat.evtId = 0;
			eventTimerFormat.evtValue = new List<int>();
			eventTimerFormat.evtValue.Add(eventTimerFormat.timeSpanSec);
			eventTimerFormat.trigger = true;
			SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
			SuperGameMaster.saveData.lastTravelTime = eventTimerFormat.timeSpanSec;
			Debug.Log(string.Concat(new object[]
			{
				"[TravelSimulator] 放浪中のため、帰還イベントを生成しました。\u3000home = ",
				SuperGameMaster.saveData.home,
				" / drift = ",
				SuperGameMaster.saveData.drift
			}));
		}
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x0002C570 File Offset: 0x0002A970
	public bool ResetBag()
	{
		List<int> bagList = SuperGameMaster.GetBagList();
		List<int> deskList = SuperGameMaster.GetDeskList();
		if (bagList[0] != -1)
		{
			string text = "[TravelSimulator] 旅行イベントを作成しました：かばん / bag[";
			foreach (int num in bagList)
			{
				text = text + num + ",";
			}
			text.Remove(text.Length - 1, 1);
			text += "] / desk[";
			foreach (int num2 in deskList)
			{
				text = text + num2 + ",";
			}
			text.Remove(text.Length - 1, 1);
			text += "]";
			Debug.Log(text);
			return true;
		}
		if (deskList[0] != -1 || deskList[1] != -1)
		{
			List<int> list = new List<int>();
			if (deskList[0] != -1)
			{
				list.Add(deskList[0]);
			}
			if (deskList[1] != -1)
			{
				list.Add(deskList[1]);
			}
			int id = list[UnityEngine.Random.Range(0, list.Count)];
			bagList[0] = id;
			deskList[deskList.FindIndex((int item) => item.Equals(id))] = -1;
			list = new List<int>();
			if (deskList[2] != -1)
			{
				list.Add(deskList[2]);
			}
			if (deskList[3] != -1)
			{
				list.Add(deskList[3]);
			}
			if (bagList[1] == -1 && list.Count > 0)
			{
				id = list[UnityEngine.Random.Range(0, list.Count)];
				bagList[1] = id;
				deskList[deskList.FindIndex((int item) => item.Equals(id))] = -1;
			}
			list = new List<int>();
			if (deskList[4] != -1)
			{
				list.Add(deskList[4]);
			}
			if (deskList[5] != -1)
			{
				list.Add(deskList[5]);
			}
			if (deskList[6] != -1)
			{
				list.Add(deskList[6]);
			}
			if (deskList[7] != -1)
			{
				list.Add(deskList[7]);
			}
			for (int i = 2; i <= 3; i++)
			{
				if (bagList[i] == -1)
				{
					if (list.Count <= 0)
					{
						break;
					}
					id = list[UnityEngine.Random.Range(0, list.Count)];
					bagList[i] = id;
					deskList[deskList.FindIndex((int item) => item.Equals(id))] = -1;
					list.RemoveAt(list.FindIndex((int item) => item.Equals(id)));
				}
			}
			SuperGameMaster.SaveBagList(bagList);
			SuperGameMaster.SaveDeskList(deskList);
			string text2 = "[TravelSimulator] 旅行イベントが作成できます：つくえ / bag[";
			foreach (int num3 in bagList)
			{
				text2 = text2 + num3 + ",";
			}
			text2.Remove(text2.Length - 1, 1);
			text2 += "] / desk[";
			foreach (int num4 in deskList)
			{
				text2 = text2 + num4 + ",";
			}
			text2.Remove(text2.Length - 1, 1);
			text2 += "]";
			Debug.Log(text2);
			return true;
		}
		string text3 = "[TravelSimulator] かばん・つくえに弁当がありません：（放浪） / bag[";
		foreach (int num5 in bagList)
		{
			text3 = text3 + num5 + ",";
		}
		text3.Remove(text3.Length - 1, 1);
		text3 += "] / desk[";
		foreach (int num6 in deskList)
		{
			text3 = text3 + num6 + ",";
		}
		text3.Remove(text3.Length - 1, 1);
		text3 += "]";
		Debug.Log(text3);
		return false;
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x0002CAD4 File Offset: 0x0002AED4
	public void SetReturnState()
	{
		SuperGameMaster.saveData.home = true;
		SuperGameMaster.saveData.drift = false;
		SuperGameMaster.saveData.restTime = 0;
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0002CAF7 File Offset: 0x0002AEF7
	public void SetDriftState()
	{
		SuperGameMaster.saveData.home = false;
		SuperGameMaster.saveData.drift = true;
		SuperGameMaster.saveData.restTime = 0;
		SuperGameMaster.saveData.lastTravelTime = 0;
		SuperGameMaster.saveData.standbyWait = 0;
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x0002CB30 File Offset: 0x0002AF30
	public void GoTravel(int addTime)
	{
		this.GoTravel(addTime, -1);
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x0002CB3C File Offset: 0x0002AF3C
	public void GoTravel(int addTime, int _goalNodeId)
	{
		int num = SuperGameMaster.travel.getGoal();
		if (_goalNodeId != -1)
		{
			num = _goalNodeId;
		}
		int start = SuperGameMaster.travel.getStart(num);
		AreaType area = SuperGameMaster.sDataBase.get_NodeDB_AreaType(start);
		TravelSimulator.Travel travel = SuperGameMaster.travel.TestTravel(TravelSimulator.TestMode.NONE, area, start, num, new List<int>());
		List<ItemListFormat> list = new List<ItemListFormat>();
		list = SuperGameMaster.travel.getItem(travel.route);
		List<TravelSimulator.Event> list2 = new List<TravelSimulator.Event>();
		list2 = SuperGameMaster.travel.getEvt(travel);
		int num2 = 0;
		EventTimerFormat eventTimerFormat = new EventTimerFormat();
		int num3 = -1;
		int num4 = 4;
		string text = string.Empty;
		List<int> list3 = new List<int>();
		for (int i = 0; i < list2.Count; i++)
		{
			if (list2[i].code == TravelSimulator.EventCode.Picture_Unique)
			{
				list3.Add(i);
			}
		}
		if (list3.Count >= 1)
		{
			num4--;
			int index = UnityEngine.Random.Range(0, list3.Count);
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Unique（",
				list2[list3[index]].val,
				"）"
			});
			list3.RemoveAt(index);
			for (int j = 0; j < list3.Count; j++)
			{
				list2[list3[j]].code = TravelSimulator.EventCode.NONE;
			}
		}
		list3 = new List<int>();
		for (int k = 0; k < list2.Count; k++)
		{
			if (list2[k].code == TravelSimulator.EventCode.Picture_Tools)
			{
				list3.Add(k);
			}
		}
		if (list3.Count >= 1)
		{
			num4--;
			int index2 = UnityEngine.Random.Range(0, list3.Count);
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"Tools（",
				list2[list3[index2]].val,
				"）"
			});
			list3.RemoveAt(index2);
			for (int l = 0; l < list3.Count; l++)
			{
				list2[list3[l]].code = TravelSimulator.EventCode.NONE;
			}
		}
		list3 = new List<int>();
		for (int m = 0; m < list2.Count; m++)
		{
			if (list2[m].code == TravelSimulator.EventCode.Picture_Normal)
			{
				list3.Add(m);
			}
		}
		if (list3.Count >= 1)
		{
			text += "Normal（";
			for (int n = 0; n < num4; n++)
			{
				if (list3.Count == 0)
				{
					break;
				}
				int index3 = UnityEngine.Random.Range(0, list3.Count);
				text = text + list2[list3[index3]].val + ",";
				list3.RemoveAt(index3);
			}
			for (int num5 = 0; num5 < list3.Count; num5++)
			{
				list2[list3[num5]].code = TravelSimulator.EventCode.NONE;
			}
			text += "）";
		}
		foreach (TravelSimulator.Event @event in list2)
		{
			num2 += @event.time;
			TravelSimulator.EventCode code = @event.code;
			switch (code)
			{
			case TravelSimulator.EventCode.Picture_Normal:
			{
				int num6 = SuperGameMaster.sDataBase.search_NodeEdgeDBIndex_forId(@event.val);
				int num7 = -1;
				int num8 = -1;
				if (num6 != -1)
				{
					NodeEdgeDataFormat nodeEdgeDataFormat = SuperGameMaster.sDataBase.get_NodeEdgeDB(num6);
					num7 = SuperGameMaster.sDataBase.search_PictureTagDBIndex_forTag(nodeEdgeDataFormat.normalTag);
				}
				if (num7 != -1)
				{
					PictureTagDataFormat pictureTagDataFormat = SuperGameMaster.sDataBase.get_PictureTagDB(num7);
					int num9 = UnityEngine.Random.Range(0, pictureTagDataFormat.picName.Length);
					num8 = SuperGameMaster.sDataBase.search_PictureDBIndex_forName(pictureTagDataFormat.picName[num9]);
				}
				if (num8 != -1)
				{
					PictureDataFormat pictureDataFormat = SuperGameMaster.sDataBase.get_PictureDB(num8);
					eventTimerFormat = new EventTimerFormat();
					eventTimerFormat.id = -1;
					eventTimerFormat.timeSpanSec = num2 * 60 + addTime;
					eventTimerFormat.activeTime = -1;
					eventTimerFormat.addTime = new DateTime(1970, 1, 1);
					eventTimerFormat.evtType = TimerEvent.Type.Picture;
					eventTimerFormat.evtId = pictureDataFormat.id;
					eventTimerFormat.evtValue = new List<int>();
					eventTimerFormat.evtValue.Add(num3);
					eventTimerFormat.trigger = true;
					SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
				}
				break;
			}
			case TravelSimulator.EventCode.Picture_Tools:
			{
				int num10 = SuperGameMaster.sDataBase.search_NodeEdgeDBIndex_forId(@event.val);
				int num11 = -1;
				int num12 = -1;
				NodeEdgeDataFormat nodeEdgeDataFormat2 = new NodeEdgeDataFormat();
				if (num10 != -1)
				{
					nodeEdgeDataFormat2 = SuperGameMaster.sDataBase.get_NodeEdgeDB(num10);
					num11 = SuperGameMaster.sDataBase.search_PictureTagDBIndex_forTag(nodeEdgeDataFormat2.toolsTag);
				}
				if (num11 != -1)
				{
					PictureTagDataFormat pictureTagDataFormat2 = SuperGameMaster.sDataBase.get_PictureTagDB(num11);
					List<TravelSimulator.ItemEffect> list4 = new List<TravelSimulator.ItemEffect>();
					list4 = this.getBagItemEffect(EffectType.EVT_WAY);
					int num13 = 0;
					foreach (TravelSimulator.ItemEffect itemEffect in list4)
					{
						switch (itemEffect.elm)
						{
						case EffectElm.E_MOUNTAIN:
							if (nodeEdgeDataFormat2.wayType == WayType.Mountain && num13 < itemEffect.value)
							{
								num13 = itemEffect.value;
							}
							break;
						case EffectElm.E_SEA:
							if (nodeEdgeDataFormat2.wayType == WayType.Sea && num13 < itemEffect.value)
							{
								num13 = itemEffect.value;
							}
							break;
						case EffectElm.E_CAVE:
							if (nodeEdgeDataFormat2.wayType == WayType.Cave && num13 < itemEffect.value)
							{
								num13 = itemEffect.value;
							}
							break;
						case EffectElm.E_NORMAL:
							if (nodeEdgeDataFormat2.wayType == WayType.NONE && num13 < itemEffect.value)
							{
								num13 = itemEffect.value;
							}
							break;
						}
					}
					num12 = SuperGameMaster.sDataBase.search_PictureDBIndex_forName(pictureTagDataFormat2.picName[num13]);
				}
				if (num12 != -1)
				{
					PictureDataFormat pictureDataFormat2 = SuperGameMaster.sDataBase.get_PictureDB(num12);
					eventTimerFormat = new EventTimerFormat();
					eventTimerFormat.id = -1;
					eventTimerFormat.timeSpanSec = num2 * 60 + addTime;
					eventTimerFormat.activeTime = -1;
					eventTimerFormat.addTime = new DateTime(1970, 1, 1);
					eventTimerFormat.evtType = TimerEvent.Type.Picture;
					eventTimerFormat.evtId = pictureDataFormat2.id;
					eventTimerFormat.evtValue = new List<int>();
					eventTimerFormat.evtValue.Add(num3);
					eventTimerFormat.trigger = true;
					SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
				}
				break;
			}
			case TravelSimulator.EventCode.Picture_Unique:
			{
				int num14 = SuperGameMaster.sDataBase.search_NodeEdgeDBIndex_forId(@event.val);
				int num15 = -1;
				int num16 = -1;
				if (num14 != -1)
				{
					NodeEdgeDataFormat nodeEdgeDataFormat3 = SuperGameMaster.sDataBase.get_NodeEdgeDB(num14);
					num15 = SuperGameMaster.sDataBase.search_PictureTagDBIndex_forTag(nodeEdgeDataFormat3.uniqueTag);
				}
				if (num15 != -1)
				{
					PictureTagDataFormat pictureTagDataFormat3 = SuperGameMaster.sDataBase.get_PictureTagDB(num15);
					int num17 = (pictureTagDataFormat3.picName.Length - 1) / 2;
					bool flag = true;
					int num18 = 0;
					while (num18 < num17)
					{
						int val = Convert.ToInt32(pictureTagDataFormat3.picName[2 + num18 * 2]);
						string text3 = pictureTagDataFormat3.picName[1 + num18 * 2];
						if (text3 == null)
						{
							goto IL_C64;
						}
						if (!(text3 == "_TRAVELER"))
						{
							if (!(text3 == "_TRAVELER_ID"))
							{
								if (!(text3 == "_ITEM"))
								{
									goto IL_C64;
								}
								if (SuperGameMaster.GetBagList().FindIndex((int rec) => rec.Equals(val)) == -1)
								{
									flag = false;
								}
							}
							else if (num3 != val)
							{
								flag = false;
							}
						}
						else if (val == 0 && num3 != -1)
						{
							flag = false;
						}
						else if (val == 1 && num3 == -1)
						{
							flag = false;
						}
						else if (val != 0 && val != 1)
						{
							flag = false;
						}
						IL_C6C:
						num18++;
						continue;
						IL_C64:
						flag = false;
						goto IL_C6C;
					}
					if (flag)
					{
						num16 = SuperGameMaster.sDataBase.search_PictureDBIndex_forName(pictureTagDataFormat3.picName[0]);
						Debug.Log("[TravelSimurator] 成功：ユニーク判定：" + pictureTagDataFormat3.picName[0]);
					}
					else
					{
						Debug.Log("[TravelSimurator] 成功：ユニーク判定：" + pictureTagDataFormat3.picName[0]);
					}
				}
				if (num16 != -1)
				{
					PictureDataFormat pictureDataFormat3 = SuperGameMaster.sDataBase.get_PictureDB(num16);
					eventTimerFormat = new EventTimerFormat();
					eventTimerFormat.id = -1;
					eventTimerFormat.timeSpanSec = num2 * 60 + addTime;
					eventTimerFormat.activeTime = -1;
					eventTimerFormat.addTime = new DateTime(1970, 1, 1);
					eventTimerFormat.evtType = TimerEvent.Type.Picture;
					eventTimerFormat.evtId = pictureDataFormat3.id;
					eventTimerFormat.evtValue = new List<int>();
					eventTimerFormat.evtValue.Add(num3);
					eventTimerFormat.trigger = true;
					SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
				}
				break;
			}
			case TravelSimulator.EventCode.Traveler:
				num3 = @event.val;
				break;
			default:
				if (code != TravelSimulator.EventCode.GOAL)
				{
					if (code == TravelSimulator.EventCode.TimeUp)
					{
						eventTimerFormat = new EventTimerFormat();
						eventTimerFormat.id = -1;
						eventTimerFormat.timeSpanSec = num2 * 60 + addTime;
						eventTimerFormat.activeTime = -1;
						eventTimerFormat.addTime = new DateTime(1970, 1, 1);
						eventTimerFormat.evtType = TimerEvent.Type.BackHome;
						eventTimerFormat.evtId = num;
						eventTimerFormat.evtValue = new List<int>();
						eventTimerFormat.evtValue.Add(-1);
						eventTimerFormat.evtValue.Add(num2 * 60);
						eventTimerFormat.evtValue.Add(list[0].stock);
						eventTimerFormat.evtValue.Add(list[1].stock);
						eventTimerFormat.trigger = true;
						SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
						eventTimerFormat = new EventTimerFormat();
						eventTimerFormat.id = -1;
						eventTimerFormat.timeSpanSec = addTime;
						eventTimerFormat.activeTime = num2 * 60;
						eventTimerFormat.addTime = new DateTime(1970, 1, 1);
						eventTimerFormat.evtType = TimerEvent.Type.GoTravel;
						eventTimerFormat.evtId = 0;
						eventTimerFormat.evtValue = new List<int>();
						eventTimerFormat.evtValue.Add(eventTimerFormat.activeTime);
						eventTimerFormat.trigger = false;
						SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
						SuperGameMaster.saveData.lastTravelTime = num2 * 60 + addTime;
					}
				}
				else
				{
					eventTimerFormat = new EventTimerFormat();
					eventTimerFormat.id = -1;
					eventTimerFormat.timeSpanSec = num2 * 60 + addTime;
					eventTimerFormat.activeTime = -1;
					eventTimerFormat.addTime = new DateTime(1970, 1, 1);
					eventTimerFormat.evtType = TimerEvent.Type.BackHome;
					eventTimerFormat.evtId = num;
					eventTimerFormat.evtValue = new List<int>();
					eventTimerFormat.evtValue.Add(0);
					eventTimerFormat.evtValue.Add(num2 * 60);
					eventTimerFormat.evtValue.Add(list[0].stock);
					eventTimerFormat.evtValue.Add(list[1].stock);
					list.RemoveAt(0);
					list.RemoveAt(0);
					foreach (ItemListFormat itemListFormat in list)
					{
						eventTimerFormat.evtValue.Add(itemListFormat.id);
						eventTimerFormat.evtValue.Add(itemListFormat.stock);
					}
					eventTimerFormat.trigger = true;
					SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
					int num19 = SuperGameMaster.sDataBase.search_NodeDBIndex_forId(num);
					int num20 = -1;
					int num21 = -1;
					if (num19 != -1)
					{
						NodeDataFormat nodeDataFormat = SuperGameMaster.sDataBase.get_NodeDB(num19);
						num20 = SuperGameMaster.sDataBase.search_PictureTagDBIndex_forTag(nodeDataFormat.picTag);
					}
					if (num20 != -1)
					{
						PictureTagDataFormat pictureTagDataFormat4 = SuperGameMaster.sDataBase.get_PictureTagDB(num20);
						num21 = SuperGameMaster.sDataBase.search_PictureDBIndex_forName(pictureTagDataFormat4.picName[0]);
					}
					if (num21 != -1)
					{
						PictureDataFormat pictureDataFormat4 = SuperGameMaster.sDataBase.get_PictureDB(num21);
						eventTimerFormat = new EventTimerFormat();
						eventTimerFormat.id = -1;
						eventTimerFormat.timeSpanSec = num2 * 60 + addTime;
						eventTimerFormat.activeTime = -1;
						eventTimerFormat.addTime = new DateTime(1970, 1, 1);
						eventTimerFormat.evtType = TimerEvent.Type.Picture;
						eventTimerFormat.evtId = pictureDataFormat4.id;
						eventTimerFormat.evtValue = new List<int>();
						eventTimerFormat.evtValue.Add(num3);
						eventTimerFormat.evtValue.Add(num3);
						eventTimerFormat.trigger = true;
						SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
					}
					eventTimerFormat = new EventTimerFormat();
					eventTimerFormat.id = -1;
					eventTimerFormat.timeSpanSec = addTime;
					eventTimerFormat.activeTime = num2 * 60;
					eventTimerFormat.addTime = new DateTime(1970, 1, 1);
					eventTimerFormat.evtType = TimerEvent.Type.GoTravel;
					eventTimerFormat.evtId = 0;
					eventTimerFormat.evtValue = new List<int>();
					eventTimerFormat.evtValue.Add(eventTimerFormat.activeTime);
					eventTimerFormat.trigger = false;
					SuperGameMaster.evtMgr.TimerAdd(eventTimerFormat);
					SuperGameMaster.saveData.lastTravelTime = num2 * 60 + addTime;
				}
				break;
			}
		}
		Debug.Log("[TravelSimulator] GoTravel：" + num);
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x0002D92C File Offset: 0x0002BD2C
	public TravelSimulator.Travel TestTravel(TravelSimulator.TestMode MODE, AreaType area, int startNodeId, int goalNodeId, List<int> pathNode)
	{
		List<TravelSimulator.DNode> dnode = this.getDNode(area);
		TravelSimulator.Travel travel = new TravelSimulator.Travel();
		travel.route = new List<int>();
		travel.pathNode = new List<int>();
		travel.nodeType = new List<NodeType>();
		travel.edgeRoute = new List<int>();
		travel.edgeType = new List<WayType>();
		travel.edgeCost = new List<int>();
		switch (MODE + 1)
		{
		case TravelSimulator.TestMode.Manual:
			break;
		case TravelSimulator.TestMode.SemiAuto:
			return this.MoveCheck(dnode, startNodeId, goalNodeId, pathNode);
		case TravelSimulator.TestMode.Auto:
			break;
		case TravelSimulator.TestMode.MODE_MAX:
			break;
		default:
			return travel;
		}
		NodeDataFormat nodeDataFormat = SuperGameMaster.sDataBase.get_NodeDB_forId(goalNodeId);
		int pathId = nodeDataFormat.pathId;
		pathNode = new List<int>();
		pathNode.Add(pathId);
		travel = this.MoveCheck(dnode, startNodeId, goalNodeId, pathNode);
		pathNode.AddRange(this.getDetour(dnode, travel.route, 3, new List<int>
		{
			startNodeId,
			goalNodeId,
			pathId
		}));
		travel = this.MoveCheck(dnode, startNodeId, goalNodeId, pathNode);
		return travel;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x0002DA3C File Offset: 0x0002BE3C
	public List<TravelSimulator.DNode> getDNode(AreaType area)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < 5; i++)
		{
			list.Add(100);
		}
		List<TravelSimulator.ItemEffect> list2 = new List<TravelSimulator.ItemEffect>();
		list2 = this.getBagItemEffect(EffectType.WAY_SPEED);
		foreach (TravelSimulator.ItemEffect itemEffect in list2)
		{
			EffectElm elm = itemEffect.elm;
			switch (elm)
			{
			case EffectElm.W_MOUNTAIN:
				list[1] = (int)((double)((float)list[1] * (100f - (float)itemEffect.value)) * 0.01);
				break;
			case EffectElm.W_SEA:
				list[2] = (int)((double)((float)list[2] * (100f - (float)itemEffect.value)) * 0.01);
				break;
			case EffectElm.W_CAVE:
				list[3] = (int)((double)((float)list[3] * (100f - (float)itemEffect.value)) * 0.01);
				break;
			default:
				if (elm != EffectElm.W_NONE)
				{
					if (elm == EffectElm.W_ALL)
					{
						list[4] = (int)((double)((float)list[4] * (100f - (float)itemEffect.value)) * 0.01);
					}
				}
				else
				{
					list[0] = (int)((double)((float)list[0] * (100f - (float)itemEffect.value)) * 0.01);
				}
				break;
			}
		}
		List<TravelSimulator.DNode> list3 = new List<TravelSimulator.DNode>();
		int id = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(area);
		int num = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(area + 1);
		for (int j = SuperGameMaster.sDataBase.search_NodeDBIndex_forId(id); j < SuperGameMaster.sDataBase.count_NodeDB(); j++)
		{
			NodeDataFormat nodeDataFormat = SuperGameMaster.sDataBase.get_NodeDB(j);
			if (nodeDataFormat.id == num)
			{
				break;
			}
			TravelSimulator.DNode dnode = new TravelSimulator.DNode();
			dnode.nodeId = nodeDataFormat.id;
			dnode.nodeType = nodeDataFormat.type;
			NodeConnectDataFormat nodeConnectDataFormat = SuperGameMaster.sDataBase.get_NodeConnectDB_forId(nodeDataFormat.id);
			dnode.edges_to = new List<int>();
			dnode.edges_cost = new List<int>();
			dnode.edges = new List<int>();
			dnode.edges_type = new List<WayType>();
			foreach (int id2 in nodeConnectDataFormat.edge)
			{
				NodeEdgeDataFormat nodeEdgeDataFormat = SuperGameMaster.sDataBase.get_NodeEdgeDB_forId(id2);
				if (nodeEdgeDataFormat.plug[0] != nodeDataFormat.id)
				{
					dnode.edges_to.Add(nodeEdgeDataFormat.plug[0]);
				}
				if (nodeEdgeDataFormat.plug[1] != nodeDataFormat.id)
				{
					dnode.edges_to.Add(nodeEdgeDataFormat.plug[1]);
				}
				double num2 = (double)nodeEdgeDataFormat.time;
				if (nodeEdgeDataFormat.wayType != WayType.NONE)
				{
					num2 += (double)nodeEdgeDataFormat.plusTime * ((double)list[(int)nodeEdgeDataFormat.wayType] * 0.01);
				}
				else
				{
					num2 *= (double)list[(int)nodeEdgeDataFormat.wayType] * 0.01;
				}
				num2 *= (double)list[4] * 0.01;
				dnode.edges_cost.Add((int)num2);
				dnode.edges_type.Add(nodeEdgeDataFormat.wayType);
				dnode.edges.Add(nodeEdgeDataFormat.id);
			}
			dnode.cost = 99999999;
			dnode.route = new List<int>();
			list3.Add(dnode);
		}
		return list3;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x0002DE18 File Offset: 0x0002C218
	public TravelSimulator.Travel MoveCheck(List<TravelSimulator.DNode> refNODE, int startNodeId, int goalNodeId, List<int> pathNode)
	{
		TravelSimulator.<MoveCheck>c__AnonStorey2 <MoveCheck>c__AnonStorey = new TravelSimulator.<MoveCheck>c__AnonStorey2();
		<MoveCheck>c__AnonStorey.pathNode = pathNode;
		List<TravelSimulator.DNode> list = new List<TravelSimulator.DNode>();
		foreach (TravelSimulator.DNode dnode in refNODE)
		{
			dnode.cost = 99999999;
			dnode.route = new List<int>();
			list.Add(new TravelSimulator.DNode(dnode));
		}
		<MoveCheck>c__AnonStorey.travel = new TravelSimulator.Travel();
		<MoveCheck>c__AnonStorey.travel.route = new List<int>();
		<MoveCheck>c__AnonStorey.travel.pathNode = new List<int>();
		<MoveCheck>c__AnonStorey.travel.nodeType = new List<NodeType>();
		<MoveCheck>c__AnonStorey.travel.edgeRoute = new List<int>();
		<MoveCheck>c__AnonStorey.travel.edgeType = new List<WayType>();
		<MoveCheck>c__AnonStorey.travel.edgeCost = new List<int>();
		int i;
		if (<MoveCheck>c__AnonStorey.pathNode.Count == 0)
		{
			TravelSimulator.DNode dnode2 = this.Dijkstra(list, startNodeId, goalNodeId);
			<MoveCheck>c__AnonStorey.travel.route.AddRange(dnode2.route);
			<MoveCheck>c__AnonStorey.travel.route.Add(goalNodeId);
			<MoveCheck>c__AnonStorey.travel.cost = dnode2.cost;
		}
		else
		{
			List<TravelSimulator.DNode> list2 = new List<TravelSimulator.DNode>();
			List<TravelSimulator.DNode> list3 = new List<TravelSimulator.DNode>();
			foreach (int num in <MoveCheck>c__AnonStorey.pathNode)
			{
				list2.Add(this.Dijkstra(list, startNodeId, num));
				list3.Add(this.Dijkstra(list, num, goalNodeId));
			}
			if (<MoveCheck>c__AnonStorey.pathNode.Count == 1)
			{
				<MoveCheck>c__AnonStorey.travel.route.AddRange(list2[0].route);
				<MoveCheck>c__AnonStorey.travel.route.AddRange(list3[0].route);
				<MoveCheck>c__AnonStorey.travel.route.Add(goalNodeId);
				<MoveCheck>c__AnonStorey.travel.cost = list2[0].cost + list3[0].cost;
				<MoveCheck>c__AnonStorey.travel.pathNode.Add(<MoveCheck>c__AnonStorey.pathNode[0]);
			}
			else
			{
				List<int> list4 = new List<int>();
				for (int m = 0; m < <MoveCheck>c__AnonStorey.pathNode.Count; m++)
				{
					list4.Add(m);
				}
				List<List<int>> list5 = new List<List<int>>(this.Permutation_int(list4));
				List<List<TravelSimulator.DNode>> list6 = new List<List<TravelSimulator.DNode>>();
				int i;
				for (i = 0; i < <MoveCheck>c__AnonStorey.pathNode.Count; i++)
				{
					List<TravelSimulator.DNode> list7 = new List<TravelSimulator.DNode>();
					for (int j = 0; j < <MoveCheck>c__AnonStorey.pathNode.Count; j++)
					{
						if (i < j)
						{
							list7.Add(this.Dijkstra(list, <MoveCheck>c__AnonStorey.pathNode[i], <MoveCheck>c__AnonStorey.pathNode[j]));
						}
						else if (i > j)
						{
							int index = list.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(<MoveCheck>c__AnonStorey.pathNode[i]));
							TravelSimulator.DNode dnode3 = new TravelSimulator.DNode(list[index]);
							dnode3.route = new List<int>(list6[j][i].route);
							dnode3.route.RemoveAt(0);
							dnode3.route.Add(<MoveCheck>c__AnonStorey.pathNode[i]);
							dnode3.route.Reverse();
							dnode3.cost = list6[j][i].cost;
							list7.Add(new TravelSimulator.DNode(dnode3));
						}
						else
						{
							int index2 = list.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(<MoveCheck>c__AnonStorey.pathNode[i]));
							list7.Add(new TravelSimulator.DNode(new TravelSimulator.DNode(list[index2])
							{
								route = new List<int>(),
								cost = 0
							}));
						}
					}
					list6.Add(list7);
				}
				<MoveCheck>c__AnonStorey.travel.cost = 99999999;
				<MoveCheck>c__AnonStorey.travel.route = new List<int>();
				foreach (List<int> list8 in list5)
				{
					int num2 = 0;
					List<int> list9 = new List<int>();
					list9.AddRange(list2[list8[0]].route);
					num2 += list2[list8[0]].cost;
					for (int k = 0; k < list8.Count - 1; k++)
					{
						list9.AddRange(list6[list8[k]][list8[k + 1]].route);
						num2 += list6[list8[k]][list8[k + 1]].cost;
					}
					list9.AddRange(list3[list8[list8.Count - 1]].route);
					list9.Add(goalNodeId);
					num2 += list3[list8[list8.Count - 1]].cost;
					if (<MoveCheck>c__AnonStorey.travel.cost > num2)
					{
						<MoveCheck>c__AnonStorey.travel.cost = num2;
						<MoveCheck>c__AnonStorey.travel.route = new List<int>(list9);
						<MoveCheck>c__AnonStorey.travel.pathNode = new List<int>();
						foreach (int index3 in list8)
						{
							<MoveCheck>c__AnonStorey.travel.pathNode.Add(<MoveCheck>c__AnonStorey.pathNode[index3]);
						}
					}
				}
			}
		}
		List<int> list10 = new List<int>();
		List<WayType> list11 = new List<WayType>();
		List<int> list12 = new List<int>();
		List<NodeType> list13 = new List<NodeType>();
		for (i = 0; i < <MoveCheck>c__AnonStorey.travel.route.Count; i++)
		{
			int index4 = list.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(<MoveCheck>c__AnonStorey.travel.route[i]));
			list13.Add(list[index4].nodeType);
			if (i == <MoveCheck>c__AnonStorey.travel.route.Count - 1)
			{
				break;
			}
			WayType item = WayType.NONE;
			int item2 = -1;
			int item3 = 0;
			for (int l = 0; l < list[index4].edges_to.Count; l++)
			{
				if (list[index4].edges_to[l] == <MoveCheck>c__AnonStorey.travel.route[i + 1])
				{
					item2 = list[index4].edges[l];
					item = list[index4].edges_type[l];
					item3 = list[index4].edges_cost[l];
					break;
				}
			}
			list10.Add(item2);
			list11.Add(item);
			list12.Add(item3);
		}
		<MoveCheck>c__AnonStorey.travel.nodeType = new List<NodeType>(list13);
		<MoveCheck>c__AnonStorey.travel.edgeRoute = new List<int>(list10);
		<MoveCheck>c__AnonStorey.travel.edgeType = new List<WayType>(list11);
		<MoveCheck>c__AnonStorey.travel.edgeCost = new List<int>(list12);
		return <MoveCheck>c__AnonStorey.travel;
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x0002E670 File Offset: 0x0002CA70
	public TravelSimulator.DNode Dijkstra(List<TravelSimulator.DNode> refNODE, int startNodeId, int goalNodeId)
	{
		List<TravelSimulator.DNode> list = new List<TravelSimulator.DNode>();
		foreach (TravelSimulator.DNode dnode in refNODE)
		{
			dnode.cost = 99999999;
			dnode.route = new List<int>();
			list.Add(new TravelSimulator.DNode(dnode));
		}
		int index = list.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(startNodeId));
		list[index].cost = 0;
		List<TravelSimulator.DNode> taskNode = new List<TravelSimulator.DNode>();
		taskNode.Add(list[index]);
		while (taskNode.Count != 0)
		{
			while (taskNode[0].edges_to.Count != 0)
			{
				int num = taskNode.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(taskNode[0].edges_to[0]));
				if (num == -1)
				{
					num = taskNode.Count;
					int index2 = list.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(taskNode[0].edges_to[0]));
					taskNode.Add(list[index2]);
				}
				if (taskNode[num].cost > taskNode[0].cost + taskNode[0].edges_cost[0])
				{
					taskNode[num].cost = taskNode[0].cost + taskNode[0].edges_cost[0];
					taskNode[num].route = new List<int>(taskNode[0].route);
					taskNode[num].route.Add(taskNode[0].nodeId);
				}
				taskNode[0].edges_to.RemoveAt(0);
				taskNode[0].edges_cost.RemoveAt(0);
				int num2 = taskNode[num].edges_to.FindIndex((int node) => node.Equals(taskNode[0].nodeId));
				if (num2 != -1)
				{
					taskNode[num].edges_to.RemoveAt(num2);
					taskNode[num].edges_cost.RemoveAt(num2);
				}
			}
			int index3 = list.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(taskNode[0].nodeId));
			list[index3].cost = taskNode[0].cost;
			list[index3].route = new List<int>(taskNode[0].route);
			taskNode.RemoveAt(0);
			taskNode.Sort((TravelSimulator.DNode x, TravelSimulator.DNode y) => x.cost - y.cost);
			if (list[index3].nodeId == goalNodeId)
			{
				break;
			}
		}
		int index4 = list.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(goalNodeId));
		string text = "移動ノード：" + list[index4].route.Count + " / ";
		foreach (int num3 in list[index4].route)
		{
			text = text + num3.ToString() + " > ";
		}
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"(",
			goalNodeId,
			")"
		});
		return list[index4];
	}

	// Token: 0x06000738 RID: 1848 RVA: 0x0002EAA4 File Offset: 0x0002CEA4
	public List<List<int>> Permutation_int(List<int> list)
	{
		if (list.Count == 1)
		{
			return new List<List<int>>
			{
				list
			};
		}
		List<List<int>> list2 = new List<List<int>>();
		for (int i = 0; i < list.Count; i++)
		{
			List<int> list3 = new List<int>(list);
			list3.RemoveAt(i);
			foreach (List<int> list4 in this.Permutation_int(list3))
			{
				list4.Insert(0, list[i]);
				list2.Add(list4);
			}
		}
		return list2;
	}

	// Token: 0x06000739 RID: 1849 RVA: 0x0002EB5C File Offset: 0x0002CF5C
	public List<int> getDetour(List<TravelSimulator.DNode> refNODE, List<int> route, int detourCnt, List<int> excludeNode)
	{
		TravelSimulator.<getDetour>c__AnonStorey7 <getDetour>c__AnonStorey = new TravelSimulator.<getDetour>c__AnonStorey7();
		<getDetour>c__AnonStorey.route = route;
		<getDetour>c__AnonStorey.NODE = new List<TravelSimulator.DNode>();
		foreach (TravelSimulator.DNode original in refNODE)
		{
			<getDetour>c__AnonStorey.NODE.Add(new TravelSimulator.DNode(original));
		}
		if (<getDetour>c__AnonStorey.route.Count < detourCnt)
		{
			detourCnt = <getDetour>c__AnonStorey.route.Count;
		}
		List<int> list = new List<int>();
		for (int k = 0; k < detourCnt; k++)
		{
			int routeIndex = UnityEngine.Random.Range(0, <getDetour>c__AnonStorey.route.Count);
			if (list.FindIndex((int index) => index.Equals(routeIndex)) != -1)
			{
				k--;
			}
			else
			{
				list.Add(routeIndex);
			}
		}
		<getDetour>c__AnonStorey.detourId = new List<int>();
		using (List<int>.Enumerator enumerator2 = list.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				TravelSimulator.<getDetour>c__AnonStorey8 <getDetour>c__AnonStorey3 = new TravelSimulator.<getDetour>c__AnonStorey8();
				<getDetour>c__AnonStorey3.routeIndex = enumerator2.Current;
				int pointNodeIndex = <getDetour>c__AnonStorey.NODE.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(<getDetour>c__AnonStorey.route[<getDetour>c__AnonStorey3.routeIndex]));
				int point_to_edgeNum = UnityEngine.Random.Range(0, <getDetour>c__AnonStorey.NODE[pointNodeIndex].edges_to.Count);
				for (int j = 0; j < <getDetour>c__AnonStorey.NODE[pointNodeIndex].edges_to.Count; j++)
				{
					int index2 = <getDetour>c__AnonStorey.NODE.FindIndex((TravelSimulator.DNode node) => node.nodeId.Equals(<getDetour>c__AnonStorey.NODE[pointNodeIndex].edges_to[point_to_edgeNum]));
					if (<getDetour>c__AnonStorey.NODE[index2].nodeType == NodeType.Goal || <getDetour>c__AnonStorey.NODE[index2].nodeType == NodeType.Detour)
					{
						<getDetour>c__AnonStorey.detourId.Add(<getDetour>c__AnonStorey.NODE[index2].nodeId);
						break;
					}
					point_to_edgeNum++;
					if (point_to_edgeNum >= <getDetour>c__AnonStorey.NODE[pointNodeIndex].edges_to.Count)
					{
						point_to_edgeNum = 0;
					}
				}
			}
		}
		int i;
		for (i = <getDetour>c__AnonStorey.detourId.Count - 1; i >= 0; i--)
		{
			if (excludeNode.FindIndex((int id) => id.Equals(<getDetour>c__AnonStorey.detourId[i])) != -1)
			{
				<getDetour>c__AnonStorey.detourId.RemoveAt(i);
			}
			else
			{
				int num = <getDetour>c__AnonStorey.detourId.FindIndex((int index) => index.Equals(<getDetour>c__AnonStorey.detourId[i]));
				if (num != i)
				{
					<getDetour>c__AnonStorey.detourId.RemoveAt(i);
				}
			}
		}
		return <getDetour>c__AnonStorey.detourId;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x0002EEC8 File Offset: 0x0002D2C8
	public int getGoal()
	{
		return this.getGoal(AreaType.NONE);
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x0002EED4 File Offset: 0x0002D2D4
	public int getGoal(AreaType areaFix)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < 5; i++)
		{
			list.Add(100);
		}
		List<TravelSimulator.ItemEffect> list2 = new List<TravelSimulator.ItemEffect>();
		list2 = this.getBagItemEffect(EffectType.AREA_DECIDE);
		foreach (TravelSimulator.ItemEffect itemEffect in list2)
		{
			int num = itemEffect.elm - EffectElm.A_EAST;
			if (itemEffect.elm == EffectElm.A_NEW_AREA)
			{
				num = 4;
			}
			if (itemEffect.value == -2)
			{
				list[num] = -2;
			}
			if (itemEffect.value == -1 && list[num] != -2)
			{
				list[num] = -1;
			}
			if (list[num] != -1 && list[num] != -2)
			{
				List<int> list3;
				int index;
				(list3 = list)[index = num] = list3[index] + itemEffect.value;
			}
		}
		if (list[4] != 100)
		{
			List<bool> undevelopedArea = this.get_UndevelopedArea();
			int num2 = list[4] - 100;
			for (int j = 0; j < 4; j++)
			{
				if (undevelopedArea[j])
				{
					if (num2 == -2)
					{
						list[j] = -2;
					}
					if (num2 == -1 && list[j] != -2)
					{
						list[j] = -1;
					}
					if (list[j] != -1 && list[j] != -2)
					{
						List<int> list3;
						int index2;
						(list3 = list)[index2 = j] = list3[index2] + num2;
					}
				}
			}
		}
		List<int> list4 = new List<int>();
		list4.AddRange(SuperGameMaster.GetBagList());
		List<NodeGoalDataFormat> list5 = new List<NodeGoalDataFormat>();
		List<int> list6 = new List<int>();
		list6 = SuperGameMaster.sDataBase.getList_NodeGoalIdList();
		foreach (int id in list6)
		{
			list5.Add(new NodeGoalDataFormat
			{
				id = id,
				itemPer = new int[1]
			});
		}
		for (int k = 0; k < list4.Count; k++)
		{
			if (list4[k] != -1 || k == 2 || k == 3)
			{
				List<int> list_NodeGoalItemPer = SuperGameMaster.sDataBase.getList_NodeGoalItemPer(list4[k]);
				if (list_NodeGoalItemPer != null)
				{
					for (int l = 0; l < list_NodeGoalItemPer.Count; l++)
					{
						list5[l].itemPer[0] += list_NodeGoalItemPer[l];
					}
				}
			}
		}
		for (int m = list5.Count - 1; m >= 0; m--)
		{
			if (list5[m].itemPer[0] <= 0)
			{
				list5.RemoveAt(m);
			}
		}
		if (areaFix != AreaType.NONE)
		{
			int num3 = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(areaFix);
			int num4 = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(areaFix + 1);
			for (int n = list5.Count - 1; n >= 0; n--)
			{
				if (list5[n].id < num3 || list5[n].id >= num4)
				{
					list5.RemoveAt(n);
				}
			}
		}
		if (areaFix == AreaType.NONE)
		{
			int num5 = 0;
			if (list.FindIndex((int rec) => rec.Equals(-1)) != -1)
			{
				num5 = -1;
			}
			if (list.FindIndex((int rec) => rec.Equals(-2)) != -1)
			{
				num5 = -2;
			}
			if (num5 != 0)
			{
				for (int num6 = 0; num6 < list.Count - 1; num6++)
				{
					if (list[num6] != num5)
					{
						int num7 = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex((AreaType)num6);
						int num8 = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(num6 + AreaType.West);
						for (int num9 = list5.Count - 1; num9 >= 0; num9--)
						{
							if (list5[num9].id >= num7 && list5[num9].id < num8)
							{
								list5.RemoveAt(num9);
							}
						}
					}
				}
			}
			else
			{
				for (int num10 = 0; num10 < list.Count - 1; num10++)
				{
					if (list[num10] != 100)
					{
						int num11 = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex((AreaType)num10);
						int num12 = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(num10 + AreaType.West);
						for (int num13 = list5.Count - 1; num13 >= 0; num13--)
						{
							if (list5[num13].id >= num11 && list5[num13].id < num12)
							{
								list5[num13].itemPer[0] = (int)((double)(list5[num13].itemPer[0] * list[num10]) * 0.01);
							}
						}
					}
				}
			}
		}
		for (int num14 = 1; num14 < list5.Count; num14++)
		{
			list5[num14].itemPer[0] += list5[num14 - 1].itemPer[0];
		}
		if (list5.Count == 0)
		{
			Debug.Log("[TravelSimulator] 目的地が見つかりませんでした：" + areaFix.ToString() + ")");
			return -1;
		}
		int num15 = -1;
		int num16 = UnityEngine.Random.Range(0, list5[list5.Count - 1].itemPer[0]);
		for (int num17 = 0; num17 < list5.Count; num17++)
		{
			if (num16 < list5[num17].itemPer[0])
			{
				num15 = list5[num17].id;
				break;
			}
		}
		Debug.Log(string.Concat(new object[]
		{
			"[TravelSimulator] 目的地を取得しました：",
			num15,
			" ( ",
			num16,
			" ) "
		}));
		return num15;
	}

	// Token: 0x0600073C RID: 1852 RVA: 0x0002F570 File Offset: 0x0002D970
	public int getStart(int goalNodeId)
	{
		List<TravelSimulator.ItemEffect> list = new List<TravelSimulator.ItemEffect>();
		list = this.getBagItemEffect(EffectType.TELEPORT);
		bool flag = false;
		foreach (TravelSimulator.ItemEffect itemEffect in list)
		{
			if (itemEffect.value == 1)
			{
				flag = true;
			}
		}
		int result;
		if (!flag)
		{
			result = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(goalNodeId);
		}
		else
		{
			NodeDataFormat nodeDataFormat = SuperGameMaster.sDataBase.get_NodeDB_forId(goalNodeId);
			result = nodeDataFormat.pathId;
		}
		return result;
	}

	// Token: 0x0600073D RID: 1853 RVA: 0x0002F614 File Offset: 0x0002DA14
	public List<ItemListFormat> getItem(List<int> refRouteNode)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int stock = 0;
		int num5 = 0;
		List<TravelSimulator.ItemEffect> list = new List<TravelSimulator.ItemEffect>();
		list = this.getBagItemEffect(EffectType.ITEM_PERCENT);
		foreach (TravelSimulator.ItemEffect itemEffect in list)
		{
			switch (itemEffect.elm)
			{
			case EffectElm.I_UP:
				num += itemEffect.value;
				num2 += itemEffect.value;
				break;
			case EffectElm.I_CLOVER:
				num3 += itemEffect.value;
				break;
			case EffectElm.I_CLOVER_RND:
				num4 += itemEffect.value;
				break;
			case EffectElm.I_TICKET:
				num5 += itemEffect.value;
				break;
			}
		}
		stock = UnityEngine.Random.Range(num3, num3 + num4 + 1);
		List<ItemListFormat> list2 = new List<ItemListFormat>();
		if (refRouteNode.Count == 0)
		{
			return list2;
		}
		List<int> list3 = new List<int>(refRouteNode);
		int id = list3[list3.Count - 1];
		NodeDataFormat nodeDataFormat = SuperGameMaster.sDataBase.get_NodeDB_forId(id);
		int pathNodeId = nodeDataFormat.pathId;
		int index = list3.FindIndex((int route) => route.Equals(pathNodeId));
		list3.RemoveAt(index);
		NodeItemDataFormat nodeItemDataFormat = new NodeItemDataFormat();
		nodeItemDataFormat = SuperGameMaster.sDataBase.get_NodeItemDB_forId(pathNodeId);
		bool flag = false;
		if (nodeItemDataFormat.collectionId != -1 && !SuperGameMaster.FindCollection(nodeItemDataFormat.collectionId))
		{
			int num6 = 100 - num;
			int key = SuperGameMaster.CollectionFailedCount(nodeItemDataFormat.collectionId);
			if (num6 < Define.COLLECT_PER[key])
			{
				num6 = Define.COLLECT_PER[key];
			}
			int num7 = UnityEngine.Random.Range(0, num6);
			if (Define.COLLECT_PER[key] > num7)
			{
				list2.Add(new ItemListFormat
				{
					id = nodeItemDataFormat.collectionId + 10000,
					stock = 1
				});
				flag = true;
			}
		}
		if (list2.Count == 0 && nodeItemDataFormat.specialtyId.Length != 0)
		{
			int num8 = 0;
			for (int i = 0; i < nodeItemDataFormat.specialtyId.Length; i++)
			{
				num8 += nodeItemDataFormat.specialtyPer[i];
			}
			int num9 = UnityEngine.Random.Range(0, num8);
			for (int j = 0; j < nodeItemDataFormat.specialtyId.Length; j++)
			{
				if (nodeItemDataFormat.specialtyPer[j] > num9)
				{
					list2.Add(new ItemListFormat
					{
						id = nodeItemDataFormat.specialtyId[j],
						stock = 1
					});
					break;
				}
				num9 -= nodeItemDataFormat.specialtyPer[j];
			}
		}
		foreach (int id2 in list3)
		{
			NodeItemDataFormat nodeItemDataFormat2 = new NodeItemDataFormat();
			nodeItemDataFormat2 = SuperGameMaster.sDataBase.get_NodeItemDB_forId(id2);
			if (nodeItemDataFormat2.specialtyId.Length != 0)
			{
				int num10 = 100 - num;
				if (num10 < 60)
				{
					num10 = 60;
				}
				int num11 = UnityEngine.Random.Range(0, num10);
				if (60 > num11)
				{
					int num12 = 0;
					for (int k = 0; k < nodeItemDataFormat2.specialtyId.Length; k++)
					{
						num12 += nodeItemDataFormat2.specialtyPer[k];
					}
					num11 = UnityEngine.Random.Range(0, num12);
					for (int l = 0; l < nodeItemDataFormat2.specialtyId.Length; l++)
					{
						if (nodeItemDataFormat2.specialtyPer[l] > num11)
						{
							list2.Add(new ItemListFormat
							{
								id = nodeItemDataFormat2.specialtyId[l],
								stock = 1
							});
							break;
						}
						num11 -= nodeItemDataFormat2.specialtyPer[l];
					}
					if ((list2.Count >= 10 && flag) || list2.Count >= 11)
					{
						break;
					}
				}
			}
		}
		list2.Insert(0, new ItemListFormat
		{
			id = -1,
			stock = stock
		});
		list2.Insert(1, new ItemListFormat
		{
			id = -2,
			stock = num5
		});
		string text = string.Empty;
		foreach (ItemListFormat itemListFormat in list2)
		{
			text += itemListFormat.id.ToString();
			if (itemListFormat.stock != 1)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"(*",
					itemListFormat.stock,
					")"
				});
			}
			text += ",";
		}
		if (list2.Count != 0)
		{
			text = text.Remove(text.Length - 1, 1);
		}
		return new List<ItemListFormat>(list2);
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x0002FB98 File Offset: 0x0002DF98
	public List<TravelSimulator.Event> getEvt(TravelSimulator.Travel refTravel)
	{
		int num = -1;
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		for (int i = 0; i < SuperGameMaster.sDataBase.count_PictureCharaDB(); i++)
		{
			PictureCharaDataFormat pictureCharaDataFormat = SuperGameMaster.sDataBase.get_PictureCharaDB(i);
			dictionary.Add(pictureCharaDataFormat.name, pictureCharaDataFormat.id);
		}
		TravelSimulator.Travel travel = new TravelSimulator.Travel(refTravel);
		List<TravelSimulator.Event> list = new List<TravelSimulator.Event>();
		int num2 = 100;
		int num3 = 0;
		int num4 = 60;
		List<int> list2 = new List<int>();
		for (int j = 0; j < 5; j++)
		{
			list2.Add(0);
		}
		int num5 = 0;
		List<int> list3 = new List<int>();
		for (int k = 0; k < SuperGameMaster.sDataBase.count_PictureCharaDB() - 1; k++)
		{
			list3.Add(0);
		}
		bool flag = false;
		List<TravelSimulator.ItemEffect> list4 = new List<TravelSimulator.ItemEffect>();
		list4 = this.getBagItemEffect(EffectType.HP);
		list4.AddRange(this.getBagItemEffect(EffectType.EVT_WAY));
		list4.AddRange(this.getBagItemEffect(EffectType.FRIENDSHIP));
		foreach (TravelSimulator.ItemEffect itemEffect in list4)
		{
			EffectElm elm = itemEffect.elm;
			switch (elm)
			{
			case EffectElm.E_MOUNTAIN:
				if (list2[0] < Define.PICTURE_TOOLS_PLUSPER[itemEffect.value])
				{
					list2[0] = Define.PICTURE_TOOLS_PLUSPER[itemEffect.value];
				}
				break;
			case EffectElm.E_SEA:
				if (list2[1] < Define.PICTURE_TOOLS_PLUSPER[itemEffect.value])
				{
					list2[1] = Define.PICTURE_TOOLS_PLUSPER[itemEffect.value];
				}
				break;
			case EffectElm.E_CAVE:
				if (list2[2] < Define.PICTURE_TOOLS_PLUSPER[itemEffect.value])
				{
					list2[2] = Define.PICTURE_TOOLS_PLUSPER[itemEffect.value];
				}
				break;
			case EffectElm.E_NORMAL:
				if (list2[3] < Define.PICTURE_TOOLS_PLUSPER[itemEffect.value])
				{
					list2[3] = Define.PICTURE_TOOLS_PLUSPER[itemEffect.value];
				}
				break;
			default:
				switch (elm)
				{
				case EffectElm.F_NONE:
					if (num5 != -1)
					{
						num5 += itemEffect.value;
					}
					if (itemEffect.value == -1)
					{
						num5 = -1;
						flag = true;
					}
					break;
				case EffectElm.F_0:
					if (list3[0] != -1)
					{
						List<int> list5;
						(list5 = list3)[0] = list5[0] + itemEffect.value;
					}
					if (itemEffect.value == -1)
					{
						list3[0] = -1;
						flag = true;
					}
					break;
				case EffectElm.F_1:
					if (list3[1] != -1)
					{
						List<int> list5;
						(list5 = list3)[1] = list5[1] + itemEffect.value;
					}
					if (itemEffect.value == -1)
					{
						list3[1] = -1;
						flag = true;
					}
					break;
				case EffectElm.F_2:
					if (list3[2] != -1)
					{
						List<int> list5;
						(list5 = list3)[2] = list5[2] + itemEffect.value;
					}
					if (itemEffect.value == -1)
					{
						list3[2] = -1;
						flag = true;
					}
					break;
				default:
					switch (elm)
					{
					case EffectElm.H_UP:
						num3 += itemEffect.value;
						break;
					case EffectElm.H_UP_RND:
						num3 += UnityEngine.Random.Range(0, itemEffect.value);
						break;
					case EffectElm.H_MAXTIME:
						num4 += (itemEffect.value - 1) * 60;
						break;
					}
					break;
				}
				break;
			}
		}
		num2 += (int)((double)(num2 * num3) * 0.01);
		list.Add(new TravelSimulator.Event
		{
			code = TravelSimulator.EventCode.START,
			val = travel.route[0],
			time = 0
		});
		if (flag)
		{
			List<int> list6 = new List<int>();
			if (num5 == -1)
			{
				for (int l = 0; l < list3.Count; l++)
				{
					list6.Add(l);
				}
			}
			else
			{
				for (int m = 0; m < list3.Count; m++)
				{
					if (list3[m] == -1)
					{
						list6.Add(m);
					}
				}
			}
			int index = UnityEngine.Random.Range(0, list6.Count);
			num = list6[index];
			list.Add(new TravelSimulator.Event
			{
				code = TravelSimulator.EventCode.Traveler,
				val = num,
				time = 0
			});
		}
		else
		{
			int num6 = UnityEngine.Random.Range(0, 100);
			for (int n = 0; n < list3.Count; n++)
			{
				if (list3[n] > num6)
				{
					list.Add(new TravelSimulator.Event
					{
						code = TravelSimulator.EventCode.Traveler,
						val = n,
						time = 0
					});
					num = n;
					break;
				}
				num6 -= list3[n];
			}
		}
		for (int num7 = 1; num7 < travel.route.Count; num7++)
		{
			TravelSimulator.Event @event = new TravelSimulator.Event();
			int num8 = travel.edgeCost[num7 - 1];
			int num9 = 0;
			List<TravelSimulator.Event> list7 = new List<TravelSimulator.Event>();
			list7 = this.restCheck(num2, num8);
			if (list7.Count != 0)
			{
				int num10 = 0;
				int num11;
				for (num11 = 0; num11 < list7.Count; num11++)
				{
					num10 = list7[num11].time;
					if (list7[num11].code == TravelSimulator.EventCode.Rest)
					{
						num9 += num10;
					}
					num4 -= num10;
					if (num4 <= 0)
					{
						break;
					}
				}
				if (num4 <= 0)
				{
					list7.RemoveRange(num11, list7.Count - num11);
					TravelSimulator.Event event2 = new TravelSimulator.Event();
					@event.code = TravelSimulator.EventCode.TimeUp;
					@event.val = travel.route[travel.route.Count - 1];
					@event.time = num4 + num10;
					list7.Add(@event);
					list.AddRange(list7);
					Debug.Log("[TravelSimulator] 旅の途中で力尽きました（休憩中）");
					break;
				}
				list.AddRange(list7);
				num8 -= num9;
				num2 = 100;
			}
			if (num4 - num8 < 0)
			{
				list.Add(new TravelSimulator.Event
				{
					code = TravelSimulator.EventCode.TimeUp,
					val = travel.route[travel.route.Count - 1],
					time = num4
				});
				Debug.Log("[TravelSimulator] 旅の途中で力尽きました");
				break;
			}
			num4 -= num8;
			if (num7 != travel.route.Count - 1)
			{
				NodeEdgeDataFormat nodeEdgeDataFormat = SuperGameMaster.sDataBase.get_NodeEdgeDB_forId(travel.edgeRoute[num7 - 1]);
				if (num == -1 && nodeEdgeDataFormat.enc_per != 0)
				{
					int num12 = dictionary[nodeEdgeDataFormat.enc_name];
					int num13 = nodeEdgeDataFormat.enc_per;
					num13 += num5;
					if (UnityEngine.Random.Range(0, 100) < nodeEdgeDataFormat.enc_per)
					{
						@event = new TravelSimulator.Event();
						@event.code = TravelSimulator.EventCode.Traveler;
						@event.val = num12;
						@event.time = num8;
						num2 -= num8;
						list.Add(@event);
						num = num12;
						num8 = 0;
					}
				}
				if (0 < nodeEdgeDataFormat.nTagPer + nodeEdgeDataFormat.tTagPer + nodeEdgeDataFormat.uTagPer && 70 > UnityEngine.Random.Range(0, 100))
				{
					int num14 = UnityEngine.Random.Range(0, 100);
					if (nodeEdgeDataFormat.uTagPer > num14)
					{
						@event = new TravelSimulator.Event();
						@event.code = TravelSimulator.EventCode.Picture_Unique;
						@event.val = nodeEdgeDataFormat.id;
						@event.time = num8;
						num2 -= num8;
						list.Add(@event);
						num14 += 99999;
						num8 = 0;
					}
					num14 -= nodeEdgeDataFormat.uTagPer;
					int index2 = nodeEdgeDataFormat.wayType - WayType.Mountain;
					if (nodeEdgeDataFormat.wayType == WayType.NONE)
					{
						index2 = 3;
					}
					if (nodeEdgeDataFormat.tTagPer + list2[index2] > num14)
					{
						@event = new TravelSimulator.Event();
						@event.code = TravelSimulator.EventCode.Picture_Tools;
						@event.val = nodeEdgeDataFormat.id;
						@event.time = num8;
						num2 -= num8;
						list.Add(@event);
						num14 += 99999;
						num8 = 0;
					}
					num14 -= nodeEdgeDataFormat.tTagPer + list2[index2];
					if (nodeEdgeDataFormat.nTagPer > num14)
					{
						@event = new TravelSimulator.Event();
						@event.code = TravelSimulator.EventCode.Picture_Normal;
						@event.val = nodeEdgeDataFormat.id;
						@event.time = num8;
						num2 -= num8;
						list.Add(@event);
						num14 += 99999;
						num8 = 0;
					}
					num14 -= nodeEdgeDataFormat.nTagPer;
				}
			}
			@event = new TravelSimulator.Event();
			@event.code = TravelSimulator.EventCode.Arrival;
			if (num7 == travel.route.Count - 1)
			{
				@event.code = TravelSimulator.EventCode.GOAL;
			}
			@event.val = travel.route[num7];
			@event.time = num8;
			num2 -= num8;
			list.Add(@event);
		}
		return list;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00030514 File Offset: 0x0002E914
	public List<TravelSimulator.Event> restCheck(int refHP, int refcost)
	{
		List<TravelSimulator.Event> list = new List<TravelSimulator.Event>();
		for (int i = refHP - refcost; i < 0; i += 100)
		{
			TravelSimulator.Event @event = new TravelSimulator.Event();
			@event.code = TravelSimulator.EventCode.Rest;
			@event.val = 0;
			@event.time = 100;
			if (list.Count == 0)
			{
				@event.time = refHP;
			}
			list.Add(@event);
			list.Add(new TravelSimulator.Event
			{
				code = TravelSimulator.EventCode.RestComplete,
				val = 0,
				time = 180
			});
		}
		return list;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0003059C File Offset: 0x0002E99C
	public List<TravelSimulator.ItemEffect> getBagItemEffect(EffectType Type)
	{
		List<TravelSimulator.ItemEffect> list = new List<TravelSimulator.ItemEffect>();
		List<int> list2 = new List<int>(SuperGameMaster.GetBagList());
		foreach (int num in list2)
		{
			if (num != -1)
			{
				ItemDataFormat itemDataFormat = new ItemDataFormat();
				itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(num);
				for (int i = 0; i < itemDataFormat.effectType.Length; i++)
				{
					if (itemDataFormat.effectType[i] == Type)
					{
						list.Add(new TravelSimulator.ItemEffect
						{
							type = itemDataFormat.effectType[i],
							elm = itemDataFormat.effectElm[i],
							value = itemDataFormat.effectValue[i]
						});
					}
				}
			}
		}
		return new List<TravelSimulator.ItemEffect>(list);
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x00030698 File Offset: 0x0002EA98
	public List<bool> get_UndevelopedArea()
	{
		List<int> list = new List<int>();
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		for (int i = 0; i < 4; i++)
		{
			list.Add(0);
			list2.Add(0);
			list3.Add(0);
		}
		for (int j = 0; j < SuperGameMaster.sDataBase.count_NodePrefDB(); j++)
		{
			NodePrefDataFormat nodePrefDataFormat = SuperGameMaster.sDataBase.get_NodePrefDB(j);
			AreaType areaType = SuperGameMaster.sDataBase.get_NodeDB_AreaType(nodePrefDataFormat.id);
			if (!SuperGameMaster.FindCollection(nodePrefDataFormat.collectionId) && nodePrefDataFormat.collectionId != -1)
			{
				List<int> list4;
				int index;
				(list4 = list)[index = (int)areaType] = list4[index] + 1;
			}
			foreach (int specialtyId2 in nodePrefDataFormat.specialtyId)
			{
				if (!SuperGameMaster.FindSpecialty(specialtyId2))
				{
					List<int> list4;
					int index2;
					(list4 = list2)[index2 = (int)areaType] = list4[index2] + 1;
					break;
				}
			}
		}
		List<bool> list5 = new List<bool>();
		if (list.FindIndex((int num) => !num.Equals(0)) != -1)
		{
			for (int l = 0; l < 4; l++)
			{
				if (list[l] > 0)
				{
					list5.Add(true);
				}
				else
				{
					list5.Add(false);
				}
			}
		}
		else if (list2.FindIndex((int num) => !num.Equals(0)) != -1)
		{
			for (int m = 0; m < 4; m++)
			{
				if (list2[m] > 0)
				{
					list5.Add(true);
				}
				else
				{
					list5.Add(false);
				}
			}
		}
		else
		{
			for (int n = 0; n < 4; n++)
			{
				list5.Add(true);
			}
		}
		return list5;
	}

	// Token: 0x0400059D RID: 1437
	public bool DebugTest_Notification;

	// Token: 0x020000EC RID: 236
	public class DNode
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x000308DC File Offset: 0x0002ECDC
		public DNode(TravelSimulator.DNode original)
		{
			this.nodeId = original.nodeId;
			this.edges_to = new List<int>(original.edges_to);
			this.edges_cost = new List<int>(original.edges_cost);
			this.nodeType = original.nodeType;
			this.edges = new List<int>(original.edges);
			this.edges_type = new List<WayType>(original.edges_type);
			this.cost = original.cost;
			this.route = new List<int>(original.route);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00030968 File Offset: 0x0002ED68
		public DNode()
		{
		}

		// Token: 0x040005A3 RID: 1443
		public int nodeId;

		// Token: 0x040005A4 RID: 1444
		public List<int> edges_to;

		// Token: 0x040005A5 RID: 1445
		public List<int> edges_cost;

		// Token: 0x040005A6 RID: 1446
		public NodeType nodeType;

		// Token: 0x040005A7 RID: 1447
		public List<int> edges;

		// Token: 0x040005A8 RID: 1448
		public List<WayType> edges_type;

		// Token: 0x040005A9 RID: 1449
		public int cost;

		// Token: 0x040005AA RID: 1450
		public List<int> route;
	}

	// Token: 0x020000ED RID: 237
	public class Travel
	{
		// Token: 0x06000749 RID: 1865 RVA: 0x00030970 File Offset: 0x0002ED70
		public Travel(TravelSimulator.Travel original)
		{
			this.cost = original.cost;
			this.route = new List<int>(original.route);
			this.pathNode = new List<int>(original.pathNode);
			this.nodeType = new List<NodeType>(original.nodeType);
			this.edgeRoute = new List<int>(original.edgeRoute);
			this.edgeType = new List<WayType>(original.edgeType);
			this.edgeCost = new List<int>(original.edgeCost);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000309F5 File Offset: 0x0002EDF5
		public Travel()
		{
		}

		// Token: 0x040005AB RID: 1451
		public int cost;

		// Token: 0x040005AC RID: 1452
		public List<int> route;

		// Token: 0x040005AD RID: 1453
		public List<int> pathNode;

		// Token: 0x040005AE RID: 1454
		public List<NodeType> nodeType;

		// Token: 0x040005AF RID: 1455
		public List<int> edgeRoute;

		// Token: 0x040005B0 RID: 1456
		public List<WayType> edgeType;

		// Token: 0x040005B1 RID: 1457
		public List<int> edgeCost;
	}

	// Token: 0x020000EE RID: 238
	public class Event
	{
		// Token: 0x040005B2 RID: 1458
		public int time;

		// Token: 0x040005B3 RID: 1459
		public TravelSimulator.EventCode code;

		// Token: 0x040005B4 RID: 1460
		public int val;
	}

	// Token: 0x020000EF RID: 239
	public class ItemEffect
	{
		// Token: 0x040005B5 RID: 1461
		public EffectType type;

		// Token: 0x040005B6 RID: 1462
		public EffectElm elm;

		// Token: 0x040005B7 RID: 1463
		public int value;
	}

	// Token: 0x020000F0 RID: 240
	public enum TestMode
	{
		// Token: 0x040005B9 RID: 1465
		NONE = -1,
		// Token: 0x040005BA RID: 1466
		Manual,
		// Token: 0x040005BB RID: 1467
		SemiAuto,
		// Token: 0x040005BC RID: 1468
		Auto,
		// Token: 0x040005BD RID: 1469
		MODE_MAX
	}

	// Token: 0x020000F1 RID: 241
	public enum EventCode
	{
		// Token: 0x040005BF RID: 1471
		NONE = -1,
		// Token: 0x040005C0 RID: 1472
		START,
		// Token: 0x040005C1 RID: 1473
		GOAL,
		// Token: 0x040005C2 RID: 1474
		TimeUp,
		// Token: 0x040005C3 RID: 1475
		Arrival,
		// Token: 0x040005C4 RID: 1476
		Rest,
		// Token: 0x040005C5 RID: 1477
		RestComplete,
		// Token: 0x040005C6 RID: 1478
		Camp,
		// Token: 0x040005C7 RID: 1479
		Shortcut,
		// Token: 0x040005C8 RID: 1480
		Encount,
		// Token: 0x040005C9 RID: 1481
		Picture_Normal,
		// Token: 0x040005CA RID: 1482
		Picture_Tools,
		// Token: 0x040005CB RID: 1483
		Picture_Unique,
		// Token: 0x040005CC RID: 1484
		Traveler,
		// Token: 0x040005CD RID: 1485
		Event_MAX
	}
}
