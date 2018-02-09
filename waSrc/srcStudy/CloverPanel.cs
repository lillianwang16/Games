using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004D RID: 77
public class CloverPanel : MonoBehaviour
{
	// Token: 0x060002B4 RID: 692 RVA: 0x0000BCB4 File Offset: 0x0000A0B4
	private void Start()
	{
		this.showCloverValue = SuperGameMaster.CloverPointStock();
		this.numBase.GetComponent<Animator>().enabled = false;
		base.GetComponent<NumObjCreater>().createNumObj(this.showCloverValue, this.numBase.transform.localPosition, (int)this.numBase.transform.GetComponent<RectTransform>().sizeDelta.x);
	}

	// Token: 0x060002B5 RID: 693 RVA: 0x0000BD1C File Offset: 0x0000A11C
	private void Update()
	{
		if (SuperGameMaster.CloverPointStock() != this.showCloverValue)
		{
			this.numBase.GetComponent<Animator>().enabled = true;
			this.showCloverValue = SuperGameMaster.CloverPointStock();
			base.GetComponent<NumObjCreater>().createNumObj(this.showCloverValue, this.numBase.transform.localPosition, (int)this.numBase.transform.GetComponent<RectTransform>().sizeDelta.x);
		}
	}

	// Token: 0x060002B6 RID: 694 RVA: 0x0000BD94 File Offset: 0x0000A194
	public void BtnDisabled(bool flag)
	{
		this.BuyBtn.GetComponent<Button>().interactable = !flag;
	}

	// Token: 0x04000167 RID: 359
	public GameObject numBase;

	// Token: 0x04000168 RID: 360
	public int showCloverValue;

	// Token: 0x04000169 RID: 361
	public GameObject BuyBtn;
}
