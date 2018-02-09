using System;
using System.Collections.Generic;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x0200000D RID: 13
	public class AdLoader
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002F0C File Offset: 0x0000130C
		private AdLoader(AdLoader.Builder builder)
		{
			this.AdUnitId = string.Copy(builder.AdUnitId);
			this.CustomNativeTemplateClickHandlers = new Dictionary<string, Action<CustomNativeTemplateAd, string>>(builder.CustomNativeTemplateClickHandlers);
			this.TemplateIds = new HashSet<string>(builder.TemplateIds);
			this.AdTypes = new HashSet<NativeAdType>(builder.AdTypes);
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildAdLoaderClient", BindingFlags.Static | BindingFlags.Public);
			this.adLoaderClient = (IAdLoaderClient)method.Invoke(null, new object[]
			{
				this
			});
			this.adLoaderClient.OnCustomNativeTemplateAdLoaded += delegate(object sender, CustomNativeEventArgs args)
			{
				if (this.OnCustomNativeTemplateAdLoaded != null)
				{
					this.OnCustomNativeTemplateAdLoaded(this, args);
				}
			};
			this.adLoaderClient.OnAdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args)
			{
				if (this.OnAdFailedToLoad != null)
				{
					this.OnAdFailedToLoad(this, args);
				}
			};
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000029 RID: 41 RVA: 0x00002FC8 File Offset: 0x000013C8
		// (remove) Token: 0x0600002A RID: 42 RVA: 0x00003000 File Offset: 0x00001400
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600002B RID: 43 RVA: 0x00003038 File Offset: 0x00001438
		// (remove) Token: 0x0600002C RID: 44 RVA: 0x00003070 File Offset: 0x00001470
		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000030A6 File Offset: 0x000014A6
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000030AE File Offset: 0x000014AE
		public Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateClickHandlers { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000030B7 File Offset: 0x000014B7
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000030BF File Offset: 0x000014BF
		public string AdUnitId { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000030C8 File Offset: 0x000014C8
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000030D0 File Offset: 0x000014D0
		public HashSet<NativeAdType> AdTypes { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000030D9 File Offset: 0x000014D9
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000030E1 File Offset: 0x000014E1
		public HashSet<string> TemplateIds { get; private set; }

		// Token: 0x06000035 RID: 53 RVA: 0x000030EA File Offset: 0x000014EA
		public void LoadAd(AdRequest request)
		{
			this.adLoaderClient.LoadAd(request);
		}

		// Token: 0x0400002F RID: 47
		private IAdLoaderClient adLoaderClient;

		// Token: 0x0200000E RID: 14
		public class Builder
		{
			// Token: 0x06000038 RID: 56 RVA: 0x0000312C File Offset: 0x0000152C
			public Builder(string adUnitId)
			{
				this.AdUnitId = adUnitId;
				this.AdTypes = new HashSet<NativeAdType>();
				this.TemplateIds = new HashSet<string>();
				this.CustomNativeTemplateClickHandlers = new Dictionary<string, Action<CustomNativeTemplateAd, string>>();
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000039 RID: 57 RVA: 0x0000315C File Offset: 0x0000155C
			// (set) Token: 0x0600003A RID: 58 RVA: 0x00003164 File Offset: 0x00001564
			internal string AdUnitId { get; private set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x0600003B RID: 59 RVA: 0x0000316D File Offset: 0x0000156D
			// (set) Token: 0x0600003C RID: 60 RVA: 0x00003175 File Offset: 0x00001575
			internal HashSet<NativeAdType> AdTypes { get; private set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x0600003D RID: 61 RVA: 0x0000317E File Offset: 0x0000157E
			// (set) Token: 0x0600003E RID: 62 RVA: 0x00003186 File Offset: 0x00001586
			internal HashSet<string> TemplateIds { get; private set; }

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600003F RID: 63 RVA: 0x0000318F File Offset: 0x0000158F
			// (set) Token: 0x06000040 RID: 64 RVA: 0x00003197 File Offset: 0x00001597
			internal Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateClickHandlers { get; private set; }

			// Token: 0x06000041 RID: 65 RVA: 0x000031A0 File Offset: 0x000015A0
			public AdLoader.Builder ForCustomNativeAd(string templateId)
			{
				this.TemplateIds.Add(templateId);
				this.AdTypes.Add(NativeAdType.CustomTemplate);
				return this;
			}

			// Token: 0x06000042 RID: 66 RVA: 0x000031BD File Offset: 0x000015BD
			public AdLoader.Builder ForCustomNativeAd(string templateId, Action<CustomNativeTemplateAd, string> callback)
			{
				this.TemplateIds.Add(templateId);
				this.CustomNativeTemplateClickHandlers[templateId] = callback;
				this.AdTypes.Add(NativeAdType.CustomTemplate);
				return this;
			}

			// Token: 0x06000043 RID: 67 RVA: 0x000031E7 File Offset: 0x000015E7
			public AdLoader Build()
			{
				return new AdLoader(this);
			}
		}
	}
}
