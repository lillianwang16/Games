using System;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[Serializable]
public class NodeGoalDataFormat
{
	// Token: 0x06000457 RID: 1111 RVA: 0x0001F67F File Offset: 0x0001DA7F
	public NodeGoalDataFormat(NodeGoalDataFormat original)
	{
		this.id = original.id;
		this.itemPer = original.itemPer;
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0001F69F File Offset: 0x0001DA9F
	public NodeGoalDataFormat()
	{
	}

	// Token: 0x0400049D RID: 1181
	[Tooltip("ID")]
	public int id;

	// Token: 0x0400049E RID: 1182
	[Tooltip("ノード別アイテム確立リスト")]
	[Space(10f)]
	public int[] itemPer;
}
