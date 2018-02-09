using System;
using System.Collections.Generic;
using Flag;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000053 RID: 83
public class FrogStatePanel : MonoBehaviour
{
	// Token: 0x060002ED RID: 749 RVA: 0x0000DA4C File Offset: 0x0000BE4C
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.ClosePanel(false);
		});
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0000DA78 File Offset: 0x0000BE78
	public void OpenPanel()
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.GetData();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Frog"]);
		this.BackFunc();
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0000DAE8 File Offset: 0x0000BEE8
	public void ClosePanel(bool save)
	{
		string text = this.InputUI.GetComponent<InputField>().text;
		if (text == string.Empty)
		{
			this.InputUI.GetComponent<InputField>().text = this.beforeName;
		}
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		base.gameObject.SetActive(false);
		if (save)
		{
			this.SaveData();
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		base.GetComponentInParent<UIMaster_MainIn>().BackFunc();
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000DB9C File Offset: 0x0000BF9C
	public void CheckGetAchive()
	{
		this.getAchiIdList = new List<int>();
		int num = SuperGameMaster.sDataBase.count_AchieveDB();
		for (int i = 0; i < num; i++)
		{
			AchieveDataFormat achieveDataFormat = SuperGameMaster.sDataBase.get_AchieveDB(i);
			int id = achieveDataFormat.id;
			if (!SuperGameMaster.CheckAchieveFlag(id))
			{
				if (achieveDataFormat != null)
				{
					if (achieveDataFormat.flagType[0] == Flag.Type.DEFAULT)
					{
						SuperGameMaster.Set_GetAchieve(id);
						this.getAchiIdList.Add(id);
						break;
					}
					int num2 = 0;
					for (int j = 0; j < achieveDataFormat.flagType.Length; j++)
					{
						Sign sign = achieveDataFormat.flagSign[j];
						if (sign != Sign.MORE)
						{
							if (sign != Sign.LESS)
							{
								if (sign == Sign.EQUAL)
								{
									if (SuperGameMaster.get_Flag(achieveDataFormat.flagType[j]) == achieveDataFormat.flagValue[j])
									{
										num2++;
									}
								}
							}
							else if (SuperGameMaster.get_Flag(achieveDataFormat.flagType[j]) <= achieveDataFormat.flagValue[j])
							{
								num2++;
							}
						}
						else if (SuperGameMaster.get_Flag(achieveDataFormat.flagType[j]) >= achieveDataFormat.flagValue[j])
						{
							num2++;
						}
					}
					if (num2 == achieveDataFormat.flagType.Length)
					{
						this.getAchiIdList.Add(id);
					}
				}
			}
		}
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x0000DCF8 File Offset: 0x0000C0F8
	public void GetAchiveDisp()
	{
		if (this.getAchiIdList.Count > 0)
		{
			int num = this.getAchiIdList[0];
			AchieveDataFormat achieveDataFormat = SuperGameMaster.sDataBase.get_AchieveDB_forId(num);
			if (achieveDataFormat == null)
			{
				return;
			}
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("称号「" + achieveDataFormat.name + "」\nを獲得しました");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.GetAchiveDisp();
			});
			SuperGameMaster.Set_GetAchieve(num);
			this.getAchiIdList.RemoveAt(0);
			base.GetComponentInParent<UIMaster>().OnSave();
			if (num == 0)
			{
				HelpPanel help = base.GetComponentInParent<UIMaster_MainIn>().HelpUI.GetComponent<HelpPanel>();
				help.OpenPanel(string.Concat(new string[]
				{
					"一定の条件を満たすと\n",
					SuperGameMaster.GetFrogName(),
					"の<color=#61a8c7><b>称号</b></color>を獲得します\n称号はかっこいいものから\nちょっと変わったものまで\nさまざまなものがあります\n称号は",
					SuperGameMaster.GetFrogName(),
					"をタップして\nセットすることができます"
				}));
				help.ResetOnClick_Screen();
				help.SetOnClick_Screen(delegate
				{
					help.ClosePanel();
				});
				help.gameObject.SetActive(false);
				confilm.SetOnClick_Screen(delegate
				{
					help.gameObject.SetActive(true);
				});
			}
		}
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0000DE74 File Offset: 0x0000C274
	public void GetData()
	{
		this.beforeName = SuperGameMaster.GetFrogName();
		this.InputUI.GetComponent<InputField>().text = this.beforeName;
		this.achieveId = SuperGameMaster.GetAchieveId();
		AchieveDataFormat achieveDataFormat = SuperGameMaster.sDataBase.get_AchieveDB_forId(this.achieveId);
		this.AchieveUI.GetComponentInChildren<Text>().text = achieveDataFormat.name;
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0000DED4 File Offset: 0x0000C2D4
	public void SetAchiData(int achiId)
	{
		this.achieveId = achiId;
		AchieveDataFormat achieveDataFormat = SuperGameMaster.sDataBase.get_AchieveDB_forId(this.achieveId);
		this.AchieveUI.GetComponentInChildren<Text>().text = achieveDataFormat.name;
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0000DF0F File Offset: 0x0000C30F
	public void SaveData()
	{
		SuperGameMaster.SetFrogState(this.InputUI.GetComponent<InputField>().text, this.achieveId);
		base.GetComponentInParent<UIMaster>().OnSave();
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0000DF38 File Offset: 0x0000C338
	public void OutOfFocus()
	{
		string text = this.InputUI.GetComponent<InputField>().text;
		if (text.Length > 5)
		{
			this.InputUI.GetComponent<InputField>().text = text.Remove(5, text.Length - 5);
		}
	}

	// Token: 0x04000198 RID: 408
	public GameObject ConfilmUI;

	// Token: 0x04000199 RID: 409
	public GameObject InputUI;

	// Token: 0x0400019A RID: 410
	public GameObject AchieveUI;

	// Token: 0x0400019B RID: 411
	private int achieveId;

	// Token: 0x0400019C RID: 412
	private List<int> getAchiIdList;

	// Token: 0x0400019D RID: 413
	private string beforeName;
}
