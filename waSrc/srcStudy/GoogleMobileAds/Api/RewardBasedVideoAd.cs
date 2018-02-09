using System;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x0200001C RID: 28
	public class RewardBasedVideoAd
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00004230 File Offset: 0x00002630
		private RewardBasedVideoAd()
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildRewardBasedVideoAdClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (IRewardBasedVideoAdClient)method.Invoke(null, null);
			this.client.CreateRewardBasedVideoAd();
			this.client.OnAdLoaded += delegate(object sender, EventArgs args)
			{
				if (this.OnAdLoaded != null)
				{
					this.OnAdLoaded(this, args);
				}
			};
			this.client.OnAdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args)
			{
				if (this.OnAdFailedToLoad != null)
				{
					this.OnAdFailedToLoad(this, args);
				}
			};
			this.client.OnAdOpening += delegate(object sender, EventArgs args)
			{
				if (this.OnAdOpening != null)
				{
					this.OnAdOpening(this, args);
				}
			};
			this.client.OnAdStarted += delegate(object sender, EventArgs args)
			{
				if (this.OnAdStarted != null)
				{
					this.OnAdStarted(this, args);
				}
			};
			this.client.OnAdClosed += delegate(object sender, EventArgs args)
			{
				if (this.OnAdClosed != null)
				{
					this.OnAdClosed(this, args);
				}
			};
			this.client.OnAdLeavingApplication += delegate(object sender, EventArgs args)
			{
				if (this.OnAdLeavingApplication != null)
				{
					this.OnAdLeavingApplication(this, args);
				}
			};
			this.client.OnAdRewarded += delegate(object sender, Reward args)
			{
				if (this.OnAdRewarded != null)
				{
					this.OnAdRewarded(this, args);
				}
			};
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000431B File Offset: 0x0000271B
		public static RewardBasedVideoAd Instance
		{
			get
			{
				return RewardBasedVideoAd.instance;
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000CF RID: 207 RVA: 0x00004324 File Offset: 0x00002724
		// (remove) Token: 0x060000D0 RID: 208 RVA: 0x0000435C File Offset: 0x0000275C
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060000D1 RID: 209 RVA: 0x00004394 File Offset: 0x00002794
		// (remove) Token: 0x060000D2 RID: 210 RVA: 0x000043CC File Offset: 0x000027CC
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060000D3 RID: 211 RVA: 0x00004404 File Offset: 0x00002804
		// (remove) Token: 0x060000D4 RID: 212 RVA: 0x0000443C File Offset: 0x0000283C
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060000D5 RID: 213 RVA: 0x00004474 File Offset: 0x00002874
		// (remove) Token: 0x060000D6 RID: 214 RVA: 0x000044AC File Offset: 0x000028AC
		public event EventHandler<EventArgs> OnAdStarted;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060000D7 RID: 215 RVA: 0x000044E4 File Offset: 0x000028E4
		// (remove) Token: 0x060000D8 RID: 216 RVA: 0x0000451C File Offset: 0x0000291C
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060000D9 RID: 217 RVA: 0x00004554 File Offset: 0x00002954
		// (remove) Token: 0x060000DA RID: 218 RVA: 0x0000458C File Offset: 0x0000298C
		public event EventHandler<Reward> OnAdRewarded;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060000DB RID: 219 RVA: 0x000045C4 File Offset: 0x000029C4
		// (remove) Token: 0x060000DC RID: 220 RVA: 0x000045FC File Offset: 0x000029FC
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x060000DD RID: 221 RVA: 0x00004632 File Offset: 0x00002A32
		public void LoadAd(AdRequest request, string adUnitId)
		{
			this.client.LoadAd(request, adUnitId);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004641 File Offset: 0x00002A41
		public bool IsLoaded()
		{
			return this.client.IsLoaded();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000464E File Offset: 0x00002A4E
		public void Show()
		{
			this.client.ShowRewardBasedVideoAd();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000465B File Offset: 0x00002A5B
		public string MediationAdapterClassName()
		{
			return this.client.MediationAdapterClassName();
		}

		// Token: 0x04000077 RID: 119
		private IRewardBasedVideoAdClient client;

		// Token: 0x04000078 RID: 120
		private static readonly RewardBasedVideoAd instance = new RewardBasedVideoAd();
	}
}
