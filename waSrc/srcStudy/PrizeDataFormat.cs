using System;
using Prize;
using UnityEngine;

// Token: 0x020000A0 RID: 160
[Serializable]
public class PrizeDataFormat
{
	// Token: 0x0600042C RID: 1068 RVA: 0x0001C9FC File Offset: 0x0001ADFC
	public PrizeDataFormat(PrizeDataFormat original)
	{
		this.id = original.id;
		this.stock = original.stock;
		this.rank = original.rank;
		this.itemId = original.itemId;
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x0001CA34 File Offset: 0x0001AE34
	public PrizeDataFormat()
	{
	}

	// Token: 0x04000426 RID: 1062
	[Tooltip("アイテムID")]
	public int itemId;

	// Token: 0x04000427 RID: 1063
	[Tooltip("景品の数")]
	public int stock;

	// Token: 0x04000428 RID: 1064
	[Tooltip("ランク（球の色）")]
	public Rank rank;

	// Token: 0x04000429 RID: 1065
	[Tooltip("景品ID")]
	[Space(10f)]
	public int id;
}
