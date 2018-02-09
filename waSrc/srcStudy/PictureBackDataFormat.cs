using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[Serializable]
public class PictureBackDataFormat
{
	// Token: 0x06000437 RID: 1079 RVA: 0x0001CAD8 File Offset: 0x0001AED8
	public PictureBackDataFormat(PictureBackDataFormat original)
	{
		this.id = original.id;
		this.name = original.name;
		this.path = original.path;
		this.fixPos = original.fixPos;
		this.rndPos = original.rndPos;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0001CB27 File Offset: 0x0001AF27
	public PictureBackDataFormat()
	{
	}

	// Token: 0x04000437 RID: 1079
	[Tooltip("ID")]
	public int id;

	// Token: 0x04000438 RID: 1080
	[Tooltip("管理名")]
	[Space(10f)]
	public string name;

	// Token: 0x04000439 RID: 1081
	[Tooltip("パス")]
	public string[] path;

	// Token: 0x0400043A RID: 1082
	[Tooltip("表示基準修正値")]
	[Space(10f)]
	public Vector2 fixPos;

	// Token: 0x0400043B RID: 1083
	[Tooltip("可動範囲修正値")]
	public int[] rndPos;
}
