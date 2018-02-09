using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000074 RID: 116
public class PrizeListScrollView : MonoBehaviour
{
	// Token: 0x060003F6 RID: 1014 RVA: 0x00019EB4 File Offset: 0x000182B4
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CloseScrollView();
		});
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00019EE0 File Offset: 0x000182E0
	public void OpenScrollView()
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.LoadImages();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc();
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x00019F50 File Offset: 0x00018350
	public void CloseScrollView()
	{
		base.gameObject.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		this.DeleteImage();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		base.GetComponentInParent<UIMaster_Raffle>().BackFunc();
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x00019FC4 File Offset: 0x000183C4
	public void LoadImages()
	{
		for (int i = 0; i < SuperGameMaster.sDataBase.count_PrizeDB(); i++)
		{
			PrizeDataFormat prizeDataFormat = SuperGameMaster.sDataBase.get_PrizeDB(i);
			ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(prizeDataFormat.itemId);
			if (prizeDataFormat.itemId == -1 || itemDataFormat != null)
			{
				int rank = (int)prizeDataFormat.rank;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemPref);
				gameObject.transform.SetParent(this.ItemPanels[rank].GetComponent<RectTransform>(), false);
				if (prizeDataFormat.itemId != -1)
				{
					gameObject.transform.GetChild(0).GetComponentInChildren<Image>().sprite = itemDataFormat.img;
				}
			}
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0001A078 File Offset: 0x00018478
	public void DeleteImage()
	{
		for (int i = 0; i < this.ItemPanels.Length; i++)
		{
			RectTransform component = this.ItemPanels[i].GetComponent<RectTransform>();
			for (int j = 0; j < component.transform.childCount; j++)
			{
				UnityEngine.Object.Destroy(component.GetChild(j).gameObject);
			}
		}
	}

	// Token: 0x0400028B RID: 651
	public GameObject[] ItemPanels;

	// Token: 0x0400028C RID: 652
	public GameObject itemPref;
}
