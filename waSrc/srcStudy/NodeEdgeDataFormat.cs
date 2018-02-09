using System;
using Node;
using UnityEngine;

// Token: 0x020000B6 RID: 182
[Serializable]
public class NodeEdgeDataFormat
{
	// Token: 0x06000455 RID: 1109 RVA: 0x0001F5C8 File Offset: 0x0001D9C8
	public NodeEdgeDataFormat(NodeEdgeDataFormat original)
	{
		this.id = original.id;
		this.plug = original.plug;
		this.wayType = original.wayType;
		this.time = original.time;
		this.plusTime = original.plusTime;
		this.normalTag = original.normalTag;
		this.toolsTag = original.toolsTag;
		this.uniqueTag = original.uniqueTag;
		this.nTagPer = original.nTagPer;
		this.tTagPer = original.tTagPer;
		this.uTagPer = original.uTagPer;
		this.enc_name = original.enc_name;
		this.enc_per = original.enc_per;
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001F677 File Offset: 0x0001DA77
	public NodeEdgeDataFormat()
	{
	}

	// Token: 0x04000490 RID: 1168
	[Tooltip("ID")]
	public int id;

	// Token: 0x04000491 RID: 1169
	[Tooltip("接続ノード")]
	[Space(10f)]
	public int[] plug;

	// Token: 0x04000492 RID: 1170
	[Tooltip("道の種類")]
	[Space(10f)]
	public WayType wayType;

	// Token: 0x04000493 RID: 1171
	[Tooltip("移動時間")]
	public int time;

	// Token: 0x04000494 RID: 1172
	[Tooltip("追加移動時間")]
	public int plusTime;

	// Token: 0x04000495 RID: 1173
	[Tooltip("通常写真タグ")]
	public string normalTag;

	// Token: 0x04000496 RID: 1174
	[Tooltip("道具写真タグ")]
	public string toolsTag;

	// Token: 0x04000497 RID: 1175
	[Tooltip("ユニーク写真タグ")]
	public string uniqueTag;

	// Token: 0x04000498 RID: 1176
	[Tooltip("通常写真確率")]
	public int nTagPer;

	// Token: 0x04000499 RID: 1177
	[Tooltip("道具写真確率")]
	public int tTagPer;

	// Token: 0x0400049A RID: 1178
	[Tooltip("ユニーク写真確率")]
	public int uTagPer;

	// Token: 0x0400049B RID: 1179
	[Tooltip("遭遇キャラ名")]
	public string enc_name;

	// Token: 0x0400049C RID: 1180
	[Tooltip("キャラ遭遇率")]
	public int enc_per;
}
