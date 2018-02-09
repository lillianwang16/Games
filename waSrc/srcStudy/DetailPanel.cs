using System;
using Collection;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000050 RID: 80
public class DetailPanel : MonoBehaviour
{
	// Token: 0x060002D4 RID: 724 RVA: 0x0000C4B8 File Offset: 0x0000A8B8
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.PushCloseButton();
		});
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x0000C4E4 File Offset: 0x0000A8E4
	public void OpenPanel(int index, ShowType showType)
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		if (showType != ShowType.Collect)
		{
			if (showType == ShowType.Specialty)
			{
				SpecialtyDataFormat specialtyDataFormat = SuperGameMaster.sDataBase.get_SpecialtyDB(index);
				ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(specialtyDataFormat.itemId);
				this.DetailNameText.GetComponent<Text>().text = itemDataFormat.name;
				this.DetailImage.GetComponent<Image>().sprite = itemDataFormat.img;
				this.DetailPlaceText.GetComponent<Text>().text = specialtyDataFormat.place;
				this.DetailInfoText.GetComponent<Text>().text = itemDataFormat.info;
			}
		}
		else
		{
			CollectionDataFormat collectionDataFormat = SuperGameMaster.sDataBase.get_CollectDB(index);
			this.DetailNameText.GetComponent<Text>().text = collectionDataFormat.name;
			this.DetailImage.GetComponent<Image>().sprite = collectionDataFormat.img;
			this.DetailPlaceText.GetComponent<Text>().text = collectionDataFormat.place;
			this.DetailInfoText.GetComponent<Text>().text = collectionDataFormat.info;
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		this.BackFunc();
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x0000C644 File Offset: 0x0000AA44
	public void PushCloseButton()
	{
		base.gameObject.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		base.GetComponentInParent<UIMaster_Present>().BackFunc();
	}

	// Token: 0x0400017B RID: 379
	public GameObject DetailNameText;

	// Token: 0x0400017C RID: 380
	public GameObject DetailImage;

	// Token: 0x0400017D RID: 381
	public GameObject DetailPlaceText;

	// Token: 0x0400017E RID: 382
	public GameObject DetailInfoText;

	// Token: 0x0400017F RID: 383
	public int collectIndex;
}
