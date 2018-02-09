using System;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000022 RID: 34
	public interface IMobileAdsClient
	{
		// Token: 0x06000143 RID: 323
		void Initialize(string appId);

		// Token: 0x06000144 RID: 324
		void SetApplicationVolume(float volume);

		// Token: 0x06000145 RID: 325
		void SetApplicationMuted(bool muted);
	}
}
