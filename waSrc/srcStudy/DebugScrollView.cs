using System;
using System.Collections.Generic;
using Flag;
using TimerEvent;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000070 RID: 112
public class DebugScrollView : MonoBehaviour
{
	// Token: 0x060003CC RID: 972 RVA: 0x00016E20 File Offset: 0x00015220
	public void OpenView()
	{
		base.gameObject.SetActive(true);
		base.transform.parent.GetComponentInParent<UIMaster>().freezeObject(true);
		base.transform.parent.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.GetEventList();
	}

	// Token: 0x060003CD RID: 973 RVA: 0x00016E84 File Offset: 0x00015284
	public void CloseView()
	{
		base.gameObject.SetActive(false);
		base.transform.parent.GetComponentInParent<UIMaster>().freezeObject(false);
		base.transform.parent.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00016EE4 File Offset: 0x000152E4
	public void GetEventList()
	{
		List<EventTimerFormat> list = new List<EventTimerFormat>();
		string text = "\n\n\n";
		text += "【旅行変数】#################### \n";
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"  home = ",
			SuperGameMaster.saveData.home,
			" / drift = ",
			SuperGameMaster.saveData.drift,
			"\n"
		});
		if (!SuperGameMaster.saveData.standby)
		{
			text += "<color=#999999>";
		}
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			" standbyWait [ ",
			SuperGameMaster.saveData.standby,
			" ] = ",
			SuperGameMaster.saveData.standbyWait / 3600,
			"h ",
			SuperGameMaster.saveData.standbyWait % 3600 / 60,
			"m ",
			SuperGameMaster.saveData.standbyWait % 60,
			"s（",
			SuperGameMaster.saveData.standbyWait,
			"）\n"
		});
		if (!SuperGameMaster.saveData.standby)
		{
			text += "</color>";
		}
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"  restTime = ",
			SuperGameMaster.saveData.restTime / 3600,
			"h ",
			SuperGameMaster.saveData.restTime % 3600 / 60,
			"m ",
			SuperGameMaster.saveData.restTime % 60,
			"s（",
			SuperGameMaster.saveData.restTime,
			"）\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"  lastTravelTime = ",
			SuperGameMaster.saveData.lastTravelTime / 3600,
			"h ",
			SuperGameMaster.saveData.lastTravelTime % 3600 / 60,
			"m ",
			SuperGameMaster.saveData.lastTravelTime % 60,
			"s（",
			SuperGameMaster.saveData.lastTravelTime,
			"）\n"
		});
		list = SuperGameMaster.evtMgr.get_TimerList();
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n【予約イベント】[",
			list.Count,
			"] (evtList_timer) =========== \n"
		});
		foreach (EventTimerFormat eventTimerFormat in list)
		{
			switch (eventTimerFormat.evtType)
			{
			case TimerEvent.Type.GoTravel:
				text += "<color=#99ff99>";
				break;
			case TimerEvent.Type.BackHome:
				text += "<color=#99ff99>";
				break;
			case TimerEvent.Type.Picture:
				text += "<color=#ccffcc>";
				break;
			case TimerEvent.Type.Drift:
				text += "<color=#ff9999>";
				break;
			case TimerEvent.Type.Return:
				text += "<color=#ff9999>";
				break;
			case TimerEvent.Type.Friend:
				text += "<color=#ccccff>";
				break;
			case TimerEvent.Type.Gift:
				text += "<color=#9999ff>";
				break;
			default:
				text += "<color=white>";
				break;
			}
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"<b>\u3000[ID = ",
				eventTimerFormat.id,
				"] 発生まで > ",
				eventTimerFormat.timeSpanSec / 3600,
				"h ",
				eventTimerFormat.timeSpanSec % 3600 / 60,
				"m ",
				eventTimerFormat.timeSpanSec % 60,
				"s（",
				eventTimerFormat.timeSpanSec,
				"）\n</b>"
			});
			text2 = text;
			object[] array = new object[6];
			array[0] = text2;
			array[1] = "  \u3000        evtType = ";
			int num = 2;
			TimerEvent.Type evtType = eventTimerFormat.evtType;
			array[num] = evtType.ToString();
			array[3] = " / evtId = ";
			array[4] = eventTimerFormat.evtId;
			array[5] = "\n";
			text = string.Concat(array);
			text += "  \u3000        value(";
			foreach (int num2 in eventTimerFormat.evtValue)
			{
				text = text + num2 + ",";
			}
			text += ")\n";
			if (eventTimerFormat.trigger)
			{
				text += "    \u3000      Active時間：（なし)\n";
			}
			else
			{
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					" \u3000         Active時間：",
					eventTimerFormat.activeTime / 3600,
					"h ",
					eventTimerFormat.activeTime % 3600 / 60,
					"m ",
					eventTimerFormat.activeTime % 60,
					"s（",
					eventTimerFormat.activeTime,
					"）\n"
				});
			}
			text = text + " \u3000         追加時刻：" + eventTimerFormat.addTime.ToString() + "\n";
			text += "</color>";
		}
		list = SuperGameMaster.evtMgr.get_ActiveList();
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n【実行中イベント】[",
			list.Count,
			"] (evtList_active) ========== \n"
		});
		foreach (EventTimerFormat eventTimerFormat2 in SuperGameMaster.saveData.evtList_active)
		{
			switch (eventTimerFormat2.evtType)
			{
			case TimerEvent.Type.GoTravel:
				text += "<color=#99ff99>";
				break;
			case TimerEvent.Type.BackHome:
				text += "<color=#99ff99>";
				break;
			case TimerEvent.Type.Picture:
				text += "<color=#ccffcc>";
				break;
			case TimerEvent.Type.Drift:
				text += "<color=#ff9999>";
				break;
			case TimerEvent.Type.Return:
				text += "<color=#ff9999>";
				break;
			case TimerEvent.Type.Friend:
				text += "<color=#ccccff>";
				break;
			case TimerEvent.Type.Gift:
				text += "<color=#9999ff>";
				break;
			default:
				text += "<color=white>";
				break;
			}
			if (!eventTimerFormat2.trigger)
			{
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"<b>\u3000[ID = ",
					eventTimerFormat2.id,
					"] 終了まで > ",
					eventTimerFormat2.timeSpanSec / 3600,
					"h ",
					eventTimerFormat2.timeSpanSec % 3600 / 60,
					"m ",
					eventTimerFormat2.timeSpanSec % 60,
					"s（",
					eventTimerFormat2.timeSpanSec,
					"）\n</b>"
				});
			}
			else
			{
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"\u3000[ID = ",
					eventTimerFormat2.id,
					"] （Trigger）\n"
				});
			}
			text2 = text;
			object[] array2 = new object[6];
			array2[0] = text2;
			array2[1] = "     \u3000     evtType = ";
			int num3 = 2;
			TimerEvent.Type evtType2 = eventTimerFormat2.evtType;
			array2[num3] = evtType2.ToString();
			array2[3] = " / evtId = ";
			array2[4] = eventTimerFormat2.evtId;
			array2[5] = "\n";
			text = string.Concat(array2);
			text += "  \u3000        value(";
			foreach (int num4 in eventTimerFormat2.evtValue)
			{
				text = text + num4 + ",";
			}
			text += ")\n";
			if (eventTimerFormat2.trigger)
			{
				text += "    \u3000      Active指定時間：（なし)\n";
			}
			else
			{
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					" \u3000         Active指定時間：",
					eventTimerFormat2.activeTime / 3600,
					"h ",
					eventTimerFormat2.activeTime % 3600 / 60,
					"m ",
					eventTimerFormat2.activeTime % 60,
					"s（",
					eventTimerFormat2.activeTime,
					"）\n"
				});
			}
			text = text + " \u3000         追加時刻：" + eventTimerFormat2.addTime.ToString() + "\n";
			text += "</color>";
		}
		this.Result_text.GetComponent<Text>().text = text;
	}

	// Token: 0x060003CF RID: 975 RVA: 0x000178D0 File Offset: 0x00015CD0
	public void GetFlagList()
	{
		string text = "\n\n\n";
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"version = ",
			SuperGameMaster.saveData.version,
			" ( ",
			SuperGameMaster.saveData.version_start,
			" ) \n\n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"【補填フラグ】\u3000[",
			SuperGameMaster.saveData.hoten.Count,
			"] xxxxxxxxxxxxxxxx \n"
		});
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			" CallBackFlag ：",
			SuperGameMaster.saveData.iapCallBackCnt,
			" \n"
		});
		for (int i = 0; i < SuperGameMaster.saveData.hoten.Count; i++)
		{
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				" [",
				i,
				"] : ",
				SuperGameMaster.saveData.hoten[i],
				"\n"
			});
		}
		List<int> flagList = SuperGameMaster.get_FlagList();
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n\n【フラグリスト】\u3000[",
			flagList.Count,
			"] ............. \n"
		});
		for (int j = 0; j < flagList.Count; j++)
		{
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				" [",
				j,
				"] : <b>",
				(Flag.Type)j,
				"</b> = ",
				flagList[j],
				"\n"
			});
		}
		Step tutorialStep = SuperGameMaster.GetTutorialStep();
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n\n【チュートリアル進捗】[save / ",
			tutorialStep,
			"(",
			(int)tutorialStep,
			")] ............. \n"
		});
		for (int k = 0; k < 7; k++)
		{
			bool firstFlag = SuperGameMaster.GetFirstFlag((Flag)k);
			if (firstFlag)
			{
				text += "<color=#999999>";
			}
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				" [",
				k,
				"] : <b>",
				(Flag)k,
				"</b> = ",
				firstFlag,
				"\n"
			});
			if (firstFlag)
			{
				text += "</color>";
			}
		}
		if (!SuperGameMaster.tutorial.TutorialComplete())
		{
			tutorialStep = SuperGameMaster.tutorial.tutorialStep;
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"\n\n＜チュートリアル中＞ [",
				tutorialStep,
				"(",
				(int)tutorialStep,
				")] ======== \n"
			});
			for (int l = 0; l < 13; l++)
			{
				if (tutorialStep > (Step)l)
				{
					text += "<color=#999999>";
				}
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"  (",
					l,
					") ",
					(Step)l,
					"\n"
				});
				if (tutorialStep > (Step)l)
				{
					text += "</color>";
				}
			}
			text += "================================= \n\n\n";
		}
		this.Result_text.GetComponent<Text>().text = text;
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00017C80 File Offset: 0x00016080
	public void GetCloverList()
	{
		string text = "\n\n\n";
		List<CloverDataFormat> cloverList = SuperGameMaster.GetCloverList();
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"【クローバー生成リスト】\u3000[",
			cloverList.Count,
			"] ~~~~~~~~~~ \n"
		});
		for (int i = 0; i < cloverList.Count; i++)
		{
			if (cloverList[i].timeSpanSec < 0)
			{
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"<color=#999999>\u3000[",
					i,
					"]：（生成済み）\n</color>"
				});
			}
			else
			{
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"<color=white><b>\u3000[",
					i,
					"] > ",
					cloverList[i].timeSpanSec / 3600,
					"h ",
					cloverList[i].timeSpanSec % 3600 / 60,
					"m ",
					cloverList[i].timeSpanSec % 60,
					"s（",
					cloverList[i].timeSpanSec,
					")\n</b></color>"
				});
			}
		}
		this.Result_text.GetComponent<Text>().text = text;
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00017DE4 File Offset: 0x000161E4
	public void GetBagList()
	{
		string text = "\n";
		text += "\n\n【支度リスト】 (bagList/DeskList) ~~~~~~~~~~~ \n";
		text += "\u3000かばん：\n";
		foreach (int num in SuperGameMaster.GetBagList())
		{
			if (num == -1)
			{
				text += "\u3000\u3000\u3000\u3000\u3000- ,\n";
			}
			else
			{
				ItemDataFormat itemDataFormat = new ItemDataFormat();
				itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(num);
				text = text + "\u3000\u3000\u3000\u3000\u3000" + itemDataFormat.name + ",\n";
			}
		}
		text += "\u3000つくえ：\n";
		foreach (int num2 in SuperGameMaster.GetDeskList())
		{
			if (num2 == -1)
			{
				text += "\u3000\u3000\u3000\u3000\u3000\u3000- ,\n";
			}
			else
			{
				ItemDataFormat itemDataFormat2 = new ItemDataFormat();
				itemDataFormat2 = SuperGameMaster.sDataBase.get_ItemDB_forId(num2);
				text = text + "\u3000\u3000\u3000\u3000\u3000" + itemDataFormat2.name + ",\n";
			}
		}
		text += "\n~~~~~~~~~（仮想リスト）~~~~~~~~~\n";
		text += "\u3000かばん：\n";
		foreach (int num3 in SuperGameMaster.GetBagList_virtual())
		{
			if (num3 == -1)
			{
				text += "\u3000\u3000\u3000\u3000\u3000\u3000- ,\n";
			}
			else
			{
				ItemDataFormat itemDataFormat3 = new ItemDataFormat();
				itemDataFormat3 = SuperGameMaster.sDataBase.get_ItemDB_forId(num3);
				text = text + "\u3000\u3000\u3000\u3000\u3000" + itemDataFormat3.name + ",\n";
			}
		}
		text += "\u3000つくえ：\n";
		foreach (int num4 in SuperGameMaster.GetDeskList_virtual())
		{
			if (num4 == -1)
			{
				text += "\u3000\u3000\u3000\u3000\u3000\u3000- ,\n";
			}
			else
			{
				ItemDataFormat itemDataFormat4 = new ItemDataFormat();
				itemDataFormat4 = SuperGameMaster.sDataBase.get_ItemDB_forId(num4);
				text = text + "\u3000\u3000\u3000\u3000\u3000" + itemDataFormat4.name + ",\n";
			}
		}
		this.Result_text.GetComponent<Text>().text = text;
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0001807C File Offset: 0x0001647C
	public void Push_Refresh()
	{
		SuperGameMaster.evtMgr.ResetEvent();
		SuperGameMaster.travel.SetReturnState();
		SuperGameMaster.saveData.lastTravelTime = 0;
		SuperGameMaster.saveData.standby = false;
		this.GetEventList();
	}

	// Token: 0x0400026C RID: 620
	public GameObject Result_text;
}
