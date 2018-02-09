using System;
using System.Reflection;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000019 RID: 25
	public class MobileAds
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00003D7A File Offset: 0x0000217A
		public static void Initialize(string appId)
		{
			MobileAds.client.Initialize(appId);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003D87 File Offset: 0x00002187
		public static void SetApplicationMuted(bool muted)
		{
			MobileAds.client.SetApplicationMuted(muted);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003D94 File Offset: 0x00002194
		public static void SetApplicationVolume(float volume)
		{
			MobileAds.client.SetApplicationVolume(volume);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003DA4 File Offset: 0x000021A4
		private static IMobileAdsClient GetMobileAdsClient()
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("MobileAdsInstance", BindingFlags.Static | BindingFlags.Public);
			return (IMobileAdsClient)method.Invoke(null, null);
		}

		// Token: 0x0400006E RID: 110
		private static readonly IMobileAdsClient client = MobileAds.GetMobileAdsClient();
	}
}
