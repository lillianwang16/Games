using System;
using UnityEngine;

// Token: 0x0200004E RID: 78
public class CollectionMovePanel : MonoBehaviour
{
	// Token: 0x060002B8 RID: 696 RVA: 0x0000BDB4 File Offset: 0x0000A1B4
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CloseUI();
		});
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x0000BDE0 File Offset: 0x0000A1E0
	public void OpenUI()
	{
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		base.gameObject.SetActive(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc();
	}

	// Token: 0x060002BA RID: 698 RVA: 0x0000BE4C File Offset: 0x0000A24C
	public void CloseUI()
	{
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		base.gameObject.SetActive(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes != Scenes.MainOut)
		{
			if (nowScenes == Scenes.MainIn)
			{
				base.GetComponentInParent<UIMaster_MainIn>().BackFunc();
			}
		}
		else
		{
			base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
		}
	}

	// Token: 0x060002BB RID: 699 RVA: 0x0000BEE8 File Offset: 0x0000A2E8
	public void Push_Present()
	{
		base.GetComponentInParent<UIMaster>().changeScene(Scenes.Present);
		base.GetComponentInParent<UIMaster>().freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_PageNext"]);
	}

	// Token: 0x060002BC RID: 700 RVA: 0x0000BF1B File Offset: 0x0000A31B
	public void Push_Album()
	{
		base.GetComponentInParent<UIMaster>().changeScene(Scenes.Album);
		base.GetComponentInParent<UIMaster>().freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_PageNext"]);
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000BF4E File Offset: 0x0000A34E
	public void Push_Snap()
	{
		base.GetComponentInParent<UIMaster>().changeScene(Scenes.Snap);
		base.GetComponentInParent<UIMaster>().freezeAll(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_PageNext"]);
	}

	// Token: 0x0400016A RID: 362
	public HelpPanel helpUI;
}
