using System;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[Serializable]
public class NodePrefDataFormat
{
	// Token: 0x0600045B RID: 1115 RVA: 0x0001F6E7 File Offset: 0x0001DAE7
	public NodePrefDataFormat(NodePrefDataFormat original)
	{
		this.id = original.id;
		this.collectionId = original.collectionId;
		this.specialtyId = original.specialtyId;
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0001F713 File Offset: 0x0001DB13
	public NodePrefDataFormat()
	{
	}

	// Token: 0x040004A3 RID: 1187
	[Tooltip("ノードID")]
	public int id;

	// Token: 0x040004A4 RID: 1188
	[Tooltip("いっぴんID")]
	[Space(10f)]
	public int collectionId;

	// Token: 0x040004A5 RID: 1189
	[Tooltip("名物ID")]
	public int[] specialtyId;
}
