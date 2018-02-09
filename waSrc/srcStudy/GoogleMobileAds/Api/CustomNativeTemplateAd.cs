using System;
using System.Collections.Generic;
using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000015 RID: 21
	public class CustomNativeTemplateAd
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000391C File Offset: 0x00001D1C
		internal CustomNativeTemplateAd(ICustomNativeTemplateClient client)
		{
			this.client = client;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000392B File Offset: 0x00001D2B
		public List<string> GetAvailableAssetNames()
		{
			return this.client.GetAvailableAssetNames();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003938 File Offset: 0x00001D38
		public string GetCustomTemplateId()
		{
			return this.client.GetTemplateId();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003948 File Offset: 0x00001D48
		public Texture2D GetTexture2D(string key)
		{
			byte[] imageByteArray = this.client.GetImageByteArray(key);
			if (imageByteArray == null)
			{
				return null;
			}
			return Utils.GetTexture2DFromByteArray(imageByteArray);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003970 File Offset: 0x00001D70
		public string GetText(string key)
		{
			return this.client.GetText(key);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000397E File Offset: 0x00001D7E
		public void PerformClick(string assetName)
		{
			this.client.PerformClick(assetName);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000398C File Offset: 0x00001D8C
		public void RecordImpression()
		{
			this.client.RecordImpression();
		}

		// Token: 0x04000062 RID: 98
		private ICustomNativeTemplateClient client;
	}
}
