using System;

namespace Tutorial
{
	// Token: 0x0200008C RID: 140
	[Serializable]
	public enum Step
	{
		// Token: 0x040003BD RID: 957
		NONE = -1,
		// Token: 0x040003BE RID: 958
		a0_MO_FrogTap,
		// Token: 0x040003BF RID: 959
		a1_MO_FrogName,
		// Token: 0x040003C0 RID: 960
		a2_MO_GoHome,
		// Token: 0x040003C1 RID: 961
		b0_MI_GoOut,
		// Token: 0x040003C2 RID: 962
		c0_MO_GetStandby,
		// Token: 0x040003C3 RID: 963
		c1_MO_GetClover,
		// Token: 0x040003C4 RID: 964
		c2_MO_GoShop,
		// Token: 0x040003C5 RID: 965
		d0_SH_BuyStandby,
		// Token: 0x040003C6 RID: 966
		d1_SH_BuyItem,
		// Token: 0x040003C7 RID: 967
		d2_SH_GoHome,
		// Token: 0x040003C8 RID: 968
		e0_MI_OpenBag,
		// Token: 0x040003C9 RID: 969
		e1_MI_ReStart,
		// Token: 0x040003CA RID: 970
		f0_MO_Standby,
		// Token: 0x040003CB RID: 971
		_StepMax,
		// Token: 0x040003CC RID: 972
		Complete = 99
	}
}
