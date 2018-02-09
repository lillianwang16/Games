using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x0200001E RID: 30
	public interface IAdLoaderClient
	{
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000117 RID: 279
		// (remove) Token: 0x06000118 RID: 280
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000119 RID: 281
		// (remove) Token: 0x0600011A RID: 282
		event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		// Token: 0x0600011B RID: 283
		void LoadAd(AdRequest request);
	}
}
