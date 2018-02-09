using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000057 RID: 87
public class InfoPanel : MonoBehaviour
{
	// Token: 0x06000313 RID: 787 RVA: 0x0000EC7C File Offset: 0x0000D07C
	public void SetInfoPanel(int index)
	{
		this.setShopIndex = index;
		if (this.setShopIndex != -1)
		{
			ShopDataFormat shopDataFormat = SuperGameMaster.sDataBase.get_ShopDB(this.setShopIndex);
			this.InfoText.GetComponent<Text>().text = shopDataFormat.info;
		}
		else
		{
			this.InfoText.GetComponent<Text>().text = string.Empty;
		}
	}

	// Token: 0x040001AA RID: 426
	public GameObject InfoText;

	// Token: 0x040001AB RID: 427
	public int setShopIndex;
}
