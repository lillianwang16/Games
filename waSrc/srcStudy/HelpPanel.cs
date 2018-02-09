using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000054 RID: 84
public class HelpPanel : MonoBehaviour
{
	// Token: 0x060002F8 RID: 760 RVA: 0x0000DFF4 File Offset: 0x0000C3F4
	public void OpenPanel(string text)
	{
		this.tapBlock = true;
		this.timer = 0f;
		this.ScreenButton.GetComponent<Button>().enabled = false;
		this.PanelText.GetComponent<Text>().text = text;
		base.gameObject.SetActive(true);
		if (!this.HelpWindow.activeSelf)
		{
			this.HelpWindow.SetActive(true);
			base.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.3f);
		}
		if (text == string.Empty)
		{
			this.HelpWindow.SetActive(false);
			base.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
		}
		if (text != string.Empty)
		{
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		}
		this.ScreenButton.SetActive(true);
		this.BackFuncFlag = base.GetComponentInParent<UIMaster>().BackFunc_GetStopFlag();
		if (SuperGameMaster.GetNowScenes() != Scenes.InitScene && !this.BackFuncFlag)
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Stop(true);
		}
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0000E128 File Offset: 0x0000C528
	public void ClosePanel()
	{
		base.gameObject.SetActive(false);
		if (SuperGameMaster.GetNowScenes() != Scenes.InitScene && !this.BackFuncFlag)
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Stop(false);
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0000E158 File Offset: 0x0000C558
	public void Update()
	{
		if (this.tapBlock)
		{
			if (this.timer > this.limitTimer)
			{
				this.ScreenButton.GetComponent<Button>().enabled = true;
				this.tapBlock = false;
			}
			this.timer += Time.deltaTime;
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.ScreenButton.GetComponent<Button>().onClick.Invoke();
		}
	}

	// Token: 0x060002FB RID: 763 RVA: 0x0000E1D1 File Offset: 0x0000C5D1
	public void SetOnClick_Screen(UnityAction func)
	{
		this.ScreenButton.GetComponent<Button>().onClick.AddListener(func);
	}

	// Token: 0x060002FC RID: 764 RVA: 0x0000E1E9 File Offset: 0x0000C5E9
	public void ResetOnClick_Screen()
	{
		this.ScreenButton.GetComponent<Button>().onClick.RemoveAllListeners();
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0000E200 File Offset: 0x0000C600
	public void ActionStock_New(string text)
	{
		this.ActionStock.Add(new List<UnityAction>());
		this.ActionStock_Add(delegate
		{
			this.OpenPanel(text);
		});
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0000E243 File Offset: 0x0000C643
	public void ActionStock_Add(UnityAction func)
	{
		this.ActionStock[this.ActionStock.Count - 1].Add(func);
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0000E264 File Offset: 0x0000C664
	public void ActionStock_Next()
	{
		this.ActionStock[0][0]();
		this.ActionStock[0].RemoveAt(0);
		this.ResetOnClick_Screen();
		foreach (UnityAction onClick_Screen in this.ActionStock[0])
		{
			this.SetOnClick_Screen(onClick_Screen);
		}
		this.ActionStock.RemoveAt(0);
	}

	// Token: 0x0400019E RID: 414
	public GameObject PanelText;

	// Token: 0x0400019F RID: 415
	public GameObject ScreenButton;

	// Token: 0x040001A0 RID: 416
	public GameObject HelpWindow;

	// Token: 0x040001A1 RID: 417
	[Space(10f)]
	public bool tapBlock;

	// Token: 0x040001A2 RID: 418
	public float limitTimer = 1f;

	// Token: 0x040001A3 RID: 419
	private float timer;

	// Token: 0x040001A4 RID: 420
	[Space(10f)]
	public bool BackFuncFlag;

	// Token: 0x040001A5 RID: 421
	public List<List<UnityAction>> ActionStock = new List<List<UnityAction>>();
}
