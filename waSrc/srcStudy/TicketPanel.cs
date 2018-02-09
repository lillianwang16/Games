using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class TicketPanel : MonoBehaviour
{
	// Token: 0x060003AE RID: 942 RVA: 0x00016148 File Offset: 0x00014548
	private void Start()
	{
		this.showValue = SuperGameMaster.TicketStock();
		this.numBase.GetComponent<Animator>().enabled = false;
		base.GetComponent<NumObjCreater>().createNumObj(this.showValue, this.numBase.transform.localPosition, (int)this.numBase.transform.GetComponent<RectTransform>().sizeDelta.x);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x000161B0 File Offset: 0x000145B0
	private void Update()
	{
		if (SuperGameMaster.TicketStock() != this.showValue)
		{
			this.showValue = SuperGameMaster.TicketStock();
			base.GetComponent<NumObjCreater>().createNumObj(this.showValue, this.numBase.transform.localPosition, (int)this.numBase.transform.GetComponent<RectTransform>().sizeDelta.x);
		}
	}

	// Token: 0x04000249 RID: 585
	public GameObject numBase;

	// Token: 0x0400024A RID: 586
	public int showValue;
}
