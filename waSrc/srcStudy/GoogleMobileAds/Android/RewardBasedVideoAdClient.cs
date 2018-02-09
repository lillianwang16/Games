using System;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x0200002C RID: 44
	public class RewardBasedVideoAdClient : AndroidJavaProxy, IRewardBasedVideoAdClient
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x00005E94 File Offset: 0x00004294
		public RewardBasedVideoAdClient() : base("com.google.unity.ads.UnityRewardBasedVideoAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.androidRewardBasedVideo = new AndroidJavaObject("com.google.unity.ads.RewardBasedVideo", new object[]
			{
				@static,
				this
			});
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x060001CA RID: 458 RVA: 0x00005FD8 File Offset: 0x000043D8
		// (remove) Token: 0x060001CB RID: 459 RVA: 0x00006010 File Offset: 0x00004410
		public event EventHandler<EventArgs> OnAdLoaded = delegate
		{
		};

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x060001CC RID: 460 RVA: 0x00006048 File Offset: 0x00004448
		// (remove) Token: 0x060001CD RID: 461 RVA: 0x00006080 File Offset: 0x00004480
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad = delegate
		{
		};

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x060001CE RID: 462 RVA: 0x000060B8 File Offset: 0x000044B8
		// (remove) Token: 0x060001CF RID: 463 RVA: 0x000060F0 File Offset: 0x000044F0
		public event EventHandler<EventArgs> OnAdOpening = delegate
		{
		};

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060001D0 RID: 464 RVA: 0x00006128 File Offset: 0x00004528
		// (remove) Token: 0x060001D1 RID: 465 RVA: 0x00006160 File Offset: 0x00004560
		public event EventHandler<EventArgs> OnAdStarted = delegate
		{
		};

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060001D2 RID: 466 RVA: 0x00006198 File Offset: 0x00004598
		// (remove) Token: 0x060001D3 RID: 467 RVA: 0x000061D0 File Offset: 0x000045D0
		public event EventHandler<EventArgs> OnAdClosed = delegate
		{
		};

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060001D4 RID: 468 RVA: 0x00006208 File Offset: 0x00004608
		// (remove) Token: 0x060001D5 RID: 469 RVA: 0x00006240 File Offset: 0x00004640
		public event EventHandler<Reward> OnAdRewarded = delegate
		{
		};

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x060001D6 RID: 470 RVA: 0x00006278 File Offset: 0x00004678
		// (remove) Token: 0x060001D7 RID: 471 RVA: 0x000062B0 File Offset: 0x000046B0
		public event EventHandler<EventArgs> OnAdLeavingApplication = delegate
		{
		};

		// Token: 0x060001D8 RID: 472 RVA: 0x000062E6 File Offset: 0x000046E6
		public void CreateRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("create", new object[0]);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000062FE File Offset: 0x000046FE
		public void LoadAd(AdRequest request, string adUnitId)
		{
			this.androidRewardBasedVideo.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request),
				adUnitId
			});
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00006323 File Offset: 0x00004723
		public bool IsLoaded()
		{
			return this.androidRewardBasedVideo.Call<bool>("isLoaded", new object[0]);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000633B File Offset: 0x0000473B
		public void ShowRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("show", new object[0]);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00006353 File Offset: 0x00004753
		public void DestroyRewardBasedVideoAd()
		{
			this.androidRewardBasedVideo.Call("destroy", new object[0]);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000636B File Offset: 0x0000476B
		public string MediationAdapterClassName()
		{
			return this.androidRewardBasedVideo.Call<string>("getMediationAdapterClassName", new object[0]);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00006383 File Offset: 0x00004783
		private void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000063A4 File Offset: 0x000047A4
		private void onAdFailedToLoad(string errorReason)
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

		// Token: 0x060001E0 RID: 480 RVA: 0x000063D8 File Offset: 0x000047D8
		private void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000063F6 File Offset: 0x000047F6
		private void onAdStarted()
		{
			if (this.OnAdStarted != null)
			{
				this.OnAdStarted(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006414 File Offset: 0x00004814
		private void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006434 File Offset: 0x00004834
		private void onAdRewarded(string type, float amount)
		{
			if (this.OnAdRewarded != null)
			{
				Reward e = new Reward
				{
					Type = type,
					Amount = (double)amount
				};
				this.OnAdRewarded(this, e);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006470 File Offset: 0x00004870
		private void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}

		// Token: 0x040000A0 RID: 160
		private AndroidJavaObject androidRewardBasedVideo;
	}
}
