using System;
using GoogleMobileAds.Android;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds
{
	// Token: 0x0200002E RID: 46
	public class GoogleMobileAdsClientFactory
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x000069D0 File Offset: 0x00004DD0
		public static IBannerClient BuildBannerClient()
		{
			return new BannerClient();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000069D7 File Offset: 0x00004DD7
		public static IInterstitialClient BuildInterstitialClient()
		{
			return new InterstitialClient();
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000069DE File Offset: 0x00004DDE
		public static IRewardBasedVideoAdClient BuildRewardBasedVideoAdClient()
		{
			return new RewardBasedVideoAdClient();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000069E5 File Offset: 0x00004DE5
		public static IAdLoaderClient BuildAdLoaderClient(AdLoader adLoader)
		{
			return new AdLoaderClient(adLoader);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000069ED File Offset: 0x00004DED
		public static INativeExpressAdClient BuildNativeExpressAdClient()
		{
			return new NativeExpressAdClient();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000069F4 File Offset: 0x00004DF4
		public static IMobileAdsClient MobileAdsInstance()
		{
			return MobileAdsClient.Instance;
		}
	}
}
