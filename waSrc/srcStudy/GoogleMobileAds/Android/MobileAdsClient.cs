using System;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x0200002A RID: 42
	public class MobileAdsClient : IMobileAdsClient
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00005992 File Offset: 0x00003D92
		private MobileAdsClient()
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000599A File Offset: 0x00003D9A
		public static MobileAdsClient Instance
		{
			get
			{
				return MobileAdsClient.instance;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000059A4 File Offset: 0x00003DA4
		public void Initialize(string appId)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass2.CallStatic("initialize", new object[]
			{
				@static,
				appId
			});
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000059EC File Offset: 0x00003DEC
		public void SetApplicationVolume(float volume)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass.CallStatic("setAppVolume", new object[]
			{
				volume
			});
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005A20 File Offset: 0x00003E20
		public void SetApplicationMuted(bool muted)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass.CallStatic("setAppMuted", new object[]
			{
				muted
			});
		}

		// Token: 0x04000099 RID: 153
		private static MobileAdsClient instance = new MobileAdsClient();
	}
}
