using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x0200002B RID: 43
	public class NativeExpressAdClient : AndroidJavaProxy, INativeExpressAdClient
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00005A60 File Offset: 0x00003E60
		public NativeExpressAdClient() : base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.nativeExpressAdView = new AndroidJavaObject("com.google.unity.ads.NativeExpressAd", new object[]
			{
				@static,
				this
			});
		}

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x060001B2 RID: 434 RVA: 0x00005AB0 File Offset: 0x00003EB0
		// (remove) Token: 0x060001B3 RID: 435 RVA: 0x00005AE8 File Offset: 0x00003EE8
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x060001B4 RID: 436 RVA: 0x00005B20 File Offset: 0x00003F20
		// (remove) Token: 0x060001B5 RID: 437 RVA: 0x00005B58 File Offset: 0x00003F58
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x060001B6 RID: 438 RVA: 0x00005B90 File Offset: 0x00003F90
		// (remove) Token: 0x060001B7 RID: 439 RVA: 0x00005BC8 File Offset: 0x00003FC8
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x060001B8 RID: 440 RVA: 0x00005C00 File Offset: 0x00004000
		// (remove) Token: 0x060001B9 RID: 441 RVA: 0x00005C38 File Offset: 0x00004038
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x060001BA RID: 442 RVA: 0x00005C70 File Offset: 0x00004070
		// (remove) Token: 0x060001BB RID: 443 RVA: 0x00005CA8 File Offset: 0x000040A8
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x060001BC RID: 444 RVA: 0x00005CDE File Offset: 0x000040DE
		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
		{
			this.nativeExpressAdView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				(int)position
			});
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005D0C File Offset: 0x0000410C
		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, int x, int y)
		{
			this.nativeExpressAdView.Call("create", new object[]
			{
				adUnitId,
				Utils.GetAdSizeJavaObject(adSize),
				x,
				y
			});
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005D44 File Offset: 0x00004144
		public void LoadAd(AdRequest request)
		{
			this.nativeExpressAdView.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005D65 File Offset: 0x00004165
		public void SetAdSize(AdSize adSize)
		{
			this.nativeExpressAdView.Call("setAdSize", new object[]
			{
				Utils.GetAdSizeJavaObject(adSize)
			});
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005D86 File Offset: 0x00004186
		public void ShowNativeExpressAdView()
		{
			this.nativeExpressAdView.Call("show", new object[0]);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005D9E File Offset: 0x0000419E
		public void HideNativeExpressAdView()
		{
			this.nativeExpressAdView.Call("hide", new object[0]);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005DB6 File Offset: 0x000041B6
		public void DestroyNativeExpressAdView()
		{
			this.nativeExpressAdView.Call("destroy", new object[0]);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005DCE File Offset: 0x000041CE
		public string MediationAdapterClassName()
		{
			return this.nativeExpressAdView.Call<string>("getMediationAdapterClassName", new object[0]);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005DE6 File Offset: 0x000041E6
		public void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005E04 File Offset: 0x00004204
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

		// Token: 0x060001C6 RID: 454 RVA: 0x00005E38 File Offset: 0x00004238
		public void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005E56 File Offset: 0x00004256
		public void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005E74 File Offset: 0x00004274
		public void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		// Token: 0x0400009A RID: 154
		private AndroidJavaObject nativeExpressAdView;
	}
}
