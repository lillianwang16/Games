using System;
using Node;
using UnityEngine;

// Token: 0x020000A9 RID: 169
[Serializable]
public class PictureCharaDataFormat
{
	// Token: 0x06000439 RID: 1081 RVA: 0x0001CB2F File Offset: 0x0001AF2F
	public PictureCharaDataFormat(PictureCharaDataFormat original)
	{
		this.id = original.id;
		this.name = original.name;
		this.wayType = original.wayType;
		this.posePath = original.posePath;
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0001CB67 File Offset: 0x0001AF67
	public PictureCharaDataFormat()
	{
	}

	// Token: 0x0400043C RID: 1084
	[Tooltip("ID")]
	public int id;

	// Token: 0x0400043D RID: 1085
	[Tooltip("キャラ名")]
	public string name;

	// Token: 0x0400043E RID: 1086
	[Tooltip("出現場所")]
	[Space(10f)]
	public WayType wayType;

	// Token: 0x0400043F RID: 1087
	[Tooltip("ポーズ対応画像名")]
	[Space(10f)]
	public string[] posePath;
}
