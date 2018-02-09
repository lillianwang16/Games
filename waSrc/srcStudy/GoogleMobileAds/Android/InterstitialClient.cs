using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x02000029 RID: 41
	public class InterstitialClient : AndroidJavaProxy, IInterstitialClient
	{
		// Token: 0x06000195 RID: 405 RVA: 0x000055C8 File Offset: 0x000039C8
		public InterstitialClient() : base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.interstitial = new AndroidJavaObject("com.google.unity.ads.Interstitial", new object[]
			{
				@static,
				this
			});
		}

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000196 RID: 406 RVA: 0x00005618 File Offset: 0x00003A18
		// (remove) Token: 0x06000197 RID: 407 RVA: 0x00005650 File Offset: 0x00003A50
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000198 RID: 408 RVA: 0x00005688 File Offset: 0x00003A88
		// (remove) Token: 0x06000199 RID: 409 RVA: 0x000056C0 File Offset: 0x00003AC0
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x0600019A RID: 410 RVA: 0x000056F8 File Offset: 0x00003AF8
		// (remove) Token: 0x0600019B RID: 411 RVA: 0x00005730 File Offset: 0x00003B30
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x0600019C RID: 412 RVA: 0x00005768 File Offset: 0x00003B68
		// (remove) Token: 0x0600019D RID: 413 RVA: 0x000057A0 File Offset: 0x00003BA0
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x0600019E RID: 414 RVA: 0x000057D8 File Offset: 0x00003BD8
		// (remove) Token: 0x0600019F RID: 415 RVA: 0x00005810 File Offset: 0x00003C10
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x060001A0 RID: 416 RVA: 0x00005846 File Offset: 0x00003C46
		public void CreateInterstitialAd(string adUnitId)
		{
			this.interstitial.Call("create", new object[]
			{
				adUnitId
			});
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005862 File Offset: 0x00003C62
		public void LoadAd(AdRequest request)
		{
			this.interstitial.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005883 File Offset: 0x00003C83
		public bool IsLoaded()
		{
			return this.interstitial.Call<bool>("isLoaded", new object[0]);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000589B File Offset: 0x00003C9B
		public void ShowInterstitial()
		{
			this.interstitial.Call("show", new object[0]);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000058B3 File Offset: 0x00003CB3
		public void DestroyInterstitial()
		{
			this.interstitial.Call("destroy", new object[0]);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000058CB File Offset: 0x00003CCB
		public string MediationAdapterClassName()
		{
			return this.interstitial.Call<string>("getMediationAdapterClassName", new object[0]);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000058E3 File Offset: 0x00003CE3
		public void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00005904 File Offset: 0x00003D04
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

		// Token: 0x060001A8 RID: 424 RVA: 0x00005938 File Offset: 0x00003D38
		public void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005956 File Offset: 0x00003D56
		public void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005974 File Offset: 0x00003D74
		public void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000093 RID: 147
		private AndroidJavaObject interstitial;
	}
}
