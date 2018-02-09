using System;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class AlbumPagePanel : MonoBehaviour
{
	// Token: 0x06000283 RID: 643 RVA: 0x000098A0 File Offset: 0x00007CA0
	public void SetPage(int _idx, int _max)
	{
		if (_idx != this.showIndexValue)
		{
			this.showIndexValue = _idx;
			this.numBase_index.createNumObj(this.showIndexValue, this.numBase_index.GetComponent<NumObjCreater>().prefub.transform.localPosition, (int)this.numBase_index.GetComponent<NumObjCreater>().prefub.transform.GetComponent<RectTransform>().sizeDelta.x);
		}
		if (_idx != this.showMaxValue)
		{
			this.showMaxValue = _max;
			this.numBase_max.createNumObj(this.showMaxValue, this.numBase_max.GetComponent<NumObjCreater>().prefub.transform.localPosition, (int)this.numBase_max.GetComponent<NumObjCreater>().prefub.transform.GetComponent<RectTransform>().sizeDelta.x);
		}
	}

	// Token: 0x04000138 RID: 312
	public NumObjCreater numBase_index;

	// Token: 0x04000139 RID: 313
	public NumObjCreater numBase_max;

	// Token: 0x0400013A RID: 314
	public int showIndexValue;

	// Token: 0x0400013B RID: 315
	public int showMaxValue;
}
