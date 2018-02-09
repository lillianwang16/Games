using System;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000047 RID: 71
public class BagDeskPanels : MonoBehaviour
{
	// Token: 0x06000285 RID: 645 RVA: 0x00009984 File Offset: 0x00007D84
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CloseCheck();
		});
	}

	// Token: 0x06000286 RID: 646 RVA: 0x000099B0 File Offset: 0x00007DB0
	public void PanelUpDate()
	{
		if (SuperGameMaster.GetHome())
		{
			base.GetComponent<FlickCheaker>().FlickUpdate();
		}
	}

	// Token: 0x06000287 RID: 647 RVA: 0x000099C8 File Offset: 0x00007DC8
	public void OpenPanels()
	{
		base.gameObject.SetActive(true);
		this.BagPanelUI.GetComponent<BagPanel>().Enable();
		this.DeskPanelUI.GetComponent<BagPanel>().Enable();
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		base.GetComponent<FlickCheaker>().FlickInit();
		if (!SuperGameMaster.GetHome())
		{
			base.GetComponent<Animator>().SetTrigger("StartDeskTrigger");
			this.CheckDeskHelp();
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Popup"]);
		this.BackFunc();
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00009A7C File Offset: 0x00007E7C
	public void OpenPanels_bubble()
	{
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0f));
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel("おべんとうを支度してください");
		confilm.ResetOnClick_Screen();
		confilm.SetOnClick_Screen(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.OpenPanels();
		});
	}

	// Token: 0x06000289 RID: 649 RVA: 0x00009B24 File Offset: 0x00007F24
	public void ClosePanels(bool save)
	{
		base.gameObject.SetActive(false);
		this.BagPanelUI.GetComponent<BagPanel>().Disable();
		this.DeskPanelUI.GetComponent<BagPanel>().Disable();
		this.BagPanelUI.GetComponent<BagPanel>().SaveBagList();
		this.DeskPanelUI.GetComponent<BagPanel>().SaveBagList();
		base.GetComponentInParent<UIMaster>().OnSave();
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		SuperGameMaster.travel.StandByTravel();
		this.CheckEmpty();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		base.GetComponentInParent<UIMaster_MainIn>().BackFunc();
	}

	// Token: 0x0600028A RID: 650 RVA: 0x00009BF0 File Offset: 0x00007FF0
	public void CheckEmpty()
	{
		List<int> bagList = SuperGameMaster.GetBagList();
		List<int> deskList = SuperGameMaster.GetDeskList();
		bool active = true;
		if (bagList[0] != -1)
		{
			active = false;
		}
		if (deskList[0] != -1 || deskList[1] != -1)
		{
			active = false;
		}
		if (!SuperGameMaster.tutorial.TutorialComplete())
		{
			active = false;
		}
		this.EmptyIcon.SetActive(active);
	}

	// Token: 0x0600028B RID: 651 RVA: 0x00009C54 File Offset: 0x00008054
	public List<int> Get_tmpListAll()
	{
		List<int> list = new List<int>();
		foreach (int item in this.BagPanelUI.GetComponent<BagPanel>().tmpBagList)
		{
			list.Add(item);
		}
		foreach (int item2 in this.DeskPanelUI.GetComponent<BagPanel>().tmpBagList)
		{
			list.Add(item2);
		}
		return list;
	}

	// Token: 0x0600028C RID: 652 RVA: 0x00009D18 File Offset: 0x00008118
	public void CloseCheck()
	{
		this.ClosePanels(true);
	}

	// Token: 0x0600028D RID: 653 RVA: 0x00009D21 File Offset: 0x00008121
	public void ChangeDesk()
	{
		base.GetComponent<Animator>().SetTrigger("DeskTrigger");
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
		this.CheckDeskHelp();
	}

	// Token: 0x0600028E RID: 654 RVA: 0x00009D52 File Offset: 0x00008152
	public void ChangeBag()
	{
		base.GetComponent<Animator>().SetTrigger("BagTrigger");
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
	}

	// Token: 0x0600028F RID: 655 RVA: 0x00009D7D File Offset: 0x0000817D
	public void BtnDisabled(bool flag)
	{
		this.BagOpenUI.GetComponent<Button>().interactable = !flag;
	}

	// Token: 0x06000290 RID: 656 RVA: 0x00009D94 File Offset: 0x00008194
	public void CheckDeskHelp()
	{
		if (!SuperGameMaster.GetFirstFlag(Flag.FIRST_SHOW_DESK))
		{
			SuperGameMaster.SetFirstFlag(Flag.FIRST_SHOW_DESK);
			base.GetComponent<FlickCheaker>().stopFlick(true);
			HelpPanel help = base.GetComponentInParent<UIMaster_MainIn>().HelpUI.GetComponent<HelpPanel>();
			help.OpenPanel("<color=#61a8c7><b>つくえ</b></color>にしたくをしてあげると\n帰ってくるタイミングがあわなくても\n" + SuperGameMaster.GetFrogName() + "は自分でもちものを\n選んで旅立っていきます\nこまめにつくえにしたくを\nしておいてあげましょう");
			help.ResetOnClick_Screen();
			help.SetOnClick_Screen(delegate
			{
				help.ClosePanel();
			});
			help.SetOnClick_Screen(delegate
			{
				this.GetComponent<FlickCheaker>().stopFlick(false);
			});
		}
	}

	// Token: 0x0400013C RID: 316
	public GameObject BagPanelUI;

	// Token: 0x0400013D RID: 317
	public GameObject DeskPanelUI;

	// Token: 0x0400013E RID: 318
	public GameObject ConfilmUI;

	// Token: 0x0400013F RID: 319
	public GameObject EmptyIcon;

	// Token: 0x04000140 RID: 320
	public GameObject BagOpenUI;
}
