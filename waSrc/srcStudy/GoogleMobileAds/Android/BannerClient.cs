using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x02000027 RID: 39
	public class BannerClient : AndroidJavaProxy, IBannerClient
	{
		// Token: 0x06000177 RID: 375 RVA: 0x000050CC File Offset: 0x000034CC
		public BannerClient() : base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.bannerView = new AndroidJavaObject("com.google.unity.ads.Banner", new object[]
			{
				@static,
				this
			});
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06000178 RID: 376 RVA: 0x0000511C File Offset: 0x0000351C
		// (remove) Token: 0x06000179 RID: 377 RVA: 0x00005154 File Offset: 0x00003554
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x0600017A RID: 378 RVA: 0x0000518C File Offset: 0x0000358C
		// (remove) Token: 0x0600017B RID: 379 RVA: 0x000051C4 File Offset: 0x000035C4
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x0600017C RID: 380 RVA: 0x000051FC File Offset: 0x000035FC
		// (remove) Token: 0x0600017D RID: 381 RVA: 0x00005234 File Offset: 0x00003634
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x0600017E RID: 382 RVA: 0x0000526C File Offset: 0x0000366C
		// (remove) Token: 0x0600017F RID: 383 RVA: 0x000052A4 File Offset: 0x000036A4
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000180 RID: 384 RVA: 0x000052DC File Offset: 0x000036DC
		// (remove) Token: 0x06000181 RID: 385 RVA: 0x00005314 File Offset: 0x00003714
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x06000182 RID: 386 RVA: 0x0000534A File Offset: 0x0000374A
		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.bannerView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				(int)position
			});
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005378 File Offset: 0x00003778
		public void CreateBannerView(string adUnitId, AdSize adSize, int x, int y)
		{
			this.bannerView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				x,
				y
			});
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000053B0 File Offset: 0x000037B0
		public void LoadAd(AdRequest request)
		{
			this.bannerView.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000053D1 File Offset: 0x000037D1
		public void ShowBannerView()
		{
			this.bannerView.Call("show", new object[0]);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000053E9 File Offset: 0x000037E9
		public void HideBannerView()
		{
			this.bannerView.Call("hide", new object[0]);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00005401 File Offset: 0x00003801
		public void DestroyBannerView()
		{
			this.bannerView.Call("destroy", new object[0]);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00005419 File Offset: 0x00003819
		public string MediationAdapterClassName()
		{
			return this.bannerView.Call<string>("getMediationAdapterClassName", new object[0]);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00005431 File Offset: 0x00003831
		public void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00005450 File Offset: 0x00003850
		public void onAdFailedToLoad(string errorReason)
		{
			if (this.OnAdFailedToLoad != null)
			{
				AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs
				{
					Message = errorReason
				};
				this.OnAdFailedToLoad(this, e);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00005484 File Offset: 0x00003884
		public void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000054A2 File Offset: 0x000038A2
		public void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000054C0 File Offset: 0x000038C0
		public void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		// Token: 0x0400008C RID: 140
		private AndroidJavaObject bannerView;
	}
}
