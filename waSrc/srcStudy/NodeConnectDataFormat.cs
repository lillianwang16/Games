using System;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[Serializable]
public class NodeConnectDataFormat
{
	// Token: 0x06000451 RID: 1105 RVA: 0x0001F552 File Offset: 0x0001D952
	public NodeConnectDataFormat(NodeConnectDataFormat original)
	{
		this.id = original.id;
		this.pos = original.pos;
		this.edge = original.edge;
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0001F57E File Offset: 0x0001D97E
	public NodeConnectDataFormat()
	{
	}

	// Token: 0x04000489 RID: 1161
	[Tooltip("ID")]
	public int id;

	// Token: 0x0400048A RID: 1162
	[Tooltip("ノード座標")]
	[Space(10f)]
	public Vector2 pos;

	// Token: 0x0400048B RID: 1163
	[Tooltip("接続エッジＩＤ")]
	[Space(10f)]
	public int[] edge;
}
