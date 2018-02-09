using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005B RID: 91
public class MovePanel : MonoBehaviour
{
	// Token: 0x06000336 RID: 822 RVA: 0x000119F7 File Offset: 0x0000FDF7
	public void Push_GoInHome()
	{
		base.GetComponentInParent<UIMaster>().changeScene(Scenes.MainIn);
		base.GetComponentInParent<UIMaster>().freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Move"]);
	}

	// Token: 0x06000337 RID: 823 RVA: 0x00011A2A File Offset: 0x0000FE2A
	public void Push_GoOutHome()
	{
		base.GetComponentInParent<UIMaster>().changeScene(Scenes.MainOut);
		base.GetComponentInParent<UIMaster>().freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Move"]);
	}

	// Token: 0x06000338 RID: 824 RVA: 0x00011A5D File Offset: 0x0000FE5D
	public void Push_GoShop()
	{
		base.GetComponentInParent<UIMaster>().changeScene(Scenes.Shop);
		base.GetComponentInParent<UIMaster>().freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Move"]);
	}

	// Token: 0x06000339 RID: 825 RVA: 0x00011A90 File Offset: 0x0000FE90
	public void BtnDisabled(bool inHome, bool outHome, bool shop)
	{
		this.InBtn.GetComponent<Button>().interactable = !inHome;
		this.OutBtn.GetComponent<Button>().interactable = !outHome;
		this.ShopBtn.GetComponent<Button>().interactable = !shop;
	}

	// Token: 0x040001C9 RID: 457
	[SerializeField]
	public GameObject InBtn;

	// Token: 0x040001CA RID: 458
	[SerializeField]
	public GameObject OutBtn;

	// Token: 0x040001CB RID: 459
	[SerializeField]
	public GameObject ShopBtn;
}
