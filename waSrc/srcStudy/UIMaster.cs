using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020000E3 RID: 227
public class UIMaster : MonoBehaviour
{
	// Token: 0x06000616 RID: 1558 RVA: 0x00022808 File Offset: 0x00020C08
	public virtual void UI_Awake()
	{
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0002280A File Offset: 0x00020C0A
	public virtual void UI_Start()
	{
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0002280C File Offset: 0x00020C0C
	public virtual void UI_Update()
	{
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0002280E File Offset: 0x00020C0E
	public virtual void UI_FixedUpdate()
	{
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x00022810 File Offset: 0x00020C10
	public virtual void UI_OnDisable()
	{
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x00022812 File Offset: 0x00020C12
	public virtual void UI_OnPouse()
	{
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x00022814 File Offset: 0x00020C14
	public virtual void UI_OnResume()
	{
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x00022816 File Offset: 0x00020C16
	public virtual void UI_ApplicationQuit()
	{
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x00022818 File Offset: 0x00020C18
	public virtual void freezeObject(bool flag)
	{
		this.change_ObjectFreeze = true;
		this.set_ObjectFreeze_Update = flag;
		this.set_ObjectFreeze_FixUpdate = flag;
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0002282F File Offset: 0x00020C2F
	public virtual void freezeObject(bool isUpDate, bool isFixUpdate)
	{
		this.change_ObjectFreeze = true;
		this.set_ObjectFreeze_Update = isUpDate;
		this.set_ObjectFreeze_FixUpdate = isFixUpdate;
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x00022846 File Offset: 0x00020C46
	public virtual void blockUI(bool block)
	{
		this.BlockUI.GetComponent<Image>().raycastTarget = block;
		this.isBlockUI = block;
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x00022860 File Offset: 0x00020C60
	public virtual void blockUI(bool block, Color color)
	{
		this.blockUI(block);
		this.BlockUI.GetComponent<Image>().color = color;
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0002287A File Offset: 0x00020C7A
	public virtual bool Check_blockUI()
	{
		return this.isBlockUI;
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x00022882 File Offset: 0x00020C82
	public virtual void freezeUI(bool freeze)
	{
		this.FadeUI.GetComponent<Image>().raycastTarget = freeze;
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x00022895 File Offset: 0x00020C95
	public virtual void freezeUI(bool freeze, Color col)
	{
		this.freezeUI(freeze);
		this.FadeUI.GetComponent<Image>().color = col;
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x000228AF File Offset: 0x00020CAF
	public virtual void stopUpDate_UI(bool stop)
	{
		this.isUI_Update = stop;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x000228B8 File Offset: 0x00020CB8
	public virtual void freezeAll(bool freeze)
	{
		this.freezeAll(freeze, false);
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x000228C2 File Offset: 0x00020CC2
	public virtual void freezeAll(bool freeze, bool exclude_ObjFixUpdate)
	{
		this.freezeUI(freeze);
		this.objectFreeze_Update = freeze;
		if (!exclude_ObjFixUpdate)
		{
			this.objectFreeze_FixUpdate = freeze;
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x000228DF File Offset: 0x00020CDF
	public virtual void changeScene(Scenes _nextScene)
	{
		this.GameMaster.GetComponent<GameMaster>().ChangeSceneCall(_nextScene);
		this.BackFunc_Stop(true);
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x000228F9 File Offset: 0x00020CF9
	public virtual void setFadeIn(float time)
	{
		this.FadeUI.GetComponent<FadeController>().setFadeIn(time);
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0002290C File Offset: 0x00020D0C
	public virtual void setFadeOut(float time)
	{
		this.FadeUI.GetComponent<FadeController>().setFadeOut(time);
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0002291F File Offset: 0x00020D1F
	public virtual bool checkFadeComplete()
	{
		return this.FadeUI.GetComponent<FadeController>().checkFadeComplete();
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00022931 File Offset: 0x00020D31
	public virtual void OnSave()
	{
		this.GameMaster.GetComponent<GameMaster>().OnSave();
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00022943 File Offset: 0x00020D43
	public virtual bool get_ObjectFreeze_Update()
	{
		return this.objectFreeze_Update;
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0002294B File Offset: 0x00020D4B
	public virtual void BackUpdate()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			this.BackFunc_Call();
		}
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0002295F File Offset: 0x00020D5F
	public virtual void BackFunc_Call()
	{
		if (this.stopBackChk)
		{
			return;
		}
		this.BackFunc.Invoke();
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x00022978 File Offset: 0x00020D78
	public virtual void BackFunc_Set(UnityAction func)
	{
		this.BackFunc.AddListener(func);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x00022986 File Offset: 0x00020D86
	public virtual void BackFunc_Reset()
	{
		this.BackFunc.RemoveAllListeners();
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x00022993 File Offset: 0x00020D93
	public virtual void BackFunc_Stop(bool flag)
	{
		this.stopBackChk = flag;
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0002299C File Offset: 0x00020D9C
	public virtual bool BackFunc_GetStopFlag()
	{
		return this.stopBackChk;
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x000229A4 File Offset: 0x00020DA4
	public virtual void UI_AwakeLoop()
	{
		this.UI_Awake();
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x000229AC File Offset: 0x00020DAC
	public virtual void UI_StartLoop()
	{
		this.UI_Start();
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x000229B4 File Offset: 0x00020DB4
	public virtual void UI_UpdateLoop()
	{
		if (this.change_ObjectFreeze)
		{
			this.change_ObjectFreeze = false;
			this.objectFreeze_Update = this.set_ObjectFreeze_Update;
			this.objectFreeze_FixUpdate = this.set_ObjectFreeze_FixUpdate;
		}
		if (!this.isUI_Update)
		{
			this.BackUpdate();
			this.UI_Update();
		}
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x00022A02 File Offset: 0x00020E02
	public virtual void UI_FixedUpdateLoop()
	{
		if (!this.isUI_Update)
		{
			this.UI_FixedUpdate();
		}
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x00022A15 File Offset: 0x00020E15
	public virtual void UI_OnDisableLoop()
	{
		this.UI_OnDisable();
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x00022A1D File Offset: 0x00020E1D
	public virtual void UI_OnPouseLoop()
	{
		this.UI_OnPouse();
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x00022A25 File Offset: 0x00020E25
	public virtual void UI_OnResumeLoop()
	{
		this.UI_OnResume();
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x00022A2D File Offset: 0x00020E2D
	public virtual void UI_ApplicationQuitLoop()
	{
		this.UI_ApplicationQuit();
	}

	// Token: 0x0400054A RID: 1354
	public GameObject GameMaster;

	// Token: 0x0400054B RID: 1355
	public GameObject BlockUI;

	// Token: 0x0400054C RID: 1356
	public GameObject FadeUI;

	// Token: 0x0400054D RID: 1357
	protected bool objectFreeze_Update;

	// Token: 0x0400054E RID: 1358
	protected bool objectFreeze_FixUpdate;

	// Token: 0x0400054F RID: 1359
	protected bool set_ObjectFreeze_Update;

	// Token: 0x04000550 RID: 1360
	protected bool set_ObjectFreeze_FixUpdate;

	// Token: 0x04000551 RID: 1361
	protected bool change_ObjectFreeze;

	// Token: 0x04000552 RID: 1362
	protected bool isBlockUI;

	// Token: 0x04000553 RID: 1363
	protected bool isUI_Update;

	// Token: 0x04000554 RID: 1364
	private UnityEvent BackFunc = new UnityEvent();

	// Token: 0x04000555 RID: 1365
	protected bool stopBackChk;
}
