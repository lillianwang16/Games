using System;
using UnityEngine;

// Token: 0x02000094 RID: 148
[Serializable]
public class CharacterDataFormat
{
	// Token: 0x06000418 RID: 1048 RVA: 0x0001BF04 File Offset: 0x0001A304
	public CharacterDataFormat(CharacterDataFormat original)
	{
		this.name = original.name;
		this.img = original.img;
		this.id = original.id;
		this.flagValue = original.flagValue;
		this.cloverPow = original.cloverPow;
		this.aniName = original.aniName;
		this.size = original.size;
		this.offset = original.offset;
		this.rndPos = original.rndPos;
		this.taste = original.taste;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0001BF8F File Offset: 0x0001A38F
	public CharacterDataFormat()
	{
	}

	// Token: 0x040003F6 RID: 1014
	[Tooltip("名前")]
	public string name;

	// Token: 0x040003F7 RID: 1015
	[Tooltip("キャラクター画像")]
	public Sprite img;

	// Token: 0x040003F8 RID: 1016
	[Tooltip("キャラクターID")]
	[Space(10f)]
	public int id;

	// Token: 0x040003F9 RID: 1017
	[Tooltip("開放名物数")]
	[Space(10f)]
	public int flagValue;

	// Token: 0x040003FA RID: 1018
	[Tooltip("三つ葉力")]
	public int cloverPow;

	// Token: 0x040003FB RID: 1019
	[Tooltip("アニメーション名")]
	[Space(10f)]
	public string aniName;

	// Token: 0x040003FC RID: 1020
	[Tooltip("当たり判定サイズ")]
	public Vector2 size;

	// Token: 0x040003FD RID: 1021
	[Tooltip("判定座標補正")]
	public Vector2 offset;

	// Token: 0x040003FE RID: 1022
	[Tooltip("来る可能性のある座標")]
	public Vector2[] rndPos;

	// Token: 0x040003FF RID: 1023
	[Tooltip("名産品の好みリスト")]
	[Space(10f)]
	public int[] taste;
}
