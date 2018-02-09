using System;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000017 RID: 23
	public class InterstitialAd
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000399C File Offset: 0x00001D9C
		public InterstitialAd(string adUnitId)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildInterstitialClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (IInterstitialClient)method.Invoke(null, null);
			this.client.CreateInterstitialAd(adUnitId);
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

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000092 RID: 146 RVA: 0x00003A5C File Offset: 0x00001E5C
		// (remove) Token: 0x06000093 RID: 147 RVA: 0x00003A94 File Offset: 0x00001E94
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000094 RID: 148 RVA: 0x00003ACC File Offset: 0x00001ECC
		// (remove) Token: 0x06000095 RID: 149 RVA: 0x00003B04 File Offset: 0x00001F04
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000096 RID: 150 RVA: 0x00003B3C File Offset: 0x00001F3C
		// (remove) Token: 0x06000097 RID: 151 RVA: 0x00003B74 File Offset: 0x00001F74
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000098 RID: 152 RVA: 0x00003BAC File Offset: 0x00001FAC
		// (remove) Token: 0x06000099 RID: 153 RVA: 0x00003BE4 File Offset: 0x00001FE4
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600009A RID: 154 RVA: 0x00003C1C File Offset: 0x0000201C
		// (remove) Token: 0x0600009B RID: 155 RVA: 0x00003C54 File Offset: 0x00002054
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x0600009C RID: 156 RVA: 0x00003C8A File Offset: 0x0000208A
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003C98 File Offset: 0x00002098
		public bool IsLoaded()
		{
			return this.client.IsLoaded();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003CA5 File Offset: 0x000020A5
		public void Show()
		{
			this.client.ShowInterstitial();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003CB2 File Offset: 0x000020B2
		public void Destroy()
		{
			this.client.DestroyInterstitial();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003CBF File Offset: 0x000020BF
		public string MediationAdapterClassName()
		{
			return this.client.MediationAdapterClassName();
		}

		// Token: 0x04000067 RID: 103
		private IInterstitialClient client;
	}
}
