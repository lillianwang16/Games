using System;
using Item;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000033 RID: 51
public class BagPanelButton : MonoBehaviour
{
	// Token: 0x06000212 RID: 530 RVA: 0x00006F60 File Offset: 0x00005360
	public void PushPanelButton()
	{
		this.BagPanel.GetComponent<BagPanel>().OpenBagScrollView(this.itemType, this.btnId);
	}

	// Token: 0x06000213 RID: 531 RVA: 0x00006F7E File Offset: 0x0000537E
	public void CngItemImage(Sprite itemImg)
	{
		base.GetComponent<Image>().sprite = itemImg;
	}

	// Token: 0x06000214 RID: 532 RVA: 0x00006F8C File Offset: 0x0000538C
	public void CngItemName(string itemStr)
	{
		base.GetComponentInChildren<Text>().text = itemStr;
	}

	// Token: 0x040000D5 RID: 213
	public GameObject BagPanel;

	// Token: 0x040000D6 RID: 214
	public Item.Type itemType;

	// Token: 0x040000D7 RID: 215
	public int btnId;
}
