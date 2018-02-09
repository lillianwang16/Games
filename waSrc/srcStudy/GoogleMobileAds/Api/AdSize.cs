using System;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000012 RID: 18
	public class AdSize
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003436 File Offset: 0x00001836
		public AdSize(int width, int height)
		{
			this.isSmartBanner = false;
			this.width = width;
			this.height = height;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003453 File Offset: 0x00001853
		private AdSize(bool isSmartBanner) : this(0, 0)
		{
			this.isSmartBanner = isSmartBanner;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003464 File Offset: 0x00001864
		public int Width
		{
			get
			{
				return this.width;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000346C File Offset: 0x0000186C
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003474 File Offset: 0x00001874
		public bool IsSmartBanner
		{
			get
			{
				return this.isSmartBanner;
			}
		}

		// Token: 0x04000052 RID: 82
		private bool isSmartBanner;

		// Token: 0x04000053 RID: 83
		private int width;

		// Token: 0x04000054 RID: 84
		private int height;

		// Token: 0x04000055 RID: 85
		public static readonly AdSize Banner = new AdSize(320, 50);

		// Token: 0x04000056 RID: 86
		public static readonly AdSize MediumRectangle = new AdSize(300, 250);

		// Token: 0x04000057 RID: 87
		public static readonly AdSize IABBanner = new AdSize(468, 60);

		// Token: 0x04000058 RID: 88
		public static readonly AdSize Leaderboard = new AdSize(728, 90);

		// Token: 0x04000059 RID: 89
		public static readonly AdSize SmartBanner = new AdSize(true);

		// Token: 0x0400005A RID: 90
		public static readonly int FullWidth = -1;
	}
}
