using System;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000024 RID: 36
	public interface IRewardBasedVideoAdClient
	{
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06000157 RID: 343
		// (remove) Token: 0x06000158 RID: 344
		event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06000159 RID: 345
		// (remove) Token: 0x0600015A RID: 346
		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x0600015B RID: 347
		// (remove) Token: 0x0600015C RID: 348
		event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600015D RID: 349
		// (remove) Token: 0x0600015E RID: 350
		event EventHandler<EventArgs> OnAdStarted;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600015F RID: 351
		// (remove) Token: 0x06000160 RID: 352
		event EventHandler<Reward> OnAdRewarded;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x06000161 RID: 353
		// (remove) Token: 0x06000162 RID: 354
		event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x06000163 RID: 355
		// (remove) Token: 0x06000164 RID: 356
		event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000165 RID: 357
		void CreateRewardBasedVideoAd();

		// Token: 0x06000166 RID: 358
		void LoadAd(AdRequest request, string adUnitId);

		// Token: 0x06000167 RID: 359
		bool IsLoaded();

		// Token: 0x06000168 RID: 360
		string MediationAdapterClassName();

		// Token: 0x06000169 RID: 361
		void ShowRewardBasedVideoAd();
	}
}
