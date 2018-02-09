using System;
using System.Collections;
using System.IO;
using SocialConnector;
using UnityEngine;

// Token: 0x020000BD RID: 189
public class SocialSender : MonoBehaviour
{
	// Token: 0x0600049F RID: 1183 RVA: 0x00021210 File Offset: 0x0001F610
	public IEnumerator SendSocial(string socialText, string socialURL, Texture2D sendTex, UIMaster ui)
	{
		ui.freezeUI(true);
		yield return this.SaveTmpImage(sendTex);
		this.SaveTmpImage(sendTex);
		yield return new WaitForSeconds(1f);
		SocialConnector.Share(socialText, socialURL, this.imagePath);
		ui.freezeUI(false);
		yield break;
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00021248 File Offset: 0x0001F648
	public IEnumerator SaveTmpImage(Texture2D sendTex)
	{
		byte[] pngData = sendTex.EncodeToPNG();
		this.imagePath = Application.persistentDataPath + "/tabikaeru.png";
		File.WriteAllBytes(this.imagePath, pngData);
		Debug.Log("[TwitterSender] tmp 画像保存 : path = " + this.imagePath);
		yield return null;
		yield break;
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0002126A File Offset: 0x0001F66A
	public void DeleteTmpImage()
	{
		if (this.imagePath != string.Empty)
		{
			File.Delete(this.imagePath);
			Debug.Log("[TwitterSender] tmp 画像削除 : path = " + this.imagePath);
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x000212A4 File Offset: 0x0001F6A4
	public bool twitterAppCheck()
	{
		using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("android.content.Intent", new object[0]))
		{
			androidJavaObject.Call<AndroidJavaObject>("setAction", new object[]
			{
				"android.intent.action.SEND"
			});
			androidJavaObject.Call<AndroidJavaObject>("setType", new object[]
			{
				"text/plain"
			});
			if (!string.IsNullOrEmpty("com.twitter.android"))
			{
				Debug.Log("[TwitterSender] twitter アプリを確認しました");
				return true;
			}
		}
		Debug.Log("[TwitterSender] twitter アプリを確認できませんでした");
		return false;
	}

	// Token: 0x040004B6 RID: 1206
	public const string pngFileName = "tabikaeru.png";

	// Token: 0x040004B7 RID: 1207
	public string imagePath = string.Empty;
}
