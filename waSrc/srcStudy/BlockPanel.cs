using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class BlockPanel : MonoBehaviour
{
	// Token: 0x060005DB RID: 1499 RVA: 0x000238FC File Offset: 0x00021CFC
	public void PushBlockPanel(bool noPushCheck)
	{
		if (!noPushCheck)
		{
			base.GetComponentInParent<UIMaster>().BackFunc_Call();
		}
	}
}
