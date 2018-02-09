using System;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class WebViewPanel : MonoBehaviour
{
	// Token: 0x060003B9 RID: 953 RVA: 0x000166FC File Offset: 0x00014AFC
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.ClosePanel();
		});
	}

	// Token: 0x060003BA RID: 954 RVA: 0x00016728 File Offset: 0x00014B28
	public bool OpenPanel(string _url, WebViewPanel.Back _backMode)
	{
		this.backMode = _backMode;
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("インターネットに接続できませんでした\n電波状況や端末オプションを\nご確認のうえ、再度お試しください");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			return false;
		}
		base.gameObject.SetActive(true);
		this.webViewObject.Init(null, false, string.Empty, null, null, false);
		this.webViewObject.LoadURL(_url);
		this.webViewObject.SetVisibility(true);
		float num;
		if (Screen.height / 3 > Screen.width / 2)
		{
			num = (float)Screen.width / 640f;
		}
		else
		{
			num = (float)Screen.height / 960f;
		}
		this.webViewObject.SetMargins(0, (int)(num * 70f), 0, 0);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc();
		return true;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00016838 File Offset: 0x00014C38
	public void ClosePanel()
	{
		this.webViewObject.SetVisibility(false);
		base.gameObject.SetActive(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		WebViewPanel.Back back = this.backMode;
		if (back != WebViewPanel.Back.OtherPanel)
		{
			if (back != WebViewPanel.Back.IAPPanel)
			{
				if (back != WebViewPanel.Back.Tutorial)
				{
				}
			}
			else
			{
				this.IAPUI.SetActive(true);
				this.IAPUI.GetComponent<IAPPanel>().BackFunc();
			}
		}
		else
		{
			this.OtherUI.GetComponent<OtherPanel>().MainScrollView.SetActive(true);
			this.OtherUI.GetComponent<OtherPanel>().BackFunc();
		}
	}

	// Token: 0x060003BC RID: 956 RVA: 0x000168E8 File Offset: 0x00014CE8
	public void OpenPanel_TutorialTarm()
	{
		string url = string.Empty;
		url = Define.URL_Tarm_Android;
		this.OpenPanel(url, WebViewPanel.Back.Tutorial);
	}

	// Token: 0x0400025A RID: 602
	[SerializeField]
	private WebViewObject webViewObject;

	// Token: 0x0400025B RID: 603
	[SerializeField]
	private GameObject ConfilmUI;

	// Token: 0x0400025C RID: 604
	[SerializeField]
	private GameObject OtherUI;

	// Token: 0x0400025D RID: 605
	[SerializeField]
	private GameObject IAPUI;

	// Token: 0x0400025E RID: 606
	private WebViewPanel.Back backMode;

	// Token: 0x0200006D RID: 109
	public enum Back
	{
		// Token: 0x04000260 RID: 608
		NONE = -1,
		// Token: 0x04000261 RID: 609
		OtherPanel,
		// Token: 0x04000262 RID: 610
		IAPPanel,
		// Token: 0x04000263 RID: 611
		Tutorial
	}
}
