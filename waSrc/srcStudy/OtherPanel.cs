using System;
using UnityEngine;

// Token: 0x0200005D RID: 93
public class OtherPanel : MonoBehaviour
{
	// Token: 0x06000348 RID: 840 RVA: 0x00011F8C File Offset: 0x0001038C
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CloseMainScrollView();
		});
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00011FB8 File Offset: 0x000103B8
	public void BackFunc_child(GameObject obj)
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CloseView(obj);
		});
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00011FF8 File Offset: 0x000103F8
	public void OpenMainScrollView()
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc();
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00012064 File Offset: 0x00010464
	public void CloseMainScrollView()
	{
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		base.gameObject.SetActive(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes != Scenes.MainOut)
		{
			if (nowScenes == Scenes.MainIn)
			{
				base.GetComponentInParent<UIMaster_MainIn>().BackFunc();
			}
		}
		else
		{
			base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00012100 File Offset: 0x00010500
	public void SaveData()
	{
		base.GetComponentInParent<UIMaster>().OnSave();
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00012110 File Offset: 0x00010510
	public void OpenView(GameObject obj)
	{
		this.MainScrollView.SetActive(false);
		obj.SetActive(true);
		base.GetComponentInParent<OtherPanel>().MainScrollView.SetActive(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc_child(obj);
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00012164 File Offset: 0x00010564
	public void CloseView(GameObject obj)
	{
		this.MainScrollView.SetActive(false);
		obj.SetActive(false);
		base.GetComponentInParent<OtherPanel>().MainScrollView.SetActive(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		this.BackFunc();
	}

	// Token: 0x0600034F RID: 847 RVA: 0x000121B4 File Offset: 0x000105B4
	public void Push_LawWrite()
	{
		string url = string.Empty;
		url = Define.URL_UserPolicy_Android;
		if (this.UserPolicyUI.GetComponent<WebViewPanel>().OpenPanel(url, WebViewPanel.Back.OtherPanel))
		{
			base.GetComponentInParent<OtherPanel>().MainScrollView.SetActive(false);
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x000121F8 File Offset: 0x000105F8
	public void Push_PrivacyPolicy()
	{
		string url = string.Empty;
		url = Define.URL_PrivacyPolicy_Android;
		if (this.UserPolicyUI.GetComponent<WebViewPanel>().OpenPanel(url, WebViewPanel.Back.OtherPanel))
		{
			base.GetComponentInParent<OtherPanel>().MainScrollView.SetActive(false);
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0001223C File Offset: 0x0001063C
	public void Push_Tarm()
	{
		string url = string.Empty;
		url = Define.URL_Tarm_Android;
		if (this.UserPolicyUI.GetComponent<WebViewPanel>().OpenPanel(url, WebViewPanel.Back.OtherPanel))
		{
			base.GetComponentInParent<OtherPanel>().MainScrollView.SetActive(false);
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0001227D File Offset: 0x0001067D
	public void Push_Review()
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		Application.OpenURL(Define.URL_Review_Android);
	}

	// Token: 0x040001D3 RID: 467
	public GameObject MainScrollView;

	// Token: 0x040001D4 RID: 468
	public GameObject OptionUI;

	// Token: 0x040001D5 RID: 469
	public GameObject HelpUI;

	// Token: 0x040001D6 RID: 470
	public GameObject UserPolicyUI;
}
