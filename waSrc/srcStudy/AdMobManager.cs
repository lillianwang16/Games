using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class AdMobManager : MonoBehaviour
{
	// Token: 0x06000490 RID: 1168 RVA: 0x00020DEB File Offset: 0x0001F1EB
	private void Start()
	{
		this.RequestInterstitial();
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00020DF4 File Offset: 0x0001F1F4
	public bool ShowBanner(bool _enable)
	{
		if (_enable)
		{
			this.RequestBanner();
			this.bannerView.Show();
			this.is_Banner_enable = true;
			Debug.Log("[AdMobManager] バナーを表示させます");
			return true;
		}
		this.bannerView.Hide();
		this.is_Banner_enable = false;
		Debug.Log("[AdMobManager] バナーを隠しました");
		return true;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x00020E48 File Offset: 0x0001F248
	public bool ShowInterstitial()
	{
		if (this.interstitial.IsLoaded())
		{
			this.interstitial.Show();
			this.is_Interstitial_enable = true;
			Debug.Log("[AdMobManager] インタースティシャルを表示させます");
			return true;
		}
		return false;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00020E79 File Offset: 0x0001F279
	public bool ReloadCheck()
	{
		if (this.noReload)
		{
			this.noReload = false;
			return true;
		}
		return false;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x00020E90 File Offset: 0x0001F290
	public void RequestBanner()
	{
		string adUnitId = string.Empty;
		adUnitId = Define.AdMob_Android_UnitID_Banner;
		if (this.bannerView != null)
		{
			this.interstitial.Destroy();
		}
		this.is_Banner_Loaded = false;
		this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		this.banner_request = new AdRequest.Builder().Build();
		this.bannerView.LoadAd(this.banner_request);
		this.bannerView.OnAdLoaded += this.HandleBannerSuccess;
		this.bannerView.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleBannerReLoad);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x00020F27 File Offset: 0x0001F327
	public void HandleBannerSuccess(object sender, EventArgs e)
	{
		this.is_Banner_Loaded = true;
		Debug.Log("[AdMobManager] バナーのロードに成功しました");
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00020F3A File Offset: 0x0001F33A
	public void HandleBannerReLoad(object sender, EventArgs e)
	{
		Debug.Log("[AdMobManager] バナーのロードに失敗しました");
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00020F48 File Offset: 0x0001F348
	private IEnumerator _waitConnect_Banner()
	{
		do
		{
			yield return new WaitForSeconds(30f);
		}
		while (Application.internetReachability == NetworkReachability.NotReachable);
		this.RequestBanner();
		yield break;
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00020F64 File Offset: 0x0001F364
	public void RequestInterstitial()
	{
		string adUnitId = string.Empty;
		adUnitId = Define.AdMob_Android_UnitID_Interstitial;
		if (this.nowLoadInterstitial)
		{
			return;
		}
		if (this.is_close_interstitial)
		{
			this.interstitial.Destroy();
		}
		this.is_interstitial_Loaded = false;
		this.is_close_interstitial = false;
		this.nowLoadInterstitial = true;
		this.interstitial = new InterstitialAd(adUnitId);
		this.interstitial_request = new AdRequest.Builder().Build();
		this.interstitial.LoadAd(this.interstitial_request);
		this.interstitial.OnAdClosed += this.HandleClosed;
		this.interstitial.OnAdLoaded += this.HandleInterstitialSuccess;
		this.interstitial.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleInterstitialReLoad);
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00021026 File Offset: 0x0001F426
	public void HandleClosed(object sender, EventArgs e)
	{
		this.is_close_interstitial = true;
		this.is_Interstitial_enable = false;
		this.noReload = true;
		this.RequestInterstitial();
		Debug.Log("[AdMobManager] インタースティシャルを隠しました");
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0002104D File Offset: 0x0001F44D
	public void HandleInterstitialSuccess(object sender, EventArgs e)
	{
		this.is_interstitial_Loaded = true;
		this.nowLoadInterstitial = false;
		Debug.Log("[AdMobManager] インタースティシャルのロードに成功しました");
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00021067 File Offset: 0x0001F467
	public void HandleInterstitialReLoad(object sender, EventArgs e)
	{
		this.nowLoadInterstitial = false;
		Debug.Log("[AdMobManager] インタースティシャルのロードに失敗しました");
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0002107C File Offset: 0x0001F47C
	private IEnumerator _waitConnect_Interstitial()
	{
		do
		{
			yield return new WaitForSeconds(30f);
		}
		while (Application.internetReachability == NetworkReachability.NotReachable);
		this.RequestInterstitial();
		yield break;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00021097 File Offset: 0x0001F497
	public bool ShowInterstitial_OK()
	{
		return this.interstitial.IsLoaded();
	}

	// Token: 0x040004AA RID: 1194
	[SerializeField]
	private bool test;

	// Token: 0x040004AB RID: 1195
	private InterstitialAd interstitial;

	// Token: 0x040004AC RID: 1196
	private AdRequest interstitial_request;

	// Token: 0x040004AD RID: 1197
	private BannerView bannerView;

	// Token: 0x040004AE RID: 1198
	private AdRequest banner_request;

	// Token: 0x040004AF RID: 1199
	public bool is_close_interstitial;

	// Token: 0x040004B0 RID: 1200
	public bool is_Banner_Loaded;

	// Token: 0x040004B1 RID: 1201
	public bool is_interstitial_Loaded;

	// Token: 0x040004B2 RID: 1202
	public bool is_Banner_enable;

	// Token: 0x040004B3 RID: 1203
	public bool is_Interstitial_enable;

	// Token: 0x040004B4 RID: 1204
	public bool noReload;

	// Token: 0x040004B5 RID: 1205
	public bool nowLoadInterstitial;
}
