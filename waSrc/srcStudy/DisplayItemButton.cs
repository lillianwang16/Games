using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000035 RID: 53
public class DisplayItemButton : MonoBehaviour
{
	// Token: 0x0600021B RID: 539 RVA: 0x00007048 File Offset: 0x00005448
	public void OnClick()
	{
		base.GetComponentInParent<DisplayPanel>().SetInfoPanelData(this.shopIndex, base.transform.localPosition);
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00007066 File Offset: 0x00005466
	public void setShopIndex(int _shopIndex)
	{
		this.shopIndex = _shopIndex;
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0000706F File Offset: 0x0000546F
	public void CngDisplayName(string str)
	{
		this.DisplayNameText.GetComponent<Text>().text = str;
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00007082 File Offset: 0x00005482
	public void setitemInfo(string str)
	{
		this.DisplayNameText.GetComponent<Text>().text = str;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00007098 File Offset: 0x00005498
	public void CngDisplayImage(Sprite image, int fixY)
	{
		this.DisplayImage.GetComponent<RectTransform>().localPosition = new Vector2(this.DisplayImage.GetComponent<RectTransform>().localPosition.x, this.DisplayImage.GetComponent<RectTransform>().localPosition.y + (float)fixY);
		this.DisplayImage.GetComponent<Image>().sprite = image;
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00007103 File Offset: 0x00005503
	public void CngBackImage(Sprite image)
	{
		base.GetComponent<Image>().sprite = image;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00007114 File Offset: 0x00005514
	public void CngPriceNum(int price)
	{
		base.GetComponent<NumObjCreater>().createNumObj(price, this.PriceNumBase.transform.localPosition, (int)this.PriceNumBase.transform.GetComponent<RectTransform>().sizeDelta.x);
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000715B File Offset: 0x0000555B
	public void SetSoldOut()
	{
		this.SoldOutImage.SetActive(true);
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0000716C File Offset: 0x0000556C
	public void NumDelete()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.Equals("PriceNumBase(Clone)"))
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x040000DD RID: 221
	public GameObject DisplayNameText;

	// Token: 0x040000DE RID: 222
	public GameObject DisplayImage;

	// Token: 0x040000DF RID: 223
	public GameObject PriceNumBase;

	// Token: 0x040000E0 RID: 224
	public GameObject SoldOutImage;

	// Token: 0x040000E1 RID: 225
	public int shopIndex;
}
