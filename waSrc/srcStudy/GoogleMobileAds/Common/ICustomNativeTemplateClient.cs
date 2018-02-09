using System;
using System.Collections.Generic;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000020 RID: 32
	public interface ICustomNativeTemplateClient
	{
		// Token: 0x0600012D RID: 301
		string GetTemplateId();

		// Token: 0x0600012E RID: 302
		byte[] GetImageByteArray(string key);

		// Token: 0x0600012F RID: 303
		List<string> GetAvailableAssetNames();

		// Token: 0x06000130 RID: 304
		string GetText(string key);

		// Token: 0x06000131 RID: 305
		void PerformClick(string assetName);

		// Token: 0x06000132 RID: 306
		void RecordImpression();
	}
}
