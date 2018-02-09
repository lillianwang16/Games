using System;
using System.Collections.Generic;
using Flag;
using Mail;
using TimerEvent;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class EventTimerManager : MonoBehaviour
{
	// Token: 0x06000472 RID: 1138 RVA: 0x0001FA04 File Offset: 0x0001DE04
	public void TimerAdd(EventTimerFormat addEvt)
	{
		EventTimerFormat eventTimerFormat = new EventTimerFormat(addEvt);
		if (eventTimerFormat.timeSpanSec < 0)
		{
			this.ActiveAdd(eventTimerFormat);
		}
		else
		{
			int num = 0;
			while (this.search_TimerEvtIndex_forId(num) != -1)
			{
				num++;
			}
			eventTimerFormat.id = num;
			eventTimerFormat.addTime = SuperGameMaster.GetLastDateTime().AddSeconds((double)eventTimerFormat.timeSpanSec);
			SuperGameMaster.saveData.evtList_timer.Add(new EventTimerFormat(eventTimerFormat));
			SuperGameMaster.saveData.evtList_timer.Sort((EventTimerFormat x, EventTimerFormat y) => x.timeSpanSec - y.timeSpanSec);
			object[] array = new object[8];
			array[0] = "[EventTimerManager] Timer イベントを追加： id = ";
			array[1] = eventTimerFormat.id;
			array[2] = " / timeSpan = ";
			array[3] = eventTimerFormat.timeSpanSec;
			array[4] = " || evtType = ";
			int num2 = 5;
			TimerEvent.Type evtType = eventTimerFormat.evtType;
			array[num2] = evtType.ToString();
			array[6] = " / evtId = ";
			array[7] = eventTimerFormat.evtId;
			Debug.Log(string.Concat(array));
		}
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001FB20 File Offset: 0x0001DF20
	public void ActiveAdd(EventTimerFormat addEvt)
	{
		EventTimerFormat eventTimerFormat = new EventTimerFormat(addEvt);
		eventTimerFormat.addTime = SuperGameMaster.GetLastDateTime().AddSeconds((double)eventTimerFormat.timeSpanSec);
		if (eventTimerFormat.trigger)
		{
			eventTimerFormat.timeSpanSec = int.MaxValue;
		}
		else if (eventTimerFormat.timeSpanSec <= 0)
		{
			eventTimerFormat.timeSpanSec += eventTimerFormat.activeTime;
		}
		int num = 0;
		while (this.search_ActEvtIndex_forId(num) != -1)
		{
			num++;
		}
		eventTimerFormat.id = num;
		SuperGameMaster.saveData.evtList_active.Add(new EventTimerFormat(eventTimerFormat));
		SuperGameMaster.saveData.evtList_active.Sort((EventTimerFormat x, EventTimerFormat y) => x.timeSpanSec - y.timeSpanSec);
		object[] array = new object[8];
		array[0] = "[EventTimerManager] Active イベントを追加： id = ";
		array[1] = eventTimerFormat.id;
		array[2] = " / timeSpan = ";
		array[3] = eventTimerFormat.timeSpanSec;
		array[4] = " || evtType = ";
		int num2 = 5;
		TimerEvent.Type evtType = eventTimerFormat.evtType;
		array[num2] = evtType.ToString();
		array[6] = " / evtId = ";
		array[7] = eventTimerFormat.evtId;
		Debug.Log(string.Concat(array));
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001FC5E File Offset: 0x0001E05E
	public void Proc(int addTimer)
	{
		this.Proc(addTimer, TimerEvent.Type.NONE);
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0001FC68 File Offset: 0x0001E068
	public void Proc(int addTimer, TimerEvent.Type proc_evtType)
	{
		Debug.Log(string.Concat(new object[]
		{
			"[EventTimerManager] イベントProc（",
			proc_evtType.ToString(),
			" / add = ",
			addTimer,
			"）： Timer = ",
			SuperGameMaster.saveData.evtList_timer.Count,
			" / actEvtCnt = ",
			SuperGameMaster.saveData.evtList_active.Count
		}));
		foreach (EventTimerFormat eventTimerFormat in SuperGameMaster.saveData.evtList_active)
		{
			if (!eventTimerFormat.trigger)
			{
				if (proc_evtType == TimerEvent.Type.NONE || proc_evtType == eventTimerFormat.evtType)
				{
					eventTimerFormat.timeSpanSec -= addTimer;
				}
			}
		}
		foreach (EventTimerFormat eventTimerFormat2 in SuperGameMaster.saveData.evtList_timer)
		{
			if (proc_evtType == TimerEvent.Type.NONE || proc_evtType == eventTimerFormat2.evtType)
			{
				eventTimerFormat2.timeSpanSec -= addTimer;
				if (eventTimerFormat2.timeSpanSec <= 0)
				{
					this.ActiveAdd(eventTimerFormat2);
				}
			}
		}
		this.ActiveMath(proc_evtType);
		int count = SuperGameMaster.saveData.evtList_timer.Count;
		int count2 = SuperGameMaster.saveData.evtList_active.Count;
		if (proc_evtType == TimerEvent.Type.NONE)
		{
			SuperGameMaster.saveData.evtList_timer.RemoveAll((EventTimerFormat evt) => evt.timeSpanSec < 0);
			SuperGameMaster.saveData.evtList_active.RemoveAll((EventTimerFormat evt) => evt.timeSpanSec < 0);
		}
		else
		{
			SuperGameMaster.saveData.evtList_timer.RemoveAll((EventTimerFormat evt) => evt.timeSpanSec < 0 && evt.evtType == proc_evtType);
			SuperGameMaster.saveData.evtList_active.RemoveAll((EventTimerFormat evt) => evt.timeSpanSec < 0 && evt.evtType == proc_evtType);
		}
		if (count != SuperGameMaster.saveData.evtList_timer.Count)
		{
			Debug.Log(string.Concat(new object[]
			{
				"[EventTimerManager] Timer イベントが削除されました： ",
				count,
				" > ",
				SuperGameMaster.saveData.evtList_timer.Count
			}));
		}
		if (count2 != SuperGameMaster.saveData.evtList_active.Count)
		{
			Debug.Log(string.Concat(new object[]
			{
				"[EventTimerManager] Active イベントが削除されました：",
				count2,
				" > ",
				SuperGameMaster.saveData.evtList_active.Count
			}));
		}
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0001FF8C File Offset: 0x0001E38C
	public void ActiveMath(TimerEvent.Type proc_evtType)
	{
		List<EventTimerFormat> list = new List<EventTimerFormat>();
		foreach (EventTimerFormat eventTimerFormat in SuperGameMaster.saveData.evtList_active)
		{
			if (proc_evtType == TimerEvent.Type.NONE || proc_evtType == eventTimerFormat.evtType)
			{
				switch (eventTimerFormat.evtType)
				{
				case TimerEvent.Type.GoTravel:
					if (eventTimerFormat.timeSpanSec < 0)
					{
						SuperGameMaster.set_FlagAdd(Flag.Type.GOTRAVEL, 1);
					}
					SuperGameMaster.ResetSave_BagDeskList_virtual();
					break;
				case TimerEvent.Type.BackHome:
					SuperGameMaster.ResetSave_BagDeskList_virtual();
					break;
				case TimerEvent.Type.Picture:
					if (eventTimerFormat.timeSpanSec < 0)
					{
						SuperGameMaster.set_FlagAdd(Flag.Type.GOTRAVEL, 1);
					}
					SuperGameMaster.ResetSave_BagDeskList_virtual();
					break;
				case TimerEvent.Type.Return:
					SuperGameMaster.ResetSave_BagDeskList_virtual();
					break;
				case TimerEvent.Type.Friend:
					if (eventTimerFormat.timeSpanSec <= 0)
					{
						int num = eventTimerFormat.timeSpanSec;
						int num2 = 0;
						CharacterDataFormat characterDataFormat = SuperGameMaster.sDataBase.get_CharaDB_forId(eventTimerFormat.evtId);
						IL_2E9:
						while (num + num2 <= 0)
						{
							num += 21600;
							while (UnityEngine.Random.Range(0, 100) >= 10)
							{
								num += 1800;
								if (num > 1296000)
								{
									IL_2C1:
									num2 = UnityEngine.Random.Range(6, 9);
									num2 *= 1800;
									if (characterDataFormat.flagValue > SuperGameMaster.Count_CollectionFlag())
									{
										num2 = 0;
										goto IL_2E9;
									}
									goto IL_2E9;
								}
							}
							goto IL_2C1;
						}
						list.Add(new EventTimerFormat
						{
							id = -1,
							timeSpanSec = num,
							activeTime = num2,
							addTime = new DateTime(1970, 1, 1),
							evtType = TimerEvent.Type.Friend,
							evtId = characterDataFormat.id,
							evtValue = new List<int>(),
							evtValue = 
							{
								UnityEngine.Random.Range(0, characterDataFormat.rndPos.Length),
								0,
								eventTimerFormat.evtValue[2],
								eventTimerFormat.evtValue[3],
								eventTimerFormat.evtValue[4],
								0
							},
							trigger = false
						});
					}
					break;
				case TimerEvent.Type.Gift:
				{
					int count = SuperGameMaster.saveData.MailList.Count;
					if (count >= 100)
					{
						SuperGameMaster.saveData.MailList.RemoveAt(0);
						Debug.Log("[EventTimerManager] 昔のメールを削除しました");
					}
					MailEventFormat mailEventFormat = new MailEventFormat();
					mailEventFormat.NewMail();
					int evtId = eventTimerFormat.evtId;
					CharacterDataFormat characterDataFormat2 = SuperGameMaster.sDataBase.get_CharaDB_forId(evtId);
					Define.Gift gift = (Define.Gift)eventTimerFormat.evtValue[0];
					if (gift != Define.Gift.Clover)
					{
						if (gift != Define.Gift.FourClover)
						{
							if (gift == Define.Gift.Ticket)
							{
								mailEventFormat.title = characterDataFormat2.name + "のおかえし";
								mailEventFormat.ticket = eventTimerFormat.evtValue[1];
							}
						}
						else
						{
							mailEventFormat.title = characterDataFormat2.name + "のおかえし";
							mailEventFormat.itemId = 1000;
							mailEventFormat.itemStock = eventTimerFormat.evtValue[1];
						}
					}
					else
					{
						mailEventFormat.title = characterDataFormat2.name + "のおかえし";
						mailEventFormat.CloverPoint = eventTimerFormat.evtValue[1];
					}
					mailEventFormat.senderCharaId = characterDataFormat2.id;
					mailEventFormat.mailEvt = EvtId.Gift;
					mailEventFormat.mailId = SuperGameMaster.saveData.MailList_nextId;
					mailEventFormat.date = SuperGameMaster.saveData.lastDateTime;
					SuperGameMaster.saveData.MailList.Add(mailEventFormat);
					SuperGameMaster.saveData.MailList_nextId++;
					Debug.Log(string.Concat(new object[]
					{
						"[EventTimerManager] メール追加（",
						SuperGameMaster.saveData.MailList.Count,
						"） ID:",
						mailEventFormat.mailId,
						" next:",
						SuperGameMaster.saveData.MailList_nextId
					}));
					eventTimerFormat.trigger = false;
					eventTimerFormat.timeSpanSec = -1;
					break;
				}
				}
			}
		}
		foreach (EventTimerFormat addEvt in list)
		{
			SuperGameMaster.evtMgr.TimerAdd(addEvt);
		}
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00020458 File Offset: 0x0001E858
	public List<EventTimerFormat> get_TimerList()
	{
		List<EventTimerFormat> list = new List<EventTimerFormat>();
		foreach (EventTimerFormat original in SuperGameMaster.saveData.evtList_timer)
		{
			list.Add(new EventTimerFormat(original));
		}
		return new List<EventTimerFormat>(list);
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x000204CC File Offset: 0x0001E8CC
	public List<EventTimerFormat> get_ActiveList()
	{
		List<EventTimerFormat> list = new List<EventTimerFormat>();
		foreach (EventTimerFormat original in SuperGameMaster.saveData.evtList_active)
		{
			list.Add(new EventTimerFormat(original));
		}
		return new List<EventTimerFormat>(list);
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00020540 File Offset: 0x0001E940
	public List<EventTimerFormat> get_ActEvtList_forType(TimerEvent.Type evtType)
	{
		List<EventTimerFormat> list = new List<EventTimerFormat>();
		foreach (EventTimerFormat eventTimerFormat in SuperGameMaster.saveData.evtList_active)
		{
			if (eventTimerFormat.evtType == evtType)
			{
				list.Add(new EventTimerFormat(eventTimerFormat));
			}
		}
		return new List<EventTimerFormat>(list);
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x000205C0 File Offset: 0x0001E9C0
	public List<EventTimerFormat> get_TimerEvtList_forType(TimerEvent.Type evtType)
	{
		List<EventTimerFormat> list = new List<EventTimerFormat>();
		foreach (EventTimerFormat eventTimerFormat in SuperGameMaster.saveData.evtList_timer)
		{
			if (eventTimerFormat.evtType == evtType)
			{
				list.Add(new EventTimerFormat(eventTimerFormat));
			}
		}
		return new List<EventTimerFormat>(list);
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00020640 File Offset: 0x0001EA40
	public bool delete_ActEvt_forId(int delId)
	{
		int num = this.search_ActEvtIndex_forId(delId);
		if (num == -1)
		{
			Debug.Log("[EventTimerManager] イベントの削除に失敗しました！：ID = " + delId);
			return false;
		}
		SuperGameMaster.saveData.evtList_active.RemoveAt(num);
		Debug.Log(string.Concat(new object[]
		{
			"[EventTimerManager] イベントを削除しました：ID = ",
			delId,
			" / index = ",
			num
		}));
		return true;
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x000206B8 File Offset: 0x0001EAB8
	public bool set_ActEvt_forId(int cngId, List<int> cng_evtValue)
	{
		int num = this.search_ActEvtIndex_forId(cngId);
		if (num == -1)
		{
			Debug.Log("[EventTimerManager] イベント値の変更に失敗しました！：ID = " + cngId);
			return false;
		}
		List<int> list = new List<int>();
		foreach (int item in cng_evtValue)
		{
			list.Add(item);
		}
		SuperGameMaster.saveData.evtList_active[num].evtValue = list;
		Debug.Log(string.Concat(new object[]
		{
			"[EventTimerManager] イベント値を変更しました：ID = ",
			cngId,
			" / index = ",
			num
		}));
		return true;
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x00020784 File Offset: 0x0001EB84
	public bool set_TimerEvt_forId(int cngId, List<int> cng_evtValue)
	{
		int num = this.search_TimerEvtIndex_forId(cngId);
		if (num == -1)
		{
			Debug.Log("[EventTimerManager] イベント値の変更に失敗しました！：ID = " + cngId);
			return false;
		}
		List<int> list = new List<int>();
		foreach (int item in cng_evtValue)
		{
			list.Add(item);
		}
		SuperGameMaster.saveData.evtList_timer[num].evtValue = list;
		Debug.Log(string.Concat(new object[]
		{
			"[EventTimerManager] イベント値を変更しました：ID = ",
			cngId,
			" / index = ",
			num
		}));
		return true;
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00020850 File Offset: 0x0001EC50
	public void delete_Act_Timer_EvtList_forType(TimerEvent.Type evtType)
	{
		int count = SuperGameMaster.saveData.evtList_timer.Count;
		int count2 = SuperGameMaster.saveData.evtList_active.Count;
		SuperGameMaster.saveData.evtList_timer.RemoveAll((EventTimerFormat evt) => evt.evtType.Equals(evtType));
		SuperGameMaster.saveData.evtList_active.RemoveAll((EventTimerFormat evt) => evt.evtType.Equals(evtType));
		if (count != SuperGameMaster.saveData.evtList_timer.Count)
		{
			Debug.Log(string.Concat(new object[]
			{
				"[EventTimerManager] Timer イベントを削除しました： ",
				evtType.ToString(),
				" / ",
				count,
				" > ",
				SuperGameMaster.saveData.evtList_timer.Count
			}));
		}
		if (count2 != SuperGameMaster.saveData.evtList_active.Count)
		{
			Debug.Log(string.Concat(new object[]
			{
				"[EventTimerManager] Active イベントを削除しました： ",
				evtType.ToString(),
				" / ",
				count2,
				" > ",
				SuperGameMaster.saveData.evtList_active.Count
			}));
		}
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x000209A4 File Offset: 0x0001EDA4
	public void SetTime_TimerEvt(TimerEvent.Type set_evtType, int timeSpanSec, int activeTime)
	{
		Debug.Log(string.Concat(new object[]
		{
			"[EventTimerManager] イベント発生時間を指定 [ activeTime = ",
			activeTime,
			" / timeSpanSec = ",
			timeSpanSec,
			" ]（",
			set_evtType.ToString(),
			" / Timer = ",
			SuperGameMaster.saveData.evtList_timer.Count,
			" / actEvtCnt = ",
			SuperGameMaster.saveData.evtList_active.Count
		}));
		foreach (EventTimerFormat eventTimerFormat in SuperGameMaster.saveData.evtList_timer)
		{
			if (set_evtType == eventTimerFormat.evtType)
			{
				if (timeSpanSec != -1)
				{
					eventTimerFormat.timeSpanSec = timeSpanSec;
				}
				if (activeTime != -1)
				{
					eventTimerFormat.activeTime = activeTime;
				}
			}
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00020AB0 File Offset: 0x0001EEB0
	public EventTimerFormat get_ActEvt(int index)
	{
		return new EventTimerFormat(SuperGameMaster.saveData.evtList_active[index]);
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00020AC7 File Offset: 0x0001EEC7
	public EventTimerFormat get_TimerEvt(int index)
	{
		return new EventTimerFormat(SuperGameMaster.saveData.evtList_timer[index]);
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00020ADE File Offset: 0x0001EEDE
	public int count_ActEvt()
	{
		return SuperGameMaster.saveData.evtList_active.Count;
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00020AEF File Offset: 0x0001EEEF
	public int count_TimerEvt()
	{
		return SuperGameMaster.saveData.evtList_timer.Count;
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x00020B00 File Offset: 0x0001EF00
	public EventTimerFormat get_ActEvt_forId(int id)
	{
		int num = this.search_ActEvtIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_ActEvt(num);
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x00020B28 File Offset: 0x0001EF28
	public int search_ActEvtIndex_forId(int id)
	{
		return SuperGameMaster.saveData.evtList_active.FindIndex((EventTimerFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x06000486 RID: 1158 RVA: 0x00020B60 File Offset: 0x0001EF60
	public int search_TimerEvtIndex_forId(int id)
	{
		return SuperGameMaster.saveData.evtList_timer.FindIndex((EventTimerFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x06000487 RID: 1159 RVA: 0x00020B98 File Offset: 0x0001EF98
	public int search_ActEvtIndex_forType(TimerEvent.Type type)
	{
		return SuperGameMaster.saveData.evtList_active.FindIndex((EventTimerFormat rec) => rec.evtType.Equals(type));
	}

	// Token: 0x06000488 RID: 1160 RVA: 0x00020BD0 File Offset: 0x0001EFD0
	public int search_TimerEvtIndex_forType(TimerEvent.Type type)
	{
		return SuperGameMaster.saveData.evtList_timer.FindIndex((EventTimerFormat rec) => rec.evtType.Equals(type));
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00020C08 File Offset: 0x0001F008
	public int search_TimerEvtIndex_forType_andId(TimerEvent.Type type, int type_evtId)
	{
		return SuperGameMaster.saveData.evtList_timer.FindIndex((EventTimerFormat rec) => rec.evtType.Equals(type) && rec.evtId.Equals(type_evtId));
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00020C44 File Offset: 0x0001F044
	public void ResetEvent()
	{
		SuperGameMaster.saveData.evtList_timer = new List<EventTimerFormat>();
		SuperGameMaster.saveData.evtList_active = new List<EventTimerFormat>();
	}
}
