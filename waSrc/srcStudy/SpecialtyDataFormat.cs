using System;
using UnityEngine;

// Token: 0x020000A2 RID: 162
[Serializable]
public class SpecialtyDataFormat
{
	// Token: 0x06000430 RID: 1072 RVA: 0x0001CA7C File Offset: 0x0001AE7C
	public SpecialtyDataFormat(SpecialtyDataFormat original)
	{
		this.id = original.id;
		this.itemId = original.itemId;
		this.place = original.place;
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0001CAA8 File Offset: 0x0001AEA8
	public SpecialtyDataFormat()
	{
	}

	// Token: 0x0400042E RID: 1070
	[Tooltip("ＩＤ")]
	[Space(10f)]
	public int id;

	// Token: 0x0400042F RID: 1071
	[Tooltip("登録アイテムＩＤ")]
	[Space(10f)]
	public int itemId;

	// Token: 0x04000430 RID: 1072
	[Tooltip("場所説明")]
	[Space(10f)]
	public string place;
}
