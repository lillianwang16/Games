using System;
using Item;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000036 RID: 54
public class ItemButton : MonoBehaviour
{
	// Token: 0x06000225 RID: 549 RVA: 0x000071F4 File Offset: 0x000055F4
	public void PushItemButton()
	{
		this.ItemScrollView.GetComponent<ItemScrollView>().CloseScrollView(this.itemId);
	}

	// Token: 0x06000226 RID: 550 RVA: 0x0000720C File Offset: 0x0000560C
	public void CngItemName(string nameStr)
	{
		this.ItemNameText.GetComponent<Text>().text = nameStr;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x0000721F File Offset: 0x0000561F
	public void CngItemInfo(string infoStr)
	{
		this.ItemInfoText.GetComponent<Text>().text = infoStr;
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00007234 File Offset: 0x00005634
	public void CngStockNum(int stockNum)
	{
		string text;
		if (stockNum == -1)
		{
			text = "-";
		}
		else if (stockNum == -2)
		{
			text = string.Empty;
		}
		else
		{
			text = stockNum.ToString();
		}
		this.ItemStockText.GetComponent<Text>().text = text;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00007285 File Offset: 0x00005685
	public void CngImage(Sprite itemImg)
	{
		if (itemImg == null)
		{
			this.ItemImage.SetActive(false);
			this.ItemBackImage.SetActive(false);
			return;
		}
		this.ItemImage.GetComponent<Image>().sprite = itemImg;
	}

	// Token: 0x0600022A RID: 554 RVA: 0x000072BD File Offset: 0x000056BD
	public void setItemId(int id, Item.Type type)
	{
		this.itemId = id;
		this.itemType = type;
	}

	// Token: 0x0600022B RID: 555 RVA: 0x000072CD File Offset: 0x000056CD
	public void SetCheckImage(bool flag)
	{
		this.CheckImage.SetActive(flag);
	}

	// Token: 0x0600022C RID: 556 RVA: 0x000072DB File Offset: 0x000056DB
	public void Fade(Color _color)
	{
		base.GetComponent<Image>().color = _color;
		this.ItemBackImage.GetComponent<Image>().color = _color;
		this.ItemImage.GetComponent<Image>().color = _color;
	}

	// Token: 0x040000E2 RID: 226
	public GameObject ItemScrollView;

	// Token: 0x040000E3 RID: 227
	public GameObject ItemImage;

	// Token: 0x040000E4 RID: 228
	public GameObject ItemBackImage;

	// Token: 0x040000E5 RID: 229
	public GameObject ItemNameText;

	// Token: 0x040000E6 RID: 230
	public GameObject ItemStockText;

	// Token: 0x040000E7 RID: 231
	public GameObject ItemInfoText;

	// Token: 0x040000E8 RID: 232
	public GameObject CheckImage;

	// Token: 0x040000E9 RID: 233
	public int itemId;

	// Token: 0x040000EA RID: 234
	public Item.Type itemType;
}
