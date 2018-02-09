using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000034 RID: 52
public class CollectionButton : MonoBehaviour
{
	// Token: 0x06000216 RID: 534 RVA: 0x00006FA2 File Offset: 0x000053A2
	public void OnClick()
	{
		if (!this.showFlag)
		{
			return;
		}
		this.MainScrollView.GetComponent<CollectionScrollView>().OpenInfoWindow(this.collectIndex);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
	}

	// Token: 0x06000217 RID: 535 RVA: 0x00006FDF File Offset: 0x000053DF
	public void SetData(int _collectIndex, bool _showFlag)
	{
		this.collectIndex = _collectIndex;
		this.showFlag = _showFlag;
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00006FEF File Offset: 0x000053EF
	public void CngName(string str)
	{
		this.ItemText.GetComponentInChildren<Text>().text = str;
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00007002 File Offset: 0x00005402
	public void CngImage(Sprite image)
	{
		this.ItemImage.SetActive(true);
		this.ItemImage.GetComponent<RectTransform>().sizeDelta = new Vector2(72f, 72f);
		this.ItemImage.GetComponent<Image>().sprite = image;
	}

	// Token: 0x040000D8 RID: 216
	public GameObject MainScrollView;

	// Token: 0x040000D9 RID: 217
	public GameObject ItemImage;

	// Token: 0x040000DA RID: 218
	public GameObject ItemText;

	// Token: 0x040000DB RID: 219
	public int collectIndex;

	// Token: 0x040000DC RID: 220
	public bool showFlag;
}
