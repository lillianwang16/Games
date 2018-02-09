using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class Post : MonoBehaviour
{
	// Token: 0x06000280 RID: 640 RVA: 0x00009877 File Offset: 0x00007C77
	public void OpenMailUI()
	{
		this.MailUI.GetComponent<MailScrollView>().Enable();
	}

	// Token: 0x06000281 RID: 641 RVA: 0x00009889 File Offset: 0x00007C89
	public void DispNew(bool dispFlag)
	{
		this.NewIcon.SetActive(dispFlag);
	}

	// Token: 0x04000136 RID: 310
	public GameObject MailUI;

	// Token: 0x04000137 RID: 311
	public GameObject NewIcon;
}
