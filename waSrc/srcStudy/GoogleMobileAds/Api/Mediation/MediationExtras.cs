using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Api.Mediation
{
	// Token: 0x02000018 RID: 24
	public abstract class MediationExtras
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00003D4E File Offset: 0x0000214E
		public MediationExtras()
		{
			this.Extras = new Dictionary<string, string>();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003D61 File Offset: 0x00002161
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003D69 File Offset: 0x00002169
		public Dictionary<string, string> Extras { get; protected set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A9 RID: 169
		public abstract string AndroidMediationExtraBuilderClassName { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AA RID: 170
		public abstract string IOSMediationExtraBuilderClassName { get; }
	}
}
