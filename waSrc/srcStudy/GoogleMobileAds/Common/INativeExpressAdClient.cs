using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000023 RID: 35
	public interface INativeExpressAdClient
	{
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06000146 RID: 326
		// (remove) Token: 0x06000147 RID: 327
		event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06000148 RID: 328
		// (remove) Token: 0x06000149 RID: 329
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x0600014A RID: 330
		// (remove) Token: 0x0600014B RID: 331
		event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x0600014C RID: 332
		// (remove) Token: 0x0600014D RID: 333
		event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x0600014E RID: 334
		// (remove) Token: 0x0600014F RID: 335
		event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000150 RID: 336
		void CreateNativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position);

		// Token: 0x06000151 RID: 337
		void CreateNativeExpressAdView(string adUnitId, AdSize adSize, int x, int y);

		// Token: 0x06000152 RID: 338
		void LoadAd(AdRequest request);

		// Token: 0x06000153 RID: 339
		void ShowNativeExpressAdView();

		// Token: 0x06000154 RID: 340
		void HideNativeExpressAdView();

		// Token: 0x06000155 RID: 341
		void DestroyNativeExpressAdView();

		// Token: 0x06000156 RID: 342
		string MediationAdapterClassName();
	}
}
