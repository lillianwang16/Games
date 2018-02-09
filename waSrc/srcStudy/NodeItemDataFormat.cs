using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
[Serializable]
public class NodeItemDataFormat
{
	// Token: 0x06000459 RID: 1113 RVA: 0x0001F6A7 File Offset: 0x0001DAA7
	public NodeItemDataFormat(NodeItemDataFormat original)
	{
		this.id = original.id;
		this.collectionId = original.collectionId;
		this.specialtyId = original.specialtyId;
		this.specialtyPer = original.specialtyPer;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0001F6DF File Offset: 0x0001DADF
	public NodeItemDataFormat()
	{
	}

	// Token: 0x0400049F RID: 1183
	[Tooltip("ID")]
	public int id;

	// Token: 0x040004A0 RID: 1184
	[Tooltip("いっぴんID")]
	[Space(10f)]
	public int collectionId;

	// Token: 0x040004A1 RID: 1185
	[Tooltip("名物ID")]
	[Space(10f)]
	public int[] specialtyId;

	// Token: 0x040004A2 RID: 1186
	[Tooltip("名物確立")]
	public int[] specialtyPer;
}
