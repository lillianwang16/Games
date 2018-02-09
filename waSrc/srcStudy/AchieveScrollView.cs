using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006E RID: 110
public class AchieveScrollView : MonoBehaviour
{
	// Token: 0x060003BF RID: 959 RVA: 0x00016930 File Offset: 0x00014D30
	public void BackFunc()
	{
		UIMaster componentInParent = base.transform.parent.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CloseScrollView();
		});
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00016966 File Offset: 0x00014D66
	public void OpenScrollView()
	{
		base.gameObject.SetActive(true);
		this.CreateButton();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		this.BackFunc();
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00016999 File Offset: 0x00014D99
	public void CloseScrollView()
	{
		base.gameObject.SetActive(false);
		this.DeleteButton();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		base.GetComponentInParent<FrogStatePanel>().BackFunc();
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x000169D4 File Offset: 0x00014DD4
	public void CreateButton()
	{
		for (int i = 0; i < SuperGameMaster.sDataBase.count_AchieveDB(); i++)
		{
			AchieveDataFormat achieveDataFormat = SuperGameMaster.sDataBase.get_AchieveDB(i);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.btnPref);
			gameObject.transform.SetParent(this.contentsList.GetComponent<RectTransform>(), false);
			gameObject.GetComponent<AchieveButton>().ScrollViewUI = base.gameObject;
			if (SuperGameMaster.CheckAchieveFlag(achieveDataFormat.id))
			{
				gameObject.GetComponent<AchieveButton>().SetId(achieveDataFormat.id);
				gameObject.GetComponent<AchieveButton>().CngName(achieveDataFormat.name);
				gameObject.GetComponent<AchieveButton>().CngInfo(achieveDataFormat.info);
			}
			else
			{
				gameObject.GetComponent<AchieveButton>().SetId(achieveDataFormat.id);
				gameObject.GetComponent<AchieveButton>().CngName("？？？？");
				gameObject.GetComponent<AchieveButton>().CngInfo(achieveDataFormat.info);
				gameObject.GetComponent<Button>().interactable = false;
			}
		}
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00016AC4 File Offset: 0x00014EC4
	public void DeleteButton()
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00016B0A File Offset: 0x00014F0A
	public void SelectId(int achiId)
	{
		base.GetComponentInParent<FrogStatePanel>().SetAchiData(achiId);
		this.CloseScrollView();
	}

	// Token: 0x04000264 RID: 612
	public GameObject contentsList;

	// Token: 0x04000265 RID: 613
	public GameObject btnPref;
}
