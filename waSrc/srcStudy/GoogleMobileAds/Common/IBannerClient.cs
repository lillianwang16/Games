using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x0200001F RID: 31
	public interface IBannerClient
	{
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600011C RID: 284
		// (remove) Token: 0x0600011D RID: 285
		event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600011E RID: 286
		// (remove) Token: 0x0600011F RID: 287
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000120 RID: 288
		// (remove) Token: 0x06000121 RID: 289
		event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000122 RID: 290
		// (remove) Token: 0x06000123 RID: 291
		event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06000124 RID: 292
		// (remove) Token: 0x06000125 RID: 293
		event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000126 RID: 294
		void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position);

		// Token: 0x06000127 RID: 295
		void CreateBannerView(string adUnitId, AdSize adSize, int x, int y);

		// Token: 0x06000128 RID: 296
		void LoadAd(AdRequest request);

		// Token: 0x06000129 RID: 297
		void ShowBannerView();

		// Token: 0x0600012A RID: 298
		void HideBannerView();

		// Token: 0x0600012B RID: 299
		void DestroyBannerView();

		// Token: 0x0600012C RID: 300
		string MediationAdapterClassName();
	}
}
