using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
public class ButtonOpenContentsPanel : MonoBehaviour
{
	// Token: 0x060002A0 RID: 672 RVA: 0x0000A6AA File Offset: 0x00008AAA
	public void PushOpenClosePanel(GameObject OpenPanel)
	{
		OpenPanel.gameObject.SetActive(!OpenPanel.activeSelf);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}
}
