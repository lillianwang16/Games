using System;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000013 RID: 19
	public class BannerView
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000034E4 File Offset: 0x000018E4
		public BannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildBannerClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (IBannerClient)method.Invoke(null, null);
			this.client.CreateBannerView(adUnitId, adSize, position);
			this.ConfigureBannerEvents();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003538 File Offset: 0x00001938
		public BannerView(string adUnitId, AdSize adSize, int x, int y)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildBannerClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (IBannerClient)method.Invoke(null, null);
			this.client.CreateBannerView(adUnitId, adSize, x, y);
			this.ConfigureBannerEvents();
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000072 RID: 114 RVA: 0x00003590 File Offset: 0x00001990
		// (remove) Token: 0x06000073 RID: 115 RVA: 0x000035C8 File Offset: 0x000019C8
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000074 RID: 116 RVA: 0x00003600 File Offset: 0x00001A00
		// (remove) Token: 0x06000075 RID: 117 RVA: 0x00003638 File Offset: 0x00001A38
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000076 RID: 118 RVA: 0x00003670 File Offset: 0x00001A70
		// (remove) Token: 0x06000077 RID: 119 RVA: 0x000036A8 File Offset: 0x00001AA8
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000078 RID: 120 RVA: 0x000036E0 File Offset: 0x00001AE0
		// (remove) Token: 0x06000079 RID: 121 RVA: 0x00003718 File Offset: 0x00001B18
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600007A RID: 122 RVA: 0x00003750 File Offset: 0x00001B50
		// (remove) Token: 0x0600007B RID: 123 RVA: 0x00003788 File Offset: 0x00001B88
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x0600007C RID: 124 RVA: 0x000037BE File Offset: 0x00001BBE
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000037CC File Offset: 0x00001BCC
		public void Hide()
		{
			this.client.HideBannerView();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000037D9 File Offset: 0x00001BD9
		public void Show()
		{
			this.client.ShowBannerView();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000037E6 File Offset: 0x00001BE6
		public void Destroy()
		{
			this.client.DestroyBannerView();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000037F4 File Offset: 0x00001BF4
		private void ConfigureBannerEvents()
		{
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
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003874 File Offset: 0x00001C74
		public string MediationAdapterClassName()
		{
			return this.client.MediationAdapterClassName();
		}

		// Token: 0x0400005B RID: 91
		private IBannerClient client;
	}
}
