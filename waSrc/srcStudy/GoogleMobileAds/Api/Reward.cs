using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x0200001B RID: 27
	public class Reward : EventArgs
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000420B File Offset: 0x0000260B
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004213 File Offset: 0x00002613
		public string Type { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000421C File Offset: 0x0000261C
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004224 File Offset: 0x00002624
		public double Amount { get; set; }
	}
}
