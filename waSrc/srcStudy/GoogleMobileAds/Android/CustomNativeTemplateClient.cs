using System;
using System.Collections.Generic;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	// Token: 0x02000028 RID: 40
	internal class CustomNativeTemplateClient : ICustomNativeTemplateClient
	{
		// Token: 0x0600018E RID: 398 RVA: 0x000054DE File Offset: 0x000038DE
		public CustomNativeTemplateClient(AndroidJavaObject customNativeAd)
		{
			this.customNativeAd = customNativeAd;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000054ED File Offset: 0x000038ED
		public List<string> GetAvailableAssetNames()
		{
			return new List<string>(this.customNativeAd.Call<string[]>("getAvailableAssetNames", new object[0]));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000550A File Offset: 0x0000390A
		public string GetTemplateId()
		{
			return this.customNativeAd.Call<string>("getTemplateId", new object[0]);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005524 File Offset: 0x00003924
		public byte[] GetImageByteArray(string key)
		{
			byte[] array = this.customNativeAd.Call<byte[]>("getImage", new object[]
			{
				key
			});
			if (array.Length == 0)
			{
				return null;
			}
			return array;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005558 File Offset: 0x00003958
		public string GetText(string key)
		{
			string text = this.customNativeAd.Call<string>("getText", new object[]
			{
				key
			});
			if (text.Equals(string.Empty))
			{
				return null;
			}
			return text;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005593 File Offset: 0x00003993
		public void PerformClick(string assetName)
		{
			this.customNativeAd.Call("performClick", new object[]
			{
				assetName
			});
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000055AF File Offset: 0x000039AF
		public void RecordImpression()
		{
			this.customNativeAd.Call("recordImpression", new object[0]);
		}

		// Token: 0x04000092 RID: 146
		private AndroidJavaObject customNativeAd;
	}
}
