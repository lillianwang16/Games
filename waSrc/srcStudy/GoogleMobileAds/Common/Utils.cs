﻿using System;
using UnityEngine;

namespace GoogleMobileAds.Common
{
	// Token: 0x02000025 RID: 37
	internal class Utils
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00004DE8 File Offset: 0x000031E8
		public static Texture2D GetTexture2DFromByteArray(byte[] img)
		{
			Texture2D texture2D = new Texture2D(1, 1);
			if (!texture2D.LoadImage(img))
			{
				throw new InvalidOperationException("Could not load custom native template\n                        image asset as texture");
			}
			return texture2D;
		}
	}
}
