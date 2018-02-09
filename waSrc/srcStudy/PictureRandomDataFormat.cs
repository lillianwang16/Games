using System;
using Picture;
using UnityEngine;

// Token: 0x020000AB RID: 171
[Serializable]
public class PictureRandomDataFormat
{
	// Token: 0x0600043D RID: 1085 RVA: 0x0001CC33 File Offset: 0x0001B033
	public PictureRandomDataFormat(PictureRandomDataFormat original)
	{
		this.id = original.id;
		this.rndType = original.rndType;
		this.set = original.set;
		this.setName = original.setName;
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0001CC6B File Offset: 0x0001B06B
	public PictureRandomDataFormat()
	{
	}

	// Token: 0x0400044E RID: 1102
	[Tooltip("ID")]
	public int id;

	// Token: 0x0400044F RID: 1103
	[Tooltip("タイプ")]
	[Space(10f)]
	public RndType rndType;

	// Token: 0x04000450 RID: 1104
	[Tooltip("セット名")]
	public string set;

	// Token: 0x04000451 RID: 1105
	[Tooltip("登録名リスト")]
	public string[] setName;
}
