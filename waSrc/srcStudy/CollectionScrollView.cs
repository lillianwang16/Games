using System;
using Collection;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006F RID: 111
public class CollectionScrollView : MonoBehaviour
{
	// Token: 0x060003C7 RID: 967 RVA: 0x00016B2E File Offset: 0x00014F2E
	public void init()
	{
		this.CreateButton(0);
		this.CollectBtn.GetComponent<Button>().interactable = false;
		this.SpecialtyBtn.GetComponent<Button>().interactable = true;
		SuperGameMaster.audioMgr.StopSE();
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00016B64 File Offset: 0x00014F64
	public void CreateButton(int showType)
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
		}
		this.nowShowType = (ShowType)showType;
		if (showType != 0)
		{
			if (showType == 1)
			{
				for (int j = 0; j < SuperGameMaster.sDataBase.count_SpecialtyDB(); j++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.btnPref);
					gameObject.transform.SetParent(component, false);
					SpecialtyDataFormat specialtyDataFormat = SuperGameMaster.sDataBase.get_SpecialtyDB(j);
					if (SuperGameMaster.saveData.specialtyFlags[specialtyDataFormat.id])
					{
						ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(specialtyDataFormat.itemId);
						gameObject.GetComponent<CollectionButton>().SetData(j, true);
						gameObject.GetComponent<CollectionButton>().CngName(itemDataFormat.name);
						gameObject.GetComponent<CollectionButton>().CngImage(itemDataFormat.img);
					}
					else
					{
						gameObject.GetComponent<CollectionButton>().SetData(j, false);
					}
					gameObject.GetComponent<CollectionButton>().MainScrollView = base.gameObject;
				}
				this.CollectBtn.GetComponent<Button>().interactable = true;
				this.SpecialtyBtn.GetComponent<Button>().interactable = false;
			}
		}
		else
		{
			for (int k = 0; k < SuperGameMaster.sDataBase.count_CollectDB(); k++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.btnPref);
				gameObject2.transform.SetParent(component, false);
				CollectionDataFormat collectionDataFormat = SuperGameMaster.sDataBase.get_CollectDB(k);
				if (SuperGameMaster.saveData.collectFlags[collectionDataFormat.id])
				{
					gameObject2.GetComponent<CollectionButton>().SetData(k, true);
					gameObject2.GetComponent<CollectionButton>().CngName(collectionDataFormat.name);
					gameObject2.GetComponent<CollectionButton>().CngImage(collectionDataFormat.img);
				}
				else
				{
					gameObject2.GetComponent<CollectionButton>().SetData(k, false);
				}
				gameObject2.GetComponent<CollectionButton>().MainScrollView = base.gameObject;
			}
			this.CollectBtn.GetComponent<Button>().interactable = false;
			this.SpecialtyBtn.GetComponent<Button>().interactable = true;
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00016DB4 File Offset: 0x000151B4
	public void OpenInfoWindow(int index)
	{
		this.DetailUI.GetComponent<DetailPanel>().OpenPanel(index, this.nowShowType);
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00016DD0 File Offset: 0x000151D0
	public void DellButton()
	{
		RectTransform component = this.contentsList.GetComponent<RectTransform>();
		for (int i = 0; i < component.transform.childCount; i++)
		{
			UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
		}
	}

	// Token: 0x04000266 RID: 614
	public GameObject DetailUI;

	// Token: 0x04000267 RID: 615
	public GameObject CollectBtn;

	// Token: 0x04000268 RID: 616
	public GameObject SpecialtyBtn;

	// Token: 0x04000269 RID: 617
	public GameObject btnPref;

	// Token: 0x0400026A RID: 618
	public GameObject contentsList;

	// Token: 0x0400026B RID: 619
	public ShowType nowShowType;
}
