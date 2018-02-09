using System;
using Picture;
using UnityEngine;

// Token: 0x020000AC RID: 172
[Serializable]
public class PictureTagDataFormat
{
	// Token: 0x0600043F RID: 1087 RVA: 0x0001CC73 File Offset: 0x0001B073
	public PictureTagDataFormat(PictureTagDataFormat original)
	{
		this.id = original.id;
		this.tagType = original.tagType;
		this.tag = original.tag;
		this.picName = original.picName;
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x0001CCAB File Offset: 0x0001B0AB
	public PictureTagDataFormat()
	{
	}

	// Token: 0x04000452 RID: 1106
	[Tooltip("ID")]
	public int id;

	// Token: 0x04000453 RID: 1107
	[Tooltip("タグタイプ")]
	[Space(10f)]
	public TagType tagType;

	// Token: 0x04000454 RID: 1108
	[Tooltip("タグ名")]
	public string tag;

	// Token: 0x04000455 RID: 1109
	[Tooltip("登録写真リスト")]
	public string[] picName;
}
