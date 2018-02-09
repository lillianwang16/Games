using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using GoogleMobileAds.Api.Mediation;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x0200002D RID: 45
	internal class Utils
	{
		// Token: 0x060001ED RID: 493 RVA: 0x000064A4 File Offset: 0x000048A4
		public static AndroidJavaObject GetAdSizeJavaObject(AdSize adSize)
		{
			if (adSize.IsSmartBanner)
			{
				return new AndroidJavaClass("com.google.android.gms.ads.AdSize").GetStatic<AndroidJavaObject>("SMART_BANNER");
			}
			return new AndroidJavaObject("com.google.android.gms.ads.AdSize", new object[]
			{
				adSize.Width,
				adSize.Height
			});
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006500 File Offset: 0x00004900
		public static AndroidJavaObject GetAdRequestJavaObject(AdRequest request)
		{
			AndroidJavaObject androidJavaObject = new AndroidJavaObject("com.google.android.gms.ads.AdRequest$Builder", new object[0]);
			foreach (string text in request.Keywords)
			{
				androidJavaObject.Call<AndroidJavaObject>("addKeyword", new object[]
				{
					text
				});
			}
			foreach (string text2 in request.TestDevices)
			{
				if (text2 == "SIMULATOR")
				{
					string @static = new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<string>("DEVICE_ID_EMULATOR");
					androidJavaObject.Call<AndroidJavaObject>("addTestDevice", new object[]
					{
						@static
					});
				}
				else
				{
					androidJavaObject.Call<AndroidJavaObject>("addTestDevice", new object[]
					{
						text2
					});
				}
			}
			if (request.Birthday != null)
			{
				DateTime valueOrDefault = request.Birthday.GetValueOrDefault();
				AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.util.Date", new object[]
				{
					valueOrDefault.Year,
					valueOrDefault.Month,
					valueOrDefault.Day
				});
				androidJavaObject.Call<AndroidJavaObject>("setBirthday", new object[]
				{
					androidJavaObject2
				});
			}
			if (request.Gender != null)
			{
				int? num = null;
				Gender valueOrDefault2 = request.Gender.GetValueOrDefault();
				if (valueOrDefault2 != Gender.Unknown)
				{
					if (valueOrDefault2 != Gender.Male)
					{
						if (valueOrDefault2 == Gender.Female)
						{
							num = new int?(new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_FEMALE"));
						}
					}
					else
					{
						num = new int?(new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_MALE"));
					}
				}
				else
				{
					num = new int?(new AndroidJavaClass("com.google.android.gms.ads.AdRequest").GetStatic<int>("GENDER_UNKNOWN"));
				}
				if (num != null)
				{
					androidJavaObject.Call<AndroidJavaObject>("setGender", new object[]
					{
						num
					});
				}
			}
			if (request.TagForChildDirectedTreatment != null)
			{
				androidJavaObject.Call<AndroidJavaObject>("tagForChildDirectedTreatment", new object[]
				{
					request.TagForChildDirectedTreatment.GetValueOrDefault()
				});
			}
			androidJavaObject.Call<AndroidJavaObject>("setRequestAgent", new object[]
			{
				"unity-3.9.0"
			});
			AndroidJavaObject androidJavaObject3 = new AndroidJavaObject("android.os.Bundle", new object[0]);
			foreach (KeyValuePair<string, string> keyValuePair in request.Extras)
			{
				androidJavaObject3.Call("putString", new object[]
				{
					keyValuePair.Key,
					keyValuePair.Value
				});
			}
			androidJavaObject3.Call("putString", new object[]
			{
				"is_unity",
				"1"
			});
			AndroidJavaObject androidJavaObject4 = new AndroidJavaObject("com.google.android.gms.ads.mediation.admob.AdMobExtras", new object[]
			{
				androidJavaObject3
			});
			androidJavaObject.Call<AndroidJavaObject>("addNetworkExtras", new object[]
			{
				androidJavaObject4
			});
			foreach (MediationExtras mediationExtras in request.MediationExtras)
			{
				AndroidJavaObject androidJavaObject5 = new AndroidJavaObject(mediationExtras.AndroidMediationExtraBuilderClassName, new object[0]);
				AndroidJavaObject androidJavaObject6 = new AndroidJavaObject("java.util.HashMap", new object[0]);
				foreach (KeyValuePair<string, string> keyValuePair2 in mediationExtras.Extras)
				{
					androidJavaObject6.Call<string>("put", new object[]
					{
						keyValuePair2.Key,
						keyValuePair2.Value
					});
				}
				AndroidJavaObject androidJavaObject7 = androidJavaObject5.Call<AndroidJavaObject>("buildExtras", new object[]
				{
					androidJavaObject6
				});
				if (androidJavaObject7 != null)
				{
					androidJavaObject.Call<AndroidJavaObject>("addNetworkExtrasBundle", new object[]
					{
						androidJavaObject5.Call<AndroidJavaClass>("getAdapterClass", new object[0]),
						androidJavaObject7
					});
				}
			}
			return androidJavaObject.Call<AndroidJavaObject>("build", new object[0]);
		}

		// Token: 0x040000AF RID: 175
		public const string AdListenerClassName = "com.google.android.gms.ads.AdListener";

		// Token: 0x040000B0 RID: 176
		public const string AdRequestClassName = "com.google.android.gms.ads.AdRequest";

		// Token: 0x040000B1 RID: 177
		public const string AdRequestBuilderClassName = "com.google.android.gms.ads.AdRequest$Builder";

		// Token: 0x040000B2 RID: 178
		public const string AdSizeClassName = "com.google.android.gms.ads.AdSize";

		// Token: 0x040000B3 RID: 179
		public const string AdMobExtrasClassName = "com.google.android.gms.ads.mediation.admob.AdMobExtras";

		// Token: 0x040000B4 RID: 180
		public const string PlayStorePurchaseListenerClassName = "com.google.android.gms.ads.purchase.PlayStorePurchaseListener";

		// Token: 0x040000B5 RID: 181
		public const string MobileAdsClassName = "com.google.android.gms.ads.MobileAds";

		// Token: 0x040000B6 RID: 182
		public const string BannerViewClassName = "com.google.unity.ads.Banner";

		// Token: 0x040000B7 RID: 183
		public const string InterstitialClassName = "com.google.unity.ads.Interstitial";

		// Token: 0x040000B8 RID: 184
		public const string RewardBasedVideoClassName = "com.google.unity.ads.RewardBasedVideo";

		// Token: 0x040000B9 RID: 185
		public const string NativeExpressAdViewClassName = "com.google.unity.ads.NativeExpressAd";

		// Token: 0x040000BA RID: 186
		public const string NativeAdLoaderClassName = "com.google.unity.ads.NativeAdLoader";

		// Token: 0x040000BB RID: 187
		public const string UnityAdListenerClassName = "com.google.unity.ads.UnityAdListener";

		// Token: 0x040000BC RID: 188
		public const string UnityRewardBasedVideoAdListenerClassName = "com.google.unity.ads.UnityRewardBasedVideoAdListener";

		// Token: 0x040000BD RID: 189
		public const string UnityAdLoaderListenerClassName = "com.google.unity.ads.UnityAdLoaderListener";

		// Token: 0x040000BE RID: 190
		public const string PluginUtilsClassName = "com.google.unity.ads.PluginUtils";

		// Token: 0x040000BF RID: 191
		public const string UnityActivityClassName = "com.unity3d.player.UnityPlayer";

		// Token: 0x040000C0 RID: 192
		public const string BundleClassName = "android.os.Bundle";

		// Token: 0x040000C1 RID: 193
		public const string DateClassName = "java.util.Date";
	}
}
