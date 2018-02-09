using System;
using UnityEngine;

// Token: 0x02000041 RID: 65
public class Clover : MonoBehaviour
{
	// Token: 0x0600026A RID: 618 RVA: 0x00008958 File Offset: 0x00006D58
	public void SetCloverData(int _index, CloverDataFormat cloverData)
	{
		this.index = _index;
		this.point = cloverData.point;
		this.element = cloverData.element;
		this.spriteNum = cloverData.spriteNum;
		this.getTimer = 0f;
		this.isTimer = false;
		this.isGet = false;
		this.isDestroy = false;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x000089B0 File Offset: 0x00006DB0
	public bool Proc()
	{
		if (!this.isTimer)
		{
			return false;
		}
		this.getTimer += Time.deltaTime;
		if (!this.isGet && this.getTimer >= 0.4f)
		{
			this.isGet = true;
			this.CloverGet();
			base.GetComponentInParent<CloverFarm>().SaveClover();
		}
		if (!this.isDestroy && this.getTimer >= 0.6f)
		{
			this.isDestroy = true;
		}
		return this.isDestroy;
	}

	// Token: 0x0600026C RID: 620 RVA: 0x00008A38 File Offset: 0x00006E38
	public bool CloverHitCheck(Vector2 mousePoint)
	{
		if (base.GetComponent<BoxCollider2D>().OverlapPoint(mousePoint))
		{
			base.GetComponent<Animator>().SetTrigger("GetTrigger");
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Clover"]);
			base.GetComponent<BoxCollider2D>().size = Vector2.zero;
			this.isTimer = true;
			this.getTimer = -Time.deltaTime;
			return true;
		}
		return false;
	}

	// Token: 0x0600026D RID: 621 RVA: 0x00008AA8 File Offset: 0x00006EA8
	public void CloverGet()
	{
		Debug.LogWarning("取得判定");
		int num = this.element;
		if (num != 0)
		{
			if (num == 1)
			{
				SuperGameMaster.GetItem(1000, this.point);
				if (!base.GetComponentInParent<CloverFarm>().ConfilmUI.activeSelf && SuperGameMaster.tutorial.ClockOk() && !base.GetComponentInParent<CloverFarm>().ConfilmUI.GetComponentInParent<UIMaster_MainOut>().Check_blockUI())
				{
					UIMaster_MainOut UI_Cmp = base.GetComponentInParent<CloverFarm>().ConfilmUI.GetComponentInParent<UIMaster_MainOut>();
					UI_Cmp.freezeObject(true);
					UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 0f));
					ConfilmPanel confilm = base.GetComponentInParent<CloverFarm>().ConfilmUI.GetComponent<ConfilmPanel>();
					confilm.OpenPanel(string.Empty);
					confilm.AddContents(UnityEngine.Object.Instantiate<GameObject>(base.GetComponentInParent<CloverFarm>().AddConfirm_pref));
					confilm.ResetOnClick_Screen();
					confilm.SetOnClick_Screen(delegate
					{
						confilm.ClosePanel();
					});
					confilm.SetOnClick_Screen(delegate
					{
						UI_Cmp.freezeObject(false);
					});
					confilm.SetOnClick_Screen(delegate
					{
						UI_Cmp.blockUI(false);
					});
				}
			}
		}
		else
		{
			SuperGameMaster.getCloverPoint(this.point);
		}
		this.point = 0;
		base.GetComponentInParent<CloverFarm>().Recycle(this.index);
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00008C31 File Offset: 0x00007031
	public void getPoint()
	{
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00008C33 File Offset: 0x00007033
	public void cloverDestroy()
	{
	}

	// Token: 0x04000119 RID: 281
	public int index = -1;

	// Token: 0x0400011A RID: 282
	public int point = 1;

	// Token: 0x0400011B RID: 283
	public int element;

	// Token: 0x0400011C RID: 284
	public int spriteNum;

	// Token: 0x0400011D RID: 285
	public float getTimer;

	// Token: 0x0400011E RID: 286
	public bool isTimer;

	// Token: 0x0400011F RID: 287
	public bool isGet;

	// Token: 0x04000120 RID: 288
	public bool isDestroy;
}
