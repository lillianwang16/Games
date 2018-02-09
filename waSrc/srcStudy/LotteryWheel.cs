using System;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class LotteryWheel : MonoBehaviour
{
	// Token: 0x0600027E RID: 638 RVA: 0x0000985D File Offset: 0x00007C5D
	public void OpenResultButton()
	{
		this.RafflePanelUI.GetComponent<RaffelPanel>().OpenResultButton();
	}

	// Token: 0x04000135 RID: 309
	public GameObject RafflePanelUI;
}
