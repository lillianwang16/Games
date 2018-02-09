using System;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class CharaObject : MonoBehaviour
{
	// Token: 0x0600025D RID: 605 RVA: 0x00007C63 File Offset: 0x00006063
	public void HitCheck()
	{
		base.GetComponentInParent<CharaTable>().getHitChara(this.charaId);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00007C76 File Offset: 0x00006076
	public void SetChara(int _charaId)
	{
		this.charaId = _charaId;
	}

	// Token: 0x04000110 RID: 272
	public int charaId;

	// Token: 0x04000111 RID: 273
	public string animeName;
}
