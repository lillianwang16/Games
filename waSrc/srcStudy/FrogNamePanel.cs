using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000052 RID: 82
public class FrogNamePanel : MonoBehaviour
{
	// Token: 0x060002E7 RID: 743 RVA: 0x0000D860 File Offset: 0x0000BC60
	public void OpenNamePanel()
	{
		base.gameObject.SetActive(true);
		this.InputUI.GetComponent<InputField>().text = SuperGameMaster.saveData.frogName;
		this.InputUI.GetComponent<InputField>().ActivateInputField();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x0000D8BC File Offset: 0x0000BCBC
	public void PushEnter()
	{
		string text = this.InputUI.GetComponent<InputField>().text;
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		if (text == string.Empty)
		{
			confilm.OpenPanel("名前を入力してください");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.InputUI.GetComponent<InputField>().ActivateInputField();
			});
			confilm.SetOnClick_Screen(delegate
			{
				confilm.GetComponentInParent<UIMaster>().BackFunc_Stop(true);
			});
		}
		else
		{
			this.CloseNamePanel();
		}
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000D973 File Offset: 0x0000BD73
	public void CloseNamePanel()
	{
		base.gameObject.SetActive(false);
		SuperGameMaster.saveData.frogName = this.InputUI.GetComponent<InputField>().text;
		SuperGameMaster.tutorial.StepTutorial(true);
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0000D9A6 File Offset: 0x0000BDA6
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.PushEnter();
		}
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0000D9BC File Offset: 0x0000BDBC
	public void OutOfFocus()
	{
		string text = this.InputUI.GetComponent<InputField>().text;
		if (text.Length > 5)
		{
			this.InputUI.GetComponent<InputField>().text = text.Remove(5, text.Length - 5);
		}
	}

	// Token: 0x04000196 RID: 406
	public GameObject ConfilmUI;

	// Token: 0x04000197 RID: 407
	public GameObject InputUI;
}
