using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
[Serializable]
public class CollectionDataFormat
{
	// Token: 0x0600041A RID: 1050 RVA: 0x0001BF98 File Offset: 0x0001A398
	public CollectionDataFormat(CollectionDataFormat original)
	{
		this.name = original.name;
		this.place = original.place;
		this.info = original.info;
		this.img = original.img;
		this.id = original.id;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0001BFE7 File Offset: 0x0001A3E7
	public CollectionDataFormat()
	{
	}

	// Token: 0x04000400 RID: 1024
	[Tooltip("名前")]
	public string name;

	// Token: 0x04000401 RID: 1025
	[Tooltip("出現場所")]
	public string place;

	// Token: 0x04000402 RID: 1026
	[Tooltip("説明")]
	[Multiline(3)]
	public string info;

	// Token: 0x04000403 RID: 1027
	[Tooltip("アイテム画像")]
	public Sprite img;

	// Token: 0x04000404 RID: 1028
	[Tooltip("コレクションアイテムＩＤ")]
	[Space(10f)]
	public int id;

	// Token: 0x04000405 RID: 1029
	[Tooltip("コレクションタイプ")]
	[Space(10f)]
	public int type;
}
