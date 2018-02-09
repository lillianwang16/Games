using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000032 RID: 50
public class AchieveButton : MonoBehaviour
{
	// Token: 0x0600020D RID: 525 RVA: 0x00006EEE File Offset: 0x000052EE
	public void PushButton()
	{
		this.ScrollViewUI.GetComponent<AchieveScrollView>().SelectId(this.achiId);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00006F1F File Offset: 0x0000531F
	public void CngName(string nameStr)
	{
		this.NameText.GetComponent<Text>().text = nameStr;
	}

	// Token: 0x0600020F RID: 527 RVA: 0x00006F32 File Offset: 0x00005332
	public void CngInfo(string InfoStr)
	{
		this.InfoText.GetComponent<Text>().text = string.Empty + InfoStr;
	}

	// Token: 0x06000210 RID: 528 RVA: 0x00006F4F File Offset: 0x0000534F
	public void SetId(int id)
	{
		this.achiId = id;
	}

	// Token: 0x040000D1 RID: 209
	public GameObject ScrollViewUI;

	// Token: 0x040000D2 RID: 210
	public GameObject NameText;

	// Token: 0x040000D3 RID: 211
	public GameObject InfoText;

	// Token: 0x040000D4 RID: 212
	public int achiId;
}
