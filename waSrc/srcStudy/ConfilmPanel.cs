using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200004F RID: 79
public class ConfilmPanel : MonoBehaviour
{
	// Token: 0x060002C0 RID: 704 RVA: 0x0000BF94 File Offset: 0x0000A394
	public void OpenPanel_YesNo(string text)
	{
		this.ConfilmPanelText.GetComponent<Text>().text = text;
		this.EnterPanel.SetActive(false);
		this.YesNoPanel.SetActive(true);
		this.ScreenButton.SetActive(false);
		this.SpacePanel.SetActive(true);
		base.gameObject.SetActive(true);
		SuperGameMaster.audioMgr.StopSE();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFuncFlag = base.GetComponentInParent<UIMaster>().BackFunc_GetStopFlag();
		if (SuperGameMaster.GetNowScenes() != Scenes.InitScene && !this.BackFuncFlag)
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Stop(true);
		}
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x0000C044 File Offset: 0x0000A444
	public void OpenPanel_Enter(string text)
	{
		this.ConfilmPanelText.GetComponent<Text>().text = text;
		this.EnterPanel.SetActive(true);
		this.YesNoPanel.SetActive(false);
		this.ScreenButton.SetActive(false);
		this.SpacePanel.SetActive(true);
		base.gameObject.SetActive(true);
		SuperGameMaster.audioMgr.StopSE();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFuncFlag = base.GetComponentInParent<UIMaster>().BackFunc_GetStopFlag();
		if (SuperGameMaster.GetNowScenes() != Scenes.InitScene && !this.BackFuncFlag)
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Stop(true);
		}
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000C0F4 File Offset: 0x0000A4F4
	public void OpenPanel(string text)
	{
		this.ConfilmPanelText.GetComponent<Text>().text = text;
		this.EnterPanel.SetActive(false);
		this.YesNoPanel.SetActive(false);
		this.ScreenButton.SetActive(true);
		this.SpacePanel.SetActive(false);
		base.gameObject.SetActive(true);
		SuperGameMaster.audioMgr.StopSE();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFuncFlag = base.GetComponentInParent<UIMaster>().BackFunc_GetStopFlag();
		if (SuperGameMaster.GetNowScenes() != Scenes.InitScene && !this.BackFuncFlag)
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Stop(true);
		}
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x0000C1A4 File Offset: 0x0000A5A4
	public void ClosePanel()
	{
		base.gameObject.SetActive(false);
		if (this.PlusContents != null)
		{
			UnityEngine.Object.Destroy(this.PlusContents);
			this.PlusContents = null;
			this.ConfilmPanelText.SetActive(true);
		}
		if (SuperGameMaster.GetNowScenes() != Scenes.InitScene && !this.BackFuncFlag)
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Stop(false);
		}
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x0000C20D File Offset: 0x0000A60D
	public void AddContents(GameObject _go)
	{
		_go.transform.transform.SetParent(this.TextBack.transform, false);
		this.PlusContents = _go;
		this.ConfilmPanelText.SetActive(false);
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x0000C240 File Offset: 0x0000A640
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (this.YesNoPanel.activeSelf)
			{
				this.NoButton.GetComponent<Button>().onClick.Invoke();
			}
			else if (this.ScreenButton.activeSelf)
			{
				this.ScreenButton.GetComponent<Button>().onClick.Invoke();
			}
			else if (this.EnterButton.activeSelf)
			{
				this.EnterButton.GetComponent<Button>().onClick.Invoke();
			}
		}
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x0000C2D2 File Offset: 0x0000A6D2
	public void SetOnClick_Yes(UnityAction func)
	{
		this.YesButton.GetComponent<Button>().onClick.AddListener(func);
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000C2EA File Offset: 0x0000A6EA
	public void SetOnClick_No(UnityAction func)
	{
		this.NoButton.GetComponent<Button>().onClick.AddListener(func);
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x0000C302 File Offset: 0x0000A702
	public void SetOnClick_Enter(UnityAction func)
	{
		this.EnterButton.GetComponent<Button>().onClick.AddListener(func);
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x0000C31A File Offset: 0x0000A71A
	public void SetOnClick_Screen(UnityAction func)
	{
		this.ScreenButton.GetComponent<Button>().onClick.AddListener(func);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x0000C334 File Offset: 0x0000A734
	public void ResetOnClick_Yes()
	{
		this.YesButton.GetComponent<Button>().onClick.RemoveAllListeners();
		this.SetOnClick_Yes(delegate
		{
			this.ResetOnClick_Yes();
		});
		this.SetOnClick_Yes(delegate
		{
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		});
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0000C38B File Offset: 0x0000A78B
	public void ResetOnClick_No()
	{
		this.NoButton.GetComponent<Button>().onClick.RemoveAllListeners();
		this.SetOnClick_No(delegate
		{
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		});
	}

	// Token: 0x060002CC RID: 716 RVA: 0x0000C3C5 File Offset: 0x0000A7C5
	public void ResetOnClick_Enter()
	{
		this.EnterButton.GetComponent<Button>().onClick.RemoveAllListeners();
		this.SetOnClick_Enter(delegate
		{
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		});
	}

	// Token: 0x060002CD RID: 717 RVA: 0x0000C3FF File Offset: 0x0000A7FF
	public void ResetOnClick_Screen()
	{
		this.ScreenButton.GetComponent<Button>().onClick.RemoveAllListeners();
		this.SetOnClick_Screen(delegate
		{
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		});
	}

	// Token: 0x0400016B RID: 363
	public GameObject ConfilmPanelText;

	// Token: 0x0400016C RID: 364
	public GameObject EnterPanel;

	// Token: 0x0400016D RID: 365
	public GameObject YesNoPanel;

	// Token: 0x0400016E RID: 366
	[Space(10f)]
	public GameObject ScreenButton;

	// Token: 0x0400016F RID: 367
	public GameObject YesButton;

	// Token: 0x04000170 RID: 368
	public GameObject NoButton;

	// Token: 0x04000171 RID: 369
	public GameObject EnterButton;

	// Token: 0x04000172 RID: 370
	[Space(10f)]
	public GameObject ConfilmMainPanel;

	// Token: 0x04000173 RID: 371
	[Space(10f)]
	public bool BackFuncFlag;

	// Token: 0x04000174 RID: 372
	[Header("<特殊な表示に使用>")]
	public GameObject TextBack;

	// Token: 0x04000175 RID: 373
	public GameObject PlusContents;

	// Token: 0x04000176 RID: 374
	public GameObject SpacePanel;
}
