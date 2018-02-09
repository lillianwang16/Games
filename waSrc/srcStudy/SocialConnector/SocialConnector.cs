using System;
using UnityEngine;

namespace SocialConnector
{
	// Token: 0x02000030 RID: 48
	public class SocialConnector
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00006A0C File Offset: 0x00004E0C
		private static void _Share(string text, string url, string textureUrl)
		{
			using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]))
			{
				androidJavaObject.Call<AndroidJavaObject>("setAction", new object[]
				{
					"android.intent.action.SEND"
				});
				androidJavaObject.Call<AndroidJavaObject>("setType", new object[]
				{
					(!string.IsNullOrEmpty(textureUrl)) ? "image/png" : "text/plain"
				});
				if (!string.IsNullOrEmpty(url))
				{
					text = text + "\t" + url;
				}
				if (!string.IsNullOrEmpty(text))
				{
					androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
					{
						"android.intent.extra.TEXT",
						text
					});
				}
				if (!string.IsNullOrEmpty(textureUrl))
				{
					AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.net.Uri");
					AndroidJavaObject androidJavaObject2 = new AndroidJavaObject("java.io.File", new object[]
					{
						textureUrl
					});
					androidJavaObject.Call<AndroidJavaObject>("putExtra", new object[]
					{
						"android.intent.extra.STREAM",
						androidJavaClass.CallStatic<AndroidJavaObject>("fromFile", new object[]
						{
							androidJavaObject2
						})
					});
				}
				AndroidJavaObject androidJavaObject3 = androidJavaObject.CallStatic<AndroidJavaObject>("createChooser", new object[]
				{
					androidJavaObject,
					string.Empty
				});
				androidJavaObject3.Call<AndroidJavaObject>("putExtra", new object[]
				{
					"android.intent.extra.EXTRA_INITIAL_INTENTS",
					androidJavaObject
				});
				SocialConnector.activity.Call("startActivity", new object[]
				{
					androidJavaObject3
				});
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006B98 File Offset: 0x00004F98
		public static void Share(string text)
		{
			SocialConnector.Share(text, null, null);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006BA2 File Offset: 0x00004FA2
		public static void Share(string text, string url)
		{
			SocialConnector.Share(text, url, null);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006BAC File Offset: 0x00004FAC
		public static void Share(string text, string url, string textureUrl)
		{
			SocialConnector._Share(text, url, textureUrl);
		}

		// Token: 0x040000C2 RID: 194
		private static AndroidJavaObject clazz = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

		// Token: 0x040000C3 RID: 195
		private static AndroidJavaObject activity = SocialConnector.clazz.GetStatic<AndroidJavaObject>("currentActivity");
	}
}
