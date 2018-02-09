using System;
using System.Reflection;
using GoogleMobileAds.Api;
using UnityEngine;

namespace GoogleMobileAds.Common
{
	// Token: 0x0200001D RID: 29
	public class DummyClient : IBannerClient, IInterstitialClient, IRewardBasedVideoAdClient, IAdLoaderClient, INativeExpressAdClient, IMobileAdsClient
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x0000472A File Offset: 0x00002B2A
		public DummyClient()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060000EA RID: 234 RVA: 0x0000474C File Offset: 0x00002B4C
		// (remove) Token: 0x060000EB RID: 235 RVA: 0x00004784 File Offset: 0x00002B84
		public event EventHandler<EventArgs> OnAdLoaded;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060000EC RID: 236 RVA: 0x000047BC File Offset: 0x00002BBC
		// (remove) Token: 0x060000ED RID: 237 RVA: 0x000047F4 File Offset: 0x00002BF4
		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060000EE RID: 238 RVA: 0x0000482C File Offset: 0x00002C2C
		// (remove) Token: 0x060000EF RID: 239 RVA: 0x00004864 File Offset: 0x00002C64
		public event EventHandler<EventArgs> OnAdOpening;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060000F0 RID: 240 RVA: 0x0000489C File Offset: 0x00002C9C
		// (remove) Token: 0x060000F1 RID: 241 RVA: 0x000048D4 File Offset: 0x00002CD4
		public event EventHandler<EventArgs> OnAdStarted;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060000F2 RID: 242 RVA: 0x0000490C File Offset: 0x00002D0C
		// (remove) Token: 0x060000F3 RID: 243 RVA: 0x00004944 File Offset: 0x00002D44
		public event EventHandler<EventArgs> OnAdClosed;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060000F4 RID: 244 RVA: 0x0000497C File Offset: 0x00002D7C
		// (remove) Token: 0x060000F5 RID: 245 RVA: 0x000049B4 File Offset: 0x00002DB4
		public event EventHandler<Reward> OnAdRewarded;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060000F6 RID: 246 RVA: 0x000049EC File Offset: 0x00002DEC
		// (remove) Token: 0x060000F7 RID: 247 RVA: 0x00004A24 File Offset: 0x00002E24
		public event EventHandler<EventArgs> OnAdLeavingApplication;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060000F8 RID: 248 RVA: 0x00004A5C File Offset: 0x00002E5C
		// (remove) Token: 0x060000F9 RID: 249 RVA: 0x00004A94 File Offset: 0x00002E94
		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004ACA File Offset: 0x00002ECA
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00004AEA File Offset: 0x00002EEA
		public string UserId
		{
			get
			{
				Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
				return "UserId";
			}
			set
			{
				Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004B05 File Offset: 0x00002F05
		public void Initialize(string appId)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004B20 File Offset: 0x00002F20
		public void SetApplicationMuted(bool muted)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004B3B File Offset: 0x00002F3B
		public void SetApplicationVolume(float volume)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004B56 File Offset: 0x00002F56
		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004B71 File Offset: 0x00002F71
		public void CreateBannerView(string adUnitId, AdSize adSize, int positionX, int positionY)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004B8C File Offset: 0x00002F8C
		public void LoadAd(AdRequest request)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004BA7 File Offset: 0x00002FA7
		public void ShowBannerView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004BC2 File Offset: 0x00002FC2
		public void HideBannerView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004BDD File Offset: 0x00002FDD
		public void DestroyBannerView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004BF8 File Offset: 0x00002FF8
		public void CreateInterstitialAd(string adUnitId)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004C13 File Offset: 0x00003013
		public bool IsLoaded()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
			return true;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004C2F File Offset: 0x0000302F
		public void ShowInterstitial()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004C4A File Offset: 0x0000304A
		public void DestroyInterstitial()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004C65 File Offset: 0x00003065
		public void CreateRewardBasedVideoAd()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004C80 File Offset: 0x00003080
		public void SetUserId(string userId)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004C9B File Offset: 0x0000309B
		public void LoadAd(AdRequest request, string adUnitId)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004CB6 File Offset: 0x000030B6
		public void DestroyRewardBasedVideoAd()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004CD1 File Offset: 0x000030D1
		public void ShowRewardBasedVideoAd()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004CEC File Offset: 0x000030EC
		public void CreateAdLoader(AdLoader.Builder builder)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004D07 File Offset: 0x00003107
		public void Load(AdRequest request)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004D22 File Offset: 0x00003122
		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, AdPosition position)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004D3D File Offset: 0x0000313D
		public void CreateNativeExpressAdView(string adUnitId, AdSize adSize, int positionX, int positionY)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004D58 File Offset: 0x00003158
		public void SetAdSize(AdSize adSize)
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004D73 File Offset: 0x00003173
		public void ShowNativeExpressAdView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004D8E File Offset: 0x0000318E
		public void HideNativeExpressAdView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004DA9 File Offset: 0x000031A9
		public void DestroyNativeExpressAdView()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004DC4 File Offset: 0x000031C4
		public string MediationAdapterClassName()
		{
			Debug.Log("Dummy " + MethodBase.GetCurrentMethod().Name);
			return null;
		}
	}
}
