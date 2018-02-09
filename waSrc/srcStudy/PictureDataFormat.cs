using System;
using Picture;
using UnityEngine;

// Token: 0x020000AA RID: 170
[Serializable]
public class PictureDataFormat
{
	// Token: 0x0600043B RID: 1083 RVA: 0x0001CB70 File Offset: 0x0001AF70
	public PictureDataFormat(PictureDataFormat original)
	{
		this.id = original.id;
		this.name = original.name;
		this.type = original.type;
		this.view = original.view;
		this.randomSet = original.randomSet;
		this.backImage = original.backImage;
		this.frontImage = original.frontImage;
		this.priority = original.priority;
		this.frogPose = original.frogPose;
		this.frogPose_s = original.frogPose_s;
		this.travelerPose = original.travelerPose;
		this.frogPos = original.frogPos;
		this.frogPos_s = original.frogPos_s;
		this.travelerPos = original.travelerPos;
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0001CC2B File Offset: 0x0001B02B
	public PictureDataFormat()
	{
	}

	// Token: 0x04000440 RID: 1088
	[Tooltip("ID")]
	public int id;

	// Token: 0x04000441 RID: 1089
	[Tooltip("写真名")]
	public string name;

	// Token: 0x04000442 RID: 1090
	[Tooltip("写真の系統")]
	[Space(10f)]
	public Picture.Type type;

	// Token: 0x04000443 RID: 1091
	[Tooltip("カメラ稼動範囲")]
	public Vector2 view;

	// Token: 0x04000444 RID: 1092
	[Tooltip("ランダムセットフラグ")]
	public bool randomSet;

	// Token: 0x04000445 RID: 1093
	[Tooltip("配置画像名：奥")]
	[Space(10f)]
	public string[] backImage;

	// Token: 0x04000446 RID: 1094
	[Tooltip("配置画像名：手前")]
	public string[] frontImage;

	// Token: 0x04000447 RID: 1095
	[Tooltip("表示優先度")]
	[Space(10f)]
	public bool priority;

	// Token: 0x04000448 RID: 1096
	[Tooltip("かえるポーズ名")]
	public string frogPose;

	// Token: 0x04000449 RID: 1097
	[Tooltip("かえるポーズ名（単体）")]
	public string frogPose_s;

	// Token: 0x0400044A RID: 1098
	[Tooltip("旅仲間ポーズ名")]
	public string[] travelerPose;

	// Token: 0x0400044B RID: 1099
	[Tooltip("カエル座標")]
	public Vector2 frogPos;

	// Token: 0x0400044C RID: 1100
	[Tooltip("カエル座標（単体）")]
	public Vector2 frogPos_s;

	// Token: 0x0400044D RID: 1101
	[Tooltip("旅仲間座標")]
	public Vector2[] travelerPos;
}
