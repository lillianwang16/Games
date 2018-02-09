using System;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x0200001A RID: 26
	public class NativeExpressAdView
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00003DE4 File Offset: 0x000021E4
		public NativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildNativeExpressAdClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (INativeExpressAdClient)method.Invoke(null, null);
			this.client.CreateNativeExpressAdView(adUnitId, adSize, position);
			this.ConfigureNativeExpressAdEvents();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003E38 File Offset: 0x00002238
		public NativeExpressAdView(string adUnitId, AdSize adSize, int x, int y)
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildNativeExpressAdClient", BindingFlags.Static | BindingFlags.Public);
			this.client = (INativeExpressAdClient)method.Invoke(null, null);
			this.client.CreateNativeExpressAdView(adUnitId, adSize, x, y);
			this.ConfigureNativeExpressAdEvents();
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000B3 RID: 179 RVA: 0x00003E90 File Offset: 0x00002290
		// (remove) Token: 0x060000B4 RID: 180 RVA: 0x00003EC8 File Offset: 0x000022C8
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060000B5 RID: 181 RVA: 0x00003F00 File Offset: 0x00002300
		// (remove) Token: 0x060000B6 RID: 182 RVA: 0x00003F38 File Offset: 0x00002338
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000B7 RID: 183 RVA: 0x00003F70 File Offset: 0x00002370
		// (remove) Token: 0x060000B8 RID: 184 RVA: 0x00003FA8 File Offset: 0x000023A8
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060000B9 RID: 185 RVA: 0x00003FE0 File Offset: 0x000023E0
		// (remove) Token: 0x060000BA RID: 186 RVA: 0x00004018 File Offset: 0x00002418
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000BB RID: 187 RVA: 0x00004050 File Offset: 0x00002450
		// (remove) Token: 0x060000BC RID: 188 RVA: 0x00004088 File Offset: 0x00002488
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x060000BD RID: 189 RVA: 0x000040BE File Offset: 0x000024BE
		public void LoadAd(AdRequest request)
		{
			this.client.LoadAd(request);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000040CC File Offset: 0x000024CC
		public void Hide()
		{
			this.client.HideNativeExpressAdView();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000040D9 File Offset: 0x000024D9
		public void Show()
		{
			this.client.ShowNativeExpressAdView();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000040E6 File Offset: 0x000024E6
		public void Destroy()
		{
			this.client.DestroyNativeExpressAdView();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000040F4 File Offset: 0x000024F4
		private void ConfigureNativeExpressAdEvents()
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

		// Token: 0x060000C2 RID: 194 RVA: 0x00004174 File Offset: 0x00002574
		public string MediationAdapterClassName()
		{
			return this.client.MediationAdapterClassName();
		}

		// Token: 0x0400006F RID: 111
		private INativeExpressAdClient client;
	}
}
