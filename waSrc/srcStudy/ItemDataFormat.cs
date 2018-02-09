using System;
using Item;
using UnityEngine;

// Token: 0x0200009E RID: 158
[Serializable]
public class ItemDataFormat
{
	// Token: 0x06000424 RID: 1060 RVA: 0x0001C030 File Offset: 0x0001A430
	public ItemDataFormat(ItemDataFormat original)
	{
		this.name = original.name;
		this.info = original.info;
		this.img = original.img;
		this.id = original.id;
		this.type = original.type;
		this.price = original.price;
		this.spend = original.spend;
		this.effectType = original.effectType;
		this.effectElm = original.effectElm;
		this.effectValue = original.effectValue;
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0001C0BB File Offset: 0x0001A4BB
	public ItemDataFormat()
	{
	}

	// Token: 0x0400040F RID: 1039
	[Tooltip("アイテム名")]
	public string name;

	// Token: 0x04000410 RID: 1040
	[Tooltip("アイテムの説明")]
	[Multiline(2)]
	public string info;

	// Token: 0x04000411 RID: 1041
	[Tooltip("アイテムのサムネイル画像")]
	public Sprite img;

	// Token: 0x04000412 RID: 1042
	[Tooltip("アイテムＩＤ")]
	[Space(10f)]
	public int id;

	// Token: 0x04000413 RID: 1043
	[Tooltip("アイテムの種類")]
	public Item.Type type;

	// Token: 0x04000414 RID: 1044
	[Tooltip("価格")]
	[Space(10f)]
	public int price;

	// Token: 0x04000415 RID: 1045
	[Tooltip("消費アイテムフラグ")]
	public bool spend;

	// Token: 0x04000416 RID: 1046
	[Tooltip("効果タイプ")]
	[Space(10f)]
	public EffectType[] effectType;

	// Token: 0x04000417 RID: 1047
	[Tooltip("効果属性")]
	public EffectElm[] effectElm;

	// Token: 0x04000418 RID: 1048
	[Tooltip("効果量")]
	public int[] effectValue;
}
