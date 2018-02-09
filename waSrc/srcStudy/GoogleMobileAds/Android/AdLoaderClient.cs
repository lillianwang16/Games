using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x02000026 RID: 38
	public class AdLoaderClient : AndroidJavaProxy, IAdLoaderClient
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00004E18 File Offset: 0x00003218
		public AdLoaderClient(AdLoader unityAdLoader) : base("com.google.unity.ads.UnityAdLoaderListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			this.adLoader = new AndroidJavaObject("com.google.unity.ads.NativeAdLoader", new object[]
			{
				@static,
				unityAdLoader.AdUnitId,
				this
			});
			this.CustomNativeTemplateCallbacks = unityAdLoader.CustomNativeTemplateClickHandlers;
			if (unityAdLoader.AdTypes.Contains(NativeAdType.CustomTemplate))
			{
				foreach (string text in unityAdLoader.TemplateIds)
				{
					this.adLoader.Call("configureCustomNativeTemplateAd", new object[]
					{
						text,
						this.CustomNativeTemplateCallbacks.ContainsKey(text)
					});
				}
			}
			this.adLoader.Call("create", new object[0]);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00004F18 File Offset: 0x00003318
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00004F20 File Offset: 0x00003320
		private Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateCallbacks { get; set; }

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x0600016F RID: 367 RVA: 0x00004F2C File Offset: 0x0000332C
		// (remove) Token: 0x06000170 RID: 368 RVA: 0x00004F64 File Offset: 0x00003364
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x06000171 RID: 369 RVA: 0x00004F9C File Offset: 0x0000339C
		// (remove) Token: 0x06000172 RID: 370 RVA: 0x00004FD4 File Offset: 0x000033D4
		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		// Token: 0x06000173 RID: 371 RVA: 0x0000500A File Offset: 0x0000340A
		public void LoadAd(AdRequest request)
		{
			this.adLoader.Call("loadAd", new object[]
			{
				Utils.GetAdRequestJavaObject(request)
			});
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000502C File Offset: 0x0000342C
		public void onCustomTemplateAdLoaded(AndroidJavaObject ad)
		{
			if (this.OnCustomNativeTemplateAdLoaded != null)
			{
				CustomNativeEventArgs e = new CustomNativeEventArgs
				{
					nativeAd = new CustomNativeTemplateAd(new CustomNativeTemplateClient(ad))
				};
				this.OnCustomNativeTemplateAdLoaded(this, e);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000506C File Offset: 0x0000346C
		private void onAdFailedToLoad(string errorReason)
		{
			AdFailedToLoadEventArgs e = new AdFailedToLoadEventArgs
			{
				Message = errorReason
			};
			this.OnAdFailedToLoad(this, e);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005098 File Offset: 0x00003498
		public void onCustomClick(AndroidJavaObject ad, string assetName)
		{
			CustomNativeTemplateAd customNativeTemplateAd = new CustomNativeTemplateAd(new CustomNativeTemplateClient(ad));
			this.CustomNativeTemplateCallbacks[customNativeTemplateAd.GetCustomTemplateId()](customNativeTemplateAd, assetName);
		}

		// Token: 0x04000088 RID: 136
		private AndroidJavaObject adLoader;
	}
}
