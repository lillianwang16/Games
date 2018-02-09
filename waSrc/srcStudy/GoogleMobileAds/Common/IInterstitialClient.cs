using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000021 RID: 33
	public interface IInterstitialClient
	{
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06000133 RID: 307
		// (remove) Token: 0x06000134 RID: 308
		event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000135 RID: 309
		// (remove) Token: 0x06000136 RID: 310
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06000137 RID: 311
		// (remove) Token: 0x06000138 RID: 312
		event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000139 RID: 313
		// (remove) Token: 0x0600013A RID: 314
		event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600013B RID: 315
		// (remove) Token: 0x0600013C RID: 316
		event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x0600013D RID: 317
		void CreateInterstitialAd(string adUnitId);

		// Token: 0x0600013E RID: 318
		void LoadAd(AdRequest request);

		// Token: 0x0600013F RID: 319
		bool IsLoaded();

		// Token: 0x06000140 RID: 320
		void ShowInterstitial();

		// Token: 0x06000141 RID: 321
		void DestroyInterstitial();

		// Token: 0x06000142 RID: 322
		string MediationAdapterClassName();
	}
}
