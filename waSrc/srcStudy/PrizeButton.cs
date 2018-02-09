using System;
using Prize;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003B RID: 59
public class PrizeButton : MonoBehaviour
{
	// Token: 0x0600024E RID: 590 RVA: 0x00007A9E File Offset: 0x00005E9E
	public void PushPrizeButton()
	{
		this.PrizeScrollViewUI.GetComponent<PrizeScrollView>().SelectPrize(this.prizeId);
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00007AB6 File Offset: 0x00005EB6
	public void CngPrizeName(string nameStr)
	{
		this.PrizeNameText.GetComponent<Text>().text = nameStr;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00007ACC File Offset: 0x00005ECC
	public void CngStockNum(int stockNum)
	{
		if (stockNum == 1)
		{
			this.PrizeStockText.SetActive(false);
		}
		else
		{
			this.PrizeStockText.GetComponent<Text>().text = "x " + stockNum.ToString();
		}
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00007B18 File Offset: 0x00005F18
	public void CngHaveItemStock(int stockNum)
	{
		this.PrizeHaveStockText.GetComponent<Text>().text = "所持数\u3000" + stockNum + "個";
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00007B3F File Offset: 0x00005F3F
	public void CngImage(Sprite itemImg)
	{
		this.PrizeImage.GetComponent<Image>().sprite = itemImg;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00007B52 File Offset: 0x00005F52
	public void setPrizeId(int _prizeId, Rank rank)
	{
		this.prizeId = _prizeId;
		this.prizeRank = rank;
	}

	// Token: 0x04000103 RID: 259
	public GameObject PrizeImage;

	// Token: 0x04000104 RID: 260
	public GameObject PrizeNameText;

	// Token: 0x04000105 RID: 261
	public GameObject PrizeStockText;

	// Token: 0x04000106 RID: 262
	public GameObject PrizeHaveStockText;

	// Token: 0x04000107 RID: 263
	public GameObject PrizeScrollViewUI;

	// Token: 0x04000108 RID: 264
	public int prizeId;

	// Token: 0x04000109 RID: 265
	public Rank prizeRank;
}
