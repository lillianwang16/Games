using System;
using Node;
using UnityEngine;

// Token: 0x020000B5 RID: 181
[Serializable]
public class NodeDataFormat
{
	// Token: 0x06000453 RID: 1107 RVA: 0x0001F586 File Offset: 0x0001D986
	public NodeDataFormat(NodeDataFormat original)
	{
		this.id = original.id;
		this.type = original.type;
		this.pathId = original.pathId;
		this.picTag = original.picTag;
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0001F5BE File Offset: 0x0001D9BE
	public NodeDataFormat()
	{
	}

	// Token: 0x0400048C RID: 1164
	[Tooltip("ID")]
	public int id;

	// Token: 0x0400048D RID: 1165
	[Tooltip("ノードタイプ")]
	[Space(10f)]
	public NodeType type;

	// Token: 0x0400048E RID: 1166
	[Tooltip("通過ノードID（県庁所在地）")]
	public int pathId;

	// Token: 0x0400048F RID: 1167
	[Tooltip("目的地写真タグ")]
	public string picTag;
}
