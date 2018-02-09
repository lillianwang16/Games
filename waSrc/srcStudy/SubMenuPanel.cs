using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000067 RID: 103
public class SubMenuPanel : MonoBehaviour
{
	// Token: 0x0600039A RID: 922 RVA: 0x00015C04 File Offset: 0x00014004
	public void Push_Menu()
	{
		base.GetComponent<Animator>().SetTrigger("OpenTrigger");
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00015C2F File Offset: 0x0001402F
	public void Push_Photo()
	{
		this.CameraPanelUI.GetComponent<CameraPanel>().OpenCamera();
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00015C41 File Offset: 0x00014041
	public void Push_Item()
	{
		this.ItemView.GetComponent<ItemScrollView>().OpenScrollView(base.gameObject, ItemScrollView.Mode.View);
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00015C5A File Offset: 0x0001405A
	public void Push_Collect()
	{
		this.CollectMoveUI.GetComponent<CollectionMovePanel>().OpenUI();
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00015C6C File Offset: 0x0001406C
	public void Push_Other()
	{
		this.OtherView.GetComponent<OtherPanel>().OpenMainScrollView();
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00015C80 File Offset: 0x00014080
	public void CloseCheck()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Animator component = base.GetComponent<Animator>();
			if (component.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.SubMenuPanelCloseWait"))
			{
				Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if (!base.GetComponent<Collider2D>().OverlapPoint(point))
				{
					component.SetTrigger("CloseTrigger");
				}
			}
		}
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00015CEE File Offset: 0x000140EE
	public void SubMenuMain(bool blockUI)
	{
		if (!blockUI)
		{
			this.CloseCheck();
		}
		if (this.CameraPanelUI.activeSelf)
		{
			this.CameraPanelUI.GetComponent<CameraPanel>().ControllCamera();
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00015D1C File Offset: 0x0001411C
	public void BtnDisabled(bool flag)
	{
		this.MenuBtn.GetComponent<Button>().interactable = !flag;
	}

	// Token: 0x0400023A RID: 570
	public GameObject MenuBtn;

	// Token: 0x0400023B RID: 571
	public GameObject CameraPanelUI;

	// Token: 0x0400023C RID: 572
	public GameObject ItemView;

	// Token: 0x0400023D RID: 573
	public GameObject CollectMoveUI;

	// Token: 0x0400023E RID: 574
	public GameObject OtherView;
}
