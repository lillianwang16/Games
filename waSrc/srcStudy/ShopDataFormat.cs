using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
[Serializable]
public class ShopDataFormat
{
	// Token: 0x0600042E RID: 1070 RVA: 0x0001CA3C File Offset: 0x0001AE3C
	public ShopDataFormat(ShopDataFormat original)
	{
		this.itemId = original.itemId;
		this.fixY = original.fixY;
		this.info = original.info;
		this.id = original.id;
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x0001CA74 File Offset: 0x0001AE74
	public ShopDataFormat()
	{
	}

	// Token: 0x0400042A RID: 1066
	[Tooltip("陳列アイテムID")]
	public int itemId;

	// Token: 0x0400042B RID: 1067
	[Tooltip("ショップでの表示場所修正")]
	public int fixY;

	// Token: 0x0400042C RID: 1068
	[Tooltip("ショップメッセージ")]
	public string info;

	// Token: 0x0400042D RID: 1069
	[Tooltip("ショップID")]
	[Space(10f)]
	public int id;
}
