using System;
using Flag;
using UnityEngine;

// Token: 0x02000093 RID: 147
[Serializable]
public class AchieveDataFormat
{
	// Token: 0x06000416 RID: 1046 RVA: 0x0001BEA0 File Offset: 0x0001A2A0
	public AchieveDataFormat(AchieveDataFormat original)
	{
		this.name = original.name;
		this.info = original.info;
		this.id = original.id;
		this.flagType = original.flagType;
		this.flagValue = original.flagValue;
		this.flagSign = original.flagSign;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0001BEFB File Offset: 0x0001A2FB
	public AchieveDataFormat()
	{
	}

	// Token: 0x040003F0 RID: 1008
	[Tooltip("称号名")]
	public string name;

	// Token: 0x040003F1 RID: 1009
	[Tooltip("情報")]
	public string info;

	// Token: 0x040003F2 RID: 1010
	[Tooltip("ＩＤ")]
	[Space(10f)]
	public int id;

	// Token: 0x040003F3 RID: 1011
	[Tooltip("判定フラグタイプ")]
	public Flag.Type[] flagType;

	// Token: 0x040003F4 RID: 1012
	[Tooltip("判定値")]
	public int[] flagValue;

	// Token: 0x040003F5 RID: 1013
	[Tooltip("符号")]
	public Sign[] flagSign;
}
